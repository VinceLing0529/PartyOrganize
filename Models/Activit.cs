using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace ActivityCenter.Models
{
    public class Activit
    {
        [Key]
        public int ActivitId{get;set;}
        [Required(ErrorMessage ="Must be present")]
        public string Title{get; set;}
        [Required (ErrorMessage ="Must be present")]
        [DataType(DataType.Time)]

        public DateTime Time{get;set;}
        [Required (ErrorMessage ="Must be present")]
        [FutureDateAttribute (ErrorMessage ="Must be a future date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:mm/dd}")]

        public DateTime Date{get;set;}
        public int Duration{get;set;}
        
        [Required (ErrorMessage ="Must be present")]
        public string Description{get;set;}

        public string DurationUnit{get;set;}
        public int UserId {get;set;}

        public List<Join> Guest{get;set;}
        public User Creator{get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}

public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        return value != null && (DateTime)value > DateTime.Now;
    }
}
