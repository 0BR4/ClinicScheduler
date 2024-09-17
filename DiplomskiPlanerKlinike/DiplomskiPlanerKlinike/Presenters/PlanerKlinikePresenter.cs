using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomskiPlanerKlinike.Data;
using DiplomskiPlanerKlinike.Views;
using DiplomskiPlanerKlinike.EventArgsSubclass;

namespace DiplomskiPlanerKlinike.Presenters
{
    public class PlanerKlinikePresenter
    {
        private IPlanerKlinikeView _view;
        private ClinicRepository _repository;

        public PlanerKlinikePresenter(IPlanerKlinikeView view, ClinicRepository repository)
        {
            _view = view;
            _repository = repository;

            _view.GetAllEvents += OnGetAllEvents;
            _view.DeleteEventById += new EventHandler<ClinicEventSub>(OnDeleteEventById);
            _view.DeleteEventDDP += new EventHandler<ClinicEventSub>(OnDeleteEventDDP);
        }

        private void OnGetAllEvents(object sender, EventArgs e)
        {
            _view.ClinicEventsList = _repository.GetAllEvents();
        }       

        private void OnDeleteEventById(object sender, ClinicEventSub e)
        {
            _repository.RemoveEventById(e.Id);
        }

        private void OnDeleteEventDDP(object sender, ClinicEventSub e)
        {
            _repository.RemoveEventByDDP(e.Description, e.DoctorName, e.PatientId);
        }
    }
}
