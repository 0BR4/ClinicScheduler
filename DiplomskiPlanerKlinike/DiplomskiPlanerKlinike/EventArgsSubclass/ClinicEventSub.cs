using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomskiPlanerKlinike.Models;

namespace DiplomskiPlanerKlinike.EventArgsSubclass
{
    public class ClinicEventSub : EventArgs
    {
        public int Id { get; set; }
        public String Description { get; set; }
        public String Time { get; set; }
        public String Date { get; set; }
        public String Week { get; set; }
        public String DoctorName { get; set; }
        public String PatientName { get; set; }
        public String Service { get; set; }
        public int Reacurring { get; set; }
        public String CultureInfo { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public ClinicEventSub(String time, String doctor, String service)
        {
            Time = time;
            DoctorName = doctor;
            Service = service;
        }
        //jos konstruktora za razlicite slucajeve

        public ClinicEventSub(ClinicEvent clinicEvent)
        {
            Id = clinicEvent.Id;
            Description = clinicEvent.Description;
            Service = clinicEvent.Service;
            Reacurring = clinicEvent.Reacurring;
            PatientName = clinicEvent.Patient;
            DoctorName = clinicEvent.Doctor;
            Time = clinicEvent.Time;
            Date = clinicEvent.Date;
            Week = clinicEvent.Week;
            CultureInfo = clinicEvent.CultureInfo;
            PatientId = clinicEvent.PatientId;
            DoctorId = clinicEvent.DoctorId;
        }

        public ClinicEventSub(String date)
        {
            Date = date;
        }

        public ClinicEventSub(int id)
        {
            Id = id;
        }

        public ClinicEventSub(String desc, String doctor, int patientId)
        {
            Description = desc;
            DoctorName = doctor;
            PatientId = patientId;
        }

        public ClinicEventSub(String week, String doctor)
        {
            Week = week;
            DoctorName = doctor;
        }

        public ClinicEventSub(int reacurring, String doctor)
        {
            Reacurring = reacurring;
            DoctorName = doctor;
        }

        public ClinicEventSub(String time, int patientId)
        {
            Time = time;
            PatientId = patientId;
        }
    }
}
