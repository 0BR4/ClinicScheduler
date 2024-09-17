using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegistracijaDoktora.Models;

namespace RegistracijaDoktora.EventArgsSub
{
    public class DoctorLogin
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String Specialization { get; set; }
        public int Surgery { get; set; }

        public DoctorLogin(String username, String password)
        {
            Username = username;
            Password = password;
        }

        public DoctorLogin(Doctor doctor)
        {
                Id = doctor.Id;
                Name = doctor.Name;
                Username = doctor.Username;
                Password = doctor.Password;
                Specialization = doctor.Specialization;
                Surgery = doctor.Surgery;

        }
    }
}
