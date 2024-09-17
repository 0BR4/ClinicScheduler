using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomskiPlanerKlinike.Data;
using DiplomskiPlanerKlinike.Views;

namespace DiplomskiPlanerKlinike.Presenters
{
    public class ListaPacijenataPresenter
    {
        private IListaPacijenataView _view;
        private ClinicRepository _repository;

        public ListaPacijenataPresenter(IListaPacijenataView view, ClinicRepository repository)
        {
            _view = view;
            _repository = repository;

            _view.GetAllPatients += OnGetAllPatients;
        }

        private void OnGetAllPatients(object sender, EventArgs e)
        {
            _view.PatientsList = _repository.GetAllPatients();
        }
    }
}
