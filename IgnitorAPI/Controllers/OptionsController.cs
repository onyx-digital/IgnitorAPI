using IgnitorAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IgnitorAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/options")]
    [ApiController]
    public class OptionsController : Controller
    {
        private readonly UserOptions _options;

        public OptionsController(IOptionsMonitor<UserOptions> optionsAccessor)
        {
            _options = optionsAccessor.CurrentValue;
        }

        /// <summary>
        /// Get all user options.
        /// </summary>
        /// <returns>All user options.</returns>
        [HttpGet("userOptions")]
        public ActionResult<UserOptions> GetUserOptions()
        {
            return Ok(_options);
        }

        /// <summary>
        /// Get the value of a specified user option.
        /// </summary>
        /// <param name="optionName">Specific user option required.</param>
        /// <returns>Value of the specified user option.</returns>
        /// <response code="404">Option not found.</response>
        [HttpGet("userOption/{optionName}")]
        public ActionResult<string> GetUserOption(string optionName)
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
