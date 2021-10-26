using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Green.ViewModels
{
    public class PatientRecordViewModel
    {
        [Key]
        public string UserID { get; set; }
        public string UserName { get; set; }

        public string DoctorName { get; set; }

        public string procedureType { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public double TotalPrice { get; set; }
               


    }
}