using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Green.Models
{
    public class AnnualR
    {

        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        public string Year { get; set; }
        public byte[] file { get; set; }
        [Display(Name = "Details")]
        public string location { get; set; }
    }
}