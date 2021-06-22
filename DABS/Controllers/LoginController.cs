using DABS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DABS.Controllers
{
    public class LoginController : ApiController
    {
        SqlConnection conn = new SqlConnection("data source=DESKTOP-U29P9GG\\TESTINSTANCE;initial catalog=DABS;persist security info=True;user id=sa;password=123456;MultipleActiveResultSets=True;App=EntityFramework");

        [HttpGet]
        [ActionName("login")]
        public IEnumerable<User> login(string emailId, string password)
        {
            string retResult = string.Empty;

            try
            {                
                SqlCommand cmd = new SqlCommand("login_user_details", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@emailId", emailId);
                cmd.Parameters.AddWithValue("@password", password);
              
                conn.Open();

                DataTable dt = new DataTable();
                List<User> lstUserData = new List<User>();
                User UserData = new User();
                dt.Load(cmd.ExecuteReader());

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ColumnName == "id")
                        {
                            UserData.id = (Int64)row[column];
                        }                       
                        else if (column.ColumnName == "email_address")
                        {
                            UserData.emailAddress = row[column].ToString();
                        }
                        else if (column.ColumnName == "password")
                        {
                            UserData.password = row[column].ToString();
                        }
                        else if (column.ColumnName == "phone_no")
                        {
                            UserData.phoneNo = row[column].ToString();
                        }
                        else if (column.ColumnName == "address")
                        {
                            UserData.address = row[column].ToString();
                        }
                        else if (column.ColumnName == "doctor_degree")
                        {
                            UserData.degree = row[column].ToString();
                        }
                        else if (column.ColumnName == "doctor_specialize")
                        {
                            UserData.specialization = row[column].ToString();
                        }
                        else if (column.ColumnName == "date_of_birth")
                        {
                            UserData.dateOfBirth = row[column].ToString();
                        }
                        else if (column.ColumnName == "gender")
                        {
                            UserData.gender = row[column].ToString();
                        }
                        else if (column.ColumnName == "blood_grp")
                        {
                            UserData.bloodGroup = row[column].ToString();
                        }
                        else if (column.ColumnName == "isDoctor")
                        {
                            UserData.isDoctor = (bool)row[column];
                        }
                    }
                    lstUserData.Add(UserData);
                    conn.Close();
                }

                return lstUserData;
            }
            catch (Exception e)
            {
                throw;
            }
           
        }
    }
}
