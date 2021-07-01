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

        [Required]
        public string Tag{get;set;}

        [Required(ErrorMessage ="Must be at least two person")]
        [MinValue(2)]
        [MaxValue(100)]
        public int totalnumber{get;set;}
        public int? NumberOfMale{get;set;}
        
        public int? NumbeOfFemale{get;set;}

        [Required(ErrorMessage ="Must have an location")]
        public string Location{get;set;}    

        
        public int? MinAge{get;set;}

        
        public int? MaxAge{get;set;}


        [Required (ErrorMessage ="Must be present")]
        // [FutureDateAttribute (ErrorMessage ="Must be a future date")]


        public DateTime Date{get;set;}
        public int? Duration{get;set;}

        public double? price{get;set;}
        
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

public class MaxValueAttribute : ValidationAttribute
    {
        private readonly int _maxValue;

        public MaxValueAttribute(int maxValue)
        {
            _maxValue = maxValue;
        }

        public override bool IsValid(object value)
        {
            return (int) value <= _maxValue;
        }
    }

public class MinValueAttribute : ValidationAttribute
    {
        private readonly int _MinValue;

        public MinValueAttribute(int MinValue)
        {
            _MinValue = MinValue;
        }

        public override bool IsValid(object value)
        {
            return (int) value >= _MinValue;
        }
    }
