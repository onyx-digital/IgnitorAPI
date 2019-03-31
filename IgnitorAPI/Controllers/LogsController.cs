using Microsoft.AspNetCore.Mvc;
using NLog;
using NLog.Targets;
using System;
using System.Collections.Generic;

namespace IgnitorAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : Controller
    {
        /// <summary>
        /// Get all API logs.
        /// </summary>
        /// <returns>Collection of API logs.</returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var target = LogManager.Configuration.FindTargetByName<MemoryTarget>("memory-log");
            var logs = target.Logs;

            return logs;
        }

        /// <summary>
        /// Get API logs from a starting date and time.
        /// </summary>
        /// <param name="startDate">Start date and time of the logs collection.</param>
        /// <returns>Filtered collection of API logs.</returns>
        [HttpGet("{startDate}")]
        public IEnumerable<string> Get(DateTime startDate)
        {
            var target = LogManager.Configuration.FindTargetByName<MemoryTarget>("memory-log");

            List<string> logList = new List<string>();
            foreach(string log in target.Logs)
            {
                DateTime logDate = Convert.ToDateTime(log.Substring(0, 24));

                if(logDate >= startDate)
                {
                    logList.Add(log);
                }
            }

            return logList;
        }
    }
}
