using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IgnitorAPI.Models
{
    public class UserOptions
    {
        public string Option1 { get; set; }

        public int Option2 { get; set; } = 5;

        public UserOptions()
        {
            // Set default value.
            Option1 = "value1_from_ctor";
        }
    }
}
