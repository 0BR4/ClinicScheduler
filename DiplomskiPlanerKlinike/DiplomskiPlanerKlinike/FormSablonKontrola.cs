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
    public partial class FormSablonKontrola : Form, IKontrolaView
    {
        //implementacija interfejsa
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

        public FormSablonKontrola()
        {
            InitializeComponent();
        }

        private FormPlanerKlinike mainForm = null;

        public FormSablonKontrola(Form callingForm)
        {
            mainForm = callingForm as FormPlanerKlinike;
            InitializeComponent();
            ClinicRepository clinicRepository = new ClinicRepository();
            new KontrolaPresenter(this, clinicRepository);

            //sklanjam difoltni TitleBar
            this.Text = String.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        private void FormSablonKontrola_Load(object sender, EventArgs e)
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

            textBoxCheckupPatient.AutoCompleteCustomSource = nameSuggestion;
            textBoxCheckupPatient.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBoxCheckupPatient.AutoCompleteSource = AutoCompleteSource.CustomSource;

            textBoxCheckupJMBG.AutoCompleteCustomSource = idSuggestion;
            textBoxCheckupJMBG.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBoxCheckupJMBG.AutoCompleteSource = AutoCompleteSource.CustomSource;


            //uzimamo dan iz glavne forme
            switch (DataContainer.day.ToString())
            {
                case "Monday":
                case "Ponedeljak":
                case "Monntag":
                    textBoxCheckupDate.Text = this.mainForm.LabelMondayText;
                    break;
                case "Tuesday":
                case "Utorak":
                case "Dienstag":
                    textBoxCheckupDate.Text = this.mainForm.LabelTuesdayText;
                    break;
                case "Wednesday":
                case "Sreda":
                case "Mittwoch":
                    textBoxCheckupDate.Text = this.mainForm.LabelWednesdayText;
                    break;
                case "Thursday":
                case "Četvrtak":
                case "Donnerstag":
                    textBoxCheckupDate.Text = this.mainForm.LabelThursdayText;
                    break;
                case "Friday":
                case "Petak":
                case "Freitag":
                    textBoxCheckupDate.Text = this.mainForm.LabelFridayText;
                    break;
                case "Saturday":
                case "Subota":
                case "Samstag":
                    textBoxCheckupDate.Text = this.mainForm.LabelSaturdayText;
                    break;
                case "Sunday":
                case "Nedelja":
                case "Sonntag":
                    textBoxCheckupDate.Text = this.mainForm.LabelSundayText;
                    break;
            }

            //uzimamo vreme iz glavne forme
            TimeSpan result = TimeSpan.FromHours(DataContainer.time);
            textBoxCheckupTime.Text = result.ToString("hh':'mm");

            //uzimamo ulogovanog doktora iz glavne forme
            textBoxCheckupDoctor.Text = ActiveDoctor.name;
        }

        private void buttonCheckupClear_Click(object sender, EventArgs e)
        {
            textBoxCheckupPatient.Clear();
            textBoxCheckupJMBG.Clear();
            textBoxCheckupProblem.Clear();
            textBoxCheckupNumber.Clear();
            textBoxCheckupPeriod.Clear();
            textBoxCheckupPatient.Focus();
        }

        private void buttonCheckupConfirm_Click(object sender, EventArgs e)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentUICulture;

            //proveravamo da li je pacijent zauzet za navedeno vreme i datum
            //(moguci scenario ako vise doktora pokusavaju da zakazu u isti termin istom pacijentu)
            GetAllEvents.Invoke(this, EventArgs.Empty);

            foreach (ClinicEvent ce in ClinicEventsList)
            {
                if (ce.PatientId == Int32.Parse(textBoxCheckupJMBG.Text) && ce.Time == textBoxCheckupTime.Text)
                {
                    String stringDate = ce.Date;
                    //cid je CultureInfo koji je vazio kada je event bio kreiran
                    CultureInfo cidPom = new CultureInfo(ce.CultureInfo);
                    DateTime datumEventa = DateTime.ParseExact(stringDate.Substring(1, stringDate.Length - 2), " d. MMMM yyyy. ", cidPom, DateTimeStyles.NoCurrentDateDefault);
                    String datumEventaUCi = datumEventa.ToString(" d. MMMM yyyy. ", ci);
                    datumEventaUCi = "[" + datumEventaUCi + "]";

                    if (datumEventaUCi == textBoxCheckupDate.Text)
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

            //proveravamo dal je pacijent prevashodno imao operaciju ili terapiju
            GetPatientByID.Invoke(this, new PatientSub(Int32.Parse(textBoxCheckupJMBG.Text)));

            if (!(SelectedPatient.Therapied == 1 || SelectedPatient.Operated == 1))
            {
                SelectedPatient = new Patient();

                switch (Thread.CurrentThread.CurrentUICulture.Name)
                {
                    case "sr-Latn-CS":
                        MessageBox.Show("Pacijent nije imao niti terapiju, niti operaciju!");
                        break;
                    case "de-DE":
                        MessageBox.Show("Der Patient hatte weder eine Therapie noch eine Operation!");
                        break;
                    default:
                        MessageBox.Show("The patient had neither a therapy nor a surgery!");
                        break;
                }

                mainForm.Show();
                this.Dispose();
                return;
            }

            //proveravamo jel operacija il terapija pre nove kontrole
            GetAllEvents.Invoke(this, EventArgs.Empty);

            ClinicEvent lastCEInList = ClinicEventsList.Where(element =>
            element.PatientId == Int32.Parse(textBoxCheckupJMBG.Text) && (element.Service == "Therapy" || element.Service == "Operation")).LastOrDefault();

            CultureInfo cid = new CultureInfo(lastCEInList.CultureInfo);

            ////cuvamo vreme
            DateTime eventTime = DateTime.ParseExact(lastCEInList.Time, "HH:mm", cid, DateTimeStyles.NoCurrentDateDefault);

            ////cuvamo datum
            String strDatumEventa = lastCEInList.Date;
            DateTime eventDate = DateTime.ParseExact(strDatumEventa.Substring(1, strDatumEventa.Length - 2), " d. MMMM yyyy. ", cid, DateTimeStyles.NoCurrentDateDefault);

            if (eventDate > DateTime.ParseExact(textBoxCheckupDate.Text.Substring(1, textBoxCheckupDate.Text.Length - 2), " d. MMMM yyyy. ", ci, DateTimeStyles.NoCurrentDateDefault))
            {
                switch (Thread.CurrentThread.CurrentUICulture.Name)
                {
                    case "sr-Latn-CS":
                        MessageBox.Show("Nemoguće je zakazati kontrolu na datum koji je pre operacije ili terapije!");
                        break;
                    case "de-DE":
                        MessageBox.Show("Es ist nicht möglich, eine Kontrolltermin vor einer Operation oder Therapie zu vereinbaren!");
                        break;
                    default:
                        MessageBox.Show("It is impossible to schedule a control on a date that is before a surgery or a therapy!");
                        break;
                }

                mainForm.Show();
                this.Dispose();
                return;
            }
            else if (eventDate == DateTime.ParseExact(textBoxCheckupDate.Text.Substring(1, textBoxCheckupDate.Text.Length - 2), " d. MMMM yyyy. ", ci, DateTimeStyles.NoCurrentDateDefault) &&
                eventTime > DateTime.ParseExact(textBoxCheckupTime.Text, "HH:mm", ci, DateTimeStyles.NoCurrentDateDefault))
            {
                switch (Thread.CurrentThread.CurrentUICulture.Name)
                {
                    case "sr-Latn-CS":
                        MessageBox.Show("Nemoguće je zakazati kontrolu u vreme koje je pre operacije ili terapije!");
                        break;
                    case "de-DE":
                        MessageBox.Show("Es ist nicht möglich, eine Kontrolle in den Stunden vor der Operation oder Therapie zu vereinbaren!");
                        break;
                    default:
                        MessageBox.Show("It is impossible to schedule a control in the hours before the operation or therapy!");
                        break;
                }

                mainForm.Show();
                this.Dispose();
                return;
            }

            //ubacujemo novi event (kontrolu)       
            ClinicEvent ceAdd = new ClinicEvent
            {
                Description = textBoxCheckupProblem.Text,
                Service = "Check-up",
                Reacurring = 0,
                Patient = textBoxCheckupPatient.Text,
                Doctor = textBoxCheckupDoctor.Text,
                Time = textBoxCheckupTime.Text,
                Date = textBoxCheckupDate.Text,
                Week = DataContainer.week.ToString(),
                CultureInfo = ci.ToString(),
                PatientId = Int32.Parse(textBoxCheckupJMBG.Text),
                DoctorId = ActiveDoctor.id
            };

            CreateEvent.Invoke(this, new ClinicEventSub(ceAdd));

            //ubacujemo i ostale eventove (kontrole)
            int weekToAdd = DataContainer.week;
            String dateToAdd = textBoxCheckupDate.Text;
            for (int i = 0; i < Int16.Parse(textBoxCheckupNumber.Text); i++)
            {
                //datum prepravljamo
                DateTime dateToAddDT = DateTime.Parse(dateToAdd.Substring(1, dateToAdd.Length - 2), ci, DateTimeStyles.NoCurrentDateDefault);
                var novDatum = dateToAddDT.AddDays(7 * Int16.Parse(textBoxCheckupPeriod.Text)).ToString(" d. MMMM yyyy. ", ci);
                dateToAdd = "[" + novDatum + "]";
                //nedelju prepravljamo
                weekToAdd += Int16.Parse(textBoxCheckupPeriod.Text);

                ClinicEvent ceRAdd = new ClinicEvent
                {
                    Description = textBoxCheckupProblem.Text,
                    Service = "Check-up",
                    Reacurring = 0,
                    Patient = textBoxCheckupPatient.Text,
                    Doctor = textBoxCheckupDoctor.Text,
                    Time = textBoxCheckupTime.Text,
                    Date = dateToAdd,
                    Week = weekToAdd.ToString(),
                    CultureInfo = ci.ToString(),
                    PatientId = Int32.Parse(textBoxCheckupJMBG.Text),
                    DoctorId = ActiveDoctor.id
                };

                CreateEvent.Invoke(this, new ClinicEventSub(ceRAdd));
            }

            //apdejtujemo pacijenta, controled = 1
            GetPatientByID.Invoke(this, new PatientSub(Int32.Parse(textBoxCheckupJMBG.Text)));
            SelectedPatient.Operated = 1;

            UpdatePatient.Invoke(this, new PatientSub(SelectedPatient));
            SelectedPatient = new Patient();

            //ubacujemo nov odnos doktor-pacijent u clients, ako vec ne postoji
            GetClientDP.Invoke(this, new ClientSub(ActiveDoctor.id, Int32.Parse(textBoxCheckupJMBG.Text)));

            if (SelectedClient.PatientId == 0)
            {
                Client c = new Client
                {
                    PatientId = Int32.Parse(textBoxCheckupJMBG.Text),
                    DoctorId = ActiveDoctor.id
                };

                CreateClient.Invoke(this, new ClientSub(c));
            }
            SelectedClient = new Client();

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

        private void panelCheckupTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void iconCheckupExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            mainForm.Show();
            return;
        }

        private void labelCheckupExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            mainForm.Show();
            return;
        }
    }
}
