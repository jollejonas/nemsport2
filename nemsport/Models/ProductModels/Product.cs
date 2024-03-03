using System.ComponentModel.DataAnnotations.Schema;

namespace nemsport.Models.ProductModels
{
    public class Product
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public bool forcePrice { get; set; } = false;
        public int Quantity { get; set; }
        [ForeignKey("BaseProductId")]
        public int BaseProductId { get; set; }
        [ForeignKey("BrandId")]
        public int BrandId { get; set; }
        public string Status { get; set; }
        public string StatusSoldout { get; set; }
    }
}
