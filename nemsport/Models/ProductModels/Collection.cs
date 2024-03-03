using System.ComponentModel.DataAnnotations;

namespace nemsport.Models.ProductModels
{
    public class Collection
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }

    }
}
