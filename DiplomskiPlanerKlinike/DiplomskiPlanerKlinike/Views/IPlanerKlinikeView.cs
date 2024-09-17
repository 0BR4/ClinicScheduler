using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomskiPlanerKlinike.Models;
using DiplomskiPlanerKlinike.EventArgsSubclass;

namespace DiplomskiPlanerKlinike.Views
{
    public interface IPlanerKlinikeView
    {
        List<ClinicEvent> ClinicEventsList { get; set; }

        event EventHandler GetAllEvents;
        event EventHandler<ClinicEventSub> DeleteEventById;
        event EventHandler<ClinicEventSub> DeleteEventDDP; //delete event by Desc Time Doctor Patient
    }
}
