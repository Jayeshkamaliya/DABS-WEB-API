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
    public class UserController : ApiController
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
                        else if (column.ColumnName == "name")
                        {
                            UserData.name = row[column].ToString();
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
                            object value = row[column];
                            if (value == DBNull.Value)
                                UserData.dateOfBirth ="";
                            else
                                UserData.dateOfBirth = Convert.ToDateTime(row[column]).ToShortDateString();
                           
                        }
                        else if (column.ColumnName == "gender")
                        {
                            UserData.gender = row[column].ToString();
                        }
                        else if (column.ColumnName == "bloodGroup")
                        {
                            UserData.bloodGroup = row[column].ToString();
                        }
                        else if (column.ColumnName == "isDoctor")
                        {
                            UserData.isDoctor = (bool)row[column];
                        }
                        else if (UserData.isDoctor == false && column.ColumnName == "age")
                        {
                            object value = row[column];
                            if (value == DBNull.Value)
                                UserData.age = 0;
                            else
                                UserData.age = (int)row[column];
                        }
                        else if (UserData.isDoctor == true && column.ColumnName == "experience")
                        {
                            object value = row[column];
                            if (value == DBNull.Value)
                                UserData.experience = 0;
                            else
                                UserData.experience = Convert.ToInt32(row[column]);
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

        [HttpPost]
        [ActionName("SaveUser")]
        public IEnumerable<object> saveUser(User user)
        {
            string retResult = string.Empty;
            // SqlConnection conn = new SqlConnection("data source=DESKTOP-U29P9GG\\TESTINSTANCE;initial catalog=DABS;persist security info=True;user id=sa;password=123456;MultipleActiveResultSets=True;App=EntityFramework");
            try
            {
                SqlCommand cmd = new SqlCommand("save_user_data", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@emailAddress", user.emailAddress);
                cmd.Parameters.AddWithValue("@password", user.password);
                cmd.Parameters.AddWithValue("@name", user.name);
                cmd.Parameters.AddWithValue("@phoneNo", user.phoneNo);
                cmd.Parameters.AddWithValue("@degree", user.degree);
                cmd.Parameters.AddWithValue("@specialization", user.specialization);
                cmd.Parameters.AddWithValue("@dateOfBirth", user.dateOfBirth);
                cmd.Parameters.AddWithValue("@address", user.address);
                cmd.Parameters.AddWithValue("@isDoctor", user.isDoctor);
                cmd.Parameters.AddWithValue("@gender", user.gender);
                cmd.Parameters.AddWithValue("@age", user.age);
                cmd.Parameters.AddWithValue("@experience", user.experience);
                cmd.Parameters.AddWithValue("@bloodGroup", user.bloodGroup);

                cmd.Parameters.Add("@v_saved", SqlDbType.VarChar, 10);
                cmd.Parameters["@v_saved"].Direction = ParameterDirection.Output;
                conn.Open();
                int k = cmd.ExecuteNonQuery();
                if (k != 0)
                {
                    retResult = Convert.ToString(cmd.Parameters["@v_saved"].Value);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new string[] { "saveUserStatus", retResult };
        }

    }
}
