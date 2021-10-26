using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Green.Models
{
    public class Telbook
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Office")]
        public Enums.Site Site { get; set; }

        [Required]
        [Display(Name = "Designation")]
        public Enums.Units Units { get; set; }

        [Display(Name ="CellPhone")]
        public int cellphoneNum { get; set; }
        [Display(Name = "First Name")]
        public string Name { get; set; }
        [Display(Name = "Extention")]
        public int Code { get; set; }
        
    
       
    }
}