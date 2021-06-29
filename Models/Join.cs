using System.ComponentModel.DataAnnotations;

namespace ActivityCenter.Models
{
    public class Join
    {
        [Key]
        public int JoinId{get;set;}
        [Required]
        public int ActivitId{get;set;}
        [Required]
        public int UserId{get;set;}
        
        public Activit Activit{get;set;}
        public User User{get;set;}
    }
}