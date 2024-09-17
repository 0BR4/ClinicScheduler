using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using DiplomskiPlanerKlinike.Views;
using DiplomskiPlanerKlinike.Data;
using DiplomskiPlanerKlinike.EventArgsSubclass;
using DiplomskiPlanerKlinike.Models;
using DiplomskiPlanerKlinike.Presenters;

namespace DiplomskiPlanerKlinike
{
    public partial class FormPlanerKlinike : Form, IPlanerKlinikeView
    {
        public event EventHandler GetAllEvents;
        public event EventHandler<ClinicEventSub> DeleteEventById;
        public event EventHandler<ClinicEventSub> DeleteEventDDP;

        private List<ClinicEvent> clinicEventsList = new List<ClinicEvent>();

        public List<ClinicEvent> ClinicEventsList
        {
            get
            {
                return clinicEventsList;
            }
            set
            {
                clinicEventsList = value;
            }
        }

        //lista panela
        List<Panel> activePanels = new List<Panel>();

        //draggable buttons
        bool dragExam = true;
        bool dragLab = true;
        bool dragTherapy = true;
        bool dragOperation = true;
        bool dragCheckup = true;

        public FormPlanerKlinike()
        {
            InitializeComponent();
            ClinicRepository clinicRepository = new ClinicRepository();
            new PlanerKlinikePresenter(this, clinicRepository);

            //sklanjam difoltni TitleBar
            this.Text = String.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;

            #region Kreiramo listu panela            
            activePanels.Add(panel8Mon);
            activePanels.Add(panel8Tue);
            activePanels.Add(panel8Wed);
            activePanels.Add(panel8Thu);
            activePanels.Add(panel8Fri);
            activePanels.Add(panel8Sat);
            activePanels.Add(panel8Sun);
            activePanels.Add(panel9Mon);
            activePanels.Add(panel9Tue);
            activePanels.Add(panel9Wed);
            activePanels.Add(panel9Thu);
            activePanels.Add(panel9Fri);
            activePanels.Add(panel9Sat);
            activePanels.Add(panel9Sun);
            activePanels.Add(panel10Mon);
            activePanels.Add(panel10Tue);
            activePanels.Add(panel10Wed);
            activePanels.Add(panel10Thu);
            activePanels.Add(panel10Fri);
            activePanels.Add(panel10Sat);
            activePanels.Add(panel10Sun);
            activePanels.Add(panel11Mon);
            activePanels.Add(panel11Tue);
            activePanels.Add(panel11Wed);
            activePanels.Add(panel11Thu);
            activePanels.Add(panel11Fri);
            activePanels.Add(panel11Sat);
            activePanels.Add(panel11Sun);
            activePanels.Add(panel12Mon);
            activePanels.Add(panel12Tue);
            activePanels.Add(panel12Wed);
            activePanels.Add(panel12Thu);
            activePanels.Add(panel12Fri);
            activePanels.Add(panel12Sat);
            activePanels.Add(panel12Sun);
            activePanels.Add(panel13Mon);
            activePanels.Add(panel13Tue);
            activePanels.Add(panel13Wed);
            activePanels.Add(panel13Thu);
            activePanels.Add(panel13Fri);
            activePanels.Add(panel13Sat);
            activePanels.Add(panel13Sun);
            activePanels.Add(panel14Mon);
            activePanels.Add(panel14Tue);
            activePanels.Add(panel14Wed);
            activePanels.Add(panel14Thu);
            activePanels.Add(panel14Fri);
            activePanels.Add(panel14Sat);
            activePanels.Add(panel14Sun);
            activePanels.Add(panel15Mon);
            activePanels.Add(panel15Tue);
            activePanels.Add(panel15Wed);
            activePanels.Add(panel15Thu);
            activePanels.Add(panel15Fri);
            activePanels.Add(panel15Sat);
            activePanels.Add(panel15Sun);
            activePanels.Add(panel16Mon);
            activePanels.Add(panel16Tue);
            activePanels.Add(panel16Wed);
            activePanels.Add(panel16Thu);
            activePanels.Add(panel16Fri);
            activePanels.Add(panel16Sat);
            activePanels.Add(panel16Sun);
            activePanels.Add(panel17Mon);
            activePanels.Add(panel17Tue);
            activePanels.Add(panel17Wed);
            activePanels.Add(panel17Thu);
            activePanels.Add(panel17Fri);
            activePanels.Add(panel17Sat);
            activePanels.Add(panel17Sun);
            activePanels.Add(panel18Mon);
            activePanels.Add(panel18Tue);
            activePanels.Add(panel18Wed);
            activePanels.Add(panel18Thu);
            activePanels.Add(panel18Fri);
            activePanels.Add(panel18Sat);
            activePanels.Add(panel18Sun);
            activePanels.Add(panel19Mon);
            activePanels.Add(panel19Tue);
            activePanels.Add(panel19Wed);
            activePanels.Add(panel19Thu);
            activePanels.Add(panel19Fri);
            activePanels.Add(panel19Sat);
            activePanels.Add(panel19Sun);
            activePanels.Add(panel20Mon);
            activePanels.Add(panel20Tue);
            activePanels.Add(panel20Wed);
            activePanels.Add(panel20Thu);
            activePanels.Add(panel20Fri);
            activePanels.Add(panel20Sat);
            activePanels.Add(panel20Sun);
            activePanels.Add(panel21Mon);
            activePanels.Add(panel21Tue);
            activePanels.Add(panel21Wed);
            activePanels.Add(panel21Thu);
            activePanels.Add(panel21Fri);
            activePanels.Add(panel21Sat);
            activePanels.Add(panel21Sun);
            #endregion
        }

        private void buttonSelectPagesScheduler_Click(object sender, EventArgs e)
        {
            if (panelSelectPagesScheduler.Height == 164)
            {
                panelSelectPagesScheduler.Height = 41;
            }
            else
            {
                panelSelectPagesScheduler.Height = 164;
            }
        }

        private void FormPlanerKlinike_Load(object sender, EventArgs e)
        {
            //popunjava inicijalno datume u planeru
            //na osnovu danasnjeg dana pravilno popuni planer od ponedeljka do nedelje
            CultureInfo ci = Thread.CurrentThread.CurrentUICulture;
            DayOfWeek today = ci.Calendar.GetDayOfWeek(DateTime.Today);

            //sacuvaj aktuelnu nedelju
            DataContainer.week = ci.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

            //sacuvaj aktuelnu godinu
            DataContainer.year = DateTime.Now.Year;

            //ucitavamo labele
            switch (today)
            {
                case DayOfWeek.Monday:
                    //ponedeljak
                    var mondayDate = DateTime.Now.ToString(" d. MMMM yyyy. ", ci);
                    labelMondayDate.Text = "[" + mondayDate + "]";

                    //utorak
                    var tuesdayDate = DateTime.Now.AddDays(1).ToString(" d. MMMM yyyy. ", ci);
                    labelTuesdayDate.Text = "[" + tuesdayDate + "]";

                    //sreda
                    var wednesdayDate = DateTime.Now.AddDays(2).ToString(" d. MMMM yyyy. ", ci);
                    labelWednesdayDate.Text = "[" + wednesdayDate + "]";

                    //cetvrtak
                    var thursdayDate = DateTime.Now.AddDays(3).ToString(" d. MMMM yyyy. ", ci);
                    labelThursdayDate.Text = "[" + thursdayDate + "]";

                    //petak
                    var fridayDate = DateTime.Now.AddDays(4).ToString(" d. MMMM yyyy. ", ci);
                    labelFridayDate.Text = "[" + fridayDate + "]";

                    //subota
                    var saturdayDate = DateTime.Now.AddDays(5).ToString(" d. MMMM yyyy. ", ci);
                    labelSaturdayDate.Text = "[" + saturdayDate + "]";

                    //nedelja
                    var sundayDate = DateTime.Now.AddDays(6).ToString(" d. MMMM yyyy. ", ci);
                    labelSundayDate.Text = "[" + sundayDate + "]";
                    break;
                case DayOfWeek.Tuesday:
                    //ponedeljak
                    mondayDate = DateTime.Now.AddDays(-1).ToString(" d. MMMM yyyy. ", ci);
                    labelMondayDate.Text = "[" + mondayDate + "]";

                    //utorak
                    tuesdayDate = DateTime.Now.ToString(" d. MMMM yyyy. ", ci);
                    labelTuesdayDate.Text = "[" + tuesdayDate + "]";

                    //sreda
                    wednesdayDate = DateTime.Now.AddDays(1).ToString(" d. MMMM yyyy. ", ci);
                    labelWednesdayDate.Text = "[" + wednesdayDate + "]";

                    //cetvrtak
                    thursdayDate = DateTime.Now.AddDays(2).ToString(" d. MMMM yyyy. ", ci);
                    labelThursdayDate.Text = "[" + thursdayDate + "]";

                    //petak
                    fridayDate = DateTime.Now.AddDays(3).ToString(" d. MMMM yyyy. ", ci);
                    labelFridayDate.Text = "[" + fridayDate + "]";

                    //subota
                    saturdayDate = DateTime.Now.AddDays(4).ToString(" d. MMMM yyyy. ", ci);
                    labelSaturdayDate.Text = "[" + saturdayDate + "]";

                    //nedelja
                    sundayDate = DateTime.Now.AddDays(5).ToString(" d. MMMM yyyy. ", ci);
                    labelSundayDate.Text = "[" + sundayDate + "]";
                    break;
                case DayOfWeek.Wednesday:
                    //ponedeljak
                    mondayDate = DateTime.Now.AddDays(-2).ToString(" d. MMMM yyyy. ", ci);
                    labelMondayDate.Text = "[" + mondayDate + "]";

                    //utorak
                    tuesdayDate = DateTime.Now.AddDays(-1).ToString(" d. MMMM yyyy. ", ci);
                    labelTuesdayDate.Text = "[" + tuesdayDate + "]";

                    //sreda
                    wednesdayDate = DateTime.Now.ToString(" d. MMMM yyyy. ", ci);
                    labelWednesdayDate.Text = "[" + wednesdayDate + "]";

                    //cetvrtak
                    thursdayDate = DateTime.Now.AddDays(1).ToString(" d. MMMM yyyy. ", ci);
                    labelThursdayDate.Text = "[" + thursdayDate + "]";

                    //petak
                    fridayDate = DateTime.Now.AddDays(2).ToString(" d. MMMM yyyy. ", ci);
                    labelFridayDate.Text = "[" + fridayDate + "]";

                    //subota
                    saturdayDate = DateTime.Now.AddDays(3).ToString(" d. MMMM yyyy. ", ci);
                    labelSaturdayDate.Text = "[" + saturdayDate + "]";

                    //nedelja
                    sundayDate = DateTime.Now.AddDays(4).ToString(" d. MMMM yyyy. ", ci);
                    labelSundayDate.Text = "[" + sundayDate + "]";
                    break;
                case DayOfWeek.Thursday:
                    //ponedeljak
                    mondayDate = DateTime.Now.AddDays(-3).ToString(" d. MMMM yyyy. ", ci);
                    labelMondayDate.Text = "[" + mondayDate + "]";

                    //utorak
                    tuesdayDate = DateTime.Now.AddDays(-2).ToString(" d. MMMM yyyy. ", ci);
                    labelTuesdayDate.Text = "[" + tuesdayDate + "]";

                    //sreda
                    wednesdayDate = DateTime.Now.AddDays(-1).ToString(" d. MMMM yyyy. ", ci);
                    labelWednesdayDate.Text = "[" + wednesdayDate + "]";

                    //cetvrtak
                    thursdayDate = DateTime.Now.ToString(" d. MMMM yyyy. ", ci);
                    labelThursdayDate.Text = "[" + thursdayDate + "]";

                    //petak
                    fridayDate = DateTime.Now.AddDays(1).ToString(" d. MMMM yyyy. ", ci);
                    labelFridayDate.Text = "[" + fridayDate + "]";

                    //subota
                    saturdayDate = DateTime.Now.AddDays(2).ToString(" d. MMMM yyyy. ", ci);
                    labelSaturdayDate.Text = "[" + saturdayDate + "]";

                    //nedelja
                    sundayDate = DateTime.Now.AddDays(3).ToString(" d. MMMM yyyy. ", ci);
                    labelSundayDate.Text = "[" + sundayDate + "]";
                    break;
                case DayOfWeek.Friday:
                    //ponedeljak
                    mondayDate = DateTime.Now.AddDays(-4).ToString(" d. MMMM yyyy. ", ci);
                    labelMondayDate.Text = "[" + mondayDate + "]";

                    //utorak
                    tuesdayDate = DateTime.Now.AddDays(-3).ToString(" d. MMMM yyyy. ", ci);
                    labelTuesdayDate.Text = "[" + tuesdayDate + "]";

                    //sreda
                    wednesdayDate = DateTime.Now.AddDays(-2).ToString(" d. MMMM yyyy. ", ci);
                    labelWednesdayDate.Text = "[" + wednesdayDate + "]";

                    //cetvrtak
                    thursdayDate = DateTime.Now.AddDays(-1).ToString(" d. MMMM yyyy. ", ci);
                    labelThursdayDate.Text = "[" + thursdayDate + "]";

                    //petak
                    fridayDate = DateTime.Now.ToString(" d. MMMM yyyy. ", ci);
                    labelFridayDate.Text = "[" + fridayDate + "]";

                    //subota
                    saturdayDate = DateTime.Now.AddDays(1).ToString(" d. MMMM yyyy. ", ci);
                    labelSaturdayDate.Text = "[" + saturdayDate + "]";

                    //nedelja
                    sundayDate = DateTime.Now.AddDays(2).ToString(" d. MMMM yyyy. ", ci);
                    labelSundayDate.Text = "[" + sundayDate + "]";
                    break;
                case DayOfWeek.Saturday:
                    //ponedeljak
                    mondayDate = DateTime.Now.AddDays(-5).ToString(" d. MMMM yyyy. ", ci);
                    labelMondayDate.Text = "[" + mondayDate + "]";

                    //utorak
                    tuesdayDate = DateTime.Now.AddDays(-4).ToString(" d. MMMM yyyy. ", ci);
                    labelTuesdayDate.Text = "[" + tuesdayDate + "]";

                    //sreda
                    wednesdayDate = DateTime.Now.AddDays(-3).ToString(" d. MMMM yyyy. ", ci);
                    labelWednesdayDate.Text = "[" + wednesdayDate + "]";

                    //cetvrtak
                    thursdayDate = DateTime.Now.AddDays(-2).ToString(" d. MMMM yyyy. ", ci);
                    labelThursdayDate.Text = "[" + thursdayDate + "]";

                    //petak
                    fridayDate = DateTime.Now.AddDays(-1).ToString(" d. MMMM yyyy. ", ci);
                    labelFridayDate.Text = "[" + fridayDate + "]";

                    //subota
                    saturdayDate = DateTime.Now.ToString(" d. MMMM yyyy. ", ci);
                    labelSaturdayDate.Text = "[" + saturdayDate + "]";

                    //nedelja
                    sundayDate = DateTime.Now.AddDays(1).ToString(" d. MMMM yyyy. ", ci);
                    labelSundayDate.Text = "[" + sundayDate + "]";
                    break;
                case DayOfWeek.Sunday:
                    //ponedeljak
                    mondayDate = DateTime.Now.AddDays(-6).ToString(" d. MMMM yyyy. ", ci);
                    labelMondayDate.Text = "[" + mondayDate + "]";

                    //utorak
                    tuesdayDate = DateTime.Now.AddDays(-5).ToString(" d. MMMM yyyy. ", ci);
                    labelTuesdayDate.Text = "[" + tuesdayDate + "]";

                    //sreda
                    wednesdayDate = DateTime.Now.AddDays(-4).ToString(" d. MMMM yyyy. ", ci);
                    labelWednesdayDate.Text = "[" + wednesdayDate + "]";

                    //cetvrtak
                    thursdayDate = DateTime.Now.AddDays(-3).ToString(" d. MMMM yyyy. ", ci);
                    labelThursdayDate.Text = "[" + thursdayDate + "]";

                    //petak
                    fridayDate = DateTime.Now.AddDays(-2).ToString(" d. MMMM yyyy. ", ci);
                    labelFridayDate.Text = "[" + fridayDate + "]";

                    //subota
                    saturdayDate = DateTime.Now.AddDays(-1).ToString(" d. MMMM yyyy. ", ci);
                    labelSaturdayDate.Text = "[" + saturdayDate + "]";

                    //nedelja
                    sundayDate = DateTime.Now.ToString(" d. MMMM yyyy. ", ci);
                    labelSundayDate.Text = "[" + sundayDate + "]";
                    break;

            }

            this.updatePlaner(ci.Calendar);

            //informacije koje ogranicavaju doktora
            if (ActiveDoctor.surgery != 1)
            {
                dragOperation = false;
            }
                

            //ogranicavanje doktora za usluge na osnovu specijalizacije
            switch (ActiveDoctor.specialization)
            {
                case "Laborant":
                    dragExam = false;
                    dragOperation = false;
                    dragTherapy = false;
                    dragCheckup = false;
                    break;
                case "Endokrinolog":
                    dragLab = false;
                    break;
                case "Internista":
                    dragLab = false;
                    dragOperation = false;
                    break;
                case "Onkolog":
                    dragLab = false;
                    break;
                case "Infektolog":
                    dragLab = false;
                    break;
                case "Neurolog":
                    dragLab = false;
                    break;
                case "Ginekolog":
                    dragLab = false;
                    break;
                case "Urolog":
                    dragLab = false;
                    break;
                case "Opsta medicina":
                    dragLab = false;
                    break;
                case "Ortoped":
                    dragLab = false;
                    break;
                case "Kardiolog":
                    dragLab = false;
                    break;
                case "Oftalmolog":
                    dragLab = false;
                    break;
                case "Radiolog":
                    dragLab = false;
                    dragOperation = false;
                    dragTherapy = false;
                    dragCheckup = false;
                    break;
            }
        }

        #region datum get/set
        //getteri i setter za labele dana, da preuzimam datume iz drugih formi
        public string LabelMondayText
        {
            get { return labelMondayDate.Text; }
            set { labelMondayDate.Text = value; }
        }
        public string LabelTuesdayText
        {
            get { return labelTuesdayDate.Text; }
            set { labelTuesdayDate.Text = value; }
        }
        public string LabelWednesdayText
        {
            get { return labelWednesdayDate.Text; }
            set { labelWednesdayDate.Text = value; }
        }
        public string LabelThursdayText
        {
            get { return labelThursdayDate.Text; }
            set { labelThursdayDate.Text = value; }
        }
        public string LabelFridayText
        {
            get { return labelFridayDate.Text; }
            set { labelFridayDate.Text = value; }
        }
        public string LabelSaturdayText
        {
            get { return labelSaturdayDate.Text; }
            set { labelSaturdayDate.Text = value; }
        }
        public string LabelSundayText
        {
            get { return labelSundayDate.Text; }
            set { labelSundayDate.Text = value; }
        }
        #endregion

        //prikazuje sledecih nedelju dana u kalendaru
        private void buttonNext_Click(object sender, EventArgs e)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentUICulture;

            //apdejtujemo aktuelnu nedelju
            DataContainer.week += 1;
            if (DataContainer.week > this.getWeeksInYear(DataContainer.year, ci))
            {
                DataContainer.year += 1;
                DataContainer.week = 1;
            }

            //ponedeljak
            DateTime datum = DateTime.ParseExact(labelMondayDate.Text.Substring(1, labelMondayDate.Text.Length - 2), " d. MMMM yyyy. ", ci, DateTimeStyles.NoCurrentDateDefault);
            var novDatum = datum.AddDays(7).ToString(" d. MMMM yyyy. ", ci);
            labelMondayDate.Text = "[" + novDatum + "]";

            //utorak
            datum = DateTime.ParseExact(labelTuesdayDate.Text.Substring(1, labelTuesdayDate.Text.Length - 2), " d. MMMM yyyy. ", ci, DateTimeStyles.NoCurrentDateDefault);
            novDatum = datum.AddDays(7).ToString(" d. MMMM yyyy. ", ci);
            labelTuesdayDate.Text = "[" + novDatum + "]";

            //sreda
            datum = DateTime.ParseExact(labelWednesdayDate.Text.Substring(1, labelWednesdayDate.Text.Length - 2), " d. MMMM yyyy. ", ci, DateTimeStyles.NoCurrentDateDefault);
            novDatum = datum.AddDays(7).ToString(" d. MMMM yyyy. ", ci);
            labelWednesdayDate.Text = "[" + novDatum + "]";

            //cetvrtak
            datum = DateTime.ParseExact(labelThursdayDate.Text.Substring(1, labelThursdayDate.Text.Length - 2), " d. MMMM yyyy. ", ci, DateTimeStyles.NoCurrentDateDefault);
            novDatum = datum.AddDays(7).ToString(" d. MMMM yyyy. ", ci);
            labelThursdayDate.Text = "[" + novDatum + "]";

            //petak
            datum = DateTime.ParseExact(labelFridayDate.Text.Substring(1, labelFridayDate.Text.Length - 2), " d. MMMM yyyy. ", ci, DateTimeStyles.NoCurrentDateDefault);
            novDatum = datum.AddDays(7).ToString(" d. MMMM yyyy. ", ci);
            labelFridayDate.Text = "[" + novDatum + "]";

            //subota
            datum = DateTime.ParseExact(labelSaturdayDate.Text.Substring(1, labelSaturdayDate.Text.Length - 2), " d. MMMM yyyy. ", ci, DateTimeStyles.NoCurrentDateDefault);
            novDatum = datum.AddDays(7).ToString(" d. MMMM yyyy. ", ci);
            labelSaturdayDate.Text = "[" + novDatum + "]";

            //nedelja
            datum = DateTime.ParseExact(labelSundayDate.Text.Substring(1, labelSundayDate.Text.Length - 2), " d. MMMM yyyy. ", ci, DateTimeStyles.NoCurrentDateDefault);
            novDatum = datum.AddDays(7).ToString(" d. MMMM yyyy. ", ci);
            labelSundayDate.Text = "[" + novDatum + "]";


            this.updatePlaner(ci.Calendar);
        }

        //prikazuje prethodnih nedelju dana u kalendaru
        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentUICulture;

            //apdejtujemo aktuelnu nedelju
            DataContainer.week -= 1;
            if (DataContainer.week < 1)
            {
                DataContainer.year -= 1;
                DataContainer.week = this.getWeeksInYear(DataContainer.year, ci);
            }

            //ponedeljak
            DateTime datum = DateTime.ParseExact(labelMondayDate.Text.Substring(1, labelMondayDate.Text.Length - 2), " d. MMMM yyyy. ", ci, DateTimeStyles.NoCurrentDateDefault);
            var novDatum = datum.AddDays(-7).ToString(" d. MMMM yyyy. ", ci);
            labelMondayDate.Text = "[" + novDatum + "]";

            //utorak
            datum = DateTime.ParseExact(labelTuesdayDate.Text.Substring(1, labelTuesdayDate.Text.Length - 2), " d. MMMM yyyy. ", ci, DateTimeStyles.NoCurrentDateDefault);
            novDatum = datum.AddDays(-7).ToString(" d. MMMM yyyy. ", ci);
            labelTuesdayDate.Text = "[" + novDatum + "]";

            //sreda
            datum = DateTime.ParseExact(labelWednesdayDate.Text.Substring(1, labelWednesdayDate.Text.Length - 2), " d. MMMM yyyy. ", ci, DateTimeStyles.NoCurrentDateDefault);
            novDatum = datum.AddDays(-7).ToString(" d. MMMM yyyy. ", ci);
            labelWednesdayDate.Text = "[" + novDatum + "]";

            //cetvrtak
            datum = DateTime.ParseExact(labelThursdayDate.Text.Substring(1, labelThursdayDate.Text.Length - 2), " d. MMMM yyyy. ", ci, DateTimeStyles.NoCurrentDateDefault);
            novDatum = datum.AddDays(-7).ToString(" d. MMMM yyyy. ", ci);
            labelThursdayDate.Text = "[" + novDatum + "]";

            //petak
            datum = DateTime.ParseExact(labelFridayDate.Text.Substring(1, labelFridayDate.Text.Length - 2), " d. MMMM yyyy. ", ci, DateTimeStyles.NoCurrentDateDefault);
            novDatum = datum.AddDays(-7).ToString(" d. MMMM yyyy. ", ci);
            labelFridayDate.Text = "[" + novDatum + "]";

            //subota
            datum = DateTime.ParseExact(labelSaturdayDate.Text.Substring(1, labelSaturdayDate.Text.Length - 2), " d. MMMM yyyy. ", ci, DateTimeStyles.NoCurrentDateDefault);
            novDatum = datum.AddDays(-7).ToString(" d. MMMM yyyy. ", ci);
            labelSaturdayDate.Text = "[" + novDatum + "]";

            //nedelja
            datum = DateTime.ParseExact(labelSundayDate.Text.Substring(1, labelSundayDate.Text.Length - 2), " d. MMMM yyyy. ", ci, DateTimeStyles.NoCurrentDateDefault);
            novDatum = datum.AddDays(-7).ToString(" d. MMMM yyyy. ", ci);
            labelSundayDate.Text = "[" + novDatum + "]";


            this.updatePlaner(ci.Calendar);
        }

        //filtrira planer
        private void buttonSchedulerFilter_Click(object sender, EventArgs e)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentUICulture;
            this.updatePlaner(ci.Calendar);

            foreach (var panel in activePanels)
            {
                #region Na osnovu TabIndexa apdejtujemo panele postujuci filtere
                //pomocne za hvatanje imena pacijenta
                int pFrom;
                String patientName = "";

                switch (panel.TabIndex)
                {
                    case 1:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label8Mon.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label8Mon.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label8Mon.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label8Mon.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label8Mon.Text.IndexOf("for\n") + "for\n".Length;                           
                                if (pFrom > 3)
                                    patientName = label8Mon.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel8Mon.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel8Mon.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel8Mon.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel8Mon.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel8Mon.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel8Mon.BackColor = Color.White;
                            ToolTipsText.Mon8 = "";
                            toolTipMon8.Hide(panel8Mon);
                            label8Mon.Hide();
                        }
                        break;
                    case 2:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label8Tue.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label8Tue.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label8Tue.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label8Tue.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label8Tue.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label8Tue.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel8Tue.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel8Tue.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel8Tue.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel8Tue.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel8Tue.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel8Tue.BackColor = Color.White;
                            ToolTipsText.Tue8 = "";
                            toolTipTue8.Hide(panel8Tue);
                            label8Tue.Hide();
                        }
                        break;
                    case 3:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label8Wed.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label8Wed.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label8Wed.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label8Wed.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label8Wed.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label8Wed.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel8Wed.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel8Wed.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel8Wed.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel8Wed.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel8Wed.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel8Wed.BackColor = Color.White;
                            ToolTipsText.Wed8 = "";
                            toolTipWed8.Hide(panel8Wed);
                            label8Wed.Hide();
                        }
                        break;
                    case 4:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label8Thu.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label8Thu.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label8Thu.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label8Thu.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label8Thu.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label8Thu.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel8Thu.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel8Thu.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel8Thu.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel8Thu.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel8Thu.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel8Thu.BackColor = Color.White;
                            ToolTipsText.Thu8 = "";
                            toolTipThu8.Hide(panel8Thu);
                            label8Thu.Hide();
                        }
                        break;
                    case 5:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label8Fri.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label8Fri.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label8Fri.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label8Fri.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label8Fri.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label8Fri.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel8Fri.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel8Fri.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel8Fri.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel8Fri.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel8Fri.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel8Fri.BackColor = Color.White;
                            ToolTipsText.Fri8 = "";
                            toolTipFri8.Hide(panel8Fri);
                            label8Fri.Hide();
                        }
                        break;
                    case 6:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label8Sat.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label8Sat.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label8Sat.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label8Sat.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label8Sat.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label8Sat.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel8Sat.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel8Sat.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel8Sat.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel8Sat.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel8Sat.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel8Sat.BackColor = Color.White;
                            ToolTipsText.Sat8 = "";
                            toolTipSat8.Hide(panel8Sat);
                            label8Sat.Hide();
                        }
                        break;
                    case 7:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label8Sun.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label8Sun.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label8Sun.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label8Sun.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label8Sun.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label8Sun.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel8Sun.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel8Sun.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel8Sun.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel8Sun.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel8Sun.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel8Sun.BackColor = Color.White;
                            ToolTipsText.Sun8 = "";
                            toolTipSun8.Hide(panel8Sun);
                            label8Sun.Hide();
                        }
                        break;
                    case 8:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label9Mon.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label9Mon.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label9Mon.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label9Mon.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label9Mon.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label9Mon.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel9Mon.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel9Mon.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel9Mon.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel9Mon.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel9Mon.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel9Mon.BackColor = Color.White;
                            ToolTipsText.Mon9 = "";
                            toolTipMon9.Hide(panel9Mon);
                            label9Mon.Hide();
                        }
                        break;
                    case 9:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label9Tue.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label9Tue.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label9Tue.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label9Tue.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label9Tue.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label9Tue.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel9Tue.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel9Tue.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel9Tue.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel9Tue.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel9Tue.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel9Tue.BackColor = Color.White;
                            ToolTipsText.Tue9 = "";
                            toolTipTue9.Hide(panel9Tue);
                            label9Tue.Hide();
                        }
                        break;
                    case 10:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label9Wed.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label9Wed.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label9Wed.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label9Wed.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label9Wed.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label9Wed.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel9Wed.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel9Wed.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel9Wed.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel9Wed.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel9Wed.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel9Wed.BackColor = Color.White;
                            ToolTipsText.Wed9 = "";
                            toolTipWed9.Hide(panel9Wed);
                            label9Wed.Hide();
                        }
                        break;
                    case 11:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label9Thu.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label9Thu.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label9Thu.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label9Thu.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label9Thu.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label9Thu.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel9Thu.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel9Thu.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel9Thu.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel9Thu.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel9Thu.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel9Thu.BackColor = Color.White;
                            ToolTipsText.Thu9 = "";
                            toolTipThu9.Hide(panel9Thu);
                            label9Thu.Hide();
                        }
                        break;
                    case 12:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label9Fri.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label9Fri.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label9Fri.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label9Fri.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label9Fri.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label9Fri.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel9Fri.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel9Fri.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel9Fri.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel9Fri.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel9Fri.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel9Fri.BackColor = Color.White;
                            ToolTipsText.Fri9 = "";
                            toolTipFri9.Hide(panel9Fri);
                            label9Fri.Hide();
                        }
                        break;
                    case 13:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label9Sat.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label9Sat.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label9Sat.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label9Sat.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label9Sat.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label9Sat.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel9Sat.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel9Sat.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel9Sat.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel9Sat.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel9Sat.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel9Sat.BackColor = Color.White;
                            ToolTipsText.Sat9 = "";
                            toolTipSat9.Hide(panel9Sat);
                            label9Sat.Hide();
                        }
                        break;
                    case 14:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label9Sun.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label9Sun.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label9Sun.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label9Sun.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label9Sun.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label9Sun.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel9Sun.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel9Sun.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel9Sun.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel9Sun.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel9Sun.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel9Sun.BackColor = Color.White;
                            ToolTipsText.Sun9 = "";
                            toolTipSun9.Hide(panel9Sun);
                            label9Sun.Hide();
                        }
                        break;
                    case 15:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label10Mon.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label10Mon.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label10Mon.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label10Mon.Text.Substring(pFrom);
                                break;
                            default:                                
                                pFrom = label10Mon.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label10Mon.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel10Mon.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel10Mon.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel10Mon.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel10Mon.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel10Mon.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel10Mon.BackColor = Color.White;
                            ToolTipsText.Mon10 = "";
                            toolTipMon10.Hide(panel10Mon);
                            label10Mon.Hide();
                        }
                        break;
                    case 16:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label10Tue.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label10Tue.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label10Tue.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label10Tue.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label10Tue.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label10Tue.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel10Tue.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel10Tue.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel10Tue.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel10Tue.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel10Tue.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel10Tue.BackColor = Color.White;
                            ToolTipsText.Tue10 = "";
                            toolTipTue10.Hide(panel10Tue);
                            label10Tue.Hide();
                        }
                        break;
                    case 17:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label10Wed.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label10Wed.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label10Wed.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label10Wed.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label10Wed.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label10Wed.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel10Wed.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel10Wed.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel10Wed.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel10Wed.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel10Wed.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel10Wed.BackColor = Color.White;
                            ToolTipsText.Wed10 = "";
                            toolTipWed10.Hide(panel10Wed);
                            label10Wed.Hide();
                        }
                        break;
                    case 18:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label10Thu.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label10Thu.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label10Thu.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label10Thu.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label10Thu.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label10Thu.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel10Thu.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel10Thu.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel10Thu.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel10Thu.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel10Thu.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel10Thu.BackColor = Color.White;
                            ToolTipsText.Thu10 = "";
                            toolTipThu10.Hide(panel10Thu);
                            label10Thu.Hide();
                        }
                        break;
                    case 19:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label10Fri.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label10Fri.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label10Fri.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label10Fri.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label10Fri.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label10Fri.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel10Fri.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel10Fri.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel10Fri.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel10Fri.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel10Fri.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel10Fri.BackColor = Color.White;
                            ToolTipsText.Fri10 = "";
                            toolTipFri10.Hide(panel10Fri);
                            label10Fri.Hide();
                        }
                        break;
                    case 20:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label10Sat.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label10Sat.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label10Sat.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label10Sat.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label10Sat.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label10Sat.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel10Sat.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel10Sat.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel10Sat.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel10Sat.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel10Sat.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel10Sat.BackColor = Color.White;
                            ToolTipsText.Sat10 = "";
                            toolTipSat10.Hide(panel10Sat);
                            label10Sat.Hide();
                        }
                        break;
                    case 21:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label10Sun.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label10Sun.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label10Sun.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label10Sun.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label10Sun.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label10Sun.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel10Sun.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel10Sun.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel10Sun.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel10Sun.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel10Sun.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel10Sun.BackColor = Color.White;
                            ToolTipsText.Sun10 = "";
                            toolTipSun10.Hide(panel10Sun);
                            label10Sun.Hide();
                        }
                        break;
                    case 22:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label11Mon.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label11Mon.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label11Mon.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label11Mon.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label11Mon.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label11Mon.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel11Mon.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel11Mon.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel11Mon.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel11Mon.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel11Mon.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel11Mon.BackColor = Color.White;
                            ToolTipsText.Mon11 = "";
                            toolTipMon11.Hide(panel11Mon);
                            label11Mon.Hide();
                        }
                        break;
                    case 23:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label11Tue.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label11Tue.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label11Tue.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label11Tue.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label11Tue.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label11Tue.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel11Tue.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel11Tue.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel11Tue.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel11Tue.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel11Tue.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel11Tue.BackColor = Color.White;
                            ToolTipsText.Tue11 = "";
                            toolTipTue11.Hide(panel11Tue);
                            label11Tue.Hide();
                        }
                        break;
                    case 24:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label11Wed.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label11Wed.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label11Wed.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label11Wed.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label11Wed.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label11Wed.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel11Wed.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel11Wed.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel11Wed.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel11Wed.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel11Wed.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel11Wed.BackColor = Color.White;
                            ToolTipsText.Wed11 = "";
                            toolTipWed11.Hide(panel11Wed);
                            label11Wed.Hide();
                        }
                        break;
                    case 25:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label11Thu.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label11Thu.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label11Thu.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label11Thu.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label11Thu.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label11Thu.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel11Thu.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel11Thu.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel11Thu.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel11Thu.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel11Thu.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel11Thu.BackColor = Color.White;
                            ToolTipsText.Thu11 = "";
                            toolTipThu11.Hide(panel11Thu);
                            label11Thu.Hide();
                        }
                        break;
                    case 26:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label11Fri.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label11Fri.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label11Fri.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label11Fri.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label11Fri.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label11Fri.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel11Fri.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel11Fri.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel11Fri.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel11Fri.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel11Fri.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel11Fri.BackColor = Color.White;
                            ToolTipsText.Fri11 = "";
                            toolTipFri11.Hide(panel11Fri);
                            label11Fri.Hide();
                        }
                        break;
                    case 27:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label11Sat.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label11Sat.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label11Sat.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label11Sat.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label11Sat.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label11Sat.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel11Sat.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel11Sat.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel11Sat.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel11Sat.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel11Sat.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel11Sat.BackColor = Color.White;
                            ToolTipsText.Sat11 = "";
                            toolTipSat11.Hide(panel11Sat);
                            label11Sat.Hide();
                        }
                        break;
                    case 28:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label11Sun.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label11Sun.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label11Sun.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label11Sun.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label11Sun.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label11Sun.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel11Sun.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel11Sun.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel11Sun.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel11Sun.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel11Sun.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel11Sun.BackColor = Color.White;
                            ToolTipsText.Sun11 = "";
                            toolTipSun11.Hide(panel11Sun);
                            label11Sun.Hide();
                        }
                        break;
                    case 29:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label12Mon.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label12Mon.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label12Mon.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label12Mon.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label12Mon.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label12Mon.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel12Mon.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel12Mon.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel12Mon.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel12Mon.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel12Mon.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel12Mon.BackColor = Color.White;
                            ToolTipsText.Mon12 = "";
                            toolTipMon12.Hide(panel12Mon);
                            label12Mon.Hide();
                        }
                        break;
                    case 30:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label12Tue.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label12Tue.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label12Tue.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label12Tue.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label12Tue.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label12Tue.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel12Tue.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel12Tue.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel12Tue.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel12Tue.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel12Tue.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel12Tue.BackColor = Color.White;
                            ToolTipsText.Tue12 = "";
                            toolTipTue12.Hide(panel12Tue);
                            label12Tue.Hide();
                        }
                        break;
                    case 31:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label12Wed.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label12Wed.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label12Wed.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label12Wed.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label12Wed.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label12Wed.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel12Wed.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel12Wed.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel12Wed.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel12Wed.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel12Wed.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel12Wed.BackColor = Color.White;
                            ToolTipsText.Wed12 = "";
                            toolTipWed12.Hide(panel12Wed);
                            label12Wed.Hide();
                        }
                        break;
                    case 32:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label12Thu.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label12Thu.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label12Thu.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label12Thu.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label12Thu.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label12Thu.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel12Thu.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel12Thu.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel12Thu.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel12Thu.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel12Thu.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel12Thu.BackColor = Color.White;
                            ToolTipsText.Thu12 = "";
                            toolTipThu12.Hide(panel12Thu);
                            label12Thu.Hide();
                        }
                        break;
                    case 33:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label12Fri.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label12Fri.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label12Fri.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label12Fri.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label12Fri.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label12Fri.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel12Fri.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel12Fri.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel12Fri.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel12Fri.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel12Fri.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel12Fri.BackColor = Color.White;
                            ToolTipsText.Fri12 = "";
                            toolTipFri12.Hide(panel12Fri);
                            label12Fri.Hide();
                        }
                        break;
                    case 34:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label12Sat.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label12Sat.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label12Sat.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label12Sat.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label12Sat.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label12Sat.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel12Sat.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel12Sat.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel12Sat.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel12Sat.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel12Sat.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel12Sat.BackColor = Color.White;
                            ToolTipsText.Sat12 = "";
                            toolTipSat12.Hide(panel12Sat);
                            label12Sat.Hide();
                        }
                        break;
                    case 35:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label12Sun.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label12Sun.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label12Sun.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label12Sun.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label12Sun.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label12Sun.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel12Sun.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel12Sun.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel12Sun.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel12Sun.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel12Sun.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel12Sun.BackColor = Color.White;
                            ToolTipsText.Sun12 = "";
                            toolTipSun12.Hide(panel12Sun);
                            label12Sun.Hide();
                        }
                        break;
                    case 36:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label13Mon.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label13Mon.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label13Mon.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label13Mon.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label13Mon.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label13Mon.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel13Mon.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel13Mon.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel13Mon.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel13Mon.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel13Mon.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel13Mon.BackColor = Color.White;
                            ToolTipsText.Mon13 = "";
                            toolTipMon13.Hide(panel13Mon);
                            label13Mon.Hide();
                        }
                        break;
                    case 37:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label13Tue.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label13Tue.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label13Tue.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label13Tue.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label13Tue.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label13Tue.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel13Tue.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel13Tue.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel13Tue.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel13Tue.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel13Tue.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel13Tue.BackColor = Color.White;
                            ToolTipsText.Tue13 = "";
                            toolTipTue13.Hide(panel13Tue);
                            label13Tue.Hide();
                        }
                        break;
                    case 38:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label13Wed.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label13Wed.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label13Wed.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label13Wed.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label13Wed.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label13Wed.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel13Wed.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel13Wed.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel13Wed.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel13Wed.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel13Wed.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel13Wed.BackColor = Color.White;
                            ToolTipsText.Wed13 = "";
                            toolTipWed13.Hide(panel13Wed);
                            label13Wed.Hide();
                        }
                        break;
                    case 39:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label13Thu.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label13Thu.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label13Thu.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label13Thu.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label13Thu.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label13Thu.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel13Thu.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel13Thu.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel13Thu.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel13Thu.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel13Thu.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel13Thu.BackColor = Color.White;
                            ToolTipsText.Thu13 = "";
                            toolTipThu13.Hide(panel13Thu);
                            label13Thu.Hide();
                        }
                        break;
                    case 40:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label13Fri.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label13Fri.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label13Fri.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label13Fri.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label13Fri.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label13Fri.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel13Fri.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel13Fri.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel13Fri.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel13Fri.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel13Fri.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel13Fri.BackColor = Color.White;
                            ToolTipsText.Fri13 = "";
                            toolTipFri13.Hide(panel13Fri);
                            label13Fri.Hide();
                        }
                        break;
                    case 41:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label13Sat.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label13Sat.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label13Sat.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label13Sat.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label13Sat.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label13Sat.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel13Sat.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel13Sat.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel13Sat.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel13Sat.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel13Sat.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel13Sat.BackColor = Color.White;
                            ToolTipsText.Sat13 = "";
                            toolTipSat13.Hide(panel13Sat);
                            label13Sat.Hide();
                        }
                        break;
                    case 42:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label13Sun.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label13Sun.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label13Sun.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label13Sun.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label13Sun.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label13Sun.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel13Sun.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel13Sun.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel13Sun.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel13Sun.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel13Sun.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel13Sun.BackColor = Color.White;
                            ToolTipsText.Sun13 = "";
                            toolTipSun13.Hide(panel13Sun);
                            label13Sun.Hide();
                        }
                        break;
                    case 43:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label14Mon.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label14Mon.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label14Mon.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label14Mon.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label14Mon.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label14Mon.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel14Mon.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel14Mon.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel14Mon.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel14Mon.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel14Mon.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel14Mon.BackColor = Color.White;
                            ToolTipsText.Mon14 = "";
                            toolTipMon14.Hide(panel14Mon);
                            label14Mon.Hide();
                        }
                        break;
                    case 44:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label14Tue.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label14Tue.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label14Tue.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label14Tue.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label14Tue.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label14Tue.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel14Tue.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel14Tue.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel14Tue.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel14Tue.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel14Tue.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel14Tue.BackColor = Color.White;
                            ToolTipsText.Tue14 = "";
                            toolTipTue14.Hide(panel14Tue);
                            label14Tue.Hide();
                        }
                        break;
                    case 45:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label14Wed.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label14Wed.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label14Wed.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label14Wed.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label14Wed.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label14Wed.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel14Wed.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel14Wed.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel14Wed.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel14Wed.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel14Wed.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel14Wed.BackColor = Color.White;
                            ToolTipsText.Wed14 = "";
                            toolTipWed14.Hide(panel14Wed);
                            label14Wed.Hide();
                        }
                        break;
                    case 46:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label14Thu.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label14Thu.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label14Thu.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label14Thu.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label14Thu.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label14Thu.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel14Thu.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel14Thu.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel14Thu.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel14Thu.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel14Thu.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel14Thu.BackColor = Color.White;
                            ToolTipsText.Thu14 = "";
                            toolTipThu14.Hide(panel14Thu);
                            label14Thu.Hide();
                        }
                        break;
                    case 47:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label14Fri.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label14Fri.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label14Fri.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label14Fri.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label14Fri.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label14Fri.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel14Fri.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel14Fri.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel14Fri.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel14Fri.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel14Fri.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel14Fri.BackColor = Color.White;
                            ToolTipsText.Fri14 = "";
                            toolTipFri14.Hide(panel14Fri);
                            label14Fri.Hide();
                        }
                        break;
                    case 48:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label14Sat.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label14Sat.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label14Sat.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label14Sat.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label14Sat.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label14Sat.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel14Sat.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel14Sat.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel14Sat.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel14Sat.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel14Sat.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel14Sat.BackColor = Color.White;
                            ToolTipsText.Sat14 = "";
                            toolTipSat14.Hide(panel14Sat);
                            label14Sat.Hide();
                        }
                        break;
                    case 49:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label14Sun.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label14Sun.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label14Sun.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label14Sun.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label14Sun.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label14Sun.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel14Sun.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel14Sun.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel14Sun.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel14Sun.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel14Sun.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel14Sun.BackColor = Color.White;
                            ToolTipsText.Sun14 = "";
                            toolTipSun14.Hide(panel14Sun);
                            label14Sun.Hide();
                        }
                        break;
                    case 50:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label15Mon.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label15Mon.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label15Mon.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label15Mon.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label15Mon.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label15Mon.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel15Mon.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel15Mon.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel15Mon.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel15Mon.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel15Mon.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel15Mon.BackColor = Color.White;
                            ToolTipsText.Mon15 = "";
                            toolTipMon15.Hide(panel15Mon);
                            label15Mon.Hide();
                        }
                        break;
                    case 51:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label15Tue.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label15Tue.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label15Tue.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label15Tue.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label15Tue.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label15Tue.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel15Tue.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel15Tue.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel15Tue.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel15Tue.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel15Tue.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel15Tue.BackColor = Color.White;
                            ToolTipsText.Tue15 = "";
                            toolTipTue15.Hide(panel15Tue);
                            label15Tue.Hide();
                        }
                        break;
                    case 52:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label15Wed.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label15Wed.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label15Wed.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label15Wed.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label15Wed.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label15Wed.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel15Wed.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel15Wed.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel15Wed.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel15Wed.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel15Wed.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel15Wed.BackColor = Color.White;
                            ToolTipsText.Wed15 = "";
                            toolTipWed15.Hide(panel15Wed);
                            label15Wed.Hide();
                        }
                        break;
                    case 53:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label15Thu.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label15Thu.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label15Thu.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label15Thu.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label15Thu.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label15Thu.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel15Thu.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel15Thu.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel15Thu.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel15Thu.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel15Thu.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel15Thu.BackColor = Color.White;
                            ToolTipsText.Thu15 = "";
                            toolTipThu15.Hide(panel15Thu);
                            label15Thu.Hide();
                        }
                        break;
                    case 54:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label15Fri.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label15Fri.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label15Fri.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label15Fri.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label15Fri.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label15Fri.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel15Fri.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel15Fri.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel15Fri.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel15Fri.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel15Fri.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel15Fri.BackColor = Color.White;
                            ToolTipsText.Fri15 = "";
                            toolTipFri15.Hide(panel15Fri);
                            label15Fri.Hide();
                        }
                        break;
                    case 55:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label15Sat.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label15Sat.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label15Sat.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label15Sat.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label15Sat.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label15Sat.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel15Sat.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel15Sat.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel15Sat.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel15Sat.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel15Sat.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel15Sat.BackColor = Color.White;
                            ToolTipsText.Sat15 = "";
                            toolTipSat15.Hide(panel15Sat);
                            label15Sat.Hide();
                        }
                        break;
                    case 56:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label15Sun.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label15Sun.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label15Sun.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label15Sun.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label15Sun.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label15Sun.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel15Sun.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel15Sun.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel15Sun.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel15Sun.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel15Sun.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel15Sun.BackColor = Color.White;
                            ToolTipsText.Sun15 = "";
                            toolTipSun15.Hide(panel15Sun);
                            label15Sun.Hide();
                        }
                        break;
                    case 57:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label16Mon.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label16Mon.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label16Mon.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label16Mon.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label16Mon.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label16Mon.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel16Mon.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel16Mon.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel16Mon.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel16Mon.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel16Mon.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel16Mon.BackColor = Color.White;
                            ToolTipsText.Mon16 = "";
                            toolTipMon16.Hide(panel16Mon);
                            label16Mon.Hide();
                        }
                        break;
                    case 58:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label16Tue.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label16Tue.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label16Tue.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label16Tue.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label16Tue.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label16Tue.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel16Tue.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel16Tue.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel16Tue.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel16Tue.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel16Tue.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel16Tue.BackColor = Color.White;
                            ToolTipsText.Tue16 = "";
                            toolTipTue16.Hide(panel16Tue);
                            label16Tue.Hide();
                        }
                        break;
                    case 59:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label16Wed.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label16Wed.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label16Wed.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label16Wed.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label16Wed.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label16Wed.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel16Wed.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel16Wed.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel16Wed.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel16Wed.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel16Wed.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel16Wed.BackColor = Color.White;
                            ToolTipsText.Wed16 = "";
                            toolTipWed16.Hide(panel16Wed);
                            label16Wed.Hide();
                        }
                        break;
                    case 60:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label16Thu.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label16Thu.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label16Thu.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label16Thu.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label16Thu.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label16Thu.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel16Thu.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel16Thu.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel16Thu.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel16Thu.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel16Thu.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel16Thu.BackColor = Color.White;
                            ToolTipsText.Thu16 = "";
                            toolTipThu16.Hide(panel16Thu);
                            label16Thu.Hide();
                        }
                        break;
                    case 61:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label16Fri.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label16Fri.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label16Fri.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label16Fri.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label16Fri.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label16Fri.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel16Fri.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel16Fri.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel16Fri.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel16Fri.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel16Fri.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel16Fri.BackColor = Color.White;
                            ToolTipsText.Fri16 = "";
                            toolTipFri16.Hide(panel16Fri);
                            label16Fri.Hide();
                        }
                        break;
                    case 62:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label16Sat.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label16Sat.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label16Sat.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label16Sat.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label16Sat.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label16Sat.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel16Sat.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel16Sat.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel16Sat.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel16Sat.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel16Sat.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel16Sat.BackColor = Color.White;
                            ToolTipsText.Sat16 = "";
                            toolTipSat16.Hide(panel16Sat);
                            label16Sat.Hide();
                        }
                        break;
                    case 63:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label16Sun.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label16Sun.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label16Sun.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label16Sun.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label16Sun.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label16Sun.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel16Sun.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel16Sun.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel16Sun.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel16Sun.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel16Sun.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel16Sun.BackColor = Color.White;
                            ToolTipsText.Sun16 = "";
                            toolTipSun16.Hide(panel16Sun);
                            label16Sun.Hide();
                        }
                        break;
                    case 64:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label17Mon.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label17Mon.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label17Mon.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label17Mon.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label17Mon.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label17Mon.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel17Mon.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel17Mon.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel17Mon.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel17Mon.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel17Mon.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel17Mon.BackColor = Color.White;
                            ToolTipsText.Mon17 = "";
                            toolTipMon17.Hide(panel17Mon);
                            label17Mon.Hide();
                        }
                        break;
                    case 65:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label17Tue.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label17Tue.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label17Tue.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label17Tue.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label17Tue.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label17Tue.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel17Tue.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel17Tue.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel17Tue.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel17Tue.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel17Tue.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel17Tue.BackColor = Color.White;
                            ToolTipsText.Tue17 = "";
                            toolTipTue17.Hide(panel17Tue);
                            label17Tue.Hide();
                        }
                        break;
                    case 66:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label17Wed.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label17Wed.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label17Wed.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label17Wed.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label17Wed.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label17Wed.Text.Substring(pFrom);
                                break;
                        }

                        if ((checkBoxExamination.Checked == false && panel17Wed.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel17Wed.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel17Wed.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel17Wed.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel17Wed.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel17Wed.BackColor = Color.White;
                            ToolTipsText.Wed17 = "";
                            toolTipWed17.Hide(panel17Wed);
                            label17Wed.Hide();
                        }
                        break;
                    case 67:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label17Thu.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label17Thu.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label17Thu.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label17Thu.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label17Thu.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label17Thu.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel17Thu.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel17Thu.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel17Thu.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel17Thu.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel17Thu.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel17Thu.BackColor = Color.White;
                            ToolTipsText.Thu17 = "";
                            toolTipThu17.Hide(panel17Thu);
                            label17Thu.Hide();
                        }
                        break;
                    case 68:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label17Fri.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label17Fri.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label17Fri.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label17Fri.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label17Fri.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label17Fri.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel17Fri.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel17Fri.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel17Fri.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel17Fri.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel17Fri.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel17Fri.BackColor = Color.White;
                            ToolTipsText.Fri17 = "";
                            toolTipFri17.Hide(panel17Fri);
                            label17Fri.Hide();
                        }
                        break;
                    case 69:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label17Sat.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label17Sat.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label17Sat.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label17Sat.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label17Sat.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label17Sat.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel17Sat.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel17Sat.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel17Sat.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel17Sat.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel17Sat.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel17Sat.BackColor = Color.White;
                            ToolTipsText.Sat17 = "";
                            toolTipSat17.Hide(panel17Sat);
                            label17Sat.Hide();
                        }
                        break;
                    case 70:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label17Sun.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label17Sun.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label17Sun.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label17Sun.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label17Sun.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label17Sun.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel17Sun.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel17Sun.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel17Sun.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel17Sun.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel17Sun.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel17Sun.BackColor = Color.White;
                            ToolTipsText.Sun17 = "";
                            toolTipSun17.Hide(panel17Sun);
                            label17Sun.Hide();
                        }
                        break;
                    case 71:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label18Mon.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label18Mon.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label18Mon.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label18Mon.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label18Mon.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label18Mon.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel18Mon.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel18Mon.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel18Mon.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel18Mon.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel18Mon.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel18Mon.BackColor = Color.White;
                            ToolTipsText.Mon18 = "";
                            toolTipMon18.Hide(panel18Mon);
                            label18Mon.Hide();
                        }
                        break;
                    case 72:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label18Tue.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label18Tue.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label18Tue.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label18Tue.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label18Tue.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label18Tue.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel18Tue.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel18Tue.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel18Tue.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel18Tue.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel18Tue.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel18Tue.BackColor = Color.White;
                            ToolTipsText.Tue18 = "";
                            toolTipTue18.Hide(panel18Tue);
                            label18Tue.Hide();
                        }
                        break;
                    case 73:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label18Wed.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label18Wed.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label18Wed.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label18Wed.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label18Wed.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label18Wed.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel18Wed.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel18Wed.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel18Wed.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel18Wed.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel18Wed.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel18Wed.BackColor = Color.White;
                            ToolTipsText.Wed18 = "";
                            toolTipWed18.Hide(panel18Wed);
                            label18Wed.Hide();
                        }
                        break;
                    case 74:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label18Thu.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label18Thu.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label18Thu.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label18Thu.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label18Thu.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label18Thu.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel18Thu.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel18Thu.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel18Thu.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel18Thu.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel18Thu.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel18Thu.BackColor = Color.White;
                            ToolTipsText.Thu18 = "";
                            toolTipThu18.Hide(panel18Thu);
                            label18Thu.Hide();
                        }
                        break;
                    case 75:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label18Fri.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label18Fri.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label18Fri.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label18Fri.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label18Fri.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label18Fri.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel18Fri.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel18Fri.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel18Fri.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel18Fri.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel18Fri.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel18Fri.BackColor = Color.White;
                            ToolTipsText.Fri18 = "";
                            toolTipFri18.Hide(panel18Fri);
                            label18Fri.Hide();
                        }
                        break;
                    case 76:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label18Sat.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label18Sat.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label18Sat.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label18Sat.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label18Sat.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label18Sat.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel18Sat.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel18Sat.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel18Sat.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel18Sat.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel18Sat.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel18Sat.BackColor = Color.White;
                            ToolTipsText.Sat18 = "";
                            toolTipSat18.Hide(panel18Sat);
                            label18Sat.Hide();
                        }
                        break;
                    case 77:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label18Sun.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label18Sun.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label18Sun.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label18Sun.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label18Sun.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label18Sun.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel18Sun.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel18Sun.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel18Sun.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel18Sun.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel18Sun.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel18Sun.BackColor = Color.White;
                            ToolTipsText.Sun18 = "";
                            toolTipSun18.Hide(panel18Sun);
                            label18Sun.Hide();
                        }
                        break;
                    case 78:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label19Mon.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label19Mon.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label19Mon.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label19Mon.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label19Mon.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label19Mon.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel19Mon.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel19Mon.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel19Mon.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel19Mon.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel19Mon.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel19Mon.BackColor = Color.White;
                            ToolTipsText.Mon19 = "";
                            toolTipMon19.Hide(panel19Mon);
                            label19Mon.Hide();
                        }
                        break;
                    case 79:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label19Tue.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label19Tue.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label19Tue.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label19Tue.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label19Tue.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label19Tue.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel19Tue.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel19Tue.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel19Tue.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel19Tue.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel19Tue.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel19Tue.BackColor = Color.White;
                            ToolTipsText.Tue19 = "";
                            toolTipTue19.Hide(panel19Tue);
                            label19Tue.Hide();
                        }
                        break;
                    case 80:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label19Wed.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label19Wed.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label19Wed.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label19Wed.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label19Wed.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label19Wed.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel19Wed.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel19Wed.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel19Wed.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel19Wed.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel19Wed.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel19Wed.BackColor = Color.White;
                            ToolTipsText.Wed19 = "";
                            toolTipWed19.Hide(panel19Wed);
                            label19Wed.Hide();
                        }
                        break;
                    case 81:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label19Thu.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label19Thu.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label19Thu.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label19Thu.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label19Thu.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label19Thu.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel19Thu.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel19Thu.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel19Thu.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel19Thu.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel19Thu.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel19Thu.BackColor = Color.White;
                            ToolTipsText.Thu19 = "";
                            toolTipThu19.Hide(panel19Thu);
                            label19Thu.Hide();
                        }
                        break;
                    case 82:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label19Fri.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label19Fri.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label19Fri.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label19Fri.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label19Fri.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label19Fri.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel19Fri.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel19Fri.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel19Fri.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel19Fri.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel19Fri.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel19Fri.BackColor = Color.White;
                            ToolTipsText.Fri19 = "";
                            toolTipFri19.Hide(panel19Fri);
                            label19Fri.Hide();
                        }
                        break;
                    case 83:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label19Sat.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label19Sat.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label19Sat.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label19Sat.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label19Sat.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label19Sat.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel19Sat.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel19Sat.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel19Sat.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel19Sat.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel19Sat.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel19Sat.BackColor = Color.White;
                            ToolTipsText.Sat19 = "";
                            toolTipSat19.Hide(panel19Sat);
                            label19Sat.Hide();
                        }
                        break;
                    case 84:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label19Sun.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label19Sun.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label19Sun.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label19Sun.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label19Sun.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label19Sun.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel19Sun.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel19Sun.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel19Sun.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel19Sun.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel19Sun.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel19Sun.BackColor = Color.White;
                            ToolTipsText.Sun19 = "";
                            toolTipSun19.Hide(panel19Sun);
                            label19Sun.Hide();
                        }
                        break;
                    case 85:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label20Mon.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label20Mon.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label20Mon.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label20Mon.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label20Mon.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label20Mon.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel20Mon.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel20Mon.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel20Mon.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel20Mon.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel20Mon.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel20Mon.BackColor = Color.White;
                            ToolTipsText.Mon20 = "";
                            toolTipMon20.Hide(panel20Mon);
                            label20Mon.Hide();
                        }
                        break;
                    case 86:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label20Tue.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label20Tue.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label20Tue.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label20Tue.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label20Tue.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label20Tue.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel20Tue.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel20Tue.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel20Tue.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel20Tue.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel20Tue.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel20Tue.BackColor = Color.White;
                            ToolTipsText.Tue20 = "";
                            toolTipTue20.Hide(panel20Tue);
                            label20Tue.Hide();
                        }
                        break;
                    case 87:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label20Wed.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label20Wed.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label20Wed.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label20Wed.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label20Wed.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label20Wed.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel20Wed.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel20Wed.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel20Wed.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel20Wed.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel20Wed.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel20Wed.BackColor = Color.White;
                            ToolTipsText.Wed20 = "";
                            toolTipWed20.Hide(panel20Wed);
                            label20Wed.Hide();
                        }
                        break;
                    case 88:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label20Thu.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label20Thu.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label20Thu.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label20Thu.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label20Thu.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label20Thu.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel20Thu.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel20Thu.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel20Thu.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel20Thu.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel20Thu.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel20Thu.BackColor = Color.White;
                            ToolTipsText.Thu20 = "";
                            toolTipThu20.Hide(panel20Thu);
                            label20Thu.Hide();
                        }
                        break;
                    case 89:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label20Fri.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label20Fri.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label20Fri.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label20Fri.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label20Fri.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label20Fri.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel20Fri.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel20Fri.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel20Fri.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel20Fri.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel20Fri.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel20Fri.BackColor = Color.White;
                            ToolTipsText.Fri20 = "";
                            toolTipFri20.Hide(panel20Fri);
                            label20Fri.Hide();
                        }
                        break;
                    case 90:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label20Sat.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label20Sat.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label20Sat.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label20Sat.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label20Sat.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label20Sat.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel20Sat.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel20Sat.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel20Sat.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel20Sat.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel20Sat.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel20Sat.BackColor = Color.White;
                            ToolTipsText.Sat20 = "";
                            toolTipSat20.Hide(panel20Sat);
                            label20Sat.Hide();
                        }
                        break;
                    case 91:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label20Sun.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label20Sun.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label20Sun.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label20Sun.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label20Sun.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label20Sun.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel20Sun.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel20Sun.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel20Sun.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel20Sun.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel20Sun.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel20Sun.BackColor = Color.White;
                            ToolTipsText.Sun20 = "";
                            toolTipSun20.Hide(panel20Sun);
                            label20Sun.Hide();
                        }
                        break;
                    case 92:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label21Mon.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label21Mon.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label21Mon.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label21Mon.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label21Mon.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label21Mon.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel21Mon.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel21Mon.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel21Mon.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel21Mon.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel21Mon.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel21Mon.BackColor = Color.White;
                            ToolTipsText.Mon21 = "";
                            toolTipMon21.Hide(panel21Mon);
                            label21Mon.Hide();
                        }
                        break;
                    case 93:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label21Tue.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label21Tue.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label21Tue.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label21Tue.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label21Tue.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label21Tue.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel21Tue.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel21Tue.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel21Tue.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel21Tue.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel21Tue.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel21Tue.BackColor = Color.White;
                            ToolTipsText.Tue21 = "";
                            toolTipTue21.Hide(panel21Tue);
                            label21Tue.Hide();
                        }
                        break;
                    case 94:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label21Wed.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label21Wed.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label21Wed.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label21Wed.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label21Wed.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label21Wed.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel21Wed.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel21Wed.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel21Wed.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel21Wed.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel21Wed.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel21Wed.BackColor = Color.White;
                            ToolTipsText.Wed21 = "";
                            toolTipWed21.Hide(panel21Wed);
                            label21Wed.Hide();
                        }
                        break;
                    case 95:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label21Thu.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label21Thu.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label21Thu.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label21Thu.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label21Thu.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label21Thu.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel21Thu.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel21Thu.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel21Thu.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel21Thu.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel21Thu.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel21Thu.BackColor = Color.White;
                            ToolTipsText.Thu21 = "";
                            toolTipThu21.Hide(panel21Thu);
                            label21Thu.Hide();
                        }
                        break;
                    case 96:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label21Fri.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label21Fri.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label21Fri.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label21Fri.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label21Fri.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label21Fri.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel21Fri.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel21Fri.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel21Fri.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel21Fri.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel21Fri.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel21Fri.BackColor = Color.White;
                            ToolTipsText.Fri21 = "";
                            toolTipFri21.Hide(panel21Fri);
                            label21Fri.Hide();
                        }
                        break;
                    case 97:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label21Sat.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label21Sat.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label21Sat.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label21Sat.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label21Sat.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label21Sat.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel21Sat.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel21Sat.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel21Sat.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel21Sat.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel21Sat.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel21Sat.BackColor = Color.White;
                            ToolTipsText.Sat21 = "";
                            toolTipSat21.Hide(panel21Sat);
                            label21Sat.Hide();
                        }
                        break;
                    case 98:
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                pFrom = label21Sun.Text.IndexOf("za\n") + "za\n".Length;
                                if (pFrom > 2)
                                    patientName = label21Sun.Text.Substring(pFrom);
                                break;
                            case "de-DE":
                                pFrom = label21Sun.Text.IndexOf("für\n") + "für\n".Length;
                                if (pFrom > 3)
                                    patientName = label21Sun.Text.Substring(pFrom);
                                break;
                            default:
                                pFrom = label21Sun.Text.IndexOf("for\n") + "for\n".Length;
                                if (pFrom > 3)
                                    patientName = label21Sun.Text.Substring(pFrom);
                                break;
                        }
                        if ((checkBoxExamination.Checked == false && panel21Sun.BackColor == Color.AliceBlue) ||
                            (checkBoxLab.Checked == false && panel21Sun.BackColor == Color.GreenYellow) ||
                            (checkBoxOperation.Checked == false && panel21Sun.BackColor == Color.OrangeRed) ||
                            (checkBoxTherapy.Checked == false && panel21Sun.BackColor == Color.LightPink) ||
                            (checkBoxCheckUp.Checked == false && panel21Sun.BackColor == Color.PowderBlue) ||
                            (!patientName.Contains(textBoxFilterPatient.Text) && !String.IsNullOrEmpty(textBoxFilterPatient.Text)))
                        {
                            panel21Sun.BackColor = Color.White;
                            ToolTipsText.Sun21 = "";
                            toolTipSun21.Hide(panel21Sun);
                            label21Sun.Hide();
                        }
                        break;
                }//zatvaramo switch
                #endregion
            }
        }

        //mouse down za Examination
        private void buttonExamination_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && dragExam == true)
            {
                buttonExamination.DoDragDrop(buttonExamination, DragDropEffects.Move);
            }
        }

        //mouse down za Therapy
        private void buttonTherapy_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && dragTherapy == true)
            {
                buttonTherapy.DoDragDrop(buttonTherapy, DragDropEffects.Move);
            }
        }

        //mouse down za Laboratory
        private void buttonLaboratory_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && dragLab == true)
            {
                buttonLaboratory.DoDragDrop(buttonLaboratory, DragDropEffects.Move);
            }
        }

        //mouse down za Operation
        private void buttonOperation_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && dragOperation == true)
            {
                buttonOperation.DoDragDrop(buttonOperation, DragDropEffects.Move);
            }
        }

        //mouse down za CheckUp
        private void buttonCheckUp_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && dragCheckup == true)
            {
                buttonCheckUp.DoDragDrop(buttonCheckUp, DragDropEffects.Move);
            }
        }

        //mouse down za RemoveEvent
        private void buttonRemoveEvent_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                buttonRemoveEvent.DoDragDrop(buttonRemoveEvent, DragDropEffects.Move);
            }
        }

        //drag enter za panele u kalendaru
        private void panelReplica_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        //drag drop za panele u kalendaru
        private void panelReplica_DragDrop(object sender, DragEventArgs e)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentUICulture;

            Button b = (Button)e.Data.GetData(typeof(Button));
            Panel p = (Panel)sender;
            //da ne ostane uokvireno kad se dropuje na njega
            p.BorderStyle = BorderStyle.None;

            //proverava da li vec ima event tu
            if (p.BackColor != Color.White && !(b.Text == "Remove Event" || b.Text == "Ereignis entfernen" || b.Text == "Ukloni termin"))
            {
                switch (ci.Name)
                {                    
                    case "de-DE":
                        MessageBox.Show("Dieses Zeitfenster ist bereits von einer anderen Veranstaltung belegt. Bitte wählen Sie ein anderes Zeitfenster aus.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case "sr-Latn-CS":    
                        MessageBox.Show("Ovaj termin je već zauzet drugim događajem, molimo Vas izaberite drugi termin.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    default:
                        MessageBox.Show("This time slot is already taken by another event, please select another time slot.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
                return;
            }

            DataContainer.time = p.TabIndex / 7 + 8;
            DataContainer.day = (Day)(p.TabIndex % 7);
            if (DataContainer.day == 0)
            {
                DataContainer.day = Day.Sunday;
                DataContainer.time = p.TabIndex / 7 + 7;
            }


            //preuzmemo datum gde je drag-dropovan button
            String dateDroped = "";
            switch (DataContainer.day.ToString())
            {
                case "Monday":
                case "Ponedeljak":
                case "Montag":
                    dateDroped = this.LabelMondayText;
                    break;
                case "Tuesday":
                case "Utorak":
                case "Dienstag":
                    dateDroped = this.LabelTuesdayText;
                    break;
                case "Wednesday":
                case "Sreda":
                case "Mittwoch":
                    dateDroped = this.LabelWednesdayText;
                    break;
                case "Thursday":
                case "Četvrtak":
                case "Donnerstag":
                    dateDroped = this.LabelThursdayText;
                    break;
                case "Friday":
                case "Petak":
                case "Freitag":
                    dateDroped = this.LabelFridayText;
                    break;
                case "Saturday":
                case "Subota":
                case "Samstag":
                    dateDroped = this.LabelSaturdayText;
                    break;
                case "Sunday":
                case "Nedelja":
                case "Sonntag":
                    dateDroped = this.LabelSundayText;
                    break;
            }

            //na osnovu dragovane usluge preuzimamo sledecu akciju
            FormSablonPregled formSablonPregled;
            FormSablonLab formSablonLab;
            FormSablonTerapija formSablonTerapija;
            FormSablonOperacija formSablonOperacija;
            FormSablonKontrola formSablonKontrola;

            switch (b.Text)
            {
                case "Examination":
                    DataContainer.service = "Examination";
                    formSablonPregled = new FormSablonPregled(this);
                    formSablonPregled.Show();
                    break;
                case "Untersuchung":
                    DataContainer.service = "Untersuchung";
                    formSablonPregled = new FormSablonPregled(this);
                    formSablonPregled.Show();
                    break;
                case "Pregled":
                    DataContainer.service = "Pregled";
                    formSablonPregled = new FormSablonPregled(this);
                    formSablonPregled.Show();
                    break;

                case "Laboratory":
                    DataContainer.service = "Laboratory";
                    formSablonLab = new FormSablonLab(this);
                    formSablonLab.Show();
                    break;
                case "Labor":
                    DataContainer.service = "Labor";
                    formSablonLab = new FormSablonLab(this);
                    formSablonLab.Show();
                    break;
                case "Laboratorija":
                    DataContainer.service = "Laboratorija";
                    formSablonLab = new FormSablonLab(this);
                    formSablonLab.Show();
                    break;

                case "Therapy":
                    DataContainer.service = "Therapy";
                    formSablonTerapija = new FormSablonTerapija(this);
                    formSablonTerapija.Show();
                    break;
                case "Therapie":
                    DataContainer.service = "Therapie";
                    formSablonTerapija = new FormSablonTerapija(this);
                    formSablonTerapija.Show();
                    break;
                case "Terapija":
                    DataContainer.service = "Terapija";
                    formSablonTerapija = new FormSablonTerapija(this);
                    formSablonTerapija.Show();
                    break;

                case "Operation":
                    DataContainer.service = "Operation";
                    formSablonOperacija = new FormSablonOperacija(this);
                    formSablonOperacija.Show();
                    break;
                case "Operacija":
                    DataContainer.service = "Operacija";
                    formSablonOperacija = new FormSablonOperacija(this);
                    formSablonOperacija.Show();
                    break;

                case "Check-up":
                    DataContainer.service = "Check-up";
                    formSablonKontrola = new FormSablonKontrola(this);
                    formSablonKontrola.Show();
                    break;
                case "Kontroll":
                    DataContainer.service = "Kontroll";
                    formSablonKontrola = new FormSablonKontrola(this);
                    formSablonKontrola.Show();
                    break;
                case "Kontrola":
                    DataContainer.service = "Kontrola";
                    formSablonKontrola = new FormSablonKontrola(this);
                    formSablonKontrola.Show();
                    break;

                case "Remove Event":
                case "Ereignis entfernen":
                case "Ukloni termin":
                    //proveravamo da li je event reacurring, ako nema u bazi event sa tim vremenom i datumom -> u pitanju je reacuring event                   
                    String serviceToDelete = "";
                    switch (p.BackColor.ToString())
                    {
                        case "Color [AliceBlue]":
                            serviceToDelete = "Examination";
                            break;
                        case "Color [GreenYellow]":
                            serviceToDelete = "Laboratory";
                            break;
                        case "Color [LightPink]":
                            serviceToDelete = "Therapy";
                            break;
                        case "Color [OrangeRed]":
                            serviceToDelete = "Operation";
                            break;
                        case "Color [PowderBlue]":
                            serviceToDelete = "Check-up";
                            break;
                    }

                    GetAllEvents.Invoke(this, EventArgs.Empty);

                    foreach (ClinicEvent c in ClinicEventsList) {
                        if (c.Time == TimeSpan.FromHours(DataContainer.time).ToString("hh':'mm") && c.Doctor == ActiveDoctor.name && c.Service == serviceToDelete)
                        {
                            //pomocna cid je cultureinfo potreban za konvertovanje datuma
                            String cie = c.CultureInfo;
                            CultureInfo cid = new CultureInfo(cie);
                            DateTime datumEventa = DateTime.ParseExact(dateDroped.Substring(1, dateDroped.Length - 2), " d. MMMM yyyy. ", ci, DateTimeStyles.NoCurrentDateDefault);
                            String datumEventaUCid = datumEventa.ToString(" d. MMMM yyyy. ", cid);
                            dateDroped = "[" + datumEventaUCid + "]";
                            if(c.Date == dateDroped)
                            {
                                DeleteEventById.Invoke(this, new ClinicEventSub(c.Id));
                            }

                            this.updatePlaner(ci.Calendar);

                            if (c.Reacurring > 0)
                            {
                                for (int i = 1; i < c.Reacurring; i++)
                                {
                                    //ako ga nadje onda ga i brise
                                    if (DataContainer.week - i == Int32.Parse(c.Week))
                                    {
                                        DeleteEventById.Invoke(this, new ClinicEventSub(c.Id));
                                    }
                                }
                                this.updatePlaner(ci.Calendar);
                            }

                            //posle brisanja glavnog eventa ili reacurring eventova, imamo brisanje ostalih povezanih kontrola ako je jedna obrisana
                            DeleteEventDDP.Invoke(this, new ClinicEventSub(c.Description, c.Doctor, c.PatientId));
                            this.updatePlaner(ci.Calendar);
                        }
                    }
                    ClinicEventsList = new List<ClinicEvent>();
                    break;
            }
            
        }


		//drag over za panele u kalendaru
		private void panelReplica_DragOver(object sender, DragEventArgs e)
		{
			Panel p = (Panel)sender;
			p.BorderStyle = BorderStyle.FixedSingle;
		}

		//drag leave za panele u kalendaru
		private void panelReplica_DragLeave(object sender, EventArgs e)
		{
			Panel p = (Panel)sender;
			p.BorderStyle = BorderStyle.None;
		}

		#region //mouse hover za svaki panel posebno jer svako ima svoj tooltip


		private void panel8Mon_MouseHover(object sender, EventArgs e)
		{
			toolTipMon8.Show(ToolTipsText.Mon8, panel8Mon);
		}

		private void panel8Tue_MouseHover(object sender, EventArgs e)
		{
			toolTipTue8.Show(ToolTipsText.Tue8, panel8Tue);
		}

		private void panel8Wed_MouseHover(object sender, EventArgs e)
		{
			toolTipWed8.Show(ToolTipsText.Wed8, panel8Wed);
		}

		private void panel8Thu_MouseHover(object sender, EventArgs e)
		{
			toolTipThu8.Show(ToolTipsText.Thu8, panel8Thu);
		}

		private void panel8Fri_MouseHover(object sender, EventArgs e)
		{
			toolTipFri8.Show(ToolTipsText.Fri8, panel8Fri);
		}

		private void panel8Sat_MouseHover(object sender, EventArgs e)
		{
			toolTipSat8.Show(ToolTipsText.Sat8, panel8Sat);
		}

		private void panel8Sun_MouseHover(object sender, EventArgs e)
		{
			toolTipSun8.Show(ToolTipsText.Sun8, panel8Sun);
		}

		private void panel9Mon_MouseHover(object sender, EventArgs e)
		{
			toolTipMon9.Show(ToolTipsText.Mon9, panel9Mon);
		}

		private void panel9Tue_MouseHover(object sender, EventArgs e)
		{
			toolTipTue9.Show(ToolTipsText.Tue9, panel9Tue);
		}

		private void panel9Wed_MouseHover(object sender, EventArgs e)
		{
			toolTipWed9.Show(ToolTipsText.Wed9, panel9Wed);
		}

		private void panel9Thu_MouseHover(object sender, EventArgs e)
		{
			toolTipThu9.Show(ToolTipsText.Thu9, panel9Thu);
		}

		private void panel9Fri_MouseHover(object sender, EventArgs e)
		{
			toolTipFri9.Show(ToolTipsText.Fri9, panel9Fri);
		}

		private void panel9Sat_MouseHover(object sender, EventArgs e)
		{
			toolTipSat9.Show(ToolTipsText.Sat9, panel9Sat);
		}

		private void panel9Sun_MouseHover(object sender, EventArgs e)
		{
			toolTipSun9.Show(ToolTipsText.Sun9, panel9Sun);
		}

		private void panel10Mon_MouseHover(object sender, EventArgs e)
		{
			toolTipMon10.Show(ToolTipsText.Mon10, panel10Mon);
		}

		private void panel10Tue_MouseHover(object sender, EventArgs e)
		{
			toolTipTue10.Show(ToolTipsText.Tue10, panel10Tue);
		}

		private void panel10Wed_MouseHover(object sender, EventArgs e)
		{
			toolTipWed10.Show(ToolTipsText.Wed10, panel10Wed);
		}

		private void panel10Thu_MouseHover(object sender, EventArgs e)
		{
			toolTipThu10.Show(ToolTipsText.Thu10, panel10Thu);
		}

		private void panel10Fri_MouseHover(object sender, EventArgs e)
		{
			toolTipFri10.Show(ToolTipsText.Fri10, panel10Fri);
		}

		private void panel10Sat_MouseHover(object sender, EventArgs e)
		{
			toolTipSat10.Show(ToolTipsText.Sat10, panel10Sat);
		}

		private void panel10Sun_MouseHover(object sender, EventArgs e)
		{
			toolTipSun10.Show(ToolTipsText.Sun10, panel10Sun);
		}

		private void panel11Mon_MouseHover(object sender, EventArgs e)
		{
			toolTipMon11.Show(ToolTipsText.Mon11, panel11Mon);
		}

		private void panel11Tue_MouseHover(object sender, EventArgs e)
		{
			toolTipTue11.Show(ToolTipsText.Tue11, panel11Tue);
		}

		private void panel11Wed_MouseHover(object sender, EventArgs e)
		{
			toolTipWed11.Show(ToolTipsText.Wed11, panel11Wed);
		}

		private void panel11Thu_MouseHover(object sender, EventArgs e)
		{
			toolTipThu11.Show(ToolTipsText.Thu11, panel11Thu);
		}

		private void panel11Fri_MouseHover(object sender, EventArgs e)
		{
			toolTipFri11.Show(ToolTipsText.Fri11, panel11Fri);
		}

		private void panel11Sat_MouseHover(object sender, EventArgs e)
		{
			toolTipSat11.Show(ToolTipsText.Sat11, panel11Sat);
		}

		private void panel11Sun_MouseHover(object sender, EventArgs e)
		{
			toolTipSun11.Show(ToolTipsText.Sun11, panel11Sun);
		}

		private void panel12Mon_MouseHover(object sender, EventArgs e)
		{
			toolTipMon12.Show(ToolTipsText.Mon12, panel12Mon);
		}

		private void panel12Tue_MouseHover(object sender, EventArgs e)
		{
			toolTipTue12.Show(ToolTipsText.Tue12, panel12Tue);
		}

		private void panel12Wed_MouseHover(object sender, EventArgs e)
		{
			toolTipWed12.Show(ToolTipsText.Wed12, panel12Wed);
		}

		private void panel12Thu_MouseHover(object sender, EventArgs e)
		{
			toolTipThu12.Show(ToolTipsText.Thu12, panel12Thu);
		}

		private void panel12Fri_MouseHover(object sender, EventArgs e)
		{
			toolTipFri12.Show(ToolTipsText.Fri12, panel12Fri);
		}

		private void panel12Sat_MouseHover(object sender, EventArgs e)
		{
			toolTipSat12.Show(ToolTipsText.Sat12, panel12Sat);
		}

		private void panel12Sun_MouseHover(object sender, EventArgs e)
		{
			toolTipSun12.Show(ToolTipsText.Sun12, panel12Sun);
		}

		private void panel13Mon_MouseHover(object sender, EventArgs e)
		{
			toolTipMon13.Show(ToolTipsText.Mon13, panel13Mon);
		}

		private void panel13Tue_MouseHover(object sender, EventArgs e)
		{
			toolTipTue13.Show(ToolTipsText.Tue13, panel13Tue);
		}

		private void panel13Wed_MouseHover(object sender, EventArgs e)
		{
			toolTipWed13.Show(ToolTipsText.Wed13, panel13Wed);
		}

		private void panel13Thu_MouseHover(object sender, EventArgs e)
		{
			toolTipThu13.Show(ToolTipsText.Thu13, panel13Thu);
		}

		private void panel13Fri_MouseHover(object sender, EventArgs e)
		{
			toolTipFri13.Show(ToolTipsText.Fri13, panel13Fri);
		}

		private void panel13Sat_MouseHover(object sender, EventArgs e)
		{
			toolTipSat13.Show(ToolTipsText.Sat13, panel13Sat);
		}

		private void panel13Sun_MouseHover(object sender, EventArgs e)
		{
			toolTipSun13.Show(ToolTipsText.Sun13, panel13Sun);
		}

		private void panel14Mon_MouseHover(object sender, EventArgs e)
		{
			toolTipMon14.Show(ToolTipsText.Mon14, panel14Mon);
		}

		private void panel14Tue_MouseHover(object sender, EventArgs e)
		{
			toolTipTue14.Show(ToolTipsText.Tue14, panel14Tue);
		}

		private void panel14Wed_MouseHover(object sender, EventArgs e)
		{
			toolTipWed14.Show(ToolTipsText.Wed14, panel14Wed);
		}

		private void panel14Thu_MouseHover(object sender, EventArgs e)
		{
			toolTipThu14.Show(ToolTipsText.Thu14, panel14Thu);
		}

		private void panel14Fri_MouseHover(object sender, EventArgs e)
		{
			toolTipFri14.Show(ToolTipsText.Fri14, panel14Fri);
		}

		private void panel14Sat_MouseHover(object sender, EventArgs e)
		{
			toolTipSat14.Show(ToolTipsText.Sat14, panel14Sat);
		}

		private void panel14Sun_MouseHover(object sender, EventArgs e)
		{
			toolTipSun14.Show(ToolTipsText.Sun14, panel14Sun);
		}

		private void panel15Mon_MouseHover(object sender, EventArgs e)
		{
			toolTipMon15.Show(ToolTipsText.Mon15, panel15Mon);
		}

		private void panel15Tue_MouseHover(object sender, EventArgs e)
		{
			toolTipTue15.Show(ToolTipsText.Tue15, panel15Tue);
		}

		private void panel15Wed_MouseHover(object sender, EventArgs e)
		{
			toolTipWed15.Show(ToolTipsText.Wed15, panel15Wed);
		}

		private void panel15Thu_MouseHover(object sender, EventArgs e)
		{
			toolTipThu15.Show(ToolTipsText.Thu15, panel15Thu);
		}

		private void panel15Fri_MouseHover(object sender, EventArgs e)
		{
			toolTipFri15.Show(ToolTipsText.Fri15, panel15Fri);
		}

		private void panel15Sat_MouseHover(object sender, EventArgs e)
		{
			toolTipSat15.Show(ToolTipsText.Sat15, panel15Sat);
		}

		private void panel15Sun_MouseHover(object sender, EventArgs e)
		{
			toolTipSun15.Show(ToolTipsText.Sun15, panel15Sun);
		}

		private void panel16Mon_MouseHover(object sender, EventArgs e)
		{
			toolTipMon16.Show(ToolTipsText.Mon16, panel16Mon);
		}

		private void panel16Tue_MouseHover(object sender, EventArgs e)
		{
			toolTipTue16.Show(ToolTipsText.Tue16, panel16Tue);
		}

		private void panel16Wed_MouseHover(object sender, EventArgs e)
		{
			toolTipWed16.Show(ToolTipsText.Wed16, panel16Wed);
		}

		private void panel16Thu_MouseHover(object sender, EventArgs e)
		{
			toolTipThu16.Show(ToolTipsText.Thu16, panel16Thu);
		}

		private void panel16Fri_MouseHover(object sender, EventArgs e)
		{
			toolTipFri16.Show(ToolTipsText.Fri16, panel16Fri);
		}

		private void panel16Sat_MouseHover(object sender, EventArgs e)
		{
			toolTipSat16.Show(ToolTipsText.Sat16, panel16Sat);
		}

		private void panel16Sun_MouseHover(object sender, EventArgs e)
		{
			toolTipSun16.Show(ToolTipsText.Sun16, panel16Sun);
		}

		private void panel17Mon_MouseHover(object sender, EventArgs e)
		{
			toolTipMon17.Show(ToolTipsText.Mon17, panel17Mon);
		}

		private void panel17Tue_MouseHover(object sender, EventArgs e)
		{
			toolTipTue17.Show(ToolTipsText.Tue17, panel17Tue);
		}

		private void panel17Wed_MouseHover(object sender, EventArgs e)
		{
			toolTipWed17.Show(ToolTipsText.Wed17, panel17Wed);
		}

		private void panel17Thu_MouseHover(object sender, EventArgs e)
		{
			toolTipThu17.Show(ToolTipsText.Thu17, panel17Thu);
		}

		private void panel17Fri_MouseHover(object sender, EventArgs e)
		{
			toolTipFri17.Show(ToolTipsText.Fri17, panel17Fri);
		}

		private void panel17Sat_MouseHover(object sender, EventArgs e)
		{
			toolTipSat17.Show(ToolTipsText.Sat17, panel17Sat);
		}

		private void panel17Sun_MouseHover(object sender, EventArgs e)
		{
			toolTipSun17.Show(ToolTipsText.Sun17, panel17Sun);
		}

		private void panel18Mon_MouseHover(object sender, EventArgs e)
		{
			toolTipMon18.Show(ToolTipsText.Mon18, panel18Mon);
		}

		private void panel18Tue_MouseHover(object sender, EventArgs e)
		{
			toolTipTue18.Show(ToolTipsText.Tue18, panel18Tue);
		}

		private void panel18Wed_MouseHover(object sender, EventArgs e)
		{
			toolTipWed18.Show(ToolTipsText.Wed18, panel18Wed);
		}

		private void panel18Thu_MouseHover(object sender, EventArgs e)
		{
			toolTipThu18.Show(ToolTipsText.Thu18, panel18Thu);
		}

		private void panel18Fri_MouseHover(object sender, EventArgs e)
		{
			toolTipFri18.Show(ToolTipsText.Fri18, panel18Fri);
		}

		private void panel18Sat_MouseHover(object sender, EventArgs e)
		{
			toolTipSat18.Show(ToolTipsText.Sat18, panel18Sat);
		}

		private void panel18Sun_MouseHover(object sender, EventArgs e)
		{
			toolTipSun18.Show(ToolTipsText.Sun18, panel18Sun);
		}

		private void panel19Mon_MouseHover(object sender, EventArgs e)
		{
			toolTipMon19.Show(ToolTipsText.Mon19, panel19Mon);
		}

		private void panel19Tue_MouseHover(object sender, EventArgs e)
		{
			toolTipTue19.Show(ToolTipsText.Tue19, panel19Tue);
		}

		private void panel19Wed_MouseHover(object sender, EventArgs e)
		{
			toolTipWed19.Show(ToolTipsText.Wed19, panel19Wed);
		}

		private void panel19Thu_MouseHover(object sender, EventArgs e)
		{
			toolTipThu19.Show(ToolTipsText.Thu19, panel19Thu);
		}

		private void panel19Fri_MouseHover(object sender, EventArgs e)
		{
			toolTipFri19.Show(ToolTipsText.Fri19, panel19Fri);
		}

		private void panel19Sat_MouseHover(object sender, EventArgs e)
		{
			toolTipSat19.Show(ToolTipsText.Sat19, panel19Sat);
		}

		private void panel19Sun_MouseHover(object sender, EventArgs e)
		{
			toolTipSun19.Show(ToolTipsText.Sun19, panel19Sun);
		}

		private void panel20Mon_MouseHover(object sender, EventArgs e)
		{
			toolTipMon20.Show(ToolTipsText.Mon20, panel20Mon);
		}

		private void panel20Tue_MouseHover(object sender, EventArgs e)
		{
			toolTipTue20.Show(ToolTipsText.Tue20, panel20Tue);
		}

		private void panel20Wed_MouseHover(object sender, EventArgs e)
		{
			toolTipWed20.Show(ToolTipsText.Wed20, panel20Wed);
		}

		private void panel20Thu_MouseHover(object sender, EventArgs e)
		{
			toolTipThu20.Show(ToolTipsText.Thu20, panel20Thu);
		}

		private void panel20Fri_MouseHover(object sender, EventArgs e)
		{
			toolTipFri20.Show(ToolTipsText.Fri20, panel20Fri);
		}

		private void panel20Sat_MouseHover(object sender, EventArgs e)
		{
			toolTipSat20.Show(ToolTipsText.Sat20, panel20Sat);
		}

		private void panel20Sun_MouseHover(object sender, EventArgs e)
		{
			toolTipSun20.Show(ToolTipsText.Sun20, panel20Sun);
		}

		private void panel21Mon_MouseHover(object sender, EventArgs e)
		{
			toolTipMon21.Show(ToolTipsText.Mon21, panel21Mon);
		}

		private void panel21Tue_MouseHover(object sender, EventArgs e)
		{
			toolTipTue21.Show(ToolTipsText.Tue21, panel21Tue);
		}

		private void panel21Wed_MouseHover(object sender, EventArgs e)
		{
			toolTipWed21.Show(ToolTipsText.Wed21, panel21Wed);
		}

		private void panel21Thu_MouseHover(object sender, EventArgs e)
		{
			toolTipThu21.Show(ToolTipsText.Thu21, panel21Thu);
		}

		private void panel21Fri_MouseHover(object sender, EventArgs e)
		{
			toolTipFri21.Show(ToolTipsText.Fri21, panel21Fri);
		}

		private void panel21Sat_MouseHover(object sender, EventArgs e)
		{
			toolTipSat21.Show(ToolTipsText.Sat21, panel21Sat);
		}

		private void panel21Sun_MouseHover(object sender, EventArgs e)
		{
			toolTipSun21.Show(ToolTipsText.Sun21, panel21Sun);
		}
		#endregion

		//funkcija za apdejtovanje panela u planeru
		public void updatePlaner(Calendar cal)
		{
            CultureInfo cis = Thread.CurrentThread.CurrentUICulture;

            //ocisti stare eventove iz planera
            #region resetBoje
            //ponedeljak
            panel8Mon.BackColor = Color.White;
			ToolTipsText.Mon8 = "";
            label8Mon.Text = "";
            panel9Mon.BackColor = Color.White;
			ToolTipsText.Mon9 = "";
            label9Mon.Text = "";
            panel10Mon.BackColor = Color.White;
			ToolTipsText.Mon10 = "";
            label10Mon.Text = "";
            panel11Mon.BackColor = Color.White;
			ToolTipsText.Mon11 = "";
            label11Mon.Text = "";
            panel12Mon.BackColor = Color.White;
			ToolTipsText.Mon12 = "";
            label12Mon.Text = "";
            panel13Mon.BackColor = Color.White;
			ToolTipsText.Mon13 = "";
            label13Mon.Text = "";
            panel14Mon.BackColor = Color.White;
			ToolTipsText.Mon14 = "";
            label14Mon.Text = "";
            panel15Mon.BackColor = Color.White;
			ToolTipsText.Mon15 = "";
            label15Mon.Text = "";
            panel16Mon.BackColor = Color.White;
			ToolTipsText.Mon16 = "";
            label16Mon.Text = "";
            panel17Mon.BackColor = Color.White;
			ToolTipsText.Mon17 = "";
            label17Mon.Text = "";
            panel18Mon.BackColor = Color.White;
			ToolTipsText.Mon18 = "";
            label18Mon.Text = "";
            panel19Mon.BackColor = Color.White;
			ToolTipsText.Mon19 = "";
            label19Mon.Text = "";
            panel20Mon.BackColor = Color.White;
			ToolTipsText.Mon20 = "";
            label20Mon.Text = "";
            panel21Mon.BackColor = Color.White;
			ToolTipsText.Mon21 = "";
            label21Mon.Text = "";
            //utorak
            panel8Tue.BackColor = Color.White;
			ToolTipsText.Tue8 = "";
            label8Tue.Text = "";
            panel9Tue.BackColor = Color.White;
			ToolTipsText.Tue9 = "";
            label9Tue.Text = "";
            panel10Tue.BackColor = Color.White;
			ToolTipsText.Tue10 = "";
            label10Tue.Text = "";
            panel11Tue.BackColor = Color.White;
			ToolTipsText.Tue11 = "";
            label11Tue.Text = "";
            panel12Tue.BackColor = Color.White;
			ToolTipsText.Tue12 = "";
            label12Tue.Text = "";
            panel13Tue.BackColor = Color.White;
			ToolTipsText.Tue13 = "";
            label13Tue.Text = "";
            panel14Tue.BackColor = Color.White;
			ToolTipsText.Tue14 = "";
            label14Tue.Text = "";
            panel15Tue.BackColor = Color.White;
			ToolTipsText.Tue15 = "";
            label15Tue.Text = "";
            panel16Tue.BackColor = Color.White;
			ToolTipsText.Tue16 = "";
            label16Tue.Text = "";
            panel17Tue.BackColor = Color.White;
			ToolTipsText.Tue17 = "";
            label17Tue.Text = "";
            panel18Tue.BackColor = Color.White;
			ToolTipsText.Tue18 = "";
            label18Tue.Text = "";
            panel19Tue.BackColor = Color.White;
			ToolTipsText.Tue19 = "";
            label19Tue.Text = "";
            panel20Tue.BackColor = Color.White;
			ToolTipsText.Tue20 = "";
            label20Tue.Text = "";
            panel21Tue.BackColor = Color.White;
			ToolTipsText.Tue21 = "";
            label21Tue.Text = "";
            //sreda
            panel8Wed.BackColor = Color.White;
			ToolTipsText.Wed8 = "";
            label8Wed.Text = "";
            panel9Wed.BackColor = Color.White;
			ToolTipsText.Wed9 = "";
            label9Wed.Text = "";
            panel10Wed.BackColor = Color.White;
			ToolTipsText.Wed10 = "";
            label10Wed.Text = "";
            panel11Wed.BackColor = Color.White;
			ToolTipsText.Wed11 = "";
            label11Wed.Text = "";
            panel12Wed.BackColor = Color.White;
			ToolTipsText.Wed12 = "";
            label12Wed.Text = "";
            panel13Wed.BackColor = Color.White;
			ToolTipsText.Wed13 = "";
            label13Wed.Text = "";
            panel14Wed.BackColor = Color.White;
			ToolTipsText.Wed14 = "";
            label14Wed.Text = "";
            panel15Wed.BackColor = Color.White;
			ToolTipsText.Wed15 = "";
            label15Wed.Text = "";
            panel16Wed.BackColor = Color.White;
			ToolTipsText.Wed16 = "";
            label16Wed.Text = "";
            panel17Wed.BackColor = Color.White;
			ToolTipsText.Wed17 = "";
            label17Wed.Text = "";
            panel18Wed.BackColor = Color.White;
			ToolTipsText.Wed18 = "";
            label18Wed.Text = "";
            panel19Wed.BackColor = Color.White;
			ToolTipsText.Wed19 = "";
            label19Wed.Text = "";
            panel20Wed.BackColor = Color.White;
			ToolTipsText.Wed20 = "";
            label20Wed.Text = "";
            panel21Wed.BackColor = Color.White;
			ToolTipsText.Wed21 = "";
            label21Wed.Text = "";
            //cetvrtak
            panel8Thu.BackColor = Color.White;
			ToolTipsText.Thu8 = "";
            label8Thu.Text = "";
            panel9Thu.BackColor = Color.White;
			ToolTipsText.Thu9 = "";
            label9Thu.Text = "";
            panel10Thu.BackColor = Color.White;
			ToolTipsText.Thu10 = "";
            label10Thu.Text = "";
            panel11Thu.BackColor = Color.White;
			ToolTipsText.Thu11 = "";
            label11Thu.Text = "";
            panel12Thu.BackColor = Color.White;
			ToolTipsText.Thu12 = "";
            label12Thu.Text = "";
            panel13Thu.BackColor = Color.White;
			ToolTipsText.Thu13 = "";
            label13Thu.Text = "";
            panel14Thu.BackColor = Color.White;
			ToolTipsText.Thu14 = "";
            label14Thu.Text = "";
            panel15Thu.BackColor = Color.White;
			ToolTipsText.Thu15 = "";
            label15Thu.Text = "";
            panel16Thu.BackColor = Color.White;
			ToolTipsText.Thu16 = "";
            label16Thu.Text = "";
            panel17Thu.BackColor = Color.White;
			ToolTipsText.Thu17 = "";
            label17Thu.Text = "";
            panel18Thu.BackColor = Color.White;
			ToolTipsText.Thu18 = "";
            label18Thu.Text = "";
            panel19Thu.BackColor = Color.White;
			ToolTipsText.Thu19 = "";
            label19Thu.Text = "";
            panel20Thu.BackColor = Color.White;
			ToolTipsText.Thu20 = "";
            label20Thu.Text = "";
            panel21Thu.BackColor = Color.White;
			ToolTipsText.Thu21 = "";
            label21Thu.Text = "";
            //petak
            panel8Fri.BackColor = Color.White;
			ToolTipsText.Fri8 = "";
            label8Fri.Text = "";
            panel9Fri.BackColor = Color.White;
			ToolTipsText.Fri9 = "";
            label9Fri.Text = "";
            panel10Fri.BackColor = Color.White;
			ToolTipsText.Fri10 = "";
            label10Fri.Text = "";
            panel11Fri.BackColor = Color.White;
			ToolTipsText.Fri11 = "";
            label11Fri.Text = "";
            panel12Fri.BackColor = Color.White;
			ToolTipsText.Fri12 = "";
            label12Fri.Text = "";
            panel13Fri.BackColor = Color.White;
			ToolTipsText.Fri13 = "";
            label13Fri.Text = "";
            panel14Fri.BackColor = Color.White;
			ToolTipsText.Fri14 = "";
            label14Fri.Text = "";
            panel15Fri.BackColor = Color.White;
			ToolTipsText.Fri15 = "";
            label15Fri.Text = "";
            panel16Fri.BackColor = Color.White;
			ToolTipsText.Fri16 = "";
            label16Fri.Text = "";
            panel17Fri.BackColor = Color.White;
			ToolTipsText.Fri17 = "";
            label17Fri.Text = "";
            panel18Fri.BackColor = Color.White;
			ToolTipsText.Fri18 = "";
            label18Fri.Text = "";
            panel19Fri.BackColor = Color.White;
			ToolTipsText.Fri19 = "";
            label19Fri.Text = "";
            panel20Fri.BackColor = Color.White;
			ToolTipsText.Fri20 = "";
            label20Fri.Text = "";
            panel21Fri.BackColor = Color.White;
			ToolTipsText.Fri21 = "";
            label21Fri.Text = "";
            //subota
            panel8Sat.BackColor = Color.White;
			ToolTipsText.Sat8 = "";
            label8Sat.Text = "";
            panel9Sat.BackColor = Color.White;
			ToolTipsText.Sat9 = "";
            label9Sat.Text = "";
            panel10Sat.BackColor = Color.White;
			ToolTipsText.Sat10 = "";
            label10Sat.Text = "";
            panel11Sat.BackColor = Color.White;
			ToolTipsText.Sat11 = "";
            label11Sat.Text = "";
            panel12Sat.BackColor = Color.White;
			ToolTipsText.Sat12 = "";
            label12Sat.Text = "";
            panel13Sat.BackColor = Color.White;
			ToolTipsText.Sat13 = "";
            label13Sat.Text = "";
            panel14Sat.BackColor = Color.White;
			ToolTipsText.Sat14 = "";
            label14Sat.Text = "";
            panel15Sat.BackColor = Color.White;
			ToolTipsText.Sat15 = "";
            label15Sat.Text = "";
            panel16Sat.BackColor = Color.White;
			ToolTipsText.Sat16 = "";
            label16Sat.Text = "";
            panel17Sat.BackColor = Color.White;
			ToolTipsText.Sat17 = "";
            label17Sat.Text = "";
            panel18Sat.BackColor = Color.White;
			ToolTipsText.Sat18 = "";
            label18Sat.Text = "";
            panel19Sat.BackColor = Color.White;
			ToolTipsText.Sat19 = "";
            label19Sat.Text = "";
            panel20Sat.BackColor = Color.White;
			ToolTipsText.Sat20 = "";
            label20Sat.Text = "";
            panel21Sat.BackColor = Color.White;
			ToolTipsText.Sat21 = "";
            label21Sat.Text = "";
            //nedelja
            panel8Sun.BackColor = Color.White;
			ToolTipsText.Sun8 = "";
            label8Sun.Text = "";
            panel9Sun.BackColor = Color.White;
			ToolTipsText.Sun9 = "";
            label9Sun.Text = "";
            panel10Sun.BackColor = Color.White;
			ToolTipsText.Sun10 = "";
            label10Sun.Text = "";
            panel11Sun.BackColor = Color.White;
			ToolTipsText.Sun11 = "";
            label11Sun.Text = "";
            panel12Sun.BackColor = Color.White;
			ToolTipsText.Sun12 = "";
            label12Sun.Text = "";
            panel13Sun.BackColor = Color.White;
			ToolTipsText.Sun13 = "";
            label13Sun.Text = "";
            panel14Sun.BackColor = Color.White;
			ToolTipsText.Sun14 = "";
            label14Sun.Text = "";
            panel15Sun.BackColor = Color.White;
			ToolTipsText.Sun15 = "";
            label15Sun.Text = "";
            panel16Sun.BackColor = Color.White;
			ToolTipsText.Sun16 = "";
            label16Sun.Text = "";
            panel17Sun.BackColor = Color.White;
			ToolTipsText.Sun17 = "";
            label17Sun.Text = "";
            panel18Sun.BackColor = Color.White;
			ToolTipsText.Sun18 = "";
            label18Sun.Text = "";
            panel19Sun.BackColor = Color.White;
			ToolTipsText.Sun19 = "";
            label19Sun.Text = "";
            panel20Sun.BackColor = Color.White;
			ToolTipsText.Sun20 = "";
            label20Sun.Text = "";
            panel21Sun.BackColor = Color.White;
			ToolTipsText.Sun21 = "";
            label21Sun.Text = "";
            #endregion

            //proveri i dodaj nove eventove u planer na osnovu datuma
			Color serviceClr = Color.White;
            String translatedService = "";

            #region Ne-reacurring eventovi

            GetAllEvents.Invoke(this, EventArgs.Empty);

            foreach (ClinicEvent c in ClinicEventsList)
            {
                if (c.Week == DataContainer.week.ToString() && c.Doctor == ActiveDoctor.name)
                {
                    switch (c.Service)
                    {
                        case "Examination":
                        //case "Untersuchung":
                        //case "Pregled":
                            serviceClr = Color.AliceBlue;

                            if (cis.Name == "de-DE")
                                translatedService = "Untersuchung";
                            if (cis.Name == "sr-Latn-CS")
                                translatedService = "Pregled";

                            break;

                        case "Laboratory":
                       // case "Labor":
                       // case "Laboratorija":
                            serviceClr = Color.GreenYellow;

                            if (cis.Name == "de-DE")
                                translatedService = "Labor";
                            if (cis.Name == "sr-Latn-CS")
                                translatedService = "Laboratorija";

                            break;
                        case "Therapy":
                       // case "Therapie":
                      //  case "Terapija":
                            serviceClr = Color.LightPink;

                            if (cis.Name == "de-DE")
                                translatedService = "Therapie";
                            if (cis.Name == "sr-Latn-CS")
                                translatedService = "Terapija";

                            break;
                        case "Operation":
                        //case "Operacija":
                            serviceClr = Color.OrangeRed;

                            if (cis.Name == "de-DE")
                                translatedService = "Operation";
                            if (cis.Name == "sr-Latn-CS")
                                translatedService = "Operacija";

                            break;
                        case "Check-up":
                        //case "Kontroll":
                       // case "Kontrola":
                            serviceClr = Color.PowderBlue;

                            if (cis.Name == "de-DE")
                                translatedService = "Kontroll";
                            if (cis.Name == "sr-Latn-CS")
                                translatedService = "Kontrola";

                            break;
                    }

                    String stringDate = c.Date;
                    CultureInfo ci = Thread.CurrentThread.CurrentUICulture;
                    //cid je CultureInfo koji je vazio kada je event bio kreiran
                    CultureInfo cid = new CultureInfo(c.CultureInfo);

                    //DateTime datumEventa = DateTime.Parse(stringDate.Substring(1, stringDate.Length - 2), cid, DateTimeStyles.NoCurrentDateDefault);
                    DateTime datumEventa = DateTime.ParseExact(stringDate.Substring(1, stringDate.Length - 2), " d. MMMM yyyy. ", cid, DateTimeStyles.NoCurrentDateDefault);

                    String datumEventaUCid = datumEventa.ToString(" d. MMMM yyyy. ", ci);
                    datumEventaUCid = "[" + datumEventaUCid + "]";

                    #region Tooltips i panel tekst

                    //ponedeljak                                           //ovaj deo treba da izmenjamo
                    if (c.Time == labelTime8.Text && datumEventaUCid == labelMondayDate.Text)
                    {
                        panel8Mon.BackColor = serviceClr;                       
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Mon8 = "Opis: " + c.Description;
                                label8Mon.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Mon8 = "Beschreibung: " + c.Description;
                                label8Mon.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Mon8 = "Description: " + c.Description;
                                label8Mon.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label8Mon.Show();
                    }
                    if (c.Time == labelTime9.Text && datumEventaUCid == labelMondayDate.Text)
                    {
                        panel9Mon.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Mon9 = "Opis: " + c.Description;
                                label9Mon.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Mon9 = "Beschreibung: " + c.Description;
                                label9Mon.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Mon9 = "Description: " + c.Description;
                                label9Mon.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label9Mon.Show();
                    }
                    if (c.Time == labelTime10.Text && datumEventaUCid == labelMondayDate.Text)
                    {
                        panel10Mon.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Mon10 = "Opis: " + c.Description;
                                label10Mon.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Mon10 = "Beschreibung: " + c.Description;
                                label10Mon.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Mon10 = "Description: " + c.Description;
                                label10Mon.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label10Mon.Show();
                    }
                    if (c.Time == labelTime11.Text && datumEventaUCid == labelMondayDate.Text)
                    {
                        panel11Mon.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Mon11 = "Opis: " + c.Description;
                                label11Mon.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Mon11 = "Beschreibung: " + c.Description;
                                label11Mon.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Mon11 = "Description: " + c.Description;
                                label11Mon.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label11Mon.Show();
                    }
                    if (c.Time == labelTime12.Text && datumEventaUCid == labelMondayDate.Text)
                    {
                        panel12Mon.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Mon12 = "Opis: " + c.Description;
                                label12Mon.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Mon12 = "Beschreibung: " + c.Description;
                                label12Mon.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Mon12 = "Description: " + c.Description;
                                label12Mon.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label12Mon.Show();
                    }
                    if (c.Time == labelTime13.Text && datumEventaUCid == labelMondayDate.Text)
                    {
                        panel13Mon.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Mon13 = "Opis: " + c.Description;
                                label13Mon.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Mon13 = "Beschreibung: " + c.Description;
                                label13Mon.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Mon13 = "Description: " + c.Description;
                                label13Mon.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label13Mon.Show();
                    }
                    if (c.Time == labelTime14.Text && datumEventaUCid == labelMondayDate.Text)
                    {
                        panel14Mon.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Mon14 = "Opis: " + c.Description;
                                label14Mon.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Mon14 = "Beschreibung: " + c.Description;
                                label14Mon.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Mon14 = "Description: " + c.Description;
                                label14Mon.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label14Mon.Show();
                    }
                    if (c.Time == labelTime15.Text && datumEventaUCid == labelMondayDate.Text)
                    {
                        panel15Mon.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Mon15 = "Opis: " + c.Description;
                                label15Mon.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Mon15 = "Beschreibung: " + c.Description;
                                label15Mon.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Mon15 = "Description: " + c.Description;
                                label15Mon.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label15Mon.Show();
                    }
                    if (c.Time == labelTime16.Text && datumEventaUCid == labelMondayDate.Text)
                    {
                        panel16Mon.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Mon16 = "Opis: " + c.Description;
                                label16Mon.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Mon16 = "Beschreibung: " + c.Description;
                                label16Mon.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Mon16 = "Description: " + c.Description;
                                label16Mon.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label16Mon.Show();
                    }
                    if (c.Time == labelTime17.Text && datumEventaUCid == labelMondayDate.Text)
                    {
                        panel17Mon.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Mon17 = "Opis: " + c.Description;
                                label17Mon.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Mon17 = "Beschreibung: " + c.Description;
                                label17Mon.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Mon17 = "Description: " + c.Description;
                                label17Mon.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label17Mon.Show();
                    }
                    if (c.Time == labelTime18.Text && datumEventaUCid == labelMondayDate.Text)
                    {
                        panel18Mon.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Mon18 = "Opis: " + c.Description;
                                label18Mon.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Mon18 = "Beschreibung: " + c.Description;
                                label18Mon.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Mon18 = "Description: " + c.Description;
                                label18Mon.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label18Mon.Show();
                    }
                    if (c.Time == labelTime19.Text && datumEventaUCid == labelMondayDate.Text)
                    {
                        panel19Mon.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Mon19 = "Opis: " + c.Description;
                                label19Mon.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Mon19 = "Beschreibung: " + c.Description;
                                label19Mon.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Mon19 = "Description: " + c.Description;
                                label19Mon.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label19Mon.Show();
                    }
                    if (c.Time == labelTime20.Text && datumEventaUCid == labelMondayDate.Text)
                    {
                        panel20Mon.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Mon20 = "Opis: " + c.Description;
                                label20Mon.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Mon20 = "Beschreibung: " + c.Description;
                                label20Mon.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Mon20 = "Description: " + c.Description;
                                label20Mon.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label20Mon.Show();
                    }
                    if (c.Time == labelTime21.Text && datumEventaUCid == labelMondayDate.Text)
                    {
                        panel21Mon.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Mon21 = "Opis: " + c.Description;
                                label21Mon.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Mon21 = "Beschreibung: " + c.Description;
                                label21Mon.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Mon21 = "Description: " + c.Description;
                                label21Mon.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label21Mon.Show();
                    }
                    //utorak
                    if (c.Time == labelTime8.Text && datumEventaUCid == labelTuesdayDate.Text)
                    {
                        panel8Tue.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Tue8 = "Opis: " + c.Description;
                                label8Tue.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Tue8 = "Beschreibung: " + c.Description;
                                label8Tue.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Tue8 = "Description: " + c.Description;
                                label8Tue.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label8Tue.Show();
                    }
                    if (c.Time == labelTime9.Text && datumEventaUCid == labelTuesdayDate.Text)
                    {
                        panel9Tue.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Tue9 = "Opis: " + c.Description;
                                label9Tue.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Tue9 = "Beschreibung: " + c.Description;
                                label9Tue.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Tue9 = "Description: " + c.Description;
                                label9Tue.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label9Tue.Show();
                    }
                    if (c.Time == labelTime10.Text && datumEventaUCid == labelTuesdayDate.Text)
                    {
                        panel10Tue.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Tue10 = "Opis: " + c.Description;
                                label10Tue.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Tue10 = "Beschreibung: " + c.Description;
                                label10Tue.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Tue10 = "Description: " + c.Description;
                                label10Tue.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label10Tue.Show();
                    }
                    if (c.Time == labelTime11.Text && datumEventaUCid == labelTuesdayDate.Text)
                    {
                        panel11Tue.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Tue11 = "Opis: " + c.Description;
                                label11Tue.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Tue11 = "Beschreibung: " + c.Description;
                                label11Tue.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Tue11 = "Description: " + c.Description;
                                label11Tue.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label11Tue.Show();
                    }
                    if (c.Time == labelTime12.Text && datumEventaUCid == labelTuesdayDate.Text)
                    {
                        panel12Tue.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Tue12 = "Opis: " + c.Description;
                                label12Tue.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Tue12 = "Beschreibung: " + c.Description;
                                label12Tue.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Tue12 = "Description: " + c.Description;
                                label12Tue.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label12Tue.Show();
                    }
                    if (c.Time == labelTime13.Text && datumEventaUCid == labelTuesdayDate.Text)
                    {
                        panel13Tue.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Tue13 = "Opis: " + c.Description;
                                label13Tue.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Tue13 = "Beschreibung: " + c.Description;
                                label13Tue.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Tue13 = "Description: " + c.Description;
                                label13Tue.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label13Tue.Show();
                    }
                    if (c.Time == labelTime14.Text && datumEventaUCid == labelTuesdayDate.Text)
                    {
                        panel14Tue.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Tue14 = "Opis: " + c.Description;
                                label14Tue.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Tue14 = "Beschreibung: " + c.Description;
                                label14Tue.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Tue14 = "Description: " + c.Description;
                                label14Tue.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label14Tue.Show();
                    }
                    if (c.Time == labelTime15.Text && datumEventaUCid == labelTuesdayDate.Text)
                    {
                        panel15Tue.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Tue15 = "Opis: " + c.Description;
                                label15Tue.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Tue15 = "Beschreibung: " + c.Description;
                                label15Tue.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Tue15 = "Description: " + c.Description;
                                label15Tue.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label15Tue.Show();
                    }
                    if (c.Time == labelTime16.Text && datumEventaUCid == labelTuesdayDate.Text)
                    {
                        panel16Tue.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Tue16 = "Opis: " + c.Description;
                                label16Tue.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Tue16 = "Beschreibung: " + c.Description;
                                label16Tue.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Tue16 = "Description: " + c.Description;
                                label16Tue.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label16Tue.Show();
                    }
                    if (c.Time == labelTime17.Text && datumEventaUCid == labelTuesdayDate.Text)
                    {
                        panel17Tue.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Tue17 = "Opis: " + c.Description;
                                label17Tue.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Tue17 = "Beschreibung: " + c.Description;
                                label17Tue.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Tue17 = "Description: " + c.Description;
                                label17Tue.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label17Tue.Show();
                    }
                    if (c.Time == labelTime18.Text && datumEventaUCid == labelTuesdayDate.Text)
                    {
                        panel18Tue.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Tue18 = "Opis: " + c.Description;
                                label18Tue.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Tue18 = "Beschreibung: " + c.Description;
                                label18Tue.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Tue18 = "Description: " + c.Description;
                                label18Tue.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label18Tue.Show();
                    }
                    if (c.Time == labelTime19.Text && datumEventaUCid == labelTuesdayDate.Text)
                    {
                        panel19Tue.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Tue19 = "Opis: " + c.Description;
                                label19Tue.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Tue19 = "Beschreibung: " + c.Description;
                                label19Tue.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Tue19 = "Description: " + c.Description;
                                label19Tue.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label19Tue.Show();
                    }
                    if (c.Time == labelTime20.Text && datumEventaUCid == labelTuesdayDate.Text)
                    {
                        panel20Tue.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Tue20 = "Opis: " + c.Description;
                                label20Tue.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Tue20 = "Beschreibung: " + c.Description;
                                label20Tue.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Tue20 = "Description: " + c.Description;
                                label20Tue.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label20Tue.Show();
                    }
                    if (c.Time == labelTime21.Text && datumEventaUCid == labelTuesdayDate.Text)
                    {
                        panel21Tue.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Tue21 = "Opis: " + c.Description;
                                label21Tue.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Tue21 = "Beschreibung: " + c.Description;
                                label21Tue.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Tue21 = "Description: " + c.Description;
                                label21Tue.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label21Tue.Show();
                    }
                    //sreda
                    if (c.Time == labelTime8.Text && datumEventaUCid == labelWednesdayDate.Text)
                    {
                        panel8Wed.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Wed8 = "Opis: " + c.Description;
                                label8Wed.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Wed8 = "Beschreibung: " + c.Description;
                                label8Wed.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Wed8 = "Description: " + c.Description;
                                label8Wed.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label8Wed.Show();
                    }
                    if (c.Time == labelTime9.Text && datumEventaUCid == labelWednesdayDate.Text)
                    {
                        panel9Wed.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Wed9 = "Opis: " + c.Description;
                                label9Wed.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Wed9 = "Beschreibung: " + c.Description;
                                label9Wed.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Wed9 = "Description: " + c.Description;
                                label9Wed.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label9Wed.Show();
                    }
                    if (c.Time == labelTime10.Text && datumEventaUCid == labelWednesdayDate.Text)
                    {
                        panel10Wed.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Wed10 = "Opis: " + c.Description;
                                label10Wed.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Wed10 = "Beschreibung: " + c.Description;
                                label10Wed.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Wed10 = "Description: " + c.Description;
                                label10Wed.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label10Wed.Show();
                    }
                    if (c.Time == labelTime11.Text && datumEventaUCid == labelWednesdayDate.Text)
                    {
                        panel11Wed.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Wed11 = "Opis: " + c.Description;
                                label11Wed.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Wed11 = "Beschreibung: " + c.Description;
                                label11Wed.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Wed11 = "Description: " + c.Description;
                                label11Wed.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label11Wed.Show();
                    }
                    if (c.Time == labelTime12.Text && datumEventaUCid == labelWednesdayDate.Text)
                    {
                        panel12Wed.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Wed12 = "Opis: " + c.Description;
                                label12Wed.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Wed12 = "Beschreibung: " + c.Description;
                                label12Wed.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Wed12 = "Description: " + c.Description;
                                label12Wed.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label12Wed.Show();
                    }
                    if (c.Time == labelTime13.Text && datumEventaUCid == labelWednesdayDate.Text)
                    {
                        panel13Wed.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Wed13 = "Opis: " + c.Description;
                                label13Wed.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Wed13 = "Beschreibung: " + c.Description;
                                label13Wed.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Wed13 = "Description: " + c.Description;
                                label13Wed.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label13Wed.Show();
                    }
                    if (c.Time == labelTime14.Text && datumEventaUCid == labelWednesdayDate.Text)
                    {
                        panel14Wed.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Wed14 = "Opis: " + c.Description;
                                label14Wed.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Wed14 = "Beschreibung: " + c.Description;
                                label14Wed.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Wed14 = "Description: " + c.Description;
                                label14Wed.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label14Wed.Show();
                    }
                    if (c.Time == labelTime15.Text && datumEventaUCid == labelWednesdayDate.Text)
                    {
                        panel15Wed.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Wed15 = "Opis: " + c.Description;
                                label15Wed.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Wed15 = "Beschreibung: " + c.Description;
                                label15Wed.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Wed15 = "Description: " + c.Description;
                                label15Wed.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label15Wed.Show();
                    }
                    if (c.Time == labelTime16.Text && datumEventaUCid == labelWednesdayDate.Text)
                    {
                        panel16Wed.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Wed16 = "Opis: " + c.Description;
                                label16Wed.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Wed16 = "Beschreibung: " + c.Description;
                                label16Wed.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Wed16 = "Description: " + c.Description;
                                label16Wed.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label16Wed.Show();
                    }
                    if (c.Time == labelTime17.Text && datumEventaUCid == labelWednesdayDate.Text)
                    {
                        panel17Wed.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Wed17 = "Opis: " + c.Description;
                                label17Wed.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Wed17 = "Beschreibung: " + c.Description;
                                label17Wed.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Wed17 = "Description: " + c.Description;
                                label17Wed.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label17Wed.Show();
                    }
                    if (c.Time == labelTime18.Text && datumEventaUCid == labelWednesdayDate.Text)
                    {
                        panel18Wed.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Wed18 = "Opis: " + c.Description;
                                label18Wed.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Wed18 = "Beschreibung: " + c.Description;
                                label18Wed.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Wed18 = "Description: " + c.Description;
                                label18Wed.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label18Wed.Show();
                    }
                    if (c.Time == labelTime19.Text && datumEventaUCid == labelWednesdayDate.Text)
                    {
                        panel19Wed.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Wed19 = "Opis: " + c.Description;
                                label19Wed.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Wed19 = "Beschreibung: " + c.Description;
                                label19Wed.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Wed19 = "Description: " + c.Description;
                                label19Wed.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label19Wed.Show();
                    }
                    if (c.Time == labelTime20.Text && datumEventaUCid == labelWednesdayDate.Text)
                    {
                        panel20Wed.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Wed20 = "Opis: " + c.Description;
                                label20Wed.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Wed20 = "Beschreibung: " + c.Description;
                                label20Wed.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Wed20 = "Description: " + c.Description;
                                label20Wed.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label20Wed.Show();
                    }
                    if (c.Time == labelTime21.Text && datumEventaUCid == labelWednesdayDate.Text)
                    {
                        panel21Wed.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Wed21 = "Opis: " + c.Description;
                                label21Wed.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Wed21 = "Beschreibung: " + c.Description;
                                label21Wed.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Wed21 = "Description: " + c.Description;
                                label21Wed.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label21Wed.Show();
                    }
                    //cetvrtak
                    if (c.Time == labelTime8.Text && datumEventaUCid == labelThursdayDate.Text)
                    {
                        panel8Thu.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Thu8 = "Opis: " + c.Description;
                                label8Thu.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Thu8 = "Beschreibung: " + c.Description;
                                label8Thu.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Thu8 = "Description: " + c.Description;
                                label8Thu.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label8Thu.Show();
                    }
                    if (c.Time == labelTime9.Text && datumEventaUCid == labelThursdayDate.Text)
                    {
                        panel9Thu.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Thu9 = "Opis: " + c.Description;
                                label9Thu.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Thu9 = "Beschreibung: " + c.Description;
                                label9Thu.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Thu9 = "Description: " + c.Description;
                                label9Thu.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label9Thu.Show();
                    }
                    if (c.Time == labelTime10.Text && datumEventaUCid == labelThursdayDate.Text)
                    {
                        panel10Thu.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Thu10 = "Opis: " + c.Description;
                                label10Thu.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Thu10 = "Beschreibung: " + c.Description;
                                label10Thu.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Thu10 = "Description: " + c.Description;
                                label10Thu.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label10Thu.Show();
                    }
                    if (c.Time == labelTime11.Text && datumEventaUCid == labelThursdayDate.Text)
                    {
                        panel11Thu.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Thu11 = "Opis: " + c.Description;
                                label11Thu.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Thu11 = "Beschreibung: " + c.Description;
                                label11Thu.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Thu11 = "Description: " + c.Description;
                                label11Thu.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label11Thu.Show();
                    }
                    if (c.Time == labelTime12.Text && datumEventaUCid == labelThursdayDate.Text)
                    {
                        panel12Thu.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Thu12 = "Opis: " + c.Description;
                                label12Thu.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Thu12 = "Beschreibung: " + c.Description;
                                label12Thu.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Thu12 = "Description: " + c.Description;
                                label12Thu.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label12Thu.Show();
                    }
                    if (c.Time == labelTime13.Text && datumEventaUCid == labelThursdayDate.Text)
                    {
                        panel13Thu.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Thu13 = "Opis: " + c.Description;
                                label13Thu.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Thu13 = "Beschreibung: " + c.Description;
                                label13Thu.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Thu13 = "Description: " + c.Description;
                                label13Thu.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label13Thu.Show();
                    }
                    if (c.Time == labelTime14.Text && datumEventaUCid == labelThursdayDate.Text)
                    {
                        panel14Thu.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Thu14 = "Opis: " + c.Description;
                                label14Thu.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Thu14 = "Beschreibung: " + c.Description;
                                label14Thu.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Thu14 = "Description: " + c.Description;
                                label14Thu.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label14Thu.Show();
                    }
                    if (c.Time == labelTime15.Text && datumEventaUCid == labelThursdayDate.Text)
                    {
                        panel15Thu.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Thu15 = "Opis: " + c.Description;
                                label15Thu.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Thu15 = "Beschreibung: " + c.Description;
                                label15Thu.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Thu15 = "Description: " + c.Description;
                                label15Thu.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label15Thu.Show();
                    }
                    if (c.Time == labelTime16.Text && datumEventaUCid == labelThursdayDate.Text)
                    {
                        panel16Thu.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Thu16 = "Opis: " + c.Description;
                                label16Thu.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Thu16 = "Beschreibung: " + c.Description;
                                label16Thu.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Thu16 = "Description: " + c.Description;
                                label16Thu.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label16Thu.Show();
                    }
                    if (c.Time == labelTime17.Text && datumEventaUCid == labelThursdayDate.Text)
                    {
                        panel17Thu.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Thu17 = "Opis: " + c.Description;
                                label17Thu.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Thu17 = "Beschreibung: " + c.Description;
                                label17Thu.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Thu17 = "Description: " + c.Description;
                                label17Thu.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label17Thu.Show();
                    }
                    if (c.Time == labelTime18.Text && datumEventaUCid == labelThursdayDate.Text)
                    {
                        panel18Thu.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Thu18 = "Opis: " + c.Description;
                                label18Thu.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Thu18 = "Beschreibung: " + c.Description;
                                label18Thu.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Thu18 = "Description: " + c.Description;
                                label18Thu.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label18Thu.Show();
                    }
                    if (c.Time == labelTime19.Text && datumEventaUCid == labelThursdayDate.Text)
                    {
                        panel19Thu.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Thu19 = "Opis: " + c.Description;
                                label19Thu.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Thu19 = "Beschreibung: " + c.Description;
                                label19Thu.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Thu19 = "Description: " + c.Description;
                                label19Thu.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label19Thu.Show();
                    }
                    if (c.Time == labelTime20.Text && datumEventaUCid == labelThursdayDate.Text)
                    {
                        panel20Thu.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Thu20 = "Opis: " + c.Description;
                                label20Thu.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Thu20 = "Beschreibung: " + c.Description;
                                label20Thu.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Thu20 = "Description: " + c.Description;
                                label20Thu.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label20Thu.Show();
                    }
                    if (c.Time == labelTime21.Text && datumEventaUCid == labelThursdayDate.Text)
                    {
                        panel21Thu.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Thu21 = "Opis: " + c.Description;
                                label21Thu.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Thu21 = "Beschreibung: " + c.Description;
                                label21Thu.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Thu21 = "Description: " + c.Description;
                                label21Thu.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label21Thu.Show();
                    }
                    //petak
                    if (c.Time == labelTime8.Text && datumEventaUCid == labelFridayDate.Text)
                    {
                        panel8Fri.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Fri8 = "Opis: " + c.Description;
                                label8Fri.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Fri8 = "Beschreibung: " + c.Description;
                                label8Fri.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Fri8 = "Description: " + c.Description;
                                label8Fri.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label8Fri.Show();
                    }
                    if (c.Time == labelTime9.Text && datumEventaUCid == labelFridayDate.Text)
                    {
                        panel9Fri.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Fri9 = "Opis: " + c.Description;
                                label9Fri.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Fri9 = "Beschreibung: " + c.Description;
                                label9Fri.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Fri9 = "Description: " + c.Description;
                                label9Fri.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label9Fri.Show();
                    }
                    if (c.Time == labelTime10.Text && datumEventaUCid == labelFridayDate.Text)
                    {
                        panel10Fri.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Fri10 = "Opis: " + c.Description;
                                label10Fri.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Fri10 = "Beschreibung: " + c.Description;
                                label10Fri.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Fri10 = "Description: " + c.Description;
                                label10Fri.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label10Fri.Show();
                    }
                    if (c.Time == labelTime11.Text && datumEventaUCid == labelFridayDate.Text)
                    {
                        panel11Fri.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Fri11 = "Opis: " + c.Description;
                                label11Fri.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Fri11 = "Beschreibung: " + c.Description;
                                label11Fri.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Fri11 = "Description: " + c.Description;
                                label11Fri.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label11Fri.Show();
                    }
                    if (c.Time == labelTime12.Text && datumEventaUCid == labelFridayDate.Text)
                    {
                        panel12Fri.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Fri12 = "Opis: " + c.Description;
                                label12Fri.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Fri12 = "Beschreibung: " + c.Description;
                                label12Fri.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Fri12 = "Description: " + c.Description;
                                label12Fri.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label12Fri.Show();
                    }
                    if (c.Time == labelTime13.Text && datumEventaUCid == labelFridayDate.Text)
                    {
                        panel13Fri.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Fri13 = "Opis: " + c.Description;
                                label13Fri.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Fri13 = "Beschreibung: " + c.Description;
                                label13Fri.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Fri13 = "Description: " + c.Description;
                                label13Fri.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label13Fri.Show();
                    }
                    if (c.Time == labelTime14.Text && datumEventaUCid == labelFridayDate.Text)
                    {
                        panel14Fri.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Fri14 = "Opis: " + c.Description;
                                label14Fri.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Fri14 = "Beschreibung: " + c.Description;
                                label14Fri.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Fri14 = "Description: " + c.Description;
                                label14Fri.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label14Fri.Show();
                    }
                    if (c.Time == labelTime15.Text && datumEventaUCid == labelFridayDate.Text)
                    {
                        panel15Fri.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Fri15 = "Opis: " + c.Description;
                                label15Fri.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Fri15 = "Beschreibung: " + c.Description;
                                label15Fri.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Fri15 = "Description: " + c.Description;
                                label15Fri.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label15Fri.Show();
                    }
                    if (c.Time == labelTime16.Text && datumEventaUCid == labelFridayDate.Text)
                    {
                        panel16Fri.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Fri16 = "Opis: " + c.Description;
                                label16Fri.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Fri16 = "Beschreibung: " + c.Description;
                                label16Fri.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Fri16 = "Description: " + c.Description;
                                label16Fri.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label16Fri.Show();
                    }
                    if (c.Time == labelTime17.Text && datumEventaUCid == labelFridayDate.Text)
                    {
                        panel17Fri.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Fri17 = "Opis: " + c.Description;
                                label17Fri.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Fri17 = "Beschreibung: " + c.Description;
                                label17Fri.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Fri17 = "Description: " + c.Description;
                                label17Fri.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label17Fri.Show();
                    }
                    if (c.Time == labelTime18.Text && datumEventaUCid == labelFridayDate.Text)
                    {
                        panel18Fri.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Fri18 = "Opis: " + c.Description;
                                label18Fri.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Fri18 = "Beschreibung: " + c.Description;
                                label18Fri.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Fri18 = "Description: " + c.Description;
                                label18Fri.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label18Fri.Show();
                    }
                    if (c.Time == labelTime19.Text && datumEventaUCid == labelFridayDate.Text)
                    {
                        panel19Fri.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Fri19 = "Opis: " + c.Description;
                                label19Fri.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Fri19 = "Beschreibung: " + c.Description;
                                label19Fri.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Fri19 = "Description: " + c.Description;
                                label19Fri.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label19Fri.Show();
                    }
                    if (c.Time == labelTime20.Text && datumEventaUCid == labelFridayDate.Text)
                    {
                        panel20Fri.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Fri20 = "Opis: " + c.Description;
                                label20Fri.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Fri20 = "Beschreibung: " + c.Description;
                                label20Fri.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Fri20 = "Description: " + c.Description;
                                label20Fri.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label20Fri.Show();
                    }
                    if (c.Time == labelTime21.Text && datumEventaUCid == labelFridayDate.Text)
                    {
                        panel21Fri.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Fri21 = "Opis: " + c.Description;
                                label21Fri.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Fri21 = "Beschreibung: " + c.Description;
                                label21Fri.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Fri21 = "Description: " + c.Description;
                                label21Fri.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label21Fri.Show();
                    }
                    //subota
                    if (c.Time == labelTime8.Text && datumEventaUCid == labelSaturdayDate.Text)
                    {
                        panel8Sat.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sat8 = "Opis: " + c.Description;
                                label8Sat.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sat8 = "Beschreibung: " + c.Description;
                                label8Sat.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sat8 = "Description: " + c.Description;
                                label8Sat.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label8Sat.Show();
                    }
                    if (c.Time == labelTime9.Text && datumEventaUCid == labelSaturdayDate.Text)
                    {
                        panel9Sat.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sat9 = "Opis: " + c.Description;
                                label9Sat.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sat9 = "Beschreibung: " + c.Description;
                                label9Sat.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sat9 = "Description: " + c.Description;
                                label9Sat.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label9Sat.Show();
                    }
                    if (c.Time == labelTime10.Text && datumEventaUCid == labelSaturdayDate.Text)
                    {
                        panel10Sat.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sat10 = "Opis: " + c.Description;
                                label10Sat.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sat10 = "Beschreibung: " + c.Description;
                                label10Sat.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sat10 = "Description: " + c.Description;
                                label10Sat.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label10Sat.Show();
                    }
                    if (c.Time == labelTime11.Text && datumEventaUCid == labelSaturdayDate.Text)
                    {
                        panel11Sat.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sat11 = "Opis: " + c.Description;
                                label11Sat.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sat11 = "Beschreibung: " + c.Description;
                                label11Sat.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sat11 = "Description: " + c.Description;
                                label11Sat.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label11Sat.Show();
                    }
                    if (c.Time == labelTime12.Text && datumEventaUCid == labelSaturdayDate.Text)
                    {
                        panel12Sat.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sat12 = "Opis: " + c.Description;
                                label12Sat.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sat12 = "Beschreibung: " + c.Description;
                                label12Sat.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sat12 = "Description: " + c.Description;
                                label12Sat.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label12Sat.Show();
                    }
                    if (c.Time == labelTime13.Text && datumEventaUCid == labelSaturdayDate.Text)
                    {
                        panel13Sat.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sat13 = "Opis: " + c.Description;
                                label13Sat.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sat13 = "Beschreibung: " + c.Description;
                                label13Sat.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sat13 = "Description: " + c.Description;
                                label13Sat.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label13Sat.Show();
                    }
                    if (c.Time == labelTime14.Text && datumEventaUCid == labelSaturdayDate.Text)
                    {
                        panel14Sat.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sat14 = "Opis: " + c.Description;
                                label14Sat.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sat14 = "Beschreibung: " + c.Description;
                                label14Sat.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sat14 = "Description: " + c.Description;
                                label14Sat.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label14Sat.Show();
                    }
                    if (c.Time == labelTime15.Text && datumEventaUCid == labelSaturdayDate.Text)
                    {
                        panel15Sat.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sat15 = "Opis: " + c.Description;
                                label15Sat.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sat15 = "Beschreibung: " + c.Description;
                                label15Sat.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sat15 = "Description: " + c.Description;
                                label15Sat.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label15Sat.Show();
                    }
                    if (c.Time == labelTime16.Text && datumEventaUCid == labelSaturdayDate.Text)
                    {
                        panel16Sat.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sat16 = "Opis: " + c.Description;
                                label16Sat.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sat16 = "Beschreibung: " + c.Description;
                                label16Sat.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sat16 = "Description: " + c.Description;
                                label16Sat.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label16Sat.Show();
                    }
                    if (c.Time == labelTime17.Text && datumEventaUCid == labelSaturdayDate.Text)
                    {
                        panel17Sat.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sat17 = "Opis: " + c.Description;
                                label17Sat.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sat17 = "Beschreibung: " + c.Description;
                                label17Sat.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sat17 = "Description: " + c.Description;
                                label17Sat.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label17Sat.Show();
                    }
                    if (c.Time == labelTime18.Text && datumEventaUCid == labelSaturdayDate.Text)
                    {
                        panel18Sat.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sat18 = "Opis: " + c.Description;
                                label18Sat.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sat18 = "Beschreibung: " + c.Description;
                                label18Sat.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sat18 = "Description: " + c.Description;
                                label18Sat.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label18Sat.Show();
                    }
                    if (c.Time == labelTime19.Text && datumEventaUCid == labelSaturdayDate.Text)
                    {
                        panel19Sat.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sat19 = "Opis: " + c.Description;
                                label19Sat.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sat19 = "Beschreibung: " + c.Description;
                                label19Sat.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sat19 = "Description: " + c.Description;
                                label19Sat.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label19Sat.Show();
                    }
                    if (c.Time == labelTime20.Text && datumEventaUCid == labelSaturdayDate.Text)
                    {
                        panel20Sat.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sat20 = "Opis: " + c.Description;
                                label20Sat.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sat20 = "Beschreibung: " + c.Description;
                                label20Sat.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sat20 = "Description: " + c.Description;
                                label20Sat.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label20Sat.Show();
                    }
                    if (c.Time == labelTime21.Text && datumEventaUCid == labelSaturdayDate.Text)
                    {
                        panel21Sat.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sat21 = "Opis: " + c.Description;
                                label21Sat.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sat21 = "Beschreibung: " + c.Description;
                                label21Sat.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sat21 = "Description: " + c.Description;
                                label21Sat.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label21Sat.Show();
                    }
                    //nedelja
                    if (c.Time == labelTime8.Text && datumEventaUCid == labelSundayDate.Text)
                    {
                        panel8Sun.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sun8 = "Opis: " + c.Description;
                                label8Sun.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sun8 = "Beschreibung: " + c.Description;
                                label8Sun.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sun8 = "Description: " + c.Description;
                                label8Sun.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label8Sun.Show();
                    }
                    if (c.Time == labelTime9.Text && datumEventaUCid == labelSundayDate.Text)
                    {
                        panel9Sun.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sun9 = "Opis: " + c.Description;
                                label9Sun.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sun9 = "Beschreibung: " + c.Description;
                                label9Sun.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sun9 = "Description: " + c.Description;
                                label9Sun.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label9Sun.Show();
                    }
                    if (c.Time == labelTime10.Text && datumEventaUCid == labelSundayDate.Text)
                    {
                        panel10Sun.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sun10 = "Opis: " + c.Description;
                                label10Sun.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sun10 = "Beschreibung: " + c.Description;
                                label10Sun.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sun10 = "Description: " + c.Description;
                                label10Sun.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label10Sun.Show();
                    }
                    if (c.Time == labelTime11.Text && datumEventaUCid == labelSundayDate.Text)
                    {
                        panel11Sun.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sun11 = "Opis: " + c.Description;
                                label11Sun.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sun11 = "Beschreibung: " + c.Description;
                                label11Sun.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sun11 = "Description: " + c.Description;
                                label11Sun.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label11Sun.Show();
                    }
                    if (c.Time == labelTime12.Text && datumEventaUCid == labelSundayDate.Text)
                    {
                        panel12Sun.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sun12 = "Opis: " + c.Description;
                                label12Sun.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sun12 = "Beschreibung: " + c.Description;
                                label12Sun.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sun12 = "Description: " + c.Description;
                                label12Sun.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label12Sun.Show();
                    }
                    if (c.Time == labelTime13.Text && datumEventaUCid == labelSundayDate.Text)
                    {
                        panel13Sun.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sun13 = "Opis: " + c.Description;
                                label13Sun.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sun13 = "Beschreibung: " + c.Description;
                                label13Sun.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sun13 = "Description: " + c.Description;
                                label13Sun.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label13Sun.Show();
                    }
                    if (c.Time == labelTime14.Text && datumEventaUCid == labelSundayDate.Text)
                    {
                        panel14Sun.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sun14 = "Opis: " + c.Description;
                                label14Sun.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sun14 = "Beschreibung: " + c.Description;
                                label14Sun.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sun14 = "Description: " + c.Description;
                                label14Sun.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label14Sun.Show();
                    }
                    if (c.Time == labelTime15.Text && datumEventaUCid == labelSundayDate.Text)
                    {
                        panel15Sun.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sun15 = "Opis: " + c.Description;
                                label15Sun.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sun15 = "Beschreibung: " + c.Description;
                                label15Sun.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sun15 = "Description: " + c.Description;
                                label15Sun.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label15Sun.Show();
                    }
                    if (c.Time == labelTime16.Text && datumEventaUCid == labelSundayDate.Text)
                    {
                        panel16Sun.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sun16 = "Opis: " + c.Description;
                                label16Sun.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sun16 = "Beschreibung: " + c.Description;
                                label16Sun.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sun16 = "Description: " + c.Description;
                                label16Sun.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label16Sun.Show();
                    }
                    if (c.Time == labelTime17.Text && datumEventaUCid == labelSundayDate.Text)
                    {
                        panel17Sun.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sun17 = "Opis: " + c.Description;
                                label17Sun.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sun17 = "Beschreibung: " + c.Description;
                                label17Sun.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sun17 = "Description: " + c.Description;
                                label17Sun.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label17Sun.Show();
                    }
                    if (c.Time == labelTime18.Text && datumEventaUCid == labelSundayDate.Text)
                    {
                        panel18Sun.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sun18 = "Opis: " + c.Description;
                                label18Sun.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sun18 = "Beschreibung: " + c.Description;
                                label18Sun.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sun18 = "Description: " + c.Description;
                                label18Sun.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label18Sun.Show();
                    }
                    if (c.Time == labelTime19.Text && datumEventaUCid == labelSundayDate.Text)
                    {
                        panel19Sun.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sun19 = "Opis: " + c.Description;
                                label19Sun.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sun19 = "Beschreibung: " + c.Description;
                                label19Sun.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sun19 = "Description: " + c.Description;
                                label19Sun.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label19Sun.Show();
                    }
                    if (c.Time == labelTime20.Text && datumEventaUCid == labelSundayDate.Text)
                    {
                        panel20Sun.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sun20 = "Opis: " + c.Description;
                                label20Sun.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sun20 = "Beschreibung: " + c.Description;
                                label20Sun.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sun20 = "Description: " + c.Description;
                                label20Sun.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label20Sun.Show();
                    }
                    if (c.Time == labelTime21.Text && datumEventaUCid == labelSundayDate.Text)
                    {
                        panel21Sun.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sun21 = "Opis: " + c.Description;
                                label21Sun.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sun21 = "Beschreibung: " + c.Description;
                                label21Sun.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sun21 = "Description: " + c.Description;
                                label21Sun.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label21Sun.Show();
                    }
                    #endregion //za Tooltips tekstove
                }
            }
            ClinicEventsList = new List<ClinicEvent>();

            #endregion //za ne-reacurring eventove 

            #region Duplikati reacurring eventova

            GetAllEvents.Invoke(this, EventArgs.Empty);
            
            foreach (ClinicEvent c in ClinicEventsList)
            {
                if (c.Reacurring != 0 && c.Doctor == ActiveDoctor.name)
                {
                    serviceClr = Color.LightPink;

                    if (cis.Name == "de-DE")
                        translatedService = "Therapie";
                    if (cis.Name == "sr-Latn-CS")
                        translatedService = "Terapija";
                    //jedino je Therapy reacurring

                    //uzimamo "date" vrednost eventa i dobijamo koji je dan u nedelji
                    String eventDate = c.Date;
                    CultureInfo ci = Thread.CurrentThread.CurrentUICulture;
                    CultureInfo cid = new CultureInfo(c.CultureInfo);
                    DateTime datumEventa = DateTime.ParseExact(eventDate.Substring(1, eventDate.Length - 2), " d. MMMM yyyy. ", cid, DateTimeStyles.NoCurrentDateDefault);
                    DayOfWeek danEventa = ci.Calendar.GetDayOfWeek(datumEventa);

                    //uslov za reacrruing je:
                    //-da se vreme poklapa (da bi znali koji panel)
                    //-da je odredjeni dan u nedelji (da bi znali koji panel)
                    //-da event.week + event.reacurring >= week
                    //-da event.week < week

                    //ponedeljak              
                    if (c.Time == labelTime8.Text && danEventa == DayOfWeek.Monday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel8Mon.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Mon8 = "Opis: " + c.Description;
                                label8Mon.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Mon8 = "Beschreibung: " + c.Description;
                                label8Mon.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Mon8 = "Description: " + c.Description;
                                label8Mon.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label8Mon.Show();

                    }
                    if (c.Time == labelTime9.Text && danEventa == DayOfWeek.Monday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel9Mon.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Mon9 = "Opis: " + c.Description;
                                label9Mon.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Mon9 = "Beschreibung: " + c.Description;
                                label9Mon.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Mon9 = "Description: " + c.Description;
                                label9Mon.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label9Mon.Show();
                    }
                    if (c.Time == labelTime10.Text && danEventa == DayOfWeek.Monday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel10Mon.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Mon10 = "Opis: " + c.Description;
                                label10Mon.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Mon10 = "Beschreibung: " + c.Description;
                                label10Mon.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Mon10 = "Description: " + c.Description;
                                label10Mon.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label10Mon.Show();
                    }
                    if (c.Time == labelTime11.Text && danEventa == DayOfWeek.Monday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel11Mon.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Mon11 = "Opis: " + c.Description;
                                label11Mon.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Mon11 = "Beschreibung: " + c.Description;
                                label11Mon.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Mon11 = "Description: " + c.Description;
                                label11Mon.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label11Mon.Show();
                    }
                    if (c.Time == labelTime12.Text && danEventa == DayOfWeek.Monday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel12Mon.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Mon12 = "Opis: " + c.Description;
                                label12Mon.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Mon12 = "Beschreibung: " + c.Description;
                                label12Mon.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Mon12 = "Description: " + c.Description;
                                label12Mon.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label12Mon.Show();
                    }
                    if (c.Time == labelTime13.Text && danEventa == DayOfWeek.Monday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel13Mon.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Mon13 = "Opis: " + c.Description;
                                label13Mon.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Mon13 = "Beschreibung: " + c.Description;
                                label13Mon.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Mon13 = "Description: " + c.Description;
                                label13Mon.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label13Mon.Show();
                    }
                    if (c.Time == labelTime14.Text && danEventa == DayOfWeek.Monday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel14Mon.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Mon14 = "Opis: " + c.Description;
                                label14Mon.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Mon14 = "Beschreibung: " + c.Description;
                                label14Mon.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Mon14 = "Description: " + c.Description;
                                label14Mon.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label14Mon.Show();
                    }
                    if (c.Time == labelTime15.Text && danEventa == DayOfWeek.Monday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel15Mon.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Mon15 = "Opis: " + c.Description;
                                label15Mon.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Mon15 = "Beschreibung: " + c.Description;
                                label15Mon.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Mon15 = "Description: " + c.Description;
                                label15Mon.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label15Mon.Show();
                    }
                    if (c.Time == labelTime16.Text && danEventa == DayOfWeek.Monday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel16Mon.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Mon16 = "Opis: " + c.Description;
                                label16Mon.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Mon16 = "Beschreibung: " + c.Description;
                                label16Mon.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Mon16 = "Description: " + c.Description;
                                label16Mon.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label16Mon.Show();
                    }
                    if (c.Time == labelTime17.Text && danEventa == DayOfWeek.Monday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel17Mon.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Mon17 = "Opis: " + c.Description;
                                label17Mon.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Mon17 = "Beschreibung: " + c.Description;
                                label17Mon.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Mon17 = "Description: " + c.Description;
                                label17Mon.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label17Mon.Show();
                    }
                    if (c.Time == labelTime18.Text && danEventa == DayOfWeek.Monday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel18Mon.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Mon18 = "Opis: " + c.Description;
                                label18Mon.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Mon18 = "Beschreibung: " + c.Description;
                                label18Mon.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Mon18 = "Description: " + c.Description;
                                label18Mon.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label18Mon.Show();
                    }
                    if (c.Time == labelTime19.Text && danEventa == DayOfWeek.Monday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel19Mon.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Mon19 = "Opis: " + c.Description;
                                label19Mon.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Mon19 = "Beschreibung: " + c.Description;
                                label19Mon.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Mon19 = "Description: " + c.Description;
                                label19Mon.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label19Mon.Show();
                    }
                    if (c.Time == labelTime20.Text && danEventa == DayOfWeek.Monday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel20Mon.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Mon20 = "Opis: " + c.Description;
                                label20Mon.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Mon20 = "Beschreibung: " + c.Description;
                                label20Mon.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Mon20 = "Description: " + c.Description;
                                label20Mon.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label20Mon.Show();
                    }
                    if (c.Time == labelTime21.Text && danEventa == DayOfWeek.Monday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel21Mon.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Mon21 = "Opis: " + c.Description;
                                label21Mon.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Mon21 = "Beschreibung: " + c.Description;
                                label21Mon.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Mon21 = "Description: " + c.Description;
                                label21Mon.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label21Mon.Show();
                    }
                    //utorak
                    if (c.Time == labelTime8.Text && danEventa == DayOfWeek.Tuesday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel8Tue.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Tue8 = "Opis: " + c.Description;
                                label8Tue.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Tue8 = "Beschreibung: " + c.Description;
                                label8Tue.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Tue8 = "Description: " + c.Description;
                                label8Tue.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label8Tue.Show();
                    }
                    if (c.Time == labelTime9.Text && danEventa == DayOfWeek.Tuesday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel9Tue.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Tue9 = "Opis: " + c.Description;
                                label9Tue.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Tue9 = "Beschreibung: " + c.Description;
                                label9Tue.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Tue9 = "Description: " + c.Description;
                                label9Tue.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label9Tue.Show();
                    }
                    if (c.Time == labelTime10.Text && danEventa == DayOfWeek.Tuesday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel10Tue.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Tue10 = "Opis: " + c.Description;
                                label10Tue.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Tue10 = "Beschreibung: " + c.Description;
                                label10Tue.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Tue10 = "Description: " + c.Description;
                                label10Tue.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label10Tue.Show();
                    }
                    if (c.Time == labelTime11.Text && danEventa == DayOfWeek.Tuesday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel11Tue.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Tue11 = "Opis: " + c.Description;
                                label11Tue.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Tue11 = "Beschreibung: " + c.Description;
                                label11Tue.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Tue11 = "Description: " + c.Description;
                                label11Tue.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label11Tue.Show();
                    }
                    if (c.Time == labelTime12.Text && danEventa == DayOfWeek.Tuesday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel12Tue.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Tue12 = "Opis: " + c.Description;
                                label12Tue.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Tue12 = "Beschreibung: " + c.Description;
                                label12Tue.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Tue12 = "Description: " + c.Description;
                                label12Tue.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label12Tue.Show();
                    }
                    if (c.Time == labelTime13.Text && danEventa == DayOfWeek.Tuesday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel13Tue.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Tue13 = "Opis: " + c.Description;
                                label13Tue.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Tue13 = "Beschreibung: " + c.Description;
                                label13Tue.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Tue13 = "Description: " + c.Description;
                                label13Tue.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label13Tue.Show();
                    }
                    if (c.Time == labelTime14.Text && danEventa == DayOfWeek.Tuesday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel14Tue.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Tue14 = "Opis: " + c.Description;
                                label14Tue.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Tue14 = "Beschreibung: " + c.Description;
                                label14Tue.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Tue14 = "Description: " + c.Description;
                                label14Tue.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label14Tue.Show();
                    }
                    if (c.Time == labelTime15.Text && danEventa == DayOfWeek.Tuesday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel15Tue.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Tue15 = "Opis: " + c.Description;
                                label15Tue.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Tue15 = "Beschreibung: " + c.Description;
                                label15Tue.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Tue15 = "Description: " + c.Description;
                                label15Tue.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label15Tue.Show();
                    }
                    if (c.Time == labelTime16.Text && danEventa == DayOfWeek.Tuesday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel16Tue.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Tue16 = "Opis: " + c.Description;
                                label16Tue.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Tue16 = "Beschreibung: " + c.Description;
                                label16Tue.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Tue16 = "Description: " + c.Description;
                                label16Tue.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label16Tue.Show();
                    }
                    if (c.Time == labelTime17.Text && danEventa == DayOfWeek.Tuesday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel17Tue.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Tue17 = "Opis: " + c.Description;
                                label17Tue.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Tue17 = "Beschreibung: " + c.Description;
                                label17Tue.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Tue17 = "Description: " + c.Description;
                                label17Tue.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label17Tue.Show();
                    }
                    if (c.Time == labelTime18.Text && danEventa == DayOfWeek.Tuesday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel18Tue.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Tue18 = "Opis: " + c.Description;
                                label18Tue.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Tue18 = "Beschreibung: " + c.Description;
                                label18Tue.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Tue18 = "Description: " + c.Description;
                                label18Tue.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label18Tue.Show();
                    }
                    if (c.Time == labelTime19.Text && danEventa == DayOfWeek.Tuesday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel19Tue.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Tue19 = "Opis: " + c.Description;
                                label19Tue.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Tue19 = "Beschreibung: " + c.Description;
                                label19Tue.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Tue19 = "Description: " + c.Description;
                                label19Tue.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label19Tue.Show();
                    }
                    if (c.Time == labelTime20.Text && danEventa == DayOfWeek.Tuesday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel20Tue.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Tue20 = "Opis: " + c.Description;
                                label20Tue.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Tue20 = "Beschreibung: " + c.Description;
                                label20Tue.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Tue20 = "Description: " + c.Description;
                                label20Tue.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label20Tue.Show();
                    }
                    if (c.Time == labelTime21.Text && danEventa == DayOfWeek.Tuesday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel21Tue.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Tue21 = "Opis: " + c.Description;
                                label21Tue.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Tue21 = "Beschreibung: " + c.Description;
                                label21Tue.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Tue21 = "Description: " + c.Description;
                                label21Tue.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label21Tue.Show();
                    }
                    //sreda
                    if (c.Time == labelTime8.Text && danEventa == DayOfWeek.Wednesday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel8Wed.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Wed8 = "Opis: " + c.Description;
                                label8Wed.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Wed8 = "Beschreibung: " + c.Description;
                                label8Wed.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Wed8 = "Description: " + c.Description;
                                label8Wed.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label8Wed.Show();
                    }
                    if (c.Time == labelTime9.Text && danEventa == DayOfWeek.Wednesday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel9Wed.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Wed9 = "Opis: " + c.Description;
                                label9Wed.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Wed9 = "Beschreibung: " + c.Description;
                                label9Wed.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Wed9 = "Description: " + c.Description;
                                label9Wed.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label9Wed.Show();
                    }
                    if (c.Time == labelTime10.Text && danEventa == DayOfWeek.Wednesday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel10Wed.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Wed10 = "Opis: " + c.Description;
                                label10Wed.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Wed10 = "Beschreibung: " + c.Description;
                                label10Wed.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Wed10 = "Description: " + c.Description;
                                label10Wed.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label10Wed.Show();
                    }
                    if (c.Time == labelTime11.Text && danEventa == DayOfWeek.Wednesday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel11Wed.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Wed11 = "Opis: " + c.Description;
                                label11Wed.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Wed11 = "Beschreibung: " + c.Description;
                                label11Wed.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Wed11 = "Description: " + c.Description;
                                label11Wed.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label11Wed.Show();
                    }
                    if (c.Time == labelTime12.Text && danEventa == DayOfWeek.Wednesday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel12Wed.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Wed12 = "Opis: " + c.Description;
                                label12Wed.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Wed12 = "Beschreibung: " + c.Description;
                                label12Wed.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Wed12 = "Description: " + c.Description;
                                label12Wed.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label12Wed.Show();
                    }
                    if (c.Time == labelTime13.Text && danEventa == DayOfWeek.Wednesday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel13Wed.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Wed13 = "Opis: " + c.Description;
                                label13Wed.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Wed13 = "Beschreibung: " + c.Description;
                                label13Wed.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Wed13 = "Description: " + c.Description;
                                label13Wed.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label13Wed.Show();
                    }
                    if (c.Time == labelTime14.Text && danEventa == DayOfWeek.Wednesday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel14Wed.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Wed14 = "Opis: " + c.Description;
                                label14Wed.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Wed14 = "Beschreibung: " + c.Description;
                                label14Wed.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Wed14 = "Description: " + c.Description;
                                label14Wed.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label14Wed.Show();
                    }
                    if (c.Time == labelTime15.Text && danEventa == DayOfWeek.Wednesday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel15Wed.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Wed15 = "Opis: " + c.Description;
                                label15Wed.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Wed15 = "Beschreibung: " + c.Description;
                                label15Wed.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Wed15 = "Description: " + c.Description;
                                label15Wed.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label15Wed.Show();
                    }
                    if (c.Time == labelTime16.Text && danEventa == DayOfWeek.Wednesday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel16Wed.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Wed16 = "Opis: " + c.Description;
                                label16Wed.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Wed16 = "Beschreibung: " + c.Description;
                                label16Wed.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Wed16 = "Description: " + c.Description;
                                label16Wed.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label16Wed.Show();
                    }
                    if (c.Time == labelTime17.Text && danEventa == DayOfWeek.Wednesday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel17Wed.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Wed17 = "Opis: " + c.Description;
                                label17Wed.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Wed17 = "Beschreibung: " + c.Description;
                                label17Wed.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Wed17 = "Description: " + c.Description;
                                label17Wed.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label17Wed.Show();
                    }
                    if (c.Time == labelTime18.Text && danEventa == DayOfWeek.Wednesday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel18Wed.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Wed18 = "Opis: " + c.Description;
                                label18Wed.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Wed18 = "Beschreibung: " + c.Description;
                                label18Wed.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Wed18 = "Description: " + c.Description;
                                label18Wed.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label18Wed.Show();
                    }
                    if (c.Time == labelTime19.Text && danEventa == DayOfWeek.Wednesday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel19Wed.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Wed19 = "Opis: " + c.Description;
                                label19Wed.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Wed19 = "Beschreibung: " + c.Description;
                                label19Wed.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Wed19 = "Description: " + c.Description;
                                label19Wed.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label19Wed.Show();
                    }
                    if (c.Time == labelTime20.Text && danEventa == DayOfWeek.Wednesday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel20Wed.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Wed20 = "Opis: " + c.Description;
                                label20Wed.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Wed20 = "Beschreibung: " + c.Description;
                                label20Wed.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Wed20 = "Description: " + c.Description;
                                label20Wed.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label20Wed.Show();
                    }
                    if (c.Time == labelTime21.Text && danEventa == DayOfWeek.Wednesday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel21Wed.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Wed21 = "Opis: " + c.Description;
                                label21Wed.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Wed21 = "Beschreibung: " + c.Description;
                                label21Wed.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Wed21 = "Description: " + c.Description;
                                label21Wed.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label21Wed.Show();
                    }
                    //cetvrtak
                    if (c.Time == labelTime8.Text && danEventa == DayOfWeek.Thursday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel8Thu.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Thu8 = "Opis: " + c.Description;
                                label8Thu.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Thu8 = "Beschreibung: " + c.Description;
                                label8Thu.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Thu8 = "Description: " + c.Description;
                                label8Thu.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label8Thu.Show();
                    }
                    if (c.Time == labelTime9.Text && danEventa == DayOfWeek.Thursday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel9Thu.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Thu9 = "Opis: " + c.Description;
                                label9Thu.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Thu9 = "Beschreibung: " + c.Description;
                                label9Thu.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Thu9 = "Description: " + c.Description;
                                label9Thu.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label9Thu.Show();
                    }
                    if (c.Time == labelTime10.Text && danEventa == DayOfWeek.Thursday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel10Thu.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Thu10 = "Opis: " + c.Description;
                                label10Thu.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Thu10 = "Beschreibung: " + c.Description;
                                label10Thu.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Thu10 = "Description: " + c.Description;
                                label10Thu.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label10Thu.Show();
                    }
                    if (c.Time == labelTime11.Text && danEventa == DayOfWeek.Thursday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel11Thu.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Thu11 = "Opis: " + c.Description;
                                label11Thu.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Thu11 = "Beschreibung: " + c.Description;
                                label11Thu.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Thu11 = "Description: " + c.Description;
                                label11Thu.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label11Thu.Show();
                    }
                    if (c.Time == labelTime12.Text && danEventa == DayOfWeek.Thursday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel12Thu.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Thu12 = "Opis: " + c.Description;
                                label12Thu.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Thu12 = "Beschreibung: " + c.Description;
                                label12Thu.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Thu12 = "Description: " + c.Description;
                                label12Thu.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label12Thu.Show();
                    }
                    if (c.Time == labelTime13.Text && danEventa == DayOfWeek.Thursday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel13Thu.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Thu13 = "Opis: " + c.Description;
                                label13Thu.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Thu13 = "Beschreibung: " + c.Description;
                                label13Thu.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Thu13 = "Description: " + c.Description;
                                label13Thu.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label13Thu.Show();
                    }
                    if (c.Time == labelTime14.Text && danEventa == DayOfWeek.Thursday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel14Thu.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Thu14 = "Opis: " + c.Description;
                                label14Thu.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Thu14 = "Beschreibung: " + c.Description;
                                label14Thu.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Thu14 = "Description: " + c.Description;
                                label14Thu.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label14Thu.Show();
                    }
                    if (c.Time == labelTime15.Text && danEventa == DayOfWeek.Thursday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel15Thu.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Thu15 = "Opis: " + c.Description;
                                label15Thu.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Thu15 = "Beschreibung: " + c.Description;
                                label15Thu.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Thu15 = "Description: " + c.Description;
                                label15Thu.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label15Thu.Show();
                    }
                    if (c.Time == labelTime16.Text && danEventa == DayOfWeek.Thursday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel16Thu.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Thu16 = "Opis: " + c.Description;
                                label16Thu.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Thu16 = "Beschreibung: " + c.Description;
                                label16Thu.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Thu16 = "Description: " + c.Description;
                                label16Thu.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label16Thu.Show();
                    }
                    if (c.Time == labelTime17.Text && danEventa == DayOfWeek.Thursday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel17Thu.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Thu17 = "Opis: " + c.Description;
                                label17Thu.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Thu17 = "Beschreibung: " + c.Description;
                                label17Thu.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Thu17 = "Description: " + c.Description;
                                label17Thu.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label17Thu.Show();
                    }
                    if (c.Time == labelTime18.Text && danEventa == DayOfWeek.Thursday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel18Thu.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Thu18 = "Opis: " + c.Description;
                                label18Thu.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Thu18 = "Beschreibung: " + c.Description;
                                label18Thu.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Thu18 = "Description: " + c.Description;
                                label18Thu.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label18Thu.Show();
                    }
                    if (c.Time == labelTime19.Text && danEventa == DayOfWeek.Thursday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel19Thu.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Thu19 = "Opis: " + c.Description;
                                label19Thu.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Thu19 = "Beschreibung: " + c.Description;
                                label19Thu.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Thu19 = "Description: " + c.Description;
                                label19Thu.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label19Thu.Show();
                    }
                    if (c.Time == labelTime20.Text && danEventa == DayOfWeek.Thursday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel20Thu.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Thu20 = "Opis: " + c.Description;
                                label20Thu.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Thu20 = "Beschreibung: " + c.Description;
                                label20Thu.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Thu20 = "Description: " + c.Description;
                                label20Thu.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label20Thu.Show();
                    }
                    if (c.Time == labelTime21.Text && danEventa == DayOfWeek.Thursday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel21Thu.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Thu21 = "Opis: " + c.Description;
                                label21Thu.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Thu21 = "Beschreibung: " + c.Description;
                                label21Thu.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Thu21 = "Description: " + c.Description;
                                label21Thu.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label21Thu.Show();
                    }
                    //petak
                    if (c.Time == labelTime8.Text && danEventa == DayOfWeek.Friday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel8Fri.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Fri8 = "Opis: " + c.Description;
                                label8Fri.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Fri8 = "Beschreibung: " + c.Description;
                                label8Fri.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Fri8 = "Description: " + c.Description;
                                label8Fri.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label8Fri.Show();
                    }
                    if (c.Time == labelTime9.Text && danEventa == DayOfWeek.Friday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel9Fri.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Fri9 = "Opis: " + c.Description;
                                label9Fri.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Fri9 = "Beschreibung: " + c.Description;
                                label9Fri.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Fri9 = "Description: " + c.Description;
                                label9Fri.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label9Fri.Show();
                    }
                    if (c.Time == labelTime10.Text && danEventa == DayOfWeek.Friday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel10Fri.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Fri10 = "Opis: " + c.Description;
                                label10Fri.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Fri10 = "Beschreibung: " + c.Description;
                                label10Fri.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Fri10 = "Description: " + c.Description;
                                label10Fri.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label10Fri.Show();
                    }
                    if (c.Time == labelTime11.Text && danEventa == DayOfWeek.Friday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel11Fri.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Fri11 = "Opis: " + c.Description;
                                label11Fri.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Fri11 = "Beschreibung: " + c.Description;
                                label11Fri.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Fri11 = "Description: " + c.Description;
                                label11Fri.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label11Fri.Show();
                    }
                    if (c.Time == labelTime12.Text && danEventa == DayOfWeek.Friday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel12Fri.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Fri12 = "Opis: " + c.Description;
                                label12Fri.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Fri12 = "Beschreibung: " + c.Description;
                                label12Fri.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Fri12 = "Description: " + c.Description;
                                label12Fri.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label12Fri.Show();
                    }
                    if (c.Time == labelTime13.Text && danEventa == DayOfWeek.Friday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel13Fri.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Fri13 = "Opis: " + c.Description;
                                label13Fri.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Fri13 = "Beschreibung: " + c.Description;
                                label13Fri.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Fri13 = "Description: " + c.Description;
                                label13Fri.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label13Fri.Show();
                    }
                    if (c.Time == labelTime14.Text && danEventa == DayOfWeek.Friday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel14Fri.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Fri14 = "Opis: " + c.Description;
                                label14Fri.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Fri14 = "Beschreibung: " + c.Description;
                                label14Fri.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Fri14 = "Description: " + c.Description;
                                label14Fri.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label14Fri.Show();
                    }
                    if (c.Time == labelTime15.Text && danEventa == DayOfWeek.Friday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel15Fri.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Fri15 = "Opis: " + c.Description;
                                label15Fri.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Fri15 = "Beschreibung: " + c.Description;
                                label15Fri.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Fri15 = "Description: " + c.Description;
                                label15Fri.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label15Fri.Show();
                    }
                    if (c.Time == labelTime16.Text && danEventa == DayOfWeek.Friday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel16Fri.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Fri16 = "Opis: " + c.Description;
                                label16Fri.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Fri16 = "Beschreibung: " + c.Description;
                                label16Fri.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Fri16 = "Description: " + c.Description;
                                label16Fri.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label16Fri.Show();
                    }
                    if (c.Time == labelTime17.Text && danEventa == DayOfWeek.Friday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel17Fri.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Fri17 = "Opis: " + c.Description;
                                label17Fri.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Fri17 = "Beschreibung: " + c.Description;
                                label17Fri.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Fri17 = "Description: " + c.Description;
                                label17Fri.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label17Fri.Show();
                    }
                    if (c.Time == labelTime18.Text && danEventa == DayOfWeek.Friday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel18Fri.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Fri18 = "Opis: " + c.Description;
                                label18Fri.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Fri18 = "Beschreibung: " + c.Description;
                                label18Fri.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Fri18 = "Description: " + c.Description;
                                label18Fri.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label18Fri.Show();
                    }
                    if (c.Time == labelTime19.Text && danEventa == DayOfWeek.Friday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel19Fri.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Fri19 = "Opis: " + c.Description;
                                label19Fri.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Fri19 = "Beschreibung: " + c.Description;
                                label19Fri.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Fri19 = "Description: " + c.Description;
                                label19Fri.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label19Fri.Show();
                    }
                    if (c.Time == labelTime20.Text && danEventa == DayOfWeek.Friday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel20Fri.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Fri20 = "Opis: " + c.Description;
                                label20Fri.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Fri20 = "Beschreibung: " + c.Description;
                                label20Fri.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Fri20 = "Description: " + c.Description;
                                label20Fri.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label20Fri.Show();
                    }
                    if (c.Time == labelTime21.Text && danEventa == DayOfWeek.Friday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel21Fri.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Fri21 = "Opis: " + c.Description;
                                label21Fri.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Fri21 = "Beschreibung: " + c.Description;
                                label21Fri.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Fri21 = "Description: " + c.Description;
                                label21Fri.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label21Fri.Show();
                    }
                    //subota
                    if (c.Time == labelTime8.Text && danEventa == DayOfWeek.Saturday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel8Sat.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sat8 = "Opis: " + c.Description;
                                label8Sat.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sat8 = "Beschreibung: " + c.Description;
                                label8Sat.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sat8 = "Description: " + c.Description;
                                label8Sat.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label8Sat.Show();
                    }
                    if (c.Time == labelTime9.Text && danEventa == DayOfWeek.Saturday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel9Sat.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sat9 = "Opis: " + c.Description;
                                label9Sat.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sat9 = "Beschreibung: " + c.Description;
                                label9Sat.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sat9 = "Description: " + c.Description;
                                label9Sat.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label9Sat.Show();
                    }
                    if (c.Time == labelTime10.Text && danEventa == DayOfWeek.Saturday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel10Sat.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sat10 = "Opis: " + c.Description;
                                label10Sat.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sat10 = "Beschreibung: " + c.Description;
                                label10Sat.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sat10 = "Description: " + c.Description;
                                label10Sat.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label10Sat.Show();
                    }
                    if (c.Time == labelTime11.Text && danEventa == DayOfWeek.Saturday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel11Sat.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sat11 = "Opis: " + c.Description;
                                label11Sat.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sat11 = "Beschreibung: " + c.Description;
                                label11Sat.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sat11 = "Description: " + c.Description;
                                label11Sat.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label11Sat.Show();
                    }
                    if (c.Time == labelTime12.Text && danEventa == DayOfWeek.Saturday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel12Sat.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sat12 = "Opis: " + c.Description;
                                label12Sat.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sat12 = "Beschreibung: " + c.Description;
                                label12Sat.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sat12 = "Description: " + c.Description;
                                label12Sat.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label12Sat.Show();
                    }
                    if (c.Time == labelTime13.Text && danEventa == DayOfWeek.Saturday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel13Sat.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sat13 = "Opis: " + c.Description;
                                label13Sat.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sat13 = "Beschreibung: " + c.Description;
                                label13Sat.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sat13 = "Description: " + c.Description;
                                label13Sat.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label13Sat.Show();
                    }
                    if (c.Time == labelTime14.Text && danEventa == DayOfWeek.Saturday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel14Sat.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sat14 = "Opis: " + c.Description;
                                label14Sat.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sat14 = "Beschreibung: " + c.Description;
                                label14Sat.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sat14 = "Description: " + c.Description;
                                label14Sat.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label14Sat.Show();
                    }
                    if (c.Time == labelTime15.Text && danEventa == DayOfWeek.Saturday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel15Sat.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sat15 = "Opis: " + c.Description;
                                label15Sat.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sat15 = "Beschreibung: " + c.Description;
                                label15Sat.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sat15 = "Description: " + c.Description;
                                label15Sat.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label15Sat.Show();
                    }
                    if (c.Time == labelTime16.Text && danEventa == DayOfWeek.Saturday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel16Sat.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sat16 = "Opis: " + c.Description;
                                label16Sat.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sat16 = "Beschreibung: " + c.Description;
                                label16Sat.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sat16 = "Description: " + c.Description;
                                label16Sat.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label16Sat.Show();
                    }
                    if (c.Time == labelTime17.Text && danEventa == DayOfWeek.Saturday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel17Sat.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sat17 = "Opis: " + c.Description;
                                label17Sat.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sat17 = "Beschreibung: " + c.Description;
                                label17Sat.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sat17 = "Description: " + c.Description;
                                label17Sat.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label17Sat.Show();
                    }
                    if (c.Time == labelTime18.Text && danEventa == DayOfWeek.Saturday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel18Sat.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sat18 = "Opis: " + c.Description;
                                label18Sat.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sat18 = "Beschreibung: " + c.Description;
                                label18Sat.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sat18 = "Description: " + c.Description;
                                label18Sat.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label18Sat.Show();
                    }
                    if (c.Time == labelTime19.Text && danEventa == DayOfWeek.Saturday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel19Sat.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sat19 = "Opis: " + c.Description;
                                label19Sat.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sat19 = "Beschreibung: " + c.Description;
                                label19Sat.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sat19 = "Description: " + c.Description;
                                label19Sat.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label19Sat.Show();
                    }
                    if (c.Time == labelTime20.Text && danEventa == DayOfWeek.Saturday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel20Sat.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sat20 = "Opis: " + c.Description;
                                label20Sat.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sat20 = "Beschreibung: " + c.Description;
                                label20Sat.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sat20 = "Description: " + c.Description;
                                label20Sat.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label20Sat.Show();
                    }
                    if (c.Time == labelTime21.Text && danEventa == DayOfWeek.Saturday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel21Sat.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sat21 = "Opis: " + c.Description;
                                label21Sat.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sat21 = "Beschreibung: " + c.Description;
                                label21Sat.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sat21 = "Description: " + c.Description;
                                label21Sat.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label21Sat.Show();
                    }
                    //nedelja
                    if (c.Time == labelTime8.Text && danEventa == DayOfWeek.Sunday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel8Sun.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sun8 = "Opis: " + c.Description;
                                label8Sun.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sun8 = "Beschreibung: " + c.Description;
                                label8Sun.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sun8 = "Description: " + c.Description;
                                label8Sun.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label8Sun.Show();
                    }
                    if (c.Time == labelTime9.Text && danEventa == DayOfWeek.Sunday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel9Sun.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sun9 = "Opis: " + c.Description;
                                label9Sun.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sun9 = "Beschreibung: " + c.Description;
                                label9Sun.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sun9 = "Description: " + c.Description;
                                label9Sun.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label9Sun.Show();
                    }
                    if (c.Time == labelTime10.Text && danEventa == DayOfWeek.Sunday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel10Sun.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sun10 = "Opis: " + c.Description;
                                label10Sun.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sun10 = "Beschreibung: " + c.Description;
                                label10Sun.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sun10 = "Description: " + c.Description;
                                label10Sun.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label10Sun.Show();
                    }
                    if (c.Time == labelTime11.Text && danEventa == DayOfWeek.Sunday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel11Sun.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sun11 = "Opis: " + c.Description;
                                label11Sun.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sun11 = "Beschreibung: " + c.Description;
                                label11Sun.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sun11 = "Description: " + c.Description;
                                label11Sun.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label11Sun.Show();
                    }
                    if (c.Time == labelTime12.Text && danEventa == DayOfWeek.Sunday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel12Sun.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sun12 = "Opis: " + c.Description;
                                label12Sun.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sun12 = "Beschreibung: " + c.Description;
                                label12Sun.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sun12 = "Description: " + c.Description;
                                label12Sun.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label12Sun.Show();
                    }
                    if (c.Time == labelTime13.Text && danEventa == DayOfWeek.Sunday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel13Sun.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sun13 = "Opis: " + c.Description;
                                label13Sun.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sun13 = "Beschreibung: " + c.Description;
                                label13Sun.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sun13 = "Description: " + c.Description;
                                label13Sun.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label13Sun.Show();
                    }
                    if (c.Time == labelTime14.Text && danEventa == DayOfWeek.Sunday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel14Sun.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sun14 = "Opis: " + c.Description;
                                label14Sun.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sun14 = "Beschreibung: " + c.Description;
                                label14Sun.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sun14 = "Description: " + c.Description;
                                label14Sun.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label14Sun.Show();
                    }
                    if (c.Time == labelTime15.Text && danEventa == DayOfWeek.Sunday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel15Sun.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sun15 = "Opis: " + c.Description;
                                label15Sun.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sun15 = "Beschreibung: " + c.Description;
                                label15Sun.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sun15 = "Description: " + c.Description;
                                label15Sun.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label15Sun.Show();
                    }
                    if (c.Time == labelTime16.Text && danEventa == DayOfWeek.Sunday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel16Sun.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sun16 = "Opis: " + c.Description;
                                label16Sun.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sun16 = "Beschreibung: " + c.Description;
                                label16Sun.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sun16 = "Description: " + c.Description;
                                label16Sun.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label16Sun.Show();
                    }
                    if (c.Time == labelTime17.Text && danEventa == DayOfWeek.Sunday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel17Sun.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sun17 = "Opis: " + c.Description;
                                label17Sun.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sun17 = "Beschreibung: " + c.Description;
                                label17Sun.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sun17 = "Description: " + c.Description;
                                label17Sun.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label17Sun.Show();
                    }
                    if (c.Time == labelTime18.Text && danEventa == DayOfWeek.Sunday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel18Sun.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sun18 = "Opis: " + c.Description;
                                label18Sun.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sun18 = "Beschreibung: " + c.Description;
                                label18Sun.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sun18 = "Description: " + c.Description;
                                label18Sun.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label18Sun.Show();
                    }
                    if (c.Time == labelTime19.Text && danEventa == DayOfWeek.Sunday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel19Sun.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sun19 = "Opis: " + c.Description;
                                label19Sun.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sun19 = "Beschreibung: " + c.Description;
                                label19Sun.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sun19 = "Description: " + c.Description;
                                label19Sun.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label19Sun.Show();
                    }
                    if (c.Time == labelTime20.Text && danEventa == DayOfWeek.Sunday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel20Sun.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sun20 = "Opis: " + c.Description;
                                label20Sun.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sun20 = "Beschreibung: " + c.Description;
                                label20Sun.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sun20 = "Description: " + c.Description;
                                label20Sun.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label20Sun.Show();
                    }
                    if (c.Time == labelTime21.Text && danEventa == DayOfWeek.Sunday
                        && (Int16.Parse((c.Week)) + c.Reacurring >= DataContainer.week) && (Int16.Parse((c.Week)) < DataContainer.week))
                    {
                        panel21Sun.BackColor = serviceClr;
                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                ToolTipsText.Sun21 = "Opis: " + c.Description;
                                label21Sun.Text = translatedService + " za\n" + c.Patient;
                                break;
                            case "de-DE":
                                ToolTipsText.Sun21 = "Beschreibung: " + c.Description;
                                label21Sun.Text = translatedService + " für\n" + c.Patient;
                                break;
                            default:
                                ToolTipsText.Sun21 = "Description: " + c.Description;
                                label21Sun.Text = c.Service + " for\n" + c.Patient;
                                break;
                        }
                        label21Sun.Show();
                    }
                }
			}
            ClinicEventsList = new List<ClinicEvent>();

            #endregion //dupikati reacurring eventova
        }

		//funkcija koja racuna koliko ima nedelja u godini
		public int getWeeksInYear(int year, CultureInfo ci)
		{
			DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
			DateTime date1 = new DateTime(year, 12, 31);
			return ci.Calendar.GetWeekOfYear(date1, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
		}

        //skakanje na druge stranice
        private void buttonPageListPatients_Click(object sender, EventArgs e)
        {
            FormListaPacijenata formListPat = new FormListaPacijenata(this);
            formListPat.Show();
            this.Hide();
        }

        private void buttonPageListDoctors_Click(object sender, EventArgs e)
        {
            FormListaDoktora formListDoc = new FormListaDoktora(this);
            formListDoc.Show();
            this.Hide();
        }

        private void buttonPageClinicStatistic_Click(object sender, EventArgs e)
        {
            FormStatistika formStat = new FormStatistika(this);
            formStat.Show();
            this.Hide();
        }

        #region Mouse hover za labels

        private void label8Mon_MouseHover(object sender, EventArgs e)
        {
            toolTipMon8.Show(ToolTipsText.Mon8, label8Mon);
        }

        private void label9Mon_MouseHover(object sender, EventArgs e)
        {
            toolTipMon9.Show(ToolTipsText.Mon9, label9Mon);
        }

        private void label10Mon_MouseHover(object sender, EventArgs e)
        {
            toolTipMon10.Show(ToolTipsText.Mon10, label10Mon);
        }

        private void label11Mon_MouseHover(object sender, EventArgs e)
        {
            toolTipMon11.Show(ToolTipsText.Mon11, label11Mon);
        }

        private void label12Mon_MouseHover(object sender, EventArgs e)
        {
            toolTipMon12.Show(ToolTipsText.Mon12, label12Mon);
        }

        private void label13Mon_MouseHover(object sender, EventArgs e)
        {
            toolTipMon13.Show(ToolTipsText.Mon13, label13Mon);
        }

        private void label14Mon_MouseHover(object sender, EventArgs e)
        {
            toolTipMon14.Show(ToolTipsText.Mon14, label14Mon);
        }

        private void label15Mon_MouseHover(object sender, EventArgs e)
        {
            toolTipMon15.Show(ToolTipsText.Mon15, label15Mon);
        }

        private void label16Mon_MouseHover(object sender, EventArgs e)
        {
            toolTipMon16.Show(ToolTipsText.Mon16, label16Mon);
        }

        private void label17Mon_MouseHover(object sender, EventArgs e)
        {
            toolTipMon17.Show(ToolTipsText.Mon17, label17Mon);
        }

        private void label18Mon_MouseHover(object sender, EventArgs e)
        {
            toolTipMon18.Show(ToolTipsText.Mon18, label18Mon);
        }

        private void label19Mon_MouseHover(object sender, EventArgs e)
        {
            toolTipMon19.Show(ToolTipsText.Mon19, label19Mon);
        }

        private void label20Mon_MouseHover(object sender, EventArgs e)
        {
            toolTipMon20.Show(ToolTipsText.Mon20, label20Mon);
        }

        private void label21Mon_MouseHover(object sender, EventArgs e)
        {
            toolTipMon21.Show(ToolTipsText.Mon21, label21Mon);
        }

        private void label8Tue_MouseHover(object sender, EventArgs e)
        {
            toolTipTue8.Show(ToolTipsText.Tue8, label8Tue);
        }

        private void label9Tue_MouseHover(object sender, EventArgs e)
        {
            toolTipTue9.Show(ToolTipsText.Tue9, label9Tue);
        }

        private void label10Tue_MouseHover(object sender, EventArgs e)
        {
            toolTipTue10.Show(ToolTipsText.Tue10, label10Tue);
        }

        private void label11Tue_MouseHover(object sender, EventArgs e)
        {
            toolTipTue11.Show(ToolTipsText.Tue11, label11Tue);
        }

        private void label12Tue_MouseHover(object sender, EventArgs e)
        {
            toolTipTue12.Show(ToolTipsText.Tue12, label12Tue);
        }

        private void label13Tue_MouseHover(object sender, EventArgs e)
        {
            toolTipTue13.Show(ToolTipsText.Tue13, label13Tue);
        }

        private void label14Tue_MouseHover(object sender, EventArgs e)
        {
            toolTipTue14.Show(ToolTipsText.Tue14, label14Tue);
        }

        private void label15Tue_MouseHover(object sender, EventArgs e)
        {
            toolTipTue15.Show(ToolTipsText.Tue15, label15Tue);
        }

        private void label16Tue_MouseHover(object sender, EventArgs e)
        {
            toolTipTue16.Show(ToolTipsText.Tue16, label16Tue);
        }

        private void label17Tue_MouseHover(object sender, EventArgs e)
        {
            toolTipTue17.Show(ToolTipsText.Tue17, label17Tue);
        }

        private void label18Tue_MouseHover(object sender, EventArgs e)
        {
            toolTipTue18.Show(ToolTipsText.Tue18, label18Tue);
        }

        private void label19Tue_MouseHover(object sender, EventArgs e)
        {
            toolTipTue19.Show(ToolTipsText.Tue19, label19Tue);
        }

        private void label20Tue_MouseHover(object sender, EventArgs e)
        {
            toolTipTue20.Show(ToolTipsText.Tue20, label20Tue);
        }

        private void label21Tue_MouseHover(object sender, EventArgs e)
        {
            toolTipTue21.Show(ToolTipsText.Tue21, label21Tue);
        }

        private void label8Wed_MouseHover(object sender, EventArgs e)
        {
            toolTipWed8.Show(ToolTipsText.Wed8, label8Wed);
        }

        private void label9Wed_MouseHover(object sender, EventArgs e)
        {
            toolTipWed9.Show(ToolTipsText.Wed9, label9Wed);
        }

        private void label10Wed_MouseHover(object sender, EventArgs e)
        {
            toolTipWed10.Show(ToolTipsText.Wed10, label10Wed);
        }

        private void label11Wed_MouseHover(object sender, EventArgs e)
        {
            toolTipWed11.Show(ToolTipsText.Wed11, label11Wed);
        }

        private void label12Wed_MouseHover(object sender, EventArgs e)
        {
            toolTipWed12.Show(ToolTipsText.Wed12, label12Wed);
        }

        private void label13Wed_MouseHover(object sender, EventArgs e)
        {
            toolTipWed13.Show(ToolTipsText.Wed13, label13Wed);
        }

        private void label14Wed_MouseHover(object sender, EventArgs e)
        {
            toolTipWed14.Show(ToolTipsText.Wed14, label14Wed);
        }

        private void label15Wed_MouseHover(object sender, EventArgs e)
        {
            toolTipWed15.Show(ToolTipsText.Wed15, label15Wed);
        }

        private void label16Wed_MouseHover(object sender, EventArgs e)
        {
            toolTipWed16.Show(ToolTipsText.Wed16, label16Wed);
        }

        private void label17Wed_MouseHover(object sender, EventArgs e)
        {
            toolTipWed17.Show(ToolTipsText.Wed17, label17Wed);
        }

        private void label18Wed_MouseHover(object sender, EventArgs e)
        {
            toolTipWed18.Show(ToolTipsText.Wed18, label18Wed);
        }

        private void label19Wed_MouseHover(object sender, EventArgs e)
        {
            toolTipWed19.Show(ToolTipsText.Wed19, label19Wed);
        }

        private void label20Wed_MouseHover(object sender, EventArgs e)
        {
            toolTipWed20.Show(ToolTipsText.Wed20, label20Wed);
        }

        private void label21Wed_MouseHover(object sender, EventArgs e)
        {
            toolTipWed21.Show(ToolTipsText.Wed21, label21Wed);
        }

        private void label8Thu_MouseHover(object sender, EventArgs e)
        {
            toolTipThu8.Show(ToolTipsText.Thu8, label8Thu);
        }

        private void label9Thu_MouseHover(object sender, EventArgs e)
        {
            toolTipThu9.Show(ToolTipsText.Thu9, label9Thu);
        }

        private void label10Thu_MouseHover(object sender, EventArgs e)
        {
            toolTipThu10.Show(ToolTipsText.Thu10, label10Thu);
        }

        private void label11Thu_MouseHover(object sender, EventArgs e)
        {
            toolTipThu11.Show(ToolTipsText.Thu11, label11Thu);
        }

        private void label12Thu_MouseHover(object sender, EventArgs e)
        {
            toolTipThu12.Show(ToolTipsText.Thu12, label12Thu);
        }

        private void label13Thu_MouseHover(object sender, EventArgs e)
        {
            toolTipThu13.Show(ToolTipsText.Thu13, label13Thu);
        }

        private void label14Thu_MouseHover(object sender, EventArgs e)
        {
            toolTipThu14.Show(ToolTipsText.Thu14, label14Thu);
        }

        private void label15Thu_MouseHover(object sender, EventArgs e)
        {
            toolTipThu15.Show(ToolTipsText.Thu15, label15Thu);
        }

        private void label16Thu_MouseHover(object sender, EventArgs e)
        {
            toolTipThu16.Show(ToolTipsText.Thu16, label16Thu);
        }

        private void label17Thu_MouseHover(object sender, EventArgs e)
        {
            toolTipThu17.Show(ToolTipsText.Thu17, label17Thu);
        }

        private void label18Thu_MouseHover(object sender, EventArgs e)
        {
            toolTipThu18.Show(ToolTipsText.Thu18, label18Thu);
        }

        private void label19Thu_MouseHover(object sender, EventArgs e)
        {
            toolTipThu19.Show(ToolTipsText.Thu19, label19Thu);
        }

        private void label20Thu_MouseHover(object sender, EventArgs e)
        {
            toolTipThu20.Show(ToolTipsText.Thu20, label20Thu);
        }

        private void label21Thu_MouseHover(object sender, EventArgs e)
        {
            toolTipThu21.Show(ToolTipsText.Thu21, label21Thu);
        }

        private void label8Fri_MouseHover(object sender, EventArgs e)
        {
            toolTipFri8.Show(ToolTipsText.Fri8, label8Fri);
        }

        private void label9Fri_MouseHover(object sender, EventArgs e)
        {
            toolTipFri9.Show(ToolTipsText.Fri9, label9Fri);
        }

        private void label10Fri_MouseHover(object sender, EventArgs e)
        {
            toolTipFri10.Show(ToolTipsText.Fri10, label10Fri);
        }

        private void label11Fri_MouseHover(object sender, EventArgs e)
        {
            toolTipFri11.Show(ToolTipsText.Fri11, label11Fri);
        }

        private void label12Fri_MouseHover(object sender, EventArgs e)
        {
            toolTipFri12.Show(ToolTipsText.Fri12, label12Fri);
        }

        private void label13Fri_MouseHover(object sender, EventArgs e)
        {
            toolTipFri13.Show(ToolTipsText.Fri13, label13Fri);
        }

        private void label14Fri_MouseHover(object sender, EventArgs e)
        {
            toolTipFri14.Show(ToolTipsText.Fri14, label14Fri);
        }

        private void label15Fri_MouseHover(object sender, EventArgs e)
        {
            toolTipFri15.Show(ToolTipsText.Fri15, label15Fri);
        }

        private void label16Fri_MouseHover(object sender, EventArgs e)
        {
            toolTipFri16.Show(ToolTipsText.Fri16, label16Fri);
        }

        private void label17Fri_MouseHover(object sender, EventArgs e)
        {
            toolTipFri17.Show(ToolTipsText.Fri17, label17Fri);
        }

        private void label18Fri_MouseHover(object sender, EventArgs e)
        {
            toolTipFri18.Show(ToolTipsText.Fri18, label18Fri);
        }

        private void label19Fri_MouseHover(object sender, EventArgs e)
        {
            toolTipFri19.Show(ToolTipsText.Fri19, label19Fri);
        }

        private void label20Fri_MouseHover(object sender, EventArgs e)
        {
            toolTipFri20.Show(ToolTipsText.Fri20, label20Fri);
        }

        private void label21Fri_MouseHover(object sender, EventArgs e)
        {
            toolTipFri21.Show(ToolTipsText.Fri21, label21Fri);
        }

        private void label8Sat_MouseHover(object sender, EventArgs e)
        {
            toolTipSat8.Show(ToolTipsText.Sat8, label8Sat);
        }

        private void label9Sat_MouseHover(object sender, EventArgs e)
        {
            toolTipSat9.Show(ToolTipsText.Sat9, label9Sat);
        }

        private void label10Sat_MouseHover(object sender, EventArgs e)
        {
            toolTipSat10.Show(ToolTipsText.Sat10, label10Sat);
        }

        private void label11Sat_MouseHover(object sender, EventArgs e)
        {
            toolTipSat11.Show(ToolTipsText.Sat11, label11Sat);
        }

        private void label12Sat_MouseHover(object sender, EventArgs e)
        {
            toolTipSat12.Show(ToolTipsText.Sat12, label12Sat);
        }

        private void label13Sat_MouseHover(object sender, EventArgs e)
        {
            toolTipSat13.Show(ToolTipsText.Sat13, label13Sat);
        }

        private void label14Sat_MouseHover(object sender, EventArgs e)
        {
            toolTipSat14.Show(ToolTipsText.Sat14, label14Sat);
        }

        private void label15Sat_MouseHover(object sender, EventArgs e)
        {
            toolTipSat15.Show(ToolTipsText.Sat15, label15Sat);
        }

        private void label16Sat_MouseHover(object sender, EventArgs e)
        {
            toolTipSat16.Show(ToolTipsText.Sat16, label16Sat);
        }

        private void label17Sat_MouseHover(object sender, EventArgs e)
        {
            toolTipSat17.Show(ToolTipsText.Sat17, label17Sat);
        }

        private void label18Sat_MouseHover(object sender, EventArgs e)
        {
            toolTipSat18.Show(ToolTipsText.Sat18, label18Sat);
        }

        private void label19Sat_MouseHover(object sender, EventArgs e)
        {
            toolTipSat19.Show(ToolTipsText.Sat19, label19Sat);
        }

        private void label20Sat_MouseHover(object sender, EventArgs e)
        {
            toolTipSat20.Show(ToolTipsText.Sat20, label20Sat);
        }

        private void label21Sat_MouseHover(object sender, EventArgs e)
        {
            toolTipSat21.Show(ToolTipsText.Sat21, label21Sat);
        }

        private void label8Sun_MouseHover(object sender, EventArgs e)
        {
            toolTipSun8.Show(ToolTipsText.Sun8, label8Sun);
        }

        private void label9Sun_MouseHover(object sender, EventArgs e)
        {
            toolTipSun9.Show(ToolTipsText.Sun9, label9Sun);
        }

        private void label10Sun_MouseHover(object sender, EventArgs e)
        {
            toolTipSun10.Show(ToolTipsText.Sun10, label10Sun);
        }

        private void label11Sun_MouseHover(object sender, EventArgs e)
        {
            toolTipSun11.Show(ToolTipsText.Sun11, label11Sun);
        }

        private void label12Sun_MouseHover(object sender, EventArgs e)
        {
            toolTipSun12.Show(ToolTipsText.Sun12, label12Sun);
        }

        private void label13Sun_MouseHover(object sender, EventArgs e)
        {
            toolTipSun13.Show(ToolTipsText.Sun13, label13Sun);
        }

        private void label14Sun_MouseHover(object sender, EventArgs e)
        {
            toolTipSun14.Show(ToolTipsText.Sun14, label14Sun);
        }

        private void label15Sun_MouseHover(object sender, EventArgs e)
        {
            toolTipSun15.Show(ToolTipsText.Sun15, label15Sun);
        }

        private void label16Sun_MouseHover(object sender, EventArgs e)
        {
            toolTipSun16.Show(ToolTipsText.Sun16, label16Sun);
        }

        private void label17Sun_MouseHover(object sender, EventArgs e)
        {
            toolTipSun17.Show(ToolTipsText.Sun17, label17Sun);
        }

        private void label18Sun_MouseHover(object sender, EventArgs e)
        {
            toolTipSun18.Show(ToolTipsText.Sun18, label18Sun);
        }

        private void label19Sun_MouseHover(object sender, EventArgs e)
        {
            toolTipSun19.Show(ToolTipsText.Sun19, label19Sun);
        }

        private void label20Sun_MouseHover(object sender, EventArgs e)
        {
            toolTipSun20.Show(ToolTipsText.Sun20, label20Sun);
        }

        private void label21Sun_MouseHover(object sender, EventArgs e)
        {
            toolTipSun21.Show(ToolTipsText.Sun21, label21Sun);
        }
        #endregion

        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelSchedulerTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        //Logout na ikonicu ili tekst pored
        private void iconLogout_Click(object sender, EventArgs e)
        {
            DialogResult res;
            switch (Thread.CurrentThread.CurrentUICulture.Name)
            {
                case "sr-Latn-CS":
                    res = MessageBox.Show("Da li ste sigurni da želite da se odjavite?", "Odjavi se", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    break;
                case "de-DE":
                    res = MessageBox.Show("Sie sind sicher, dass Sie ausloggen wollen?", "Ausloggen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    break;
                default:
                    res = MessageBox.Show("Are you sure you want to logout?", "Log out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    break;
            }
            
            if (res == DialogResult.Yes)
            {
                //praznimo podatke aktivnog doktora
                ActiveDoctor.username = "";
                ActiveDoctor.password = "";
                ActiveDoctor.id = 0;
                ActiveDoctor.specialization = "";
                ActiveDoctor.surgery = 0;
                ActiveDoctor.name = "";

                //vracamo se na Login formu
                FormLogin formLogin = new FormLogin();
                formLogin.Show();
                this.Hide();
            }
            else
            {
                return;
            }
        }

        private void labelLogout_Click(object sender, EventArgs e)
        {
            DialogResult res;

            switch (Thread.CurrentThread.CurrentUICulture.Name)
            {
                case "sr-Latn-CS":
                    res = MessageBox.Show("Da li ste sigurni da želite da se odjavite?", "Odjavi se", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    break;
                case "de-DE":
                    res = MessageBox.Show("Sie sind sicher, dass Sie ausloggen wollen?", "Ausloggen", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    break;
                default:
                    res = MessageBox.Show("Are you sure you want to logout?", "Log out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    break;
            }

            if (res == DialogResult.Yes)
            {
                //praznimo podatke aktivnog doktora
                ActiveDoctor.username = "";
                ActiveDoctor.password = "";
                ActiveDoctor.id = 0;
                ActiveDoctor.specialization = "";
                ActiveDoctor.surgery = 0;
                ActiveDoctor.name = "";

                //vracamo se na Login formu
                FormLogin formLogin = new FormLogin();
                formLogin.Show();
                this.Hide();
            }
            else
            {
                return;
            }
        }
    }
    //pomocni enumerator za dane
    public enum Day
	{
		Monday = 1,
		Tuesday,
		Wednesday,
		Thursday,
		Friday,
		Saturday,
		Sunday
	}

	//pomocna klasa koja pamti dan i vreme za aktivni panel
	public static class DataContainer
	{
		public static Day day;
		public static int time;
		public static String service;
		public static int week;
		public static int year;
	}

	public static class ToolTipsText
	{
		public static String Mon8;
		public static String Tue8;
		public static String Wed8;
		public static String Thu8;
		public static String Fri8;
		public static String Sat8;
		public static String Sun8;
		public static String Mon9;
		public static String Tue9;
		public static String Wed9;
		public static String Thu9;
		public static String Fri9;
		public static String Sat9;
		public static String Sun9;
		public static String Mon10;
		public static String Tue10;
		public static String Wed10;
		public static String Thu10;
		public static String Fri10;
		public static String Sat10;
		public static String Sun10;
		public static String Mon11;
		public static String Tue11;
		public static String Wed11;
		public static String Thu11;
		public static String Fri11;
		public static String Sat11;
		public static String Sun11;
		public static String Mon12;
		public static String Tue12;
		public static String Wed12;
		public static String Thu12;
		public static String Fri12;
		public static String Sat12;
		public static String Sun12;
		public static String Mon13;
		public static String Tue13;
		public static String Wed13;
		public static String Thu13;
		public static String Fri13;
		public static String Sat13;
		public static String Sun13;
		public static String Mon14;
		public static String Tue14;
		public static String Wed14;
		public static String Thu14;
		public static String Fri14;
		public static String Sat14;
		public static String Sun14;
		public static String Mon15;
		public static String Tue15;
		public static String Wed15;
		public static String Thu15;
		public static String Fri15;
		public static String Sat15;
		public static String Sun15;
		public static String Mon16;
		public static String Tue16;
		public static String Wed16;
		public static String Thu16;
		public static String Fri16;
		public static String Sat16;
		public static String Sun16;
		public static String Mon17;
		public static String Tue17;
		public static String Wed17;
		public static String Thu17;
		public static String Fri17;
		public static String Sat17;
		public static String Sun17;
		public static String Mon18;
		public static String Tue18;
		public static String Wed18;
		public static String Thu18;
		public static String Fri18;
		public static String Sat18;
		public static String Sun18;
		public static String Mon19;
		public static String Tue19;
		public static String Wed19;
		public static String Thu19;
		public static String Fri19;
		public static String Sat19;
		public static String Sun19;
		public static String Mon20;
		public static String Tue20;
		public static String Wed20;
		public static String Thu20;
		public static String Fri20;
		public static String Sat20;
		public static String Sun20;
		public static String Mon21;
		public static String Tue21;
		public static String Wed21;
		public static String Thu21;
		public static String Fri21;
		public static String Sat21;
		public static String Sun21;
	}
}
