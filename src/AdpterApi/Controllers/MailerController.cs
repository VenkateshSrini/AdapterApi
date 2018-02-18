using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AdpterApi.Configuration;
using AdpterApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AdpterApi.Controllers
{
    [Route("api/[controller]")]
    public class MailerController : Controller
    {
         ILogger _logger;
         MailerSettings _mailerSettings;
        Guid _corelationGuid;
        public MailerController(ILoggerFactory loggerFactory, IOptions<MailerSettings> mailerSettings)
        {
            _logger = loggerFactory.CreateLogger<MailerController>();
            _mailerSettings = mailerSettings.Value;
            _corelationGuid = Guid.NewGuid();
            _logger.LogInformation($"corelation guid :-{_corelationGuid.ToString()}");
        }
        [HttpGet()]
        public IActionResult Get()
        {
            return Ok("Application is running successfully");
        }

       
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AppDetails appDetails)
        {
            OutgoingMailModel mailModel = new OutgoingMailModel
            {
                FromAddress = _mailerSettings.FromAddress,
                ToAddress = new List<string>(_mailerSettings.ToAddress.Split(';')),
                Subject = $"{appDetails.AlertType} in application {appDetails.AppName} with app id {appDetails.AppID}",
                Body = appDetails.Message

            };
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = null;
            try
            {
                StringContent mailContent = new StringContent(JsonConvert.SerializeObject(mailModel), Encoding.UTF8, "application/json");
                response = await httpClient.PostAsync(_mailerSettings.MailServiceUrl, mailContent);
                response.EnsureSuccessStatusCode();
                return Ok(new APIResponse
                {
                    CorelationID = _corelationGuid.ToString(),
                    Status = "Mail sent sucessfully",
                    StatusCode = HttpStatusCode.OK
                });
            }

            catch (Exception ex)
            {
                Console.Write($"error sending mail {ex.Message}");
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new APIResponse
                {
                    CorelationID = _corelationGuid.ToString(),
                    Status = $"Error while seding mail exception:-{ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError,

                });
                
            }


        }

       
    }
}
