using System.ComponentModel.DataAnnotations;

namespace nemsport.Models
{
    public class Club
    {
        public int ClubID { get; set; }
        public string ClubName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }
    }
}
