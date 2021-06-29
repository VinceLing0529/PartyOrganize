using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivityCenter.Models
{
    public class Location
    {
        [Key]
        public int LocationId{get;set;}
        [Required]
        public string Zip { get; set; }
      
        [Required]
        public string state_name{get;set;}
        [Required]
        public string city{get;set;}
        [Required]
        public string state_id{get;set;}
        [Required]
        public double Lat{get;set;}
        [Required]
        public double Lng{get;set;}
        
    }

}