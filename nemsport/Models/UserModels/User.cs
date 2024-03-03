using nemsport.Models.ProductModels;
using System.ComponentModel.DataAnnotations;

namespace nemsport.Models.UserModels
{
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? City { get; set; }
        public DateTime JoinDate { get; set; }
        public string Role { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public virtual ICollection<ClubUser> ClubUsers { get; set; } = new List<ClubUser>();

    }
}
