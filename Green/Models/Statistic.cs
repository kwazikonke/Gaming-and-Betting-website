using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Green.Models
{
    public class Statistic
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Statistics")]
        public Enums.Stats Stats { get; set; }

       
        public string Year { get; set; }
        public byte[] file { get; set; }
        [Display(Name = "Details")]
        public string location { get; set; }
    }
}