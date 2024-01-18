using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace nemsport.Models
{
    public class ProductDetail
    {
        public int ProductID { get; set; }
        public int SizeID { get; set; }
        public int ColorID { get; set; }
        public decimal price { get; set; }
        public int Quantity { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }

        [ForeignKey("SizeID")]
        public virtual Size Size { get; set; }
        [ForeignKey("ColorID")]
        public virtual Color Color { get; set; }

        public virtual ICollection<SportCategoryRelation> SportCategories { get; set; }
        public virtual ICollection<CategoryRelation> Categories { get; set; }
    }
}
