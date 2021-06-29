using System.ComponentModel.DataAnnotations;

namespace ActivityCenter.Models
{
    public class Userinterest
    {
        [Key]
        public int User_InterestId{get;set;}
        [Required]
        public int InterestId{get;set;}
        [Required]
        public int UserId{get;set;}
        
        public Interest Interest{get;set;}
        public User User{get;set;}
    }
}