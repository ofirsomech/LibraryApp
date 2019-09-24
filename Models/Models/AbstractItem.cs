using System;

namespace Models.Models
{
    public enum CategoryBook { Horror, Comedy, Army, Action, Warior };

    public abstract class AbstractItem
    {
        public static int Count { get; private set; } = 0;
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Type { get; set; }
        public Guid ISBN { get; set; } = Guid.NewGuid();
        static int isbnCounter;
        public string Publisher { get; set; }
        public DateTime PrintDate { get; set; }
        public int CopyNumber { get; set; }
        public string Topic { get; set; }
        public double Price { get; set; }
        public double PriceAfter { get; set; }
        public int Discount { get; set; }
        public AbstractItem()
        {
            Id = ++Count;
        }
        public virtual void SetDiscount(double discount = 100)
        {
            PriceAfter = (1 - discount / 100) * Price;
        }

        //public override string ToString()
        //{
        //    return $"{Title} :{Month} {CopyNumber} : {PrintDate} -  ({GetType().Name})";
        //}
    }
}
