using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TotalNetCore.DDDAPISample.Application.Configuration.Emails;

namespace TotalNetCore.DDDAPISample.Infrastructure.Emails
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(EmailMessage message)
        {
            // Integration with email service.

            return;
        }
    }
}
