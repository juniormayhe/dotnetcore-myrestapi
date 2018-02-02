using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using RestTestWebApp.Models;
using RestTestWebApp.Services;

namespace RestTestWebApp.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/User")]
    public class UserController : Controller
    {
        readonly IUserService _userService;
        readonly IEmailService _emailService;
        readonly ILogger<UserController> _logger;

        private IStringLocalizer<UserController> _stringLocalizer;

        public string Message { get; private set; }

        public UserController(IUserService userService, IEmailService emailService, IStringLocalizer<UserController> stringLocalizer, ILogger<UserController> logger)
        {
            _userService = userService;
            _emailService = emailService;
            _stringLocalizer = stringLocalizer;
            _logger = logger;
        }

        // GET: api/User
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        [HttpPost]
        public async Task<RegistrationResponse> Post(UserModel userModel)
        {
            if (!ModelState.IsValid) {
                return await Task.FromResult<RegistrationResponse>(new RegistrationResponse { Message = _stringLocalizer["InvalidData"] });
            }

            var result = await _userService.Register(userModel);

            try {
                _emailService.SendEmail(userModel.Email, _stringLocalizer["EmailSubjectUserRegistered"], _stringLocalizer["EmailBodyUserRegistered", userModel.Email]).Wait();
            }
            catch (Exception ex) {
                string url = string.Concat(this.Request.Scheme, "://", this.Request.Host, this.Request.Path, this.Request.QueryString);
                _logger.LogError(ex, $"Falla al intentar enviar correo de confirmación de registro para {userModel.Email} en {url}");
            }

            return await Task.FromResult<RegistrationResponse>(new RegistrationResponse { Success = result, Message = _stringLocalizer["UserRegistered"] });
            
        }
        
        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
