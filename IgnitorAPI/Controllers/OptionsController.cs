using System.Threading.Tasks;
﻿using IgnitorAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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
        [HttpGet("user")]
        public UserOptions GetUserOptions()
        {
            return _options;
        }
    }
}
