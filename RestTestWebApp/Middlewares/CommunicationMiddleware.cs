using Microsoft.AspNetCore.Http;
using RestTestWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestTestWebApp.Middlewares
{
    public class CommunicationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUserService _userService;

        public CommunicationMiddleware(RequestDelegate next, IUserService userService)
        {
            _next = next;
            _userService = userService;
        }

        //this is executed every request
        public async Task Invoke(HttpContext context)
        {
            await _next.Invoke(context);
        }
    }
}
