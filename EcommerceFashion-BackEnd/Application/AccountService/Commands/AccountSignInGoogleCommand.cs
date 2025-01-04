//using Application.AccountService.Models.RequestModels;
//using AutoMapper;
//using Domain.Entities;
//using Infrastructure.Common;
//using Infrastructure.IRepositories;
//using Infrastructure.Model.ResponseModel;
//using Infrastructure.UnitOfWork;
//using Infrastructure.Utils;
//using MediatR;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http.Headers;
//using System.Net.Http.Json;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;

//namespace Application.AccountService.Commands
//{
//    public record AccountSignInGoogleCommand(string code) : IRequest<ResponseModel>;
//    public class AccountSignInGoogleCommandHanlder : IRequestHandler<AccountSignInGoogleCommand, ResponseModel>
//    {
//        private readonly IClaimService _claimService;
//        private readonly IConfiguration _configuration;
//        private readonly IMapper _mapper;
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IMediator _mediator;

//        public AccountSignInGoogleCommandHanlder(IClaimService claimService, IConfiguration configuration, IMapper mapper, IUnitOfWork unitOfWork, IMediator mediator)
//        {
//            _claimService = claimService;
//            _configuration = configuration;
//            _mapper = mapper;
//            _unitOfWork = unitOfWork;
//            _mediator = mediator;
//        }

//        public async Task<ResponseModel> Handle(AccountSignInGoogleCommand request, CancellationToken cancellationToken)
//        {
//            var clientId = _configuration["OAuth2:Google:ClientId"];
//            ArgumentException.ThrowIfNullOrWhiteSpace(clientId);
//            var clientSecret = _configuration["OAuth2:Google:ClientSecret"];
//            ArgumentException.ThrowIfNullOrWhiteSpace(clientSecret);
//            var redirectUrl = _configuration["URL:Client"];
//            ArgumentException.ThrowIfNullOrWhiteSpace(redirectUrl);

//            // Exchange authorization code for refresh and access tokens
//            // Document: https://developers.google.com/identity/protocols/oauth2/web-server#exchange-authorization-code
//            var googleTokenClient = new HttpClient();
//            var googleTokenResponse = await googleTokenClient.PostAsJsonAsync(
//                "https://oauth2.googleapis.com/token", new
//                {
//                    client_id = clientId,
//                    client_secret = clientSecret,
//                    request.code,
//                    grant_type = "authorization_code",
//                    redirect_uri = redirectUrl
//                });
//            if (!googleTokenResponse.IsSuccessStatusCode)
//                return new ResponseModel
//                {
//                    Code = StatusCodes.Status500InternalServerError,
//                    Message = "Error when trying to connect to Google API"
//                };

//            // Get user information with Google access token
//            var googleTokenModel =
//                JsonSerializer.Deserialize<Models.ResponseModels.GoogleTokenModel>(await googleTokenResponse.Content.ReadAsStringAsync());
//            var googleUserInformationClient = new HttpClient();
//            googleUserInformationClient.DefaultRequestHeaders.Authorization =
//                new AuthenticationHeaderValue("Bearer", googleTokenModel!.AccessToken);
//            var googleUserInformationResponse =
//                await googleUserInformationClient.GetAsync("https://www.googleapis.com/oauth2/v3/userinfo");
//            if (!googleUserInformationResponse.IsSuccessStatusCode)
//                return new ResponseModel
//                {
//                    Code = StatusCodes.Status500InternalServerError,
//                    Message = "Error when trying to connect to Google API"
//                };

//            // Handle business
//            var googleUserInformationModel =
//                JsonSerializer.Deserialize<GoogleUserInformationModel>(await googleUserInformationResponse.Content
//                    .ReadAsStringAsync());
//            Console.WriteLine(await googleUserInformationResponse.Content
//                .ReadAsStringAsync());
//            var account = await _unitOfWork.AccountRepository.FindByEmailAsync(googleUserInformationModel!.Email);
//            if (account != null)
//            {
//                var tokenModel = await _mediator.Send(new AccountGenerateJwtTokenCommand(account));
//                if (tokenModel != null)
//                    return new ResponseModel
//                    {
//                        Message = "Sign in successfully",
//                        Data = tokenModel
//                    };

//                return new ResponseModel
//                {
//                    Code = StatusCodes.Status500InternalServerError,
//                    Message = "Cannot sign in"
//                };
//            }

//            account = new Account
//            {
//                FirstName = googleUserInformationModel.FirstName,
//                LastName = googleUserInformationModel.LastName,
//                Username = AuthenticationTools.GenerateUsername(),
//                Email = googleUserInformationModel.Email,
//                HashedPassword = AuthenticationTools.HashPassword(AuthenticationTools.GenerateUniqueToken()),
//                Image = googleUserInformationModel.Image,
//                EmailConfirmed = true
//            };
//            account.Credit = new Credit
//            {
//                Balance = 0,
//                CreatedById = account.Id
//            };
//            await _unitOfWork.AccountRepository.AddAsync(account);

//            // Add "user" role as default
//            var role = await _unitOfWork.RoleRepository.FindByNameAsync(Domain.Enums.Role.User.ToString());
//            var accountRole = new AccountRole
//            {
//                Account = account,
//                Role = role!
//            };
//            await _unitOfWork.AccountRoleRepository.AddAsync(accountRole);
//            if (await _unitOfWork.SaveChangeAsync() > 0)
//            {

//                var tokenModel = await _mediator.Send(new AccountGenerateJwtTokenCommand(account));
//                if (tokenModel != null)
//                    return new ResponseModel
//                    {
//                        Message = "Sign in successfully",
//                        Data = tokenModel
//                    };

//                return new ResponseModel
//                {
//                    Code = StatusCodes.Status500InternalServerError,
//                    Message = "Cannot sign in"
//                };
//            }

//            return new ResponseModel
//            {
//                Code = StatusCodes.Status500InternalServerError,
//                Message = "Cannot sign in"
//            };
//        }
//    }
//}
