using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomskiPlanerKlinike.Data;
using DiplomskiPlanerKlinike.Views;

namespace DiplomskiPlanerKlinike.Presenters
{
    public class ListaDoktoraPresenter
    {
        private IListaDoktoraView _view;
        private ClinicRepository _repository;

        public ListaDoktoraPresenter(IListaDoktoraView view, ClinicRepository repository)
        {
            _view = view;
            _repository = repository;

            _view.GetAllDoctors += OnGetAllDoctors;
        }

        private void OnGetAllDoctors(object sender, EventArgs e)
        {
            _view.DoctorsList = _repository.GetAllDoctors();
        }
    }
}
