using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegistracijaDoktora.Models;
using RegistracijaDoktora.EventArgsSub;

namespace RegistracijaDoktora.Views
{
    public interface IRegistracijaView
    {
        Doctor MyActiveDoctor { get; set; }
        Doctor DoctorToAdd { get; set; }

        event EventHandler<DoctorLogin> GetActiveDoctor;
        event EventHandler<DoctorLogin> CreateDoctor;
    }
}
