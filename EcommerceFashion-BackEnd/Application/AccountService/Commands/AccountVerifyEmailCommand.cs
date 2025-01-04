using Infrastructure.Common;
using Infrastructure.Model.ResponseModel;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AccountService.Commands
{
    public record AccountVerifyEmailCommand(string email, string verificationCode) : IRequest<ResponseModel>;
    public class AccountVerifyEmailCommandHanlder : IRequestHandler<AccountVerifyEmailCommand, ResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountVerifyEmailCommandHanlder(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel> Handle(AccountVerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var account = await _unitOfWork.AccountRepository.FindByEmailAsync(request.email);
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

            if (account.VerificationCodeExpiryTime < DateTime.UtcNow)
                return new ResponseModel
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "The code is expired"
                };

            if (account.VerificationCode == request.verificationCode)
            {
                account.EmailConfirmed = true;
                account.VerificationCode = null;
                account.VerificationCodeExpiryTime = null;
                _unitOfWork.AccountRepository.Update(account);
                var result = await _unitOfWork.SaveChangeAsync();
                return result > 0 ? new ResponseModel
                {
                    Message = "Verify email successfully"
                } : new ResponseModel
                {
                    Message = "Verify email unsuccessfully"
                };
            }

            return new ResponseModel
            {
                Code = StatusCodes.Status400BadRequest,
                Message = "Cannot verify email"
            };
        }
    }
}
