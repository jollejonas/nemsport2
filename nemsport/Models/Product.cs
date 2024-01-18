using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nemsport.Models
{
    public class Product
    {
        public int BaseProductID { get; set; }
        public string ProductName { get; set; }
        public decimal BasePrice { get; set; }
        public string Description { get ; set; }
        public int ClubID { get; set; }
        public int GenderID { get; set; }

        [ForeignKey("ClubID")]
        public virtual Club Club { get; set; }
        [ForeignKey("GenderID")]
        public virtual Gender Gender { get; set; }
    }
}
