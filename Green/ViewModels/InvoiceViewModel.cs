using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Green.ViewModels
{
    public class InvoiceViewModel
    {
        [Key]
        public int InvoiceNumber { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "Full Name")]
        public string Name { get; set; }
        public string Value { get; set; }
        [Required]
        public string InvoiceDescription { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required]
        public string ProcedureName { get; set; }
        [DataType(DataType.Currency)]
        public double ProcedurePrice { get; set; }
        [DataType(DataType.Currency)]
        public double BookingPrice { get; set; }
    }
}