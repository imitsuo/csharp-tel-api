using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;


namespace tel_api.Configurations.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                var statusCode = exception is ValidationException ? HttpStatusCode.BadRequest : HttpStatusCode.InternalServerError;

                context.Response.StatusCode = (int) statusCode;
                context.Response.ContentType = "application/json";

                var error = new 
                { 
                    message = "Algo de certo deu errado !", 
                    errors = new List<string>() 
                };

                if (exception is ValidationException)
                {
                    foreach(var err in ((ValidationException)exception).Errors)
                        error.errors.Add(err.ToString());
                }
                
                await context.Response.WriteAsync(JsonConvert.SerializeObject(error));

                context.Response.Headers.Clear();
            }        
        }
    }
}
