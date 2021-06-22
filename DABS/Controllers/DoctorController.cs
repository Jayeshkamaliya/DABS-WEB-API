using DABS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Linq;

namespace DABS.Controllers
{
    public class DoctorController : ApiController
    {
        SqlConnection conn = new SqlConnection("data source=DESKTOP-U29P9GG\\TESTINSTANCE;initial catalog=DABS;persist security info=True;user id=sa;password=123456;MultipleActiveResultSets=True;App=EntityFramework");

        [HttpGet]
        [ActionName("GetScheduleData")]
        public List<DoctorSchedule> GetScheduleData(int doctorId )
        {
            List<DoctorSchedule> lstDoctorSchedules = new List<DoctorSchedule>();
            try
            {
                SqlCommand cmd = new SqlCommand("get_doctor_schedule", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@doctorId", doctorId);
                conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
               
                if (dt.Rows.Count>0)
                {
                    lstDoctorSchedules = (from DataRow row in dt.Rows
                                          select new DoctorSchedule
                                          {
                                              drScheduleId = (Int64)row["drScheduleId"],
                                              doctorId = (Int64)row["doctorId"],
                                              scheduleDate =Convert.ToDateTime(row["scheduleDate"]).ToShortDateString(),
                                              scheduleStartTime = (int)row["scheduleStartTime"],
                                              scheduleEndTime = (int)row["scheduleEndTime"],
                                              doctorScheduleStatus = (string)row["doctorScheduleStatus"],
                                              scheduleDay = (string)row["scheduleDay"]
                                          }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstDoctorSchedules;
        }

        [HttpGet]
        [ActionName("GetDoctorList")]
        public List<User> getDoctorList()
        {
            List<User> lstDoctors = new List<User>();
            try
            {
                SqlCommand cmd = new SqlCommand("get_doctor_list", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                if (dt.Rows.Count > 0)
                {
                    lstDoctors = (from DataRow row in dt.Rows
                                  select new User
                                  {
                                      id = (Int64)row["doctorId"],
                                      drScheduleId = (Int64)row["drScheduleId"],
                                      name = row["name"].ToString(),
                                      degree = row["doctor_degree"].ToString(),
                                      specialization = row["doctor_specialize"].ToString(),
                                      scheduleDay = row["scheduleDay"].ToString(),
                                      scheduleDate = Convert.ToDateTime(row["scheduleDate"]).ToShortDateString(),
                                      appointmentTime = row["scheduleStartTime"] + ":00 To " + row["scheduleEndTime"] + ": 00".ToString()
                                  }).ToList();
                }
                conn.Close();
                return lstDoctors;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        [ActionName("SaveSchedule")]
        public IEnumerable<object> SaveSchedule(DoctorSchedule doctorSchedule)
        {
            string retResult = string.Empty;
            
            try
            {
                SqlCommand cmd = new SqlCommand("save_schedule_data", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@doctorId", doctorSchedule.doctorId);
                cmd.Parameters.AddWithValue("@scheduleDate", doctorSchedule.scheduleDate);
                cmd.Parameters.AddWithValue("@scheduleStartTime", doctorSchedule.scheduleStartTime);
                cmd.Parameters.AddWithValue("@scheduleEndTime", doctorSchedule.scheduleEndTime);
                cmd.Parameters.AddWithValue("@doctorScheduleStatus", doctorSchedule.doctorScheduleStatus);
                cmd.Parameters.AddWithValue("@scheduleDay", doctorSchedule.scheduleDay);

                cmd.Parameters.Add("@v_saved", SqlDbType.VarChar, 10);
                cmd.Parameters["@v_saved"].Direction = ParameterDirection.Output;
                conn.Open();
                int k = cmd.ExecuteNonQuery();
                if (k != 0)
                {
                    retResult = Convert.ToString(cmd.Parameters["@v_saved"].Value);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new string[] { "saveSchedule", retResult };
        }

        [HttpPost]
        [ActionName("UpdateAppointmentStatus")]
        public object updateAppointmentStatus(appointment appointmentObj)
        {
            string retResult = string.Empty;

            try
            {
                SqlCommand cmd = new SqlCommand("update_appointment_status", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@appointmentId", appointmentObj.appointmentId);
                cmd.Parameters.AddWithValue("@status", appointmentObj.status);
               

                cmd.Parameters.Add("@v_updated", SqlDbType.VarChar, 10);
                cmd.Parameters["@v_updated"].Direction = ParameterDirection.Output;
                conn.Open();
                int k = cmd.ExecuteNonQuery();
                if (k != 0)
                {
                    retResult = Convert.ToString(cmd.Parameters["@v_updated"].Value);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new string[] { "updateAppointmenStatus", retResult };
        }
   
    }
}
