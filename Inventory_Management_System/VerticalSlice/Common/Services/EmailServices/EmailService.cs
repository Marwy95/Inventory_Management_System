using Inventory_Management_System.VerticalSlice.Common.Enums;
using MimeKit;
using MailKit.Net.Smtp;

namespace Inventory_Management_System.VerticalSlice.Common.Services.EmailServices
{
    public class EmailService:IEmailService
    {
        public async Task<ResultDto<bool>> SendEmailAsync(SendEmailDto payload)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(Environment.GetEnvironmentVariable("SENDER_NAME"), Environment.GetEnvironmentVariable("EMAIL_ADDRESS")));
            message.To.Add(new MailboxAddress("", payload.To));
            message.Subject = payload.Subject;
            message.Body = new TextPart("plain")
            {
                Text = payload.Body
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(Environment.GetEnvironmentVariable("EMAIL_HOST"), 587, MailKit.Security.SecureSocketOptions.StartTls);

                    client.Authenticate(Environment.GetEnvironmentVariable("EMAIL_ADDRESS"), Environment.GetEnvironmentVariable("EMAIL_PASSWORD"));
                    client.Send(message);
                }
                catch (Exception e)
                {
                    return ResultDto<bool>.Faliure(ErrorCode.UnableToSendEmail, "Sorry we are able to send email at the moment");
                }
                finally
                {

                    client.Disconnect(true);
                }
                return ResultDto<bool>.Sucess(true, "Email sent successful");

            }
        }
    }
}
