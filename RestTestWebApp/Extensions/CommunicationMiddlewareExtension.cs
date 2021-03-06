﻿using Microsoft.AspNetCore.Builder;
using RestTestWebApp.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestTestWebApp.Extensions
{
    public static class CommunicationMiddlewareExtension
    {
        public static IApplicationBuilder UseCommunicationMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CommunicationMiddleware>();
        }
    }
}
