using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivityCenter.Models
{
    public class User
    {
    [Key]
    public int UserId {get;set;}
    [Required (ErrorMessage="is required")]
    [MinLength(2, ErrorMessage="Name must be 2 characters or longer!")]
    public string Name {get;set;}
    
   
    [DataType(DataType.Password)]
    [Required (ErrorMessage="is required")]
    // [MinLength(8, ErrorMessage="Password must be 8 chracters or longer!")]
    // [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{1,}$", ErrorMessage = "Must be at least 1 number, 1 letter, and a special character")]

    public string Password {get;set;}
    [Required (ErrorMessage="is required")]

    public string Gender{get;set;}
    [EmailAddress]
    public string Email{get;set;}

    [Required(ErrorMessage = "Zip is Required")]
    [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip")]
    public string Zip { get; set; }
    public int PhoneId{get;set;}
    public int LocationId{get;set;}
public DateTime Birthday{get;set;}
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
    [NotMapped]
    [Compare("Password")]
    [DataType(DataType.Password)]
    public string Confirm {get;set;}
    
    public List<Join> Attend{get;set;}
    public List<Userinterest> interest{get;set;}
    public List<Activit> CreatedActivities {get;set;}
    public Phone phone{get;set;}
    public Location UserLocation{get;set;}
}    
public class UpdateUser
    {
    [NotMapped]
    [Required (ErrorMessage="is required")]
    [MinLength(2, ErrorMessage="Name must be 2 characters or longer!")]
    public string Name {get;set;}
    
   

    [Required (ErrorMessage="is required")]

    public string Gender{get;set;}
    [EmailAddress]
    public string Email{get;set;}

    [Required(ErrorMessage = "Zip is Required")]
    [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip")]
    public string Zip { get; set; }

    [DataType(DataType.Date)]
    [PastdateAttribute (ErrorMessage ="Must be a past date")]

    public DateTime Birthday{get;set;}
   
}    

}

public class PastdateAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        return value != null && (DateTime)value < DateTime.Now;
    }
}