using System.ComponentModel.DataAnnotations.Schema;

namespace nemsport.Models
{
    public class SportCategoryRelation
    {
        public int SportCategoryRelationID { get; set; }
        public int SportID { get; set; }
        public int BaseProductID { get; set; }

        [ForeignKey("SportID")]
        public virtual Sport Sport { get; set; }
        [ForeignKey("BaseProductID")]
        public virtual Product Product { get; set; }
    }
}
