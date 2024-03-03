using System.ComponentModel.DataAnnotations.Schema;

namespace nemsport.Models.ProductModels
{
    public class ProductAttributeValue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("ProductAttributeId")]
        public int ProductAttributeId { get; set; }
    }
}
