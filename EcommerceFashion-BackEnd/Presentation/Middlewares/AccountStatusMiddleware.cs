using System.Security.Claims;
using Domain.Common;
using Domain.Entities;
using Infrastructure.IRepositories;
using Infrastructure.Model.ResponseModel;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Text.Json;

namespace Presentation.Middlewares;

public class AccountStatusMiddleware : IMiddleware
{
    private readonly IClaimService _claimService;
    private readonly IUnitOfWork _unitOfWork;

    public AccountStatusMiddleware(IUnitOfWork unitOfWork,
        IClaimService claimService)
    {
        _unitOfWork = unitOfWork;
        _claimService = claimService;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var currentUserId = _claimService.GetCurrentUserId;
        if (currentUserId.HasValue)
        {
            var account = await _unitOfWork.AccountRepository.GetAsync(currentUserId.Value);
            if (account != null && account.IsDeleted)
            {
                var response = new ResponseModel
                {
                    Code = StatusCodes.Status401Unauthorized,
                    Message = "Your account has been deleted",
                    Data = new
                    {
                        IsDeleted = true
                    }
                };
                var jsonResponse = System.Text.Json.JsonSerializer.Serialize(response);
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(jsonResponse);

                return;
            }
        }

        await next(context);
    }
}