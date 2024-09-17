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
    public partial class FormSablonOperacija : Form, IOperacijaView
    {
        //implementacija interfejsa
        public event EventHandler GetAllEvents;
        public event EventHandler<ClinicEventSub> CreateEvent;

        public event EventHandler GetAllCodebook;

        public event EventHandler GetAllPatients;
        public event EventHandler<PatientSub> GetPatientByID;
        public event EventHandler<PatientSub> UpdatePatient;

        public event EventHandler<ClientSub> GetClientDP;      //get client by DoctorID PatientID
        public event EventHandler<ClientSub> CreateClient;

        public event EventHandler<HistorySub> CreateHistory;

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

        private List<Codebook> codebookList = new List<Codebook>();
        public List<Codebook> CodebookList
        {
            get
            {
                return codebookList;
            }
            set
            {
                codebookList = value;
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

        public FormSablonOperacija()
        {
            InitializeComponent();           
        }

        private FormPlanerKlinike mainForm = null;

        public FormSablonOperacija(Form callingForm)
        {
            mainForm = callingForm as FormPlanerKlinike;
            InitializeComponent();
            ClinicRepository clinicRepository = new ClinicRepository();
            new OperacijaPresenter(this, clinicRepository);

            //sklanjam difoltni TitleBar
            this.Text = String.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        private void FormSablonOperacija_Load(object sender, EventArgs e)
        {
            //kod da predlaze sifre, Name i ID
            GetAllCodebook.Invoke(this, EventArgs.Empty);

            GetAllPatients.Invoke(this, EventArgs.Empty);

            AutoCompleteStringCollection codeSuggestion = new AutoCompleteStringCollection();
            foreach (Codebook cb in CodebookList)
            {
                codeSuggestion.Add(cb.Code);
            }
            textBoxOperationCode.AutoCompleteCustomSource = codeSuggestion;
            textBoxOperationCode.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBoxOperationCode.AutoCompleteSource = AutoCompleteSource.CustomSource;

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

            textBoxOperationPatient.AutoCompleteCustomSource = nameSuggestion;
            textBoxOperationPatient.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBoxOperationPatient.AutoCompleteSource = AutoCompleteSource.CustomSource;

            textBoxOperationJMBG.AutoCompleteCustomSource = idSuggestion;
            textBoxOperationJMBG.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBoxOperationJMBG.AutoCompleteSource = AutoCompleteSource.CustomSource;

            //uzimamo dan iz glavne forme
            switch (DataContainer.day.ToString())
            {
                case "Monday":
                case "Ponedeljak":
                case "Monntag":
                    textBoxOperationDate.Text = this.mainForm.LabelMondayText;
                    break;
                case "Tuesday":
                case "Utorak":
                case "Dienstag":
                    textBoxOperationDate.Text = this.mainForm.LabelTuesdayText;
                    break;
                case "Wednesday":
                case "Sreda":
                case "Mittwoch":
                    textBoxOperationDate.Text = this.mainForm.LabelWednesdayText;
                    break;
                case "Thursday":
                case "Četvrtak":
                case "Donnerstag":
                    textBoxOperationDate.Text = this.mainForm.LabelThursdayText;
                    break;
                case "Friday":
                case "Petak":
                case "Freitag":
                    textBoxOperationDate.Text = this.mainForm.LabelFridayText;
                    break;
                case "Saturday":
                case "Subota":
                case "Samstag":
                    textBoxOperationDate.Text = this.mainForm.LabelSaturdayText;
                    break;
                case "Sunday":
                case "Nedelja":
                case "Sonntag":
                    textBoxOperationDate.Text = this.mainForm.LabelSundayText;
                    break;
            }

            //uzimamo vreme iz glavne forme
            TimeSpan result = TimeSpan.FromHours(DataContainer.time);
            textBoxOperationTime.Text = result.ToString("hh':'mm");

            //uzimamo ulogovanog doktora iz glavne forme
            textBoxOperationDoctor.Text = ActiveDoctor.name;
        }

        private void buttonOperationConfirm_Click(object sender, EventArgs e)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentUICulture;

            //proveravamo da li je pacijent zauzet za navedeno vreme i datum
            //(moguci scenario ako vise doktora pokusavaju da zakazu u isti termin istom pacijentu)
            GetAllEvents.Invoke(this, EventArgs.Empty);

            foreach (ClinicEvent ce in ClinicEventsList)
            {
                if (ce.PatientId == Int32.Parse(textBoxOperationJMBG.Text) && ce.Time == textBoxOperationTime.Text)
                {
                    String stringDate = ce.Date;
                    //cid je CultureInfo koji je vazio kada je event bio kreiran
                    CultureInfo cidPom = new CultureInfo(ce.CultureInfo);
                    DateTime datumEventa = DateTime.ParseExact(stringDate.Substring(1, stringDate.Length - 2), " d. MMMM yyyy. ", cidPom, DateTimeStyles.NoCurrentDateDefault);
                    String datumEventaUCi = datumEventa.ToString(" d. MMMM yyyy. ", ci);
                    datumEventaUCi = "[" + datumEventaUCi + "]";

                    if (datumEventaUCi == textBoxOperationDate.Text)
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

            //proveravamo dal je pacijent prevashodno zavrsio laboratoriju
            GetPatientByID.Invoke(this, new PatientSub(Int32.Parse(textBoxOperationJMBG.Text)));

            if (SelectedPatient.Labed != 1)
            {
                SelectedPatient = new Patient();

                switch (Thread.CurrentThread.CurrentUICulture.Name)
                {
                    case "sr-Latn-CS":
                        MessageBox.Show("Pacijent nije završio laboratoriju!");
                        break;
                    case "de-DE":
                        MessageBox.Show("Der Patient hat das Labor nicht abgeschlossen!");
                        break;
                    default:
                        MessageBox.Show("The patient did not complete the laboratory!");
                        break;
                }

                mainForm.Show();
                this.Dispose();
                return;
            }

            //proveravamo jel laboratorija pre nove operacije
            GetAllEvents.Invoke(this, EventArgs.Empty);

            ClinicEvent lastCEInList = ClinicEventsList.Where(element =>
            element.PatientId == Int32.Parse(textBoxOperationJMBG.Text) && element.Service == "Laboratory").LastOrDefault();

            CultureInfo cid = new CultureInfo(lastCEInList.CultureInfo);

            ////cuvamo vreme
            DateTime labTime = DateTime.ParseExact(lastCEInList.Time, "HH:mm", cid, DateTimeStyles.NoCurrentDateDefault);

            ////cuvamo datum
            String strDatumEventa = lastCEInList.Date;
            DateTime labDate = DateTime.ParseExact(strDatumEventa.Substring(1, strDatumEventa.Length - 2), " d. MMMM yyyy. ", cid, DateTimeStyles.NoCurrentDateDefault);

            if (labDate > DateTime.ParseExact(textBoxOperationDate.Text.Substring(1, textBoxOperationDate.Text.Length - 2), " d. MMMM yyyy. ", ci, DateTimeStyles.NoCurrentDateDefault))
            {
                switch (Thread.CurrentThread.CurrentUICulture.Name)
                {
                    case "sr-Latn-CS":
                        MessageBox.Show("Nemoguće je zakazati operaciju na datum koji je pre laboratorije!");
                        break;
                    case "de-DE":
                        MessageBox.Show("Es ist nicht möglich, einen Operationtermin vor der Labortermin zu vereinbaren!");
                        break;
                    default:
                        MessageBox.Show("It is impossible to schedule an operation on a date that is before the laboratory!");
                        break;
                }

                mainForm.Show();
                this.Dispose();
                return;
            }
            else if (labDate == DateTime.ParseExact(textBoxOperationDate.Text.Substring(1, textBoxOperationDate.Text.Length - 2), " d. MMMM yyyy. ", ci, DateTimeStyles.NoCurrentDateDefault) &&
                labTime > DateTime.ParseExact(textBoxOperationTime.Text, "HH:mm", ci, DateTimeStyles.NoCurrentDateDefault))
            {
                switch (Thread.CurrentThread.CurrentUICulture.Name)
                {
                    case "sr-Latn-CS":
                        MessageBox.Show("Nemoguće je zakazati operaciju u vreme koje je pre laboratorije!");
                        break;
                    case "de-DE":
                        MessageBox.Show("Es ist nicht möglich, eine Operation in den Stunden vor dem Labortermin zu vereinbaren!");
                        break;
                    default:
                        MessageBox.Show("It is impossible to schedule an operation in the hours before the laboratory!");
                        break;
                }

                mainForm.Show();
                this.Dispose();
                return;
            }

            //proveravamo jel dobra sifra
            if (!CodebookList.Any(cdbk => cdbk.Code.Equals(textBoxOperationCode.Text)))
            {
                switch (Thread.CurrentThread.CurrentUICulture.Name)
                {
                    case "sr-Latn-CS":
                        MessageBox.Show("Pogrešna šifra bolesti, probaj ponovo!");
                        break;
                    case "de-DE":
                        MessageBox.Show("Falscher Krankheitscode, bitte versuchen Sie es erneut!");
                        break;
                    default:
                        MessageBox.Show("Wrong disease code, please try again!");
                        break;
                }

                textBoxOperationCode.Clear();
                return;
            }

            //ubacujemo novi event            
            ClinicEvent ceAdd = new ClinicEvent
            {
                Description = textBoxOperationProblem.Text,
                Service = "Operation",
                Reacurring = 0,
                Patient = textBoxOperationPatient.Text,
                Doctor = textBoxOperationDoctor.Text,
                Time = textBoxOperationTime.Text,
                Date = textBoxOperationDate.Text,
                Week = DataContainer.week.ToString(),
                CultureInfo = ci.ToString(),
                PatientId = Int32.Parse(textBoxOperationJMBG.Text),
                DoctorId = ActiveDoctor.id
            };

            CreateEvent.Invoke(this, new ClinicEventSub(ceAdd));

            //apdejtujemo pacijenta, operated = 1 i sifru bolesti
            GetPatientByID.Invoke(this, new PatientSub(Int32.Parse(textBoxOperationJMBG.Text)));
            SelectedPatient.Operated = 1;
            SelectedPatient.Code = new StringBuilder(textBoxOperationCode.Text);

            UpdatePatient.Invoke(this, new PatientSub(SelectedPatient));
            SelectedPatient = new Patient();

            //ubacujemo nov odnos doktor-pacijent u clients, ako vec ne postoji
            GetClientDP.Invoke(this, new ClientSub(ActiveDoctor.id, Int32.Parse(textBoxOperationJMBG.Text)));

            if (SelectedClient.PatientId == 0)
            {
                Client c = new Client
                {
                    PatientId = Int32.Parse(textBoxOperationJMBG.Text),
                    DoctorId = ActiveDoctor.id
                };

                CreateClient.Invoke(this, new ClientSub(c));
            }
            SelectedClient = new Client();

            //ubacujemo nov podatak za pacijenta u history
            History historyAdd = new History
            {
                PatientId = Int32.Parse(textBoxOperationJMBG.Text),
                Code = textBoxOperationCode.Text,
                Service = "Operation",
                Time = textBoxOperationTime.Text,
                Date = textBoxOperationDate.Text
            };

            CreateHistory.Invoke(this, new HistorySub(historyAdd));

            //kraj
            mainForm.updatePlaner(ci.Calendar);
            mainForm.Show();
            this.Dispose();
            return;
        }

        private void buttonOperationClear_Click(object sender, EventArgs e)
        {
            textBoxOperationPatient.Clear();
            textBoxOperationJMBG.Clear();
            textBoxOperationProblem.Clear();
            textBoxOperationCode.Clear();
            textBoxOperationPatient.Focus();
        }

        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelOperationTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void labelOperationExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            mainForm.Show();
            return;
        }

        private void iconOperationExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            mainForm.Show();
            return;
        }
    }
}
