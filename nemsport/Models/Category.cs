using System.ComponentModel.DataAnnotations.Schema;

namespace nemsport.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int ParentCategoryID {  get; set; }

        [ForeignKey("ParentCategoryID")]
        public virtual ParentCategory ParentCategory { get; set; }
    }
}
