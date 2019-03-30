using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Targets;

namespace IgnitorAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly Microsoft.Extensions.Logging.ILogger _logger;

        public ValuesController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ValuesController>();
        }

        /// <summary>
        /// Gets a collection of values.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            _logger.LogDebug("debug");
            _logger.LogError("error");
            _logger.LogTrace("trace");
            _logger.LogInformation("info");
            _logger.LogWarning("warn");
            _logger.LogCritical("critical");

            
            var target = LogManager.Configuration.FindTargetByName<MemoryTarget>("memory-log");
            var logs = target.Logs;

            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Gets a specific value.
        /// </summary>
        /// <param name="id">ID of value to get.</param>
        /// <returns>String value.</returns>
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
