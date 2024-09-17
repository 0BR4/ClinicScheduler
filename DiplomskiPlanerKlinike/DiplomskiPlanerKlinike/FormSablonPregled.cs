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
    public partial class FormSablonPregled : Form, IPregledView
    {
        //implementacije interfejsa
        public event EventHandler GetAllEvents;
        public event EventHandler<ClinicEventSub> CreateEvent;

        public event EventHandler GetAllPatients;
        public event EventHandler<PatientSub> GetPatientByID;
        public event EventHandler<PatientSub> CreatePatient;

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

        //nastavak implementacije forme
        public FormSablonPregled()
        {
            InitializeComponent();
            ClinicRepository clinicRepository = new ClinicRepository();
            new PregledPresenter(this, clinicRepository);
        }

        private FormPlanerKlinike mainForm = null;
        
        public FormSablonPregled(Form callingForm)
        {
            mainForm = callingForm as FormPlanerKlinike;
            InitializeComponent();
            ClinicRepository clinicRepository = new ClinicRepository();
            new PregledPresenter(this, clinicRepository);

            //sklanjam difoltni TitleBar
            this.Text = String.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        private void FormSablonPregled_Load(object sender, EventArgs e)
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

            textBoxExamPatient.AutoCompleteCustomSource = nameSuggestion;
            textBoxExamPatient.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBoxExamPatient.AutoCompleteSource = AutoCompleteSource.CustomSource;

            textBoxExamJMBG.AutoCompleteCustomSource = idSuggestion;
            textBoxExamJMBG.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBoxExamJMBG.AutoCompleteSource = AutoCompleteSource.CustomSource;


            //uzimamo dan iz glavne forme
            switch (DataContainer.day.ToString())
            {
                case "Monday":
                case "Ponedeljak":
                case "Monntag":
                    textBoxExamDate.Text = this.mainForm.LabelMondayText;
                    break;
                case "Tuesday":
                case "Utorak":
                case "Dienstag":
                    textBoxExamDate.Text = this.mainForm.LabelTuesdayText;
                    break;
                case "Wednesday":
                case "Sreda":
                case "Mittwoch":
                    textBoxExamDate.Text = this.mainForm.LabelWednesdayText;
                    break;
                case "Thursday":
                case "Četvrtak":
                case "Donnerstag":
                    textBoxExamDate.Text = this.mainForm.LabelThursdayText;
                    break;
                case "Friday":
                case "Petak":
                case "Freitag":
                    textBoxExamDate.Text = this.mainForm.LabelFridayText;
                    break;
                case "Saturday":
                case "Subota":
                case "Samstag":
                    textBoxExamDate.Text = this.mainForm.LabelSaturdayText;
                    break;
                case "Sunday":
                case "Nedelja":
                case "Sonntag":
                    textBoxExamDate.Text = this.mainForm.LabelSundayText;
                    break;
            }

            //uzimamo vreme iz glavne forme
            TimeSpan result = TimeSpan.FromHours(DataContainer.time);
            textBoxExamTime.Text = result.ToString("hh':'mm");

            //uzimamo ulogovanog doktora iz glavne forme
            textBoxExamDoctor.Text = ActiveDoctor.name;

        }

        //cisti sva polja u pregled formi
        private void buttonExamClear_Click(object sender, EventArgs e)
        {
            textBoxExamPatient.Clear();
            textBoxExamJMBG.Clear();
            textBoxExamProblem.Clear();
            textBoxExamPatient.Focus();
        }

        //ubacuje nov event "pregled" u planer i gasi pregled formu
        private void buttonExamConfirm_Click(object sender, EventArgs e)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentUICulture;

            //proveravamo da li je pacijent zauzet za navedeno vreme i datum
            //(moguci scenario ako vise doktora pokusavaju da zakazu u isti termin istom pacijentu)
            GetAllEvents.Invoke(this, EventArgs.Empty);

            foreach (ClinicEvent c in ClinicEventsList)
            {
                if (c.Time == textBoxExamTime.Text && c.PatientId == Int32.Parse(textBoxExamJMBG.Text))
                {
                    String stringDate = c.Date;
                    //cid je CultureInfo koji je vazio kada je event bio kreiran
                    CultureInfo cid = new CultureInfo(c.CultureInfo);
                    DateTime datumEventa = DateTime.ParseExact(stringDate.Substring(1, stringDate.Length - 2), " d. MMMM yyyy. ", cid, DateTimeStyles.NoCurrentDateDefault);
                    String datumEventaUCi = datumEventa.ToString(" d. MMMM yyyy. ", ci);
                    datumEventaUCi = "[" + datumEventaUCi + "]";

                    if (datumEventaUCi == textBoxExamDate.Text)
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

            //ubacujemo novi event
            ClinicEvent ce = new ClinicEvent
            {
                Description = textBoxExamProblem.Text,
                Service = "Examination",
                Reacurring = 0,
                Patient = textBoxExamPatient.Text,
                Doctor = textBoxExamDoctor.Text,
                Time = textBoxExamTime.Text,
                Date = textBoxExamDate.Text,
                Week = DataContainer.week.ToString(),
                CultureInfo = ci.ToString(),
                PatientId = Int32.Parse(textBoxExamJMBG.Text),
                DoctorId = ActiveDoctor.id
            };

            CreateEvent.Invoke(this, new ClinicEventSub(ce));

            //ubacujemo novog pacijenta u patients, ako vec ne postoji
            GetPatientByID.Invoke(this, new PatientSub(Int32.Parse(textBoxExamJMBG.Text)));

            if(SelectedPatient.Jmbg == 0)
            {
                Patient patient = new Patient
                {
                    Jmbg = Int32.Parse(textBoxExamJMBG.Text),
                    Name = textBoxExamPatient.Text,
                    Phone = textBoxExamPhone.Text,
                    Code = new StringBuilder("0"),
                    Checked = 1,
                    Labed = 0,
                    Operated = 0,
                    Therapied = 0,
                    Controled = 0
                };

                CreatePatient.Invoke(this, new PatientSub(patient));                
            }
            SelectedPatient = new Patient();

            //ubacujemo nov odnos doktor-pacijent u clients, ako vec ne postoji
            GetClientDP.Invoke(this, new ClientSub(ActiveDoctor.id, Int32.Parse(textBoxExamJMBG.Text)));

            if (SelectedClient.PatientId == 0)
            {
                Client c = new Client
                {
                    PatientId = Int32.Parse(textBoxExamJMBG.Text),
                    DoctorId = ActiveDoctor.id
                };

                CreateClient.Invoke(this, new ClientSub(c));
                SelectedClient = new Client();
            }

            //kraj
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

        private void panelExamTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void labelExamExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            mainForm.Show();
            return;
        }

        private void iconExamExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            mainForm.Show();
            return;
        }
    }
}
