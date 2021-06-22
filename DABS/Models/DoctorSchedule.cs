using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DABS.Models
{
    public class DoctorSchedule
    {
        public Int64 drScheduleId { get; set; }
        public Int64 doctorId { get; set; }
        public string scheduleDate { get; set; }
        public string scheduleDay { get; set; }
        public int scheduleStartTime { get; set; }
        public int scheduleEndTime { get; set; }
        public string doctorScheduleStatus { get; set; }
        public int avgConsultingTime { get; set; }
    }


}