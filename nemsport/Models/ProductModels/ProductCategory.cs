using System.ComponentModel.DataAnnotations.Schema;

namespace nemsport.Models.ProductModels
{
    public class ProductCategory
    {
        public int Id { get; set; }
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        [ForeignKey("BaseProductId")]
        public int BaseProductId { get; set; }
    }
}
