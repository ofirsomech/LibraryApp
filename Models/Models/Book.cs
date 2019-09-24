using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{

    public class Book : AbstractItem
    {
        
        public CategoryBook categoryBook { get; set; }
        public int Copies { get; set; }

        // public Category Category { get; set; }
        public Book()
        {
            this.Type = "Book";
        }

        //TODO REMOVE
        public override string ToString()
        {
            return base.ToString() + $" ({categoryBook})";
        }
        public override void SetDiscount(double discount = 30)
        {
            PriceAfter = (1 - discount / 100) * Price;
        }

    }
}
