using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Green.Models
{
    public class Gallery
    {
          [Key]
         public int ID { get; set; }
        [Display(Name = "Picture")]
        public byte[] Picture { get; set; }
        [Display(Name = "Event Name")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}