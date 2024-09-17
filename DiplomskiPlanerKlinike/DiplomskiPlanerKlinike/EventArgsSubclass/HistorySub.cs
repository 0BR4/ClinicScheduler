using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomskiPlanerKlinike.Models;

namespace DiplomskiPlanerKlinike.EventArgsSubclass
{
    public class HistorySub : EventArgs
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public String Code { get; set; }
        public String Service { get; set; }
        public String Time { get; set; }
        public String Date { get; set; }

        public HistorySub(History history)
        {
            Id = history.Id;
            PatientId = history.PatientId;
            Code = history.Code;
            Service = history.Service;
            Time = history.Time;
            Date = history.Date;
        }
    }
}
