using Application.AccountService.Models.RequestModels;
using Domain.Common;
using Infrastructure.Common;
using Infrastructure.Model.ResponseModel;
using Infrastructure.UnitOfWork;
using Infrastructure.Utils;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AccountService.Commands
{
    public record AccountResendVerificationEmailCommand(EmailModel emailModel) : IRequest<ResponseModel>;
    public class AccountResendVerificationEmailCommandHanlder : IRequestHandler<AccountResendVerificationEmailCommand, ResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public AccountResendVerificationEmailCommandHanlder(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<ResponseModel> Handle(AccountResendVerificationEmailCommand request, CancellationToken cancellationToken)
        {
            var account = await _unitOfWork.AccountRepository.FindByEmailAsync(request.emailModel.Email);
            if (account == null)
                return new ResponseModel
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = "Account not found"
                };

            if (account.EmailConfirmed)
                return new ResponseModel
                {
                    Message = "Email has been verified"
                };

            // Update new verification code
            account.VerificationCode = AuthenticationTools.GenerateDigitCode(Constant.VerificationCodeLength);
            account.VerificationCodeExpiryTime = DateTime.UtcNow.AddMinutes(Constant.VerificationCodeValidityInMinutes);
            _unitOfWork.AccountRepository.Update(account);
            if (await _unitOfWork.SaveChangeAsync() > 0)
            {
                await _mediator.Send(new SendVerificationEmailCommand(account));

                return new ResponseModel
                {
                    Message = "Resend verification email successfully"
                };
            }

            return new ResponseModel
            {
                Code = StatusCodes.Status500InternalServerError,
                Message = "Cannot resend verification email"
            };
        }
    }
}
