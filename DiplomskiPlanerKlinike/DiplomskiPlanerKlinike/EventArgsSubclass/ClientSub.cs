using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomskiPlanerKlinike.Models;

namespace DiplomskiPlanerKlinike.EventArgsSubclass
{
    public class ClientSub : EventArgs
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public ClientSub(int doctorId, int patientId)
        {
            PatientId = patientId;
            DoctorId = doctorId;
        }

        public ClientSub(Client client)
        {
            PatientId = client.PatientId;
            DoctorId = client.DoctorId;
        }
    }
}
