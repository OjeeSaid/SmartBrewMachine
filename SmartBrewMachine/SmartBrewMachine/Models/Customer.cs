using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBrewMachine.Models
{
    public class Customer : User
    {
        public List<Recipe> SavedRecipes { get; set; } = new();
    }
}
