using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomskiPlanerKlinike.Models;

namespace DiplomskiPlanerKlinike.Views
{
    public interface IListaPacijenataView
    {
        List<Patient> PatientsList { get; set; }

        event EventHandler GetAllPatients;
    }
}
