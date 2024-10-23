using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SocialMedia.Core.Exceptions;

namespace SocialMedia.Infraestructure.Filter
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType()==typeof(BusinessExeptions)){

                var Exception=(BusinessExeptions)context.Exception;

                var validation= new{
                    Status=400,
                    Title="Bad Request",
                    Detail=Exception.Message
                };

                var json= new{
                    error=new[]{validation}
                };

                context.Result=new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.ExceptionHandled=true;
            }
        }
    }
}