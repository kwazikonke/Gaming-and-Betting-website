using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Green.Models
{
    public class Vaccancie
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name ="Department")]
        public Enums.Units Units { get; set; }

        [Required]
        [Display(Name ="Position")]
        public string Position { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ClosingDate { get; set; }
        public byte[] file { get; set; }
        [Display(Name = "Details")]
        public string location { get; set; }



    }
  
   
}