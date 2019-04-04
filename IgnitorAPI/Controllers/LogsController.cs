using Microsoft.AspNetCore.Mvc;
using NLog;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace IgnitorAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/logs")]
    [ApiController]
    public class LogsController : Controller
    {
        /// <summary>
        /// Get all API logs.
        /// </summary>
        /// <returns>Collection of API logs.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var target = LogManager.Configuration.FindTargetByName<MemoryTarget>("memory-log");
            var logs = target.Logs;

            return Ok(logs);
        }

        /// <summary>
        /// Get API logs filtered by minimum timestamp.
        /// </summary>
        /// <param name="startDate">Minimum timestamp filter.</param>
        /// <returns>Filtered collection of API logs.</returns>
        [HttpGet("date/{startDate}")]
        public ActionResult<IEnumerable<string>> Get(DateTime startDate)
        {
            List<string> logList = new List<string>();

            var target = LogManager.Configuration.FindTargetByName<MemoryTarget>("memory-log");
            foreach(string log in target.Logs)
            {
                DateTime logDate = Convert.ToDateTime(log.Substring(0, 24));

                if(logDate >= startDate)
                {
                    logList.Add(log);
                }
            }

            return Ok(logList);
        }

        /// <summary>
        /// Get API logs filtered by log level. 
        /// </summary>
        /// <param name="level">Log level filter, allowed values: Debug, Info, Warn, Error, Fatal.</param>
        /// <returns>Filtered collection of API logs.</returns>
        [HttpGet("level/{level}")]
        public ActionResult<IEnumerable<string>> Get(string level)
        {
            List<string> logList = new List<string>();

            string pattern = string.Format(@"\|{0}\|", level.ToUpper());

            var target = LogManager.Configuration.FindTargetByName<MemoryTarget>("memory-log");
            foreach (string log in target.Logs)
            {
                if (Regex.IsMatch(log, pattern))
                {
                    logList.Add(log);
                }
            }

            return Ok(logList);
        }

        /// <summary>
        /// Get API logs filtered by minimum timestamp and log level. 
        /// </summary>
        /// <param name="startDate">Minimum timestamp filter.</param>
        /// <param name="level">Log level filter, allowed values: Debug, Info, Warn, Error, Fatal.</param>
        /// <returns>Filtered collection of API logs.</returns>
        [HttpGet("datelevel/{startDate}/{level}")]
        public ActionResult<IEnumerable<string>> Get(DateTime startDate, string level)
        {
            List<string> logList = new List<string>();

            string pattern = string.Format(@"\|{0}\|", level.ToUpper());

            var target = LogManager.Configuration.FindTargetByName<MemoryTarget>("memory-log");
            foreach (string log in target.Logs)
            {
                DateTime logDate = Convert.ToDateTime(log.Substring(0, 24));

                if ((logDate >= startDate) && (Regex.IsMatch(log, pattern)))
                {
                    logList.Add(log);
                }
            }

            return Ok(logList);
        }

    }
}
