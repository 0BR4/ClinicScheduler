using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomskiPlanerKlinike.Models;

namespace DiplomskiPlanerKlinike.Views
{
    public interface IListaDoktoraView
    {
        List<Doctor> DoctorsList { get; set; }

        event EventHandler GetAllDoctors;
    }
}
