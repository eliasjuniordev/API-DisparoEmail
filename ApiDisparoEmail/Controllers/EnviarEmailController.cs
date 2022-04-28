using ApiDisparoEmail.Interface;
using ApiDisparoEmail.Model;
using ApiDisparoEmail.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ApiDisparoEmail.Service.EmailService;

namespace ApiDisparoEmail.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnviarEmailController : ControllerBase
    {

        private readonly IEmailService emailService;

        public EnviarEmailController(IEmailService emailService)
        {
            this.emailService = emailService;
        }


        [HttpPost]
        public async Task<IActionResult> EnviarEmail([FromForm] Email request)
        {
            try
            {
                await emailService.EnvioEmail(request);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
