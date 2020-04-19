using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWENAR.Services
{
    public class EmailSender : IEmailSender
    {
        async Task IEmailSender.SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await Task.FromResult(0);
        }
    }
}
