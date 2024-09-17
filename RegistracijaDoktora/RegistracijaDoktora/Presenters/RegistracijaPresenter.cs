using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegistracijaDoktora.Data;
using RegistracijaDoktora.Views;
using RegistracijaDoktora.EventArgsSub;
using RegistracijaDoktora.Models;

namespace RegistracijaDoktora.Presenters
{
    public class RegistracijaPresenter
    {
        private IRegistracijaView _view;
        private ClinicRepository _repository;

        public RegistracijaPresenter(IRegistracijaView view, ClinicRepository repository)
        {
            _view = view;
            _repository = repository;

            _view.GetActiveDoctor += new EventHandler<DoctorLogin>(OnGetActiveDoctor);
            _view.CreateDoctor += new EventHandler<DoctorLogin>(OnCreateDoctor);

        }

        private void OnGetActiveDoctor(object sender, DoctorLogin e)
        {
            _view.MyActiveDoctor = _repository.GetActiveDoctor(e.Username, e.Password);
        }
        
        private void OnCreateDoctor(object sender, DoctorLogin e)
        {
            Doctor doctor = new Doctor
            {
                Id = e.Id,
                Name = e.Name,
                Username = e.Username,
                Password = e.Password,
                Specialization = e.Specialization,
                Surgery = e.Surgery
            };

            _repository.CreateDoctor(doctor);
        }

    }
}
