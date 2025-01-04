using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Application.AccountService.Models;

namespace Application.AccountService.Commands
{
    public class SendEmailCommand : IRequest
    {
        public SendEmailModel EmailRequest { get; }

        public SendEmailCommand(SendEmailModel emailRequest)
        {
            EmailRequest = emailRequest;
        }
    }

    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand>
    {
        private readonly IConfiguration _configuration;

        public SendEmailCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            string mailServer = _configuration["EmailSettings:MailServer"]!;
            string fromEmail = _configuration["EmailSettings:FromEmail"]!;
            string password = _configuration["EmailSettings:Password"]!;
            int port = int.Parse(_configuration["EmailSettings:MailPort"]!);

            var client = new SmtpClient(mailServer, port)
            {
                Credentials = new NetworkCredential(fromEmail, password),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage(fromEmail, request.EmailRequest.ToEmail, request.EmailRequest.Subject, request.EmailRequest.Body)
            {
                IsBodyHtml = request.EmailRequest.IsBodyHtml
            };

            var rs = client.SendMailAsync(mailMessage);
            return rs;
        }
    }

}
