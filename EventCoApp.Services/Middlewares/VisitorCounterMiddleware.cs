using EventCoApp.Common.Enums;
using EventCoApp.DataAccessLibrary.DataAccess;
using EventCoApp.DataAccessLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCoApp.Services.Middlewares
{
    public class VisitorCounterMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public VisitorCounterMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context, EventCoContext eventCoContext)
        {
            string visitorId = context.Request.Cookies["VisitorId"];
            if (visitorId == null)
            {
                //don the necessary staffs here to save the count by one
                var exist = eventCoContext.Logs.Any(l => l.Type == LogType.SiteCounter);
                if (!exist)
                {
                    var log = new Log()
                    {
                        CreatedOn = DateTime.Now,
                        Type = LogType.SiteCounter,
                        SiteCounter = 1
                    };
                    eventCoContext.Logs.Add(log);
                }
                else
                {
                    var log= eventCoContext.Logs.FirstOrDefaultAsync(l=>l.Type==LogType.SiteCounter).Result;
                    log.SiteCounter++;
                }
                await eventCoContext.SaveChangesAsync();
                context.Response.Cookies.Append("VisitorId", Guid.NewGuid().ToString(), new CookieOptions()
                {
                    Path = "/",
                    HttpOnly = false,
                    Secure = false,
                });
            }

            await _requestDelegate(context);
        }
    }
}
