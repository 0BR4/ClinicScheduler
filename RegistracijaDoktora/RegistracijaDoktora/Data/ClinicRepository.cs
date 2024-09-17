using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using RegistracijaDoktora.Models;

namespace RegistracijaDoktora.Data
{
    public class ClinicRepository
    {
        private MySqlConnectionStringBuilder mscsb;

        public ClinicRepository()
        {
            mscsb = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                UserID = "root",
                Password = "Magnolia314159!",
                Database = "planer"
            };
        }

        #region Doctors CRUD

        public Doctor GetActiveDoctor(String username, String password)
        {
            Doctor doctor = new Doctor();

            using (MySqlConnection mySqlConnection = new MySqlConnection(mscsb.ConnectionString))
            {
                try
                {
                    string sql = "SELECT * FROM doctors WHERE username = '" + username +"' AND password = '" + password + "'";
                    mySqlConnection.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                doctor.Id = Int32.Parse(reader["id"].ToString());
                                doctor.Name = reader["name"].ToString();
                                doctor.Username = reader["username"].ToString();
                                doctor.Password = reader["password"].ToString();
                                doctor.Specialization = reader["specialization"].ToString();
                                doctor.Surgery = Convert.ToInt32(reader["surgery"]);
                            }
                        }
                        mySqlConnection.Close();
                    }
                }
                catch (MySqlException e)
                {
                    throw new Exception(e.Message, e.InnerException);
                }
            }

            return doctor;
        }

        public List<Doctor> GetAllDoctors()
        {
            List<Doctor> doctorsList = new List<Doctor>();

            using (MySqlConnection mySqlConnection = new MySqlConnection(mscsb.ConnectionString))
            {
                try
                {
                    string sql = "SELECT * FROM doctors";
                    mySqlConnection.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Doctor doctor = new Doctor
                                {
                                    Id = Int32.Parse(reader["id"].ToString()),
                                    Name = reader["name"].ToString(),
                                    Username = reader["username"].ToString(),
                                    Password = reader["password"].ToString(),
                                    Specialization = reader["specialization"].ToString(),
                                    Surgery = reader.GetByte("surgery")
                                };

                                doctorsList.Add(doctor);
                            }
                        }
                        mySqlConnection.Close();
                    }
                }
                catch (MySqlException e)
                {
                    throw new Exception(e.Message, e.InnerException);
                }
            }

            return doctorsList;
        }

        public void CreateDoctor(Doctor doctor)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(mscsb.ConnectionString))
            {
                try
                {
                    string sql = "INSERT INTO doctors(name, username, password, specialization, surgery) VALUES ('" + doctor.Name +
                        "', '" + doctor.Username + "', '" + doctor.Password + "', '" + doctor.Specialization + "', '" +doctor.Surgery + "');";

                    mySqlConnection.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException e)
                {
                    throw new Exception(e.Message, e.InnerException);
                }
                mySqlConnection.Close();
            }
        }

        #endregion

    }
}
