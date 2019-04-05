using IgnitorAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IgnitorAPI.Controllers
{
    /// <summary>
    /// This controller provides a public API for Option Operations.
    /// </summary>
    [Produces("application/json")]
    [Route("api/options")]
    [ApiController]
    public class OptionsController : Controller
    {
        private readonly AppOptions _options;

        public OptionsController(IOptionsMonitor<AppOptions> optionsAccessor)
        {
            _options = optionsAccessor.CurrentValue;
        }

        /// <summary>
        /// Get all application options.
        /// </summary>
        /// <returns>All application options.</returns>
        [HttpGet("appOptions")]
        public ActionResult<AppOptions> GetAppOptions()
        {
            return Ok(_options);
        }

        /// <summary>
        /// Get the value of a specified application option.
        /// </summary>
        /// <param name="optionName">Specific application option required.</param>
        /// <returns>Value of the requested option.</returns>
        /// <response code="404">Requested option not found.</response>
        [HttpGet("appOption/{optionName}")]
        public ActionResult<string> GetAppOption(string optionName)
        {
            string retVal = string.Empty;

            Type myType = _options.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

            try
            {
                PropertyInfo propInfo = props.First(p => p.Name.ToUpper() == optionName.ToUpper());

                object propValue = propInfo.GetValue(_options, null);
                retVal = propValue.ToString();
            }
            catch (Exception)
            {
                return NotFound();
            }

            return Ok(retVal);
        }
    }
}
