using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomskiPlanerKlinike.Models;
using DiplomskiPlanerKlinike.EventArgsSubclass;

namespace DiplomskiPlanerKlinike.Views
{
    public interface ILoginView
    {
        Doctor MyActiveDoctor { get; set; }

        event EventHandler<DoctorLogin> GetActiveDoctor;
    }
}
