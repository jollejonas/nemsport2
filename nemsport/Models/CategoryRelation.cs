using System.ComponentModel.DataAnnotations.Schema;

namespace nemsport.Models
{
    public class CategoryRelation
    {
        public int CategoryRelationID { get; set; }
        public int CategoryID { get; set; }
        public int BaseProductID { get; set; }

        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }
        [ForeignKey("BaseProductID")]
        public virtual Product Product { get; set; }
    }
}
