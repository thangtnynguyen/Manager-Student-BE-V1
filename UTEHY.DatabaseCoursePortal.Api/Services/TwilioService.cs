using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Twilio;
using Twilio.TwiML.Voice;
using Twilio.Types;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using Twilio.Rest.Api.V2010.Account;

namespace UTEHY.DatabaseCoursePortal.Api.Services
{
    public class TwilioService
    {
        private readonly IConfiguration _config;

        public TwilioService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> SendMessage(string message, string numberphone)
        {
            TwilioClient.Init(_config["Twilio:AccountSID"], _config["Twilio:AuthToken"]);

            var twilioMessage = MessageResource.CreateAsync(
               body: message,
               from: new PhoneNumber(_config["Twilio:PhoneNumber"]),
               to: new PhoneNumber(numberphone)
           );

            int counter = 0;
            while (!twilioMessage.IsCompleted)
            {
                await System.Threading.Tasks.Task.Delay(1000);

                counter++;

                if (counter >= 10)
                {
                    throw new Exception($"Gửi tin nhắn thất bại, quá thời gian chờ!");
                }
            }

            if (!twilioMessage.IsCompletedSuccessfully)
            {
                throw new Exception($"Gửi tin nhắn thất bại: " + twilioMessage.Result.ErrorMessage);
            }

            return true;
        }
    }
}
