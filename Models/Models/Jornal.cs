using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Jornal : AbstractItem
    {
        // public CategoryJurnal categoryJurnal { get; set; }
        public string Month { get; set; }

        public Jornal()
        {
            this.Type = "Journal";
        }

        public override void SetDiscount(double discount = 10)
        {
            PriceAfter = (1 - discount / 100) * Price;
        }
    }
}