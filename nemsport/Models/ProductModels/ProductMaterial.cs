using System.ComponentModel.DataAnnotations.Schema;

namespace nemsport.Models.ProductModels
{
    public class ProductMaterial
    {
        public int Id { get; set; }
        [ForeignKey("BaseProductId")]
        public int BaseProductId { get; set; }
        [ForeignKey("MaterialId")]
        public int MaterialId { get; set; }
    }
}
