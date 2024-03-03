using nemsport.Models.UserModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace nemsport.Models.ProductModels
{
    public class ClubUser
    {
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; } // Navigation property to User
        [ForeignKey("ClubId")]
        public int ClubId { get; set; }
        public Club Club { get; set; } // Navigation property to Club
        public bool IsClubResponsible { get; set; }
    }
}
