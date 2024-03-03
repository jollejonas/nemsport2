using System.IO;

namespace nemsport.DTOs.ProductDTOs
{
    public class BaseProductCreateDTO
    {
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
        public string Description { get; set; }
        public string Gender { get; set; }
        public int CollectionId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string UnitType { get; set; }
        public List<int> SportIds { get; set; }
    }
}
