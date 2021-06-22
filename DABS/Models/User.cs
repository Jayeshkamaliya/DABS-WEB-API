using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DABS.Models
{
    public class User
    {
        public Int64 id { get; set; }
        public string  name { get; set; }        
        public string emailAddress { get; set; }
        public string password { get; set; }
        public string phoneNo { get; set; }
        public string address { get; set; }
        public string degree { get; set; }
        public string specialization { get; set; }
        public string dateOfBirth { get; set; }
        public string gender { get; set; }
        public string bloodGroup { get; set; }
        public bool isDoctor { get; set; }
        public int age { get; set; }
        public int experience { get; set; }

        public Int64 drScheduleId { get; set; }
        public string scheduleDate { get; set; }
        public string scheduleDay { get; set; }
        public string appointmentTime { get; set; }
     
    }

}