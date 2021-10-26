using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Green.Models
{
    public class forms
    {
      [Key]
      public int ID { get; set; }
      public byte[] file { get; set; }
        [Display(Name = "Name")]
        public string FileName { get; set; }
        [Display(Name ="Details")]
      public string location { get; set; }

        [Required]
        [Display(Name = "Application Forms")]
        public Enums.ApplicationForms ApplicationForms { get; set; }

    }
}