using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    public enum CategoryBook { Horror, Comedy, Army, Action, Warior };
    [Serializable]
    public abstract class AbstractItem
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        [Required(ErrorMessage = "Required field!")]
        public string Title { get; set; }
        public string Type { get; set; } 
        public Guid ISBN { get; set; } = Guid.NewGuid();
        public string Publisher { get; set; }
        public DateTime PrintDate { get; set; }
        public int CopyNumber { get; set; }
        public string Topic { get; set; }
        [Required]
        public double Price { get; set; }
        public double PriceAfter { get; set; } 
        public int Discount { get; set; }
        public bool IsActive { get; set; } = true;



        public virtual void SetDiscount(double discount = 100)
        {
            PriceAfter = (1 - discount / 100) * Price;
        }
    }
}
