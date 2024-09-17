using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomskiPlanerKlinike.Models;

namespace DiplomskiPlanerKlinike.EventArgsSubclass
{
    public class PatientSub : EventArgs
    {
        public int JMBG { get; set; }
        public String Name { get; set; }
        public String Phone { get; set; }
        public StringBuilder Code { get; set; }
        public int Checked { get; set; }
        public int Labed { get; set; }
        public int Operated { get; set; }
        public int Therapied { get; set; }
        public int Controled { get; set; }

        public PatientSub(int id)
        {
            JMBG = id;
        }
        //jos konstruktora za razlicite slucajeve

        public PatientSub(Patient patient)
        {
            JMBG = patient.Jmbg;
            Name = patient.Name;
            Phone = patient.Phone;
            Code = patient.Code;
            Checked = patient.Checked;
            Labed = patient.Labed;
            Operated = patient.Operated;
            Therapied = patient.Therapied;
            Controled = patient.Controled;
        }
    }
}
