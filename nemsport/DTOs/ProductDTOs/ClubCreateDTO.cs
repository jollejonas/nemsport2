using System.ComponentModel.DataAnnotations;

namespace nemsport.DTOs.ProductDTOs
{
    public class ClubCreateDTO
    {
        public string ClubName { get; set; }
        public string ClubInitials { get; set; }
        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; }
        public string ClubCode { get; set; }
        public int? ResponsibleUserId { get; set; }
        
    }
}
