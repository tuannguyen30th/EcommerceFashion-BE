using Application.AccountService.Models.ResponseModels;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Infrastructure.UnitOfWork;
using Infrastructure.Utils;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AccountService.Commands
{
    public record AccountGenerateJwtTokenCommand(Account Account, ClaimsPrincipal? Principal = null) : IRequest<TokenModel?>;

    public class AccountGenerateJwtTokenCommandHandler : IRequestHandler<AccountGenerateJwtTokenCommand, TokenModel?>
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public AccountGenerateJwtTokenCommandHandler(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public async Task<TokenModel?> Handle(AccountGenerateJwtTokenCommand request, CancellationToken cancellationToken)
        {
            var account = request.Account;
            if (account == null)
                throw new ArgumentNullException(nameof(account), "Account cannot be null.");

            // Prepare claims
            var authClaims = new List<Claim>
    {
        new Claim("accountId", account.Id.ToString()),
        new Claim("accountEmail", account.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var deviceId = Guid.NewGuid();
            authClaims.Add(new Claim("deviceId", deviceId.ToString()));

            // Add roles to claims
            var roles = await _unitOfWork.RoleRepository.GetAllByAccountIdAsync(account.Id);
            Console.WriteLine($"Roles for account {account.Id}: {string.Join(", ", roles.Select(r => r.Name))}");

            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            // Generate a refresh token
            var refreshTokenString = AuthenticationTools.GenerateUniqueToken(DateTime.UtcNow.AddDays(Constant.RefreshTokenValidityInDays));

            // Create JWT without SaveChangeAsync
            var jwtToken = AuthenticationTools.CreateJwtToken(authClaims, _configuration);
            Console.WriteLine("JWT Token created successfully.");

            return new TokenModel
            {
                DeviceId = deviceId,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                RefreshToken = refreshTokenString
            };
        }

    }
}
