using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomskiPlanerKlinike.EventArgsSubclass
{
    public class DoctorLogin : EventArgs
    {
        public String Username { get; set; }
        public String Password { get; set; }

        public DoctorLogin(String username, String password)
        {
            Username = username;
            Password = password;
        }
    }
}
