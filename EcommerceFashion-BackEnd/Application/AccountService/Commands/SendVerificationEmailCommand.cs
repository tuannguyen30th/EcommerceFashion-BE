using Application.AccountService.Models;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AccountService.Commands
{
    public record SendVerificationEmailCommand(Account account) : IRequest;
    public class SendVerificationEmailCommandHandler : IRequestHandler<SendVerificationEmailCommand>
    {
        private readonly IMediator _mediator;

        public SendVerificationEmailCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(SendVerificationEmailCommand request, CancellationToken cancellationToken)
        {
            var emailRequest = new SendEmailModel
            {
                ToEmail = request.account.Email!,
                Subject = "Verify your email",
                Body = GenerateEmailBody(request.account?.FirstName ?? "User", request.account?.VerificationCode ?? ""),
                IsBodyHtml = true
            };

            var sendEmailCommand = new SendEmailCommand(emailRequest);

            await _mediator.Send(sendEmailCommand);
        }
        private string GenerateEmailBody(string fullName, string otp)
        {
            return $@"
   <body style=""display: flex; justify-content: center; align-items: center"">
    <div>
      <div
        style=""
          color: #536e88;
          width: fit-content;
          box-shadow: 0 2px 8px rgba(8, 120, 211, 0.2);
          padding: 10px;
          border-radius: 5px;
        ""
      >
        <div
          style=""
            display: flex;
            justify-content: center;
            align-items: center;
            height: 10px;
            margin-top: 0px;
            background-color: #3498db;
            font-size: 0.875rem;
            font-weight: bold;
            color: #ffffff;
          ""
        ></div>

        <h1 style=""text-align: center; color: #3498db"">
          Welcome to
          <span style=""color: #f99f41"">our website!</span>
        </h1>

        <div style=""text-align: center"">
        </div>

        <p style=""text-align: center; font-weight: bold; margin-top: 0"">
          <span style=""color: #f99f41"">THE PRODUCT </span
          ><span style=""color: #3498db"">SALE</span>
        </p>

        <div
          style=""
            width: fit-content;
            margin: auto;
            box-shadow: 0 2px 8px rgba(8, 120, 211, 0.2);
            padding-top: 10px;
            border-radius: 10px;
          ""
        >
          <p>
            Xin chào,
            <span style=""font-weight: bold; color: #0d1226"">{fullName ?? "Người dùng"}</span>
          </p>
          <p>
            <span style=""font-weight: bold"">THE PRODUCT SALE </span>would like to inform you that your account has been successfully registered.<span></span>
          </p>
          <p>
            <span>Your OTP code is: </span
            ><span style=""color: #0d1226; font-weight: bold"">{otp}</span>
          </p>
          <p>The code will expire in 15 minutes !</p>
          <p>Thank you very much for using our platform !</p>
          <p>Pleasure,</p>
          <p style=""font-weight: 700; color: #0d1226"">THE PRODUCT SALE</p>
        </div>

        <div
          style=""
            display: flex;
            justify-content: center;
            align-items: center;
            height: 40px;
            background-color: #3498db;
            font-size: 0.875rem;
            font-weight: bold;
            color: #ffffff;
          ""
        >
          © 2024 | Copyright belongs to THE PRODUCT SALE.
        </div>
      </div>
    </div>
  </body>

    ";
        }
    }
}
