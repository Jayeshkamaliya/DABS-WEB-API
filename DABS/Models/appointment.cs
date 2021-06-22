using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DABS.Models
{
    public class appointment
    {
        public Int64 appointmentId { get; set; }
        public Int64 doctorId { get; set; }
        public Int64 patientId { get; set; }
        public Int64 drScheduleId { get; set; }       
        public string appointmentNumber { get; set; }
        public string appointmentReason { get; set; }
        public string appointmentTime { get; set; }
        public string status { get; set; }

        public string appointmentDay { get; set; }
        public string appointmentDate { get; set; }
        public string doctorName { get; set; }
        public string patientName { get; set; }
    }
}