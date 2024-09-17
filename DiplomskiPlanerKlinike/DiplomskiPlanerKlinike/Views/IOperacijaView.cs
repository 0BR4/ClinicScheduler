﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomskiPlanerKlinike.Models;
using DiplomskiPlanerKlinike.EventArgsSubclass;

namespace DiplomskiPlanerKlinike.Views
{
    public interface IOperacijaView
    {
        List<ClinicEvent> ClinicEventsList { get; set; }
        List<Codebook> CodebookList { get; set; }
        List<Patient> PatientsList { get; set; }
        Patient SelectedPatient { get; set; }
        Client SelectedClient { get; set; }

        event EventHandler GetAllEvents;
        event EventHandler<ClinicEventSub> CreateEvent;

        event EventHandler GetAllCodebook;

        event EventHandler GetAllPatients;
        event EventHandler<PatientSub> GetPatientByID;
        event EventHandler<PatientSub> UpdatePatient;

        event EventHandler<ClientSub> GetClientDP;      //get client by DoctorID PatientID
        event EventHandler<ClientSub> CreateClient;

        event EventHandler<HistorySub> CreateHistory;
    }
}
