using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Green.ViewModels
{
    public class LabViewModel
    {
        [Key]

        public int LabID { get; set; }
        public string LabName { get; set; }
        public string DoctorName { get; set; }
        //public string LabName { get; set; }
        public byte[] SamplePicture { get; set; }
        public int AppointmentID { get; set; }        
        public string UserID { get; set; }
        public DateTime Date { get; set; }
        //public string LabPractitionerName { get; set; }
        public string Comment { get; set; }



    }
}