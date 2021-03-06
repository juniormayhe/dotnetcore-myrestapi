﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestTestWebApp.Services
{
    /// <summary>
    /// https://sendgrid.com/pricing/ 40.000 mails for 9.95 /monthly
    /// </summary>
    public class SendGridEmailService : IEmailService
    {
        private EmailServiceSettings _emailServiceOptions;
        private ILogger<EmailService> _logger;
        public SendGridEmailService(IOptions<EmailServiceSettings> emailServiceOptions, ILogger<EmailService> logger)
        {
            _emailServiceOptions = emailServiceOptions.Value;
            _logger = logger;
        }

        public Task SendEmail(string emailTo, string subject, string message)
        {
            _logger.LogInformation($"##Start## Sending email via SendGrid to: {emailTo} subject: {subject} message: {message}"); 
            var client = new SendGrid.SendGridClient(_emailServiceOptions.RemoteServerAPI);
            var sendGridMessage = new SendGrid.Helpers.Mail.SendGridMessage { From = new SendGrid.Helpers.Mail.EmailAddress(_emailServiceOptions.UserId) };
            sendGridMessage.AddTo(emailTo);
            sendGridMessage.Subject = subject;
            sendGridMessage.HtmlContent = message;
            client.SendEmailAsync(sendGridMessage);
            return Task.CompletedTask;
        }
    }
}
