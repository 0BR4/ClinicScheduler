using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomskiPlanerKlinike.Models
{
    //[Table("patients")]
    public class Patient
    {
        public Patient()
        {

        }

        public int Jmbg { get; set; }

       // [Required]
       // [StringLength(30)]
        public String Name { get; set; }

      //  [StringLength(30)]
        public String Phone { get; set; }

      //  [StringLength(30)]
        public StringBuilder Code { get; set; }

        public int Checked { get; set; }

        public int Labed { get; set; }

        public int Operated { get; set; }

        public int Therapied { get; set; }

        public int Controled { get; set; }
    }
}
