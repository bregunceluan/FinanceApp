using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using SendGrid;
using FinanceApp.Api.Properties.Options;

namespace FinanceApp.Api.Services;

public class EmailSender : IEmailSender
{
    private readonly string _sendGridKey;
    public EmailSender(IOptions<SendGridOptions> options)
    {
        _sendGridKey = options.Value.SendGridKey;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        if (string.IsNullOrEmpty(_sendGridKey))
        {
            throw new Exception("Null SendGridKey");
        }
        await Execute(_sendGridKey, subject, message, toEmail);
    }

    public async Task Execute(string apiKey, string subject, string message, string toEmail)
    {
        var client = new SendGridClient(apiKey);
        var msg = new SendGridMessage()
        {
            From = new EmailAddress("bregunceluan@gmail.com", "Password Recovery"),
            Subject = subject,
            PlainTextContent = message,
            HtmlContent = message
        };
        msg.AddTo(new EmailAddress(toEmail));

        msg.SetClickTracking(false, false);
        var response = await client.SendEmailAsync(msg);
        var g = response.DeserializeResponseBodyAsync().Result;
    }



}
