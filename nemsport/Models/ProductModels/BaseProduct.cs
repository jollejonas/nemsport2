using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nemsport.Models.ProductModels
{
    public class BaseProduct
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        [Required]
        public decimal BasePrice { get; set; }
        public string Description { get; set; }
        public string Gender { get; set; }
        [ForeignKey("CollectionId")]
        public int CollectionId { get; set; }
        public Collection Collection { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }
        public string UnitType { get; set; }
        public virtual ICollection<ProductSport> ProductSports { get; set; } = new List<ProductSport>();

    }
}
