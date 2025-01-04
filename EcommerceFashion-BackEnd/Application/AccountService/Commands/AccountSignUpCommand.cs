using Application.AccountService.Models.RequestModels;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Model.ResponseModel;
using Infrastructure.UnitOfWork;
using Infrastructure.Utils;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AccountService.Commands
{
    public record AccountSignUpCommand(AccountSignUpModel accountSignUpModel) : IRequest<ResponseModel>;
    public class AccountSignUpCommandHandler : IRequestHandler<AccountSignUpCommand, ResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AccountSignUpCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<ResponseModel> Handle(AccountSignUpCommand request, CancellationToken cancellationToken)
        {
            if (request.accountSignUpModel == null)
            {
                return new ResponseModel
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid account data."
                };
            }

            var accountSignUpModel = request.accountSignUpModel;

            if (string.IsNullOrWhiteSpace(accountSignUpModel.Email))
            {
                return new ResponseModel
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Email is required."
                };
            }

            var existedEmail = await _unitOfWork.AccountRepository.FindByEmailAsync(accountSignUpModel.Email);
            if (existedEmail != null)
            {
                return new ResponseModel
                {
                    Code = StatusCodes.Status409Conflict,
                    Message = "Email already exists."
                };
            }
            if (!string.IsNullOrWhiteSpace(accountSignUpModel.Username))
            {
                var existedUsername = await _unitOfWork.AccountRepository.FindByUsernameAsync(accountSignUpModel.Username);
                if (existedUsername != null)
                {
                    return new ResponseModel
                    {
                        Code = StatusCodes.Status409Conflict,
                        Message = "Username already exists."
                    };
                }
            }
            else
            {
                accountSignUpModel.Username = AuthenticationTools.GenerateUsername();
            }
            var account = new Account
            {
                FirstName = accountSignUpModel.FirstName,
                LastName = accountSignUpModel.LastName,
                Username = accountSignUpModel.Username,
                Email = accountSignUpModel.Email,
                HashedPassword = AuthenticationTools.HashPassword(accountSignUpModel.Password),
                Gender = accountSignUpModel.Gender,
                DateOfBirth = accountSignUpModel.DateOfBirth,
                PhoneNumber = accountSignUpModel.PhoneNumber,
                Address = accountSignUpModel.Address,
                VerificationCode = AuthenticationTools.GenerateDigitCode(Constant.VerificationCodeLength),
                VerificationCodeExpiryTime = DateTime.UtcNow.AddMinutes(Constant.VerificationCodeValidityInMinutes)
            };

            await _unitOfWork.AccountRepository.AddAsync(account);      
            account.Credit = new Credit
            {
                Balance = 0,
                CreatedById = account.Id
            };
            await _unitOfWork.CreditRepository.AddAsync(account.Credit);

            var role = await _unitOfWork.RoleRepository.FindByNameAsync(Domain.Enums.Role.User.ToString());
            if (role == null)
            {
                return new ResponseModel
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = "Default user role not found."
                };
            }

            var accountRole = new AccountRole
            {
                Account = account,
                Role = role
            };
            await _unitOfWork.AccountRoleRepository.AddAsync(accountRole);

            var result = await _unitOfWork.SaveChangeAsync();


            if (result > 0)
            {
                await _mediator.Send(new SendVerificationEmailCommand(account));

                return new ResponseModel
                {
                    Code = StatusCodes.Status201Created,
                    Message = "Sign up successfully, please verify your email."
                };
            }

            return new ResponseModel
            {
                Code = StatusCodes.Status500InternalServerError,
                Message = "Cannot sign up."
            };
        }
    }

}
