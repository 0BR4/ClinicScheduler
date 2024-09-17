using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomskiPlanerKlinike.Data;
using DiplomskiPlanerKlinike.Models;
using DiplomskiPlanerKlinike.Views;
using DiplomskiPlanerKlinike.EventArgsSubclass;

namespace DiplomskiPlanerKlinike.Presenters
{
    public class LabPresenter
    {
        private ILabView _view;
        private ClinicRepository _repository;

        public LabPresenter(ILabView view, ClinicRepository repository)
        {
            _view = view;
            _repository = repository;

            _view.GetAllEvents += OnGetAllEvents;
            _view.CreateEvent += new EventHandler<ClinicEventSub>(OnCreateEvent);

            _view.GetAllPatients += OnGetAllPatients;
            _view.GetPatientByID += new EventHandler<PatientSub>(OnGetPatientByID);
            _view.UpdatePatient += new EventHandler<PatientSub>(OnUpdatePatient);

            _view.GetClientDP += new EventHandler<ClientSub>(OnGetClientDP);
            _view.CreateClient += new EventHandler<ClientSub>(OnCreateClient);
        }

        private void OnGetAllEvents(object sender, EventArgs e)
        {
            _view.ClinicEventsList = _repository.GetAllEvents();
        }

        private void OnCreateEvent(object sender, ClinicEventSub e)
        {
            ClinicEvent ce = new ClinicEvent
            {
                Description = e.Description,
                Service = e.Service,
                Reacurring = e.Reacurring,
                Patient = e.PatientName,
                Doctor = e.DoctorName,
                Time = e.Time,
                Date = e.Date,
                Week = e.Week,
                CultureInfo = e.CultureInfo,
                DoctorId = e.DoctorId,
                PatientId = e.PatientId
            };

            _repository.CreateEvent(ce);
        }

        private void OnGetPatientByID(object sender, PatientSub e)
        {
            _view.SelectedPatient = _repository.GetPatientByID(e.JMBG);
        }

        private void OnUpdatePatient(object sender, PatientSub e)
        {
            Patient p = new Patient
            {
                Jmbg = e.JMBG,
                Name = e.Name,
                Phone = e.Phone,
                Code = e.Code,
                Checked = e.Checked,
                Labed = e.Labed,
                Operated = e.Operated,
                Therapied = e.Therapied,
                Controled = e.Controled
            };

            _repository.UpdatePatient(p);
        }

        private void OnGetAllPatients(object sender, EventArgs e)
        {
            _view.PatientsList = _repository.GetAllPatients();
        }

        private void OnGetClientDP(object sender, ClientSub e)
        {
            _view.SelectedClient = _repository.GetClientDP(e.DoctorId, e.PatientId);
        }

        private void OnCreateClient(object sender, ClientSub e)
        {
            Client c = new Client
            {
                DoctorId = e.DoctorId,
                PatientId = e.PatientId
            };

            _repository.CreateClient(c);
        }
    }
}
