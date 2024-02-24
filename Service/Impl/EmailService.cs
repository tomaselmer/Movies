using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

public class EmailService : IEmailService
{
    private readonly ISendGridClient _sendGridClient;

    public EmailService(ISendGridClient sendGridClient)
    {
        _sendGridClient = sendGridClient ?? throw new ArgumentNullException(nameof(sendGridClient));
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var message = new SendGridMessage
        {
            From = new EmailAddress("tomaselmerg@gmail.com", "Tomas Elmer"),
            Subject = subject,
            PlainTextContent = body,
            HtmlContent = body
        };
        message.AddTo(new EmailAddress(toEmail));

        var response = await _sendGridClient.SendEmailAsync(message);

        if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.Accepted)
        {
            throw new InvalidOperationException($"Failed to send email. Status code: {response.StatusCode}");
        }
    }
}
