using Application.AccountService.Models.RequestModels;
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
    public record AccountSignInCommand(AccountSignInModel accountSignInModel) : IRequest<ResponseModel>;
    public class AccountSignInCommandHandler : IRequestHandler<AccountSignInCommand, ResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public AccountSignInCommandHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<ResponseModel> Handle(AccountSignInCommand request, CancellationToken cancellationToken)
        {
            var accountSignInModel = request.accountSignInModel;
            var account = await _unitOfWork.AccountRepository.FindByEmailAsync(accountSignInModel.Email);
            if (account != null)
            {
                if (account.IsDeleted)
                    return new ResponseModel
                    {
                        Code = StatusCodes.Status410Gone,
                        Message = "Account has been deleted"
                    };

                if (AuthenticationTools.VerifyPassword(accountSignInModel.Password, account.HashedPassword))
                {
                    var tokenModel = await _mediator.Send(new AccountGenerateJwtTokenCommand(account));
                    if (tokenModel != null)
                        return new ResponseModel
                        {
                            Message = "Sign in successfully",
                            Data = tokenModel
                        };

                    return new ResponseModel
                    {
                        Code = StatusCodes.Status500InternalServerError,
                        Message = "Cannot sign in"
                    };
                }
            }

            return new ResponseModel
            {
                Code = StatusCodes.Status404NotFound,
                Message = "Invalid email or password"
            };
        }
    }
}
