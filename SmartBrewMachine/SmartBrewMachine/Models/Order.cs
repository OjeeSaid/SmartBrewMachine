using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBrewMachine.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = "";
        public Recipe Drink { get; set; } = new Recipe();
        public bool IsPaid { get; set; } = false;

        public string PaymentMethod { get; set; } = "";
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
