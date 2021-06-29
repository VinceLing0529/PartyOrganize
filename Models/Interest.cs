using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivityCenter.Models
{
    public class Interest
    {
        [Key]
        public int InterestId{get;set;}

        [Required]
        public string Name{get;set;}

        public List<Userinterest> Who{get;set;}
    }
}