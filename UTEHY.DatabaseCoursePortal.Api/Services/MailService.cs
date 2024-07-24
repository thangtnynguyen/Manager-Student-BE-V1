using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using UTEHY.DatabaseCoursePortal.Api.Models.Mail;
using UTEHY.DatabaseCoursePortal.Api.Exceptions;

namespace UTEHY.DatabaseCoursePortal.Api.Services
{
    public class MailService
    {
        private readonly IConfiguration _config;

        public MailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> Send(SendMailRequest request)
        {
            try
            {
                using (MailMessage mail = new MailMessage(_config["Smtp:Username"],request.ToEmail))
                {
                    mail.Subject = request.Subject;
                    mail.Body = request.Body;
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient(_config["Smtp:Host"], int.Parse(_config["Smtp:Port"])))
                    {
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(_config["Smtp:Username"], _config["Smtp:Password"]);
                        await smtp.SendMailAsync(mail);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new ApiException("Gửi email thất bại: " + ex.Message, Constants.HttpStatusCode.BadRequest);
            }
        }
    }
}
