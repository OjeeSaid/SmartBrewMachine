using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBrewMachine.Models
{
    public class Recipe
    {
        public string Name { get; set; } = "";
        public double Price { get; set; }

        public string Size { get; set; } = "Medium";
        public int ExtraSugar { get; set; } = 0;
        public bool ExtraMilk { get; set; } = false;
    }
}
