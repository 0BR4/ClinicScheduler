using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class FormSablonLab : Form, ILabView
    {
        //implementacije interfejsa
        public event EventHandler GetAllEvents;
        public event EventHandler<ClinicEventSub> CreateEvent;

        public event EventHandler GetAllPatients;
        public event EventHandler<PatientSub> GetPatientByID;
        public event EventHandler<PatientSub> UpdatePatient;

        public event EventHandler<ClientSub> GetClientDP;      //get client by DoctorID PatientID
        public event EventHandler<ClientSub> CreateClient;

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

        private List<Patient> patientsList = new List<Patient>();
        public List<Patient> PatientsList
        {
            get
            {
                return patientsList;
            }
            set
            {
                patientsList = value;
            }
        }

        //za Name suggestion
        private List<String> patientNames = new List<String>();
        public List<String> PatientNames
        {
            get
            {
                return patientNames;
            }
            set
            {
                patientNames = value;
            }
        }

        //za ID suggestion
        private List<String> patientIds = new List<String>();
        public List<String> PatientIds
        {
            get
            {
                return patientIds;
            }
            set
            {
                patientIds = value;
            }
        }

        private Patient selectedPatient = new Patient();
        public Patient SelectedPatient
        {
            get
            {
                return selectedPatient;
            }
            set
            {
                selectedPatient = value;
            }
        }

        private Client selectedClient = new Client();
        public Client SelectedClient
        {
            get
            {
                return selectedClient;
            }
            set
            {
                selectedClient = value;
            }
        }

        public FormSablonLab()
        {
            InitializeComponent();
        }

        private FormPlanerKlinike mainForm = null;

        public FormSablonLab(Form callingForm)
        {
            mainForm = callingForm as FormPlanerKlinike;
            InitializeComponent();
            ClinicRepository clinicRepository = new ClinicRepository();
            new LabPresenter(this, clinicRepository);

            //sklanjam difoltni TitleBar
            this.Text = String.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        private void FormSablonLab_Load(object sender, EventArgs e)
        {
            //kod da predlaze Name i ID pacijenata
            GetAllPatients.Invoke(this, EventArgs.Empty);

            AutoCompleteStringCollection nameSuggestion = new AutoCompleteStringCollection();
            AutoCompleteStringCollection idSuggestion = new AutoCompleteStringCollection();

            foreach (Patient p in PatientsList)
            {
                PatientNames.Add(p.Name);
                PatientIds.Add(p.Jmbg.ToString());
            }

            PatientNames = PatientNames.Distinct().ToList();
            PatientIds = PatientIds.Distinct().ToList();

            foreach (String pn in PatientNames)
                nameSuggestion.Add(pn);

            foreach (String pi in PatientIds)
                idSuggestion.Add(pi);

            textBoxLabPatient.AutoCompleteCustomSource = nameSuggestion;
            textBoxLabPatient.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBoxLabPatient.AutoCompleteSource = AutoCompleteSource.CustomSource;

            textBoxLabJMBG.AutoCompleteCustomSource = idSuggestion;
            textBoxLabJMBG.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBoxLabJMBG.AutoCompleteSource = AutoCompleteSource.CustomSource;

            //uzimamo dan iz glavne forme
            switch (DataContainer.day.ToString())
            {
                case "Monday":
                case "Ponedeljak":
                case "Monntag":
                    textBoxLabDate.Text = this.mainForm.LabelMondayText;
                    break;
                case "Tuesday":
                case "Utorak":
                case "Dienstag":
                    textBoxLabDate.Text = this.mainForm.LabelTuesdayText;
                    break;
                case "Wednesday":
                case "Sreda":
                case "Mittwoch":
                    textBoxLabDate.Text = this.mainForm.LabelWednesdayText;
                    break;
                case "Thursday":
                case "Četvrtak":
                case "Donnerstag":
                    textBoxLabDate.Text = this.mainForm.LabelThursdayText;
                    break;
                case "Friday":
                case "Petak":
                case "Freitag":
                    textBoxLabDate.Text = this.mainForm.LabelFridayText;
                    break;
                case "Saturday":
                case "Subota":
                case "Samstag":
                    textBoxLabDate.Text = this.mainForm.LabelSaturdayText;
                    break;
                case "Sunday":
                case "Nedelja":
                case "Sonntag":
                    textBoxLabDate.Text = this.mainForm.LabelSundayText;
                    break;
            }

            //uzimamo vreme iz glavne forme
            TimeSpan result = TimeSpan.FromHours(DataContainer.time);
            textBoxLabTime.Text = result.ToString("hh':'mm");

            //uzimamo ulogovanog doktora iz glavne forme
            textBoxLabDoctor.Text = ActiveDoctor.name;
        }

        private void buttonLabClear_Click(object sender, EventArgs e)
        {
            textBoxLabPatient.Clear();
            textBoxLabJMBG.Clear();
            textBoxLabProblem.Clear();
            textBoxLabPatient.Focus();
        }

        private void buttonLabConfirm_Click(object sender, EventArgs e)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentUICulture;

            //proveravamo da li je pacijent zauzet za navedeno vreme i datum
            //(moguci scenario ako vise doktora pokusavaju da zakazu u isti termin istom pacijentu)
            GetAllEvents.Invoke(this, EventArgs.Empty);

            foreach (ClinicEvent ce in ClinicEventsList)
            {
                if(ce.PatientId == Int32.Parse(textBoxLabJMBG.Text) && ce.Time == textBoxLabTime.Text)
                {
                    String stringDate = ce.Date;
                    //cid je CultureInfo koji je vazio kada je event bio kreiran
                    CultureInfo cidPom = new CultureInfo(ce.CultureInfo);
                    DateTime datumEventa = DateTime.ParseExact(stringDate.Substring(1, stringDate.Length - 2), " d. MMMM yyyy. ", cidPom, DateTimeStyles.NoCurrentDateDefault);
                    String datumEventaUCi = datumEventa.ToString(" d. MMMM yyyy. ", ci);
                    datumEventaUCi = "[" + datumEventaUCi + "]";

                    if (datumEventaUCi == textBoxLabDate.Text)
                    {
                        ClinicEventsList = new List<ClinicEvent>();

                        switch (Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "sr-Latn-CS":
                                MessageBox.Show("Pacijent je zauzet u datom terminu!");
                                break;
                            case "de-DE":
                                MessageBox.Show("Der Patient ist im angegebenen Termin beschäftigt!");
                                break;
                            default:
                                MessageBox.Show("The patient is busy in the given appointment!");
                                break;
                        }

                        mainForm.Show();
                        this.Dispose();
                        return;
                    }
                }
            }
            ClinicEventsList = new List<ClinicEvent>();

            //proveravamo jel je pacijent pregledan
            GetPatientByID.Invoke(this, new PatientSub(Int32.Parse(textBoxLabJMBG.Text)));

            if(SelectedPatient.Checked != 1)
            {
                SelectedPatient = new Patient();

                switch (Thread.CurrentThread.CurrentUICulture.Name)
                {
                    case "sr-Latn-CS":
                        MessageBox.Show("Pacijent nije pregledan!");
                        break;
                    case "de-DE":
                        MessageBox.Show("Der Patient wurde nicht untersucht!");
                        break;
                    default:
                        MessageBox.Show("The patient was not examined!");
                        break;
                }

                mainForm.Show();
                this.Dispose();
                return;
            }

            //proveravamo jel je pregled pre laboratorije
            GetAllEvents.Invoke(this, EventArgs.Empty);

            ClinicEvent lastCEInList = ClinicEventsList.Where(element =>
            element.PatientId == Int32.Parse(textBoxLabJMBG.Text) && element.Service == "Examination").LastOrDefault();

            CultureInfo cid = new CultureInfo(lastCEInList.CultureInfo);

            DateTime examTime = DateTime.ParseExact(lastCEInList.Time, "HH:mm", cid, DateTimeStyles.NoCurrentDateDefault);

            ////cuvamo datume           
            String strDatumEventa = lastCEInList.Date;
            DateTime examDate = DateTime.ParseExact(strDatumEventa.Substring(1, strDatumEventa.Length - 2), " d. MMMM yyyy. ", cid, DateTimeStyles.NoCurrentDateDefault);

            if (examDate > DateTime.ParseExact(textBoxLabDate.Text.Substring(1, textBoxLabDate.Text.Length - 2), " d. MMMM yyyy. ", ci, DateTimeStyles.NoCurrentDateDefault))
            {
                switch (Thread.CurrentThread.CurrentUICulture.Name)
                {
                    case "sr-Latn-CS":
                        MessageBox.Show("Nemoguće je zakazati laboratoriju na datum koji je pre pregleda!");
                        break;
                    case "de-DE":
                        MessageBox.Show("Es ist nicht möglich, einen Labortermin vor der Untersuchung zu vereinbaren!");
                        break;
                    default:
                        MessageBox.Show("It is impossible to schedule a laboratory on a date that is before the examination!");
                        break;
                }

                mainForm.Show();
                this.Dispose();
                return;
            }
            else if (examDate == DateTime.ParseExact(textBoxLabDate.Text.Substring(1, textBoxLabDate.Text.Length - 2), " d. MMMM yyyy. ", ci, DateTimeStyles.NoCurrentDateDefault) &&
               examTime > DateTime.ParseExact(textBoxLabTime.Text, "HH:mm", ci, DateTimeStyles.NoCurrentDateDefault))
            {
                switch (Thread.CurrentThread.CurrentUICulture.Name)
                {
                    case "sr-Latn-CS":
                        MessageBox.Show("Nemoguće je zakazati laboratoriju u vreme koje je pre pregleda!");
                        break;
                    case "de-DE":
                        MessageBox.Show("Es ist nicht möglich, eine Labortermin in den Stunden vor dem Untersuchung zu vereinbaren!");
                        break;
                    default:
                        MessageBox.Show("It is impossible to schedule a laboratory in the hours before the examination!");
                        break;
                }

                mainForm.Show();
                this.Dispose();
                return;
            }

            //ubacujemo novi event
            ClinicEvent ceAdd = new ClinicEvent
            {
                Description = textBoxLabProblem.Text,
                Service = "Laboratory",
                Reacurring = 0,
                Patient = textBoxLabPatient.Text,
                Doctor = textBoxLabDoctor.Text,
                Time = textBoxLabTime.Text,
                Date = textBoxLabDate.Text,
                Week = DataContainer.week.ToString(),
                CultureInfo = ci.ToString(),
                PatientId = Int32.Parse(textBoxLabJMBG.Text),
                DoctorId = ActiveDoctor.id
            };

            CreateEvent.Invoke(this, new ClinicEventSub(ceAdd));

            //apdejtujemo pacijenta, labed = 1
            GetPatientByID.Invoke(this, new PatientSub(Int32.Parse(textBoxLabJMBG.Text)));
            SelectedPatient.Labed = 1;

            UpdatePatient.Invoke(this, new PatientSub(SelectedPatient));
            SelectedPatient = new Patient();

            //ubacujemo nov odnos doktor-pacijent u clients, ako vec ne postoji
            GetClientDP.Invoke(this, new ClientSub(ActiveDoctor.id, Int32.Parse(textBoxLabJMBG.Text)));

            if (SelectedClient.PatientId == 0)
            {
                Client c = new Client
                {
                    PatientId = Int32.Parse(textBoxLabJMBG.Text),
                    DoctorId = ActiveDoctor.id
                };

                CreateClient.Invoke(this, new ClientSub(c));              
            }
            SelectedClient = new Client();

            mainForm.updatePlaner(ci.Calendar);
            mainForm.Show();
            this.Dispose();
            return;
        }

        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelLabTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void iconLabExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            mainForm.Show();
            return;
        }

        private void labelLabExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            mainForm.Show();
            return;
        }
    }
}
