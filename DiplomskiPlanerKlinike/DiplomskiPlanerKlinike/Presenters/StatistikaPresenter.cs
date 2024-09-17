using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomskiPlanerKlinike.Data;
using DiplomskiPlanerKlinike.Views;

namespace DiplomskiPlanerKlinike.Presenters
{
    public class StatistikaPresenter
    {
        private IStatistikaView _view;
        private ClinicRepository _repository;

        public StatistikaPresenter(IStatistikaView view, ClinicRepository repository)
        {
            _view = view;
            _repository = repository;

            _view.GetAllEvents += OnGetAllEvents;
        }

        private void OnGetAllEvents(object sender, EventArgs e)
        {
            _view.ClinicEventsList = _repository.GetAllEvents();
        }
    }
}
