using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nemsport.Models.ProductModels
{
    public class Club
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Initials { get; set; }
        public string Logo { get; set; }
        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; }
        public string Code { get; set; }

        public virtual ICollection<ClubUser> ClubUsers { get; set; } = new List<ClubUser>();

    }
}
