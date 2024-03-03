using System.ComponentModel.DataAnnotations.Schema;

namespace nemsport.Models.ProductModels
{
    public class ClubProduct
    {
        public int Id { get; set; }
        [ForeignKey("ClubId")]
        public int ClubId { get; set; }
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
    }
}
