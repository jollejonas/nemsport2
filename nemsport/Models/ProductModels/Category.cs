using System.ComponentModel.DataAnnotations.Schema;

namespace nemsport.Models.ProductModels
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("ParentCategoryId")]
        public int ParentCategoryId { get; set; }
    }
}
