using System.ComponentModel.DataAnnotations.Schema;

namespace nemsport.Models.ProductModels
{
    public class ProductSport
    {
        public int Id { get; set; }
        [ForeignKey("BaseProductId")]
        public int BaseProductId { get; set; }
        public BaseProduct BaseProduct { get; set; }
        [ForeignKey("SportId")]
        public int SportId { get; set; }
        public Sport Sport { get; set; }
    }
}
