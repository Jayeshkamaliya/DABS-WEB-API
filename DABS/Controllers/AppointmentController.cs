using DABS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Linq;

namespace DABS.Controllers
{
    public class AppointmentController : ApiController
    {
        SqlConnection conn = new SqlConnection("data source=DESKTOP-U29P9GG\\TESTINSTANCE;initial catalog=DABS;persist security info=True;user id=sa;password=123456;MultipleActiveResultSets=True;App=EntityFramework");

        [HttpPost]
        [ActionName("SaveAppointment")]
        public IEnumerable<object> SaveAppointment(appointment appointmentObj)
        {
            string retResult = string.Empty;

            try
            {
                SqlCommand cmd = new SqlCommand("save_appointment_data", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@doctorId", appointmentObj.doctorId);
                cmd.Parameters.AddWithValue("@patientId", appointmentObj.patientId);
                cmd.Parameters.AddWithValue("@doctorScheduleId", appointmentObj.drScheduleId);
                cmd.Parameters.AddWithValue("@appointmentNumber", appointmentObj.appointmentNumber);
                cmd.Parameters.AddWithValue("@appointmentReason", appointmentObj.appointmentReason);
                cmd.Parameters.AddWithValue("@appointmentTime", appointmentObj.appointmentTime);
                cmd.Parameters.AddWithValue("@status", appointmentObj.status);

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

        [HttpGet]
        [ActionName("GetLastAppointmentNo")]
        public IEnumerable<object> GetLastAppointmentNo()
        {
            string appointmentNumber = string.Empty;

            try
            {
                
                SqlCommand cmd = new SqlCommand("get_last_appointmentno", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    appointmentNumber =row["appointmentNumber"].ToString();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new string[] { "appointmentNumber", appointmentNumber };
        }

        [HttpGet]
        [ActionName("GetAppointmentList")]
        public List<appointment> GetAppointmentList(Int64 patientId,Int64 doctorId)
        {
            List<appointment> lstAppointment = new List<appointment>();
            try
            {
                SqlCommand cmd = new SqlCommand("get_appointment_byId", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@doctorId", doctorId);
                cmd.Parameters.AddWithValue("@patientId", patientId);
                conn.Open();

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                if (dt.Rows.Count > 0)
                {
                    lstAppointment = (from DataRow row in dt.Rows
                                      select new appointment
                                      {
                                          appointmentId = (Int64)row["appointmentId"],
                                          appointmentNumber = row["appointmentNumber"].ToString(),
                                          doctorName = row["DoctorName"].ToString(),
                                          patientName = row["PatientName"].ToString(),
                                          appointmentDate = Convert.ToDateTime(row["scheduleDate"]).ToShortDateString(),
                                          appointmentTime = row["appointmentTime"].ToString(),
                                          appointmentDay = row["scheduleDay"].ToString(),
                                          appointmentReason = row["appointmentReason"].ToString(),
                                          status = row["status"].ToString()                                     
                                  }).ToList();
                }
                conn.Close();
                return lstAppointment;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
