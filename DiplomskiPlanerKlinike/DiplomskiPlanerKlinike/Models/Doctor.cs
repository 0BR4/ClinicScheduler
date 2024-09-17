using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomskiPlanerKlinike.Models
{
    public class Doctor
    {
        public Doctor()
        {

        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Specialization { get; set; }

        public int Surgery { get; set; }
    }
}
