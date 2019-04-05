using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IgnitorAPI.Models
{
    /// <summary>
    /// This class models the options found in the appsettings.json file.
    /// </summary>
    public class AppOptions
    {
        public string Option1 { get; set; }

        public int Option2 { get; set; }

        public AppOptions()
        {
            // Set default value.
            Option1 = "value1_from_ctor";
            Option2 = 5;
        }
    }
}
