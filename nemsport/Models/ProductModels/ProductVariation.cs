using System.ComponentModel.DataAnnotations.Schema;

namespace nemsport.Models.ProductModels
{
    public class ProductVariation
    {
        public int Id { get; set; }
        [ForeignKey("ProductId")]
        public int ProductID { get; set; }
        [ForeignKey("ProductAttributeId")]
        public int ProductAttributeId { get; set; }
    }
}
