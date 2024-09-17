using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using DiplomskiPlanerKlinike.Models;

namespace DiplomskiPlanerKlinike.Data
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

        #region Clients CRUD

        public Client GetClientDP(int doctorId, int patientId)
        {
            Client client = new Client();

            using (MySqlConnection mySqlConnection = new MySqlConnection(mscsb.ConnectionString))
            {
                try
                {                
                    string sql = "SELECT * FROM clients WHERE doctorId = '" + doctorId + "' AND patientId = '" + patientId + "'";
                    mySqlConnection.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                client.Id = Int32.Parse(reader["id"].ToString());
                                client.DoctorId = Int32.Parse(reader["doctorId"].ToString());
                                client.PatientId = Int32.Parse(reader["patientId"].ToString());
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

            return client;
        }

        public void CreateClient(Client client)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(mscsb.ConnectionString))
            {
                try
                {
                    string sql = "INSERT INTO clients(patientId, doctorId) VALUES ('" + client.PatientId + "', '" + client.DoctorId + "');";

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

        #region Codebook CRUD

        public List<Codebook> GetAllCodebook()
        {
            List<Codebook> codebookList = new List<Codebook>();

            using (MySqlConnection mySqlConnection = new MySqlConnection(mscsb.ConnectionString))
            {
                try
                {
                    string sql = "SELECT * FROM codebook";
                    mySqlConnection.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Codebook codebook = new Codebook();

                                codebook.Id = Int32.Parse(reader["id"].ToString());
                                codebook.Code = reader["code"].ToString();
                                codebook.Disease = reader["disease"].ToString();

                                codebookList.Add(codebook);
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

            return codebookList;
        }

        #endregion

        #region Doctors CRUD

        public Doctor GetActiveDoctor(String username, String password)
        {
            Doctor doctor = new Doctor();

            using (MySqlConnection mySqlConnection = new MySqlConnection(mscsb.ConnectionString))
            {
                try
                {
                    Console.WriteLine(mscsb.ConnectionString);
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

        #endregion

        #region Events CRUD

        public List<ClinicEvent> GetAllEvents()
        {
            List<ClinicEvent> clinicEventsList = new List<ClinicEvent>();

            using (MySqlConnection mySqlConnection = new MySqlConnection(mscsb.ConnectionString))
            {
                try
                {
                    //Console.WriteLine(mscsb.ConnectionString);
                    string sql = "SELECT * FROM events";
                    mySqlConnection.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClinicEvent clinicEvent = new ClinicEvent
                                {
                                    Id = Int32.Parse(reader["id"].ToString()),
                                    Description = reader["description"].ToString(),
                                    Service = reader["service"].ToString(),
                                    Reacurring = Int32.Parse(reader["reacurring"].ToString()),
                                    Patient = reader["patient"].ToString(),
                                    Doctor = reader["doctor"].ToString(),
                                    Time = reader["time"].ToString(),
                                    Date = reader["date"].ToString(),
                                    Week = reader["week"].ToString(),
                                    CultureInfo = reader["cultureInfo"].ToString(),
                                    PatientId = Int32.Parse(reader["patientId"].ToString()),
                                    DoctorId = Int32.Parse(reader["doctorId"].ToString())
                                };

                                clinicEventsList.Add(clinicEvent);
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

            return clinicEventsList;
        }

        public ClinicEvent GetEvent(int eventId)
        {
            ClinicEvent clinicEvent = new ClinicEvent();

            using (MySqlConnection mySqlConnection = new MySqlConnection(mscsb.ConnectionString))
            {
                try
                {
                    //Console.WriteLine(mscsb.ConnectionString);
                    string sql = "SELECT * FROM events WHERE id = " + eventId;
                    mySqlConnection.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clinicEvent.Id = Int32.Parse(reader["id"].ToString());
                                clinicEvent.Description = reader["description"].ToString();
                                clinicEvent.Service = reader["service"].ToString();
                                clinicEvent.Reacurring = Int32.Parse(reader["reacurring"].ToString());
                                clinicEvent.Patient = reader["patient"].ToString();
                                clinicEvent.Doctor = reader["doctor"].ToString();
                                clinicEvent.Time = reader["time"].ToString();
                                clinicEvent.Date = reader["date"].ToString();
                                clinicEvent.Week = reader["week"].ToString();
                                clinicEvent.CultureInfo = reader["cultureInfo"].ToString();
                                clinicEvent.PatientId = Int32.Parse(reader["patientId"].ToString());
                                clinicEvent.DoctorId = Int32.Parse(reader["doctorId"].ToString());
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

            return clinicEvent;
        }

        public void CreateEvent(ClinicEvent clinicEvent)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(mscsb.ConnectionString))
            {
                try
                {
                    //Console.WriteLine(mscsb.ConnectionString);
                    string sql = "INSERT INTO events(description, service, reacurring, patient, doctor, time, date, week, cultureInfo, patientId, doctorID) VALUES ('" +
                        clinicEvent.Description + "', '" + clinicEvent.Service + "', '" + clinicEvent.Reacurring + "', '" + clinicEvent.Patient +
                        "', '" + clinicEvent.Doctor + "', '" + clinicEvent.Time + "', '" + clinicEvent.Date + "', '" + clinicEvent.Week + "', '" +
                        clinicEvent.CultureInfo + "', '" + clinicEvent.PatientId + "', '" + clinicEvent.DoctorId + "');";

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

        public void RemoveEventById(int eventId)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(mscsb.ConnectionString))
            {
                try
                {
                    //Console.WriteLine(mscsb.ConnectionString);
                    string sql = "DELETE FROM events WHERE id ='" + eventId + "'";
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

        public void RemoveEventByDDP(String desc, String doctor, int patientId)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(mscsb.ConnectionString))
            {
                try
                {
                    //Console.WriteLine(mscsb.ConnectionString);
                    string sql = "DELETE FROM events WHERE description ='" + desc + "' AND doctor = '" + doctor + "' AND patientId = '" + patientId + "'";
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

        #region History CRUD

        public void CreateHistory(History history)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(mscsb.ConnectionString))
            {
                try
                {
                    string sql = "INSERT INTO history(patientID, code, service, time, date) VALUES ('" + history.PatientId + "', '" +
                        history.Code + "', 'Therapy', '" + history.Time + "', '" + history.Date + "');";
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

        #region Patients CRUD

        public Patient GetPatientByID(int id)
        {
            Patient patient = new Patient();

            using (MySqlConnection mySqlConnection = new MySqlConnection(mscsb.ConnectionString))
            {
                try
                {
                    string sql = "SELECT * FROM patients WHERE jmbg = " + id;
                    mySqlConnection.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                patient.Jmbg = Int32.Parse(reader["jmbg"].ToString());
                                patient.Name = reader["name"].ToString();
                                patient.Phone = reader["phone"].ToString();
                                patient.Code = new StringBuilder(reader["code"].ToString());
                                patient.Checked = reader.GetByte("checked");
                                patient.Labed = reader.GetByte("labed");
                                patient.Operated = reader.GetByte("operated");
                                patient.Therapied = reader.GetByte("therapied");
                                patient.Controled = reader.GetByte("controled");
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

            return patient;
        }

        public List<Patient> GetAllPatients()
        {
            List<Patient> patientsList = new List<Patient>();

            using (MySqlConnection mySqlConnection = new MySqlConnection(mscsb.ConnectionString))
            {
                try
                {
                    string sql = "SELECT * FROM patients";
                    mySqlConnection.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Patient patient = new Patient
                                {
                                    Jmbg = Int32.Parse(reader["jmbg"].ToString()),
                                    Name = reader["name"].ToString(),
                                    Phone = reader["phone"].ToString(),
                                    Code = new StringBuilder(reader["code"].ToString()),
                                    Checked = reader.GetByte("checked"),
                                    Labed = reader.GetByte("labed"),
                                    Operated = reader.GetByte("operated"),
                                    Therapied = reader.GetByte("therapied"),
                                    Controled = reader.GetByte("controled")
                                };

                                patientsList.Add(patient);
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

            return patientsList;
        }

        public void CreatePatient(Patient patient)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(mscsb.ConnectionString))
            {
                try
                {
                    //Console.WriteLine(mscsb.ConnectionString);
                    string sql = "INSERT INTO patients(jmbg, name, phone, code, checked, labed, operated, therapied, controled) VALUES ('" +
                        patient.Jmbg + "', '" + patient.Name + "', '" + patient.Phone + "', '" + patient.Code + "', '" + patient.Checked + "', '" +
                        patient.Labed + "', '" + patient.Operated + "', '" + patient.Therapied + "', '" + patient.Controled + "');";

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

        public void UpdatePatient(Patient patient)
        {

            using (MySqlConnection mySqlConnection = new MySqlConnection(mscsb.ConnectionString))
            {
                try
                {
                    Console.WriteLine(mscsb.ConnectionString);
                    string sql = "UPDATE patients SET code = '" + patient.Code + "', checked = '" + patient.Checked + "', labed = '" + patient.Labed + "', operated = '" + patient.Operated +
                        "', therapied = '" + patient.Therapied + "', controled = '" + patient.Controled + "' WHERE jmbg = '" + patient.Jmbg + "'";
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
