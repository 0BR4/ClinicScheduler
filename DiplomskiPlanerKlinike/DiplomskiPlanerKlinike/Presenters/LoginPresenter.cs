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
    public class LoginPresenter
    {
        private ILoginView _view;
        private ClinicRepository _repository;

        public LoginPresenter(ILoginView view, ClinicRepository repository)
        {
            _view = view;
            _repository = repository;

            _view.GetActiveDoctor += new EventHandler<DoctorLogin>(OnGetActiveDoctor);
        }


        private void OnGetActiveDoctor(object sender, DoctorLogin e)
        {
            _view.MyActiveDoctor = _repository.GetActiveDoctor(e.Username, e.Password);
        }

    }
}
