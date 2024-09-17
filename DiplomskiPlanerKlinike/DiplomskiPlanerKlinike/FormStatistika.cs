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
using System.Windows.Forms.DataVisualization.Charting;
using DiplomskiPlanerKlinike.Views;
using DiplomskiPlanerKlinike.Data;
using DiplomskiPlanerKlinike.Models;
using DiplomskiPlanerKlinike.Presenters;
using FastMember;

namespace DiplomskiPlanerKlinike
{
    public partial class FormStatistika : Form, IStatistikaView
    {
        //implementacije interfejsa
        public event EventHandler GetAllEvents;

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

        public FormStatistika()
        {
            InitializeComponent();
        }

        private FormPlanerKlinike mainForm = null;

        public FormStatistika(Form callingForm)
        {
            mainForm = callingForm as FormPlanerKlinike;
            InitializeComponent();
            ClinicRepository clinicRepository = new ClinicRepository();
            new StatistikaPresenter(this, clinicRepository);

            //sklanjam difoltni TitleBar
            this.Text = String.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        private void FormStatistika_Load(object sender, EventArgs e)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentUICulture;
        }

        private void buttonSelectPagesStat_Click(object sender, EventArgs e)
        {
            if (panelSelectPagesStat.Height == 164)
            {
                panelSelectPagesStat.Height = 41;
            }
            else
            {
                panelSelectPagesStat.Height = 164;
            }
        }

        private void buttonSchedulerStat_Click(object sender, EventArgs e)
        {
            this.Dispose();
            mainForm.Show();
        }

        private void buttonListPatientsStat_Click(object sender, EventArgs e)
        {
            FormListaPacijenata formListPat = new FormListaPacijenata(this.mainForm);
            formListPat.Show();            
            this.Dispose();
        }

        private void buttonListDoctorsStat_Click(object sender, EventArgs e)
        {
            FormListaDoktora formListDoc = new FormListaDoktora(this.mainForm);
            formListDoc.Show();
            this.Dispose();
        }

        private void buttonChartGenerate_Click(object sender, EventArgs e)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentUICulture;

            var objChart = chartStat.ChartAreas[0];
            
            //x osa su meseci
            objChart.AxisX.IntervalType = DateTimeIntervalType.Number; //bilo je .Number umesto .Months
            objChart.AxisX.Minimum = 0;
            objChart.AxisX.Maximum = 12;
            objChart.AxisX.LabelStyle.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);

            //y osa je broj usluga
            objChart.AxisY.IntervalType = DateTimeIntervalType.Number;
            objChart.AxisY.Minimum = 0;
            objChart.AxisY.Maximum = 20; //TODO: promeni na vecu brojku
            objChart.AxisY.LabelStyle.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);

            chartStat.Series.Clear();

            //brojaci za svaku uslugu, duzine jednake broju meseca u godini
            int examTotal = 0;
            int labTotal = 0;
            int therapyTotal = 0;
            int operationTotal = 0;
            int checkupTotal = 0;
            var examCount = new int[12];
            var labCount = new int[12];
            var therapyCount = new int[12];
            var operationCount = new int[12];
            var checkupCount = new int[12];
            string[] months = { };

            //podesavamo chart
            switch (Thread.CurrentThread.CurrentUICulture.Name)
            {
                case "sr-Latn-CS":
                    chartStat.Series.Add("Pregledi");
                    //chartStat.Series["Examination"].Color = Color.AliceBlue;
                    chartStat.Series["Pregledi"].Color = Color.Blue;
                    chartStat.Series["Pregledi"].Legend = "Legend1";
                    chartStat.Series["Pregledi"].ChartArea = "ChartArea1";
                    chartStat.Series["Pregledi"].ChartType = SeriesChartType.Line;

                    chartStat.Series.Add("Laboratorije");
                    chartStat.Series["Laboratorije"].Color = Color.GreenYellow;
                    chartStat.Series["Laboratorije"].Legend = "Legend2";
                    chartStat.Series["Laboratorije"].ChartArea = "ChartArea1";
                    chartStat.Series["Laboratorije"].ChartType = SeriesChartType.Line;

                    chartStat.Series.Add("Terapije");
                    chartStat.Series["Terapije"].Color = Color.LightPink;
                    chartStat.Series["Terapije"].Legend = "Legend3";
                    chartStat.Series["Terapije"].ChartArea = "ChartArea1";
                    chartStat.Series["Terapije"].ChartType = SeriesChartType.Line;

                    chartStat.Series.Add("Operacije");
                    chartStat.Series["Operacije"].Color = Color.OrangeRed;
                    chartStat.Series["Operacije"].Legend = "Legend4";
                    chartStat.Series["Operacije"].ChartArea = "ChartArea1";
                    chartStat.Series["Operacije"].ChartType = SeriesChartType.Line;

                    chartStat.Series.Add("Kontrole");
                    chartStat.Series["Kontrole"].Color = Color.PowderBlue;
                    chartStat.Series["Kontrole"].Legend = "Legend5";
                    chartStat.Series["Kontrole"].ChartArea = "ChartArea1";
                    chartStat.Series["Kontrole"].ChartType = SeriesChartType.Line;

                    months = new string[12] { "Jan", "Febr", "Mart", "Apr", "Maj", "Jun", "Jul", "Avg", "Sep", "Okt", "Nov", "Dec" };
                    break;
                case "de-DE":
                    chartStat.Series.Add("Untersuchung");
                    //chartStat.Series["Examination"].Color = Color.AliceBlue;
                    chartStat.Series["Untersuchung"].Color = Color.Blue;
                    chartStat.Series["Untersuchung"].Legend = "Legend1";
                    chartStat.Series["Untersuchung"].ChartArea = "ChartArea1";
                    chartStat.Series["Untersuchung"].ChartType = SeriesChartType.Line;

                    chartStat.Series.Add("Labor");
                    chartStat.Series["Labor"].Color = Color.GreenYellow;
                    chartStat.Series["Labor"].Legend = "Legend2";
                    chartStat.Series["Labor"].ChartArea = "ChartArea1";
                    chartStat.Series["Labor"].ChartType = SeriesChartType.Line;

                    chartStat.Series.Add("Therapie");
                    chartStat.Series["Therapie"].Color = Color.LightPink;
                    chartStat.Series["Therapie"].Legend = "Legend3";
                    chartStat.Series["Therapie"].ChartArea = "ChartArea1";
                    chartStat.Series["Therapie"].ChartType = SeriesChartType.Line;

                    chartStat.Series.Add("Operation");
                    chartStat.Series["Operation"].Color = Color.OrangeRed;
                    chartStat.Series["Operation"].Legend = "Legend4";
                    chartStat.Series["Operation"].ChartArea = "ChartArea1";
                    chartStat.Series["Operation"].ChartType = SeriesChartType.Line;

                    chartStat.Series.Add("Kontroll");
                    chartStat.Series["Kontroll"].Color = Color.PowderBlue;
                    chartStat.Series["Kontroll"].Legend = "Legend5";
                    chartStat.Series["Kontroll"].ChartArea = "ChartArea1";
                    chartStat.Series["Kontroll"].ChartType = SeriesChartType.Line;

                    months = new string[12] { "Jan", "Feb", "März", "Apr", "Mai", "Juni", "Juli", "Aug", "Sept", "Okt", "Nov", "Dez" };
                    break;
                default:
                    chartStat.Series.Add("Examination");
                    //chartStat.Series["Examination"].Color = Color.AliceBlue;
                    chartStat.Series["Examination"].Color = Color.Blue;
                    chartStat.Series["Examination"].Legend = "Legend1";
                    chartStat.Series["Examination"].ChartArea = "ChartArea1";
                    chartStat.Series["Examination"].ChartType = SeriesChartType.Line;

                    chartStat.Series.Add("Laboratory");
                    chartStat.Series["Laboratory"].Color = Color.GreenYellow;
                    chartStat.Series["Laboratory"].Legend = "Legend2";
                    chartStat.Series["Laboratory"].ChartArea = "ChartArea1";
                    chartStat.Series["Laboratory"].ChartType = SeriesChartType.Line;

                    chartStat.Series.Add("Therapy");
                    chartStat.Series["Therapy"].Color = Color.LightPink;
                    chartStat.Series["Therapy"].Legend = "Legend3";
                    chartStat.Series["Therapy"].ChartArea = "ChartArea1";
                    chartStat.Series["Therapy"].ChartType = SeriesChartType.Line;

                    chartStat.Series.Add("Operation");
                    chartStat.Series["Operation"].Color = Color.OrangeRed;
                    chartStat.Series["Operation"].Legend = "Legend4";
                    chartStat.Series["Operation"].ChartArea = "ChartArea1";
                    chartStat.Series["Operation"].ChartType = SeriesChartType.Line;

                    chartStat.Series.Add("Check-up");
                    chartStat.Series["Check-up"].Color = Color.PowderBlue;
                    chartStat.Series["Check-up"].Legend = "Legend5";
                    chartStat.Series["Check-up"].ChartArea = "ChartArea1";
                    chartStat.Series["Check-up"].ChartType = SeriesChartType.Line;

                    months = new string[12] { "Jan", "Feb", "Mar", "Apr", "May", "June", "July", "Aug", "Sept", "Oct", "Nov", "Dec" };
                    break;
            }       

            //podesavamo da su meseci na x osi           
            Axis axisX = chartStat.ChartAreas[0].AxisX;
            double axisLabelPos = 0.5;
            for(int i = 0; i < months.Length; i++)
            {
                axisX.CustomLabels.Add(axisLabelPos, axisLabelPos + 1, months[i]);
                axisLabelPos = axisLabelPos + 1.0;
            }


            //vadimo eventove da bi imali podatke da crtamo na grafu
            GetAllEvents.Invoke(this, EventArgs.Empty);

            foreach(ClinicEvent ce in ClinicEventsList)
            {
                //uzimamo datum iz eventa i proveravamo da li je za aktuelnu godinu
                //ako jeste, povecavamo brojac za odgovarajucu uslugu
                String cie = ce.CultureInfo;
                CultureInfo cid = new CultureInfo(cie);
                String stringDate = ce.Date;
                DateTime holdDate = DateTime.ParseExact(stringDate.Substring(1, stringDate.Length - 2), " d. MMMM yyyy. ", cid, DateTimeStyles.NoCurrentDateDefault);

                if (holdDate.Year.ToString().Equals(""))
                {
                    switch (Thread.CurrentThread.CurrentUICulture.Name)
                    {
                        case "sr-Latn-CS":
                            MessageBox.Show("Dodajte godinu!");
                            break;
                        case "de-DE":
                            MessageBox.Show("Bitte fügen eine jahr!");
                            break;
                        default:
                            MessageBox.Show("Please add a year!");
                            break;
                    }                   
                    return;
                }

                if (holdDate.Year == Int16.Parse(textBoxChartYear.Text))
                {
                    for (int m = 0; m < 12; m++)
                    {
                        if (holdDate.Month == m + 1)
                        {
                            switch (ce.Service)
                            {
                                case "Examination":
                                    examCount[m] += 1;
                                    break;
                                case "Laboratory":
                                    labCount[m] += 1;
                                    break;
                                case "Therapy":
                                    therapyCount[m] += 1 + ce.Reacurring;
                                    break;
                                case "Operation":
                                    operationCount[m] += 1;
                                    break;
                                case "Check-up":
                                    checkupCount[m] += 1;
                                    break;
                            }
                        }
                    }
                }
            }

            //dodajemo gornju granicu grafu
            objChart.AxisY.Maximum = ClinicEventsList.Count / 2 + 50 - ((ClinicEventsList.Count/2)%50);

            //proveravamo i crtamo Examination
            if (checkBoxChartExam.Checked)
            {
                switch (Thread.CurrentThread.CurrentUICulture.Name)
                {
                    case "sr-Latn-CS":
                        chartStat.Series["Pregledi"].Points.AddXY(0, 0);
                        for (int i = 0; i < 12; i++)
                        {
                            examTotal += examCount[i];
                            chartStat.Series["Pregledi"].Points.AddXY(i + 1, examTotal);
                        }
                        break;
                    case "de-DE":
                        chartStat.Series["Untersuchung"].Points.AddXY(0, 0);
                        for (int i = 0; i < 12; i++)
                        {
                            examTotal += examCount[i];
                            chartStat.Series["Untersuchung"].Points.AddXY(i + 1, examTotal);
                        }
                        break;
                    default:
                        chartStat.Series["Examination"].Points.AddXY(0, 0);
                        for (int i = 0; i < 12; i++)
                        {
                            examTotal += examCount[i];
                            chartStat.Series["Examination"].Points.AddXY(i + 1, examTotal);
                        }
                        break;
                }              
            }

            //proveravamo i crtamo Laboratory
                    
            if (checkBoxChartLab.Checked)
            {
                switch (Thread.CurrentThread.CurrentUICulture.Name)
                {
                    case "sr-Latn-CS":
                        chartStat.Series["Laboratorije"].Points.AddXY(0, 0);
                        for (int i = 0; i < 12; i++)
                        {
                            labTotal += labCount[i];
                            chartStat.Series["Laboratorije"].Points.AddXY(i + 1, labTotal);
                        }
                        break;
                    case "de-DE":
                        chartStat.Series["Labor"].Points.AddXY(0, 0);
                        for (int i = 0; i < 12; i++)
                        {
                            labTotal += labCount[i];
                            chartStat.Series["Labor"].Points.AddXY(i + 1, labTotal);
                        }
                        break;
                    default:
                        chartStat.Series["Laboratory"].Points.AddXY(0, 0);
                        for (int i = 0; i < 12; i++)
                        {
                            labTotal += labCount[i];
                            chartStat.Series["Laboratory"].Points.AddXY(i + 1, labTotal);
                        }
                        break;
                }
            }

            //proveravamo i crtamo Therapy
            if (checkBoxChartTherapy.Checked)
            {
                switch (Thread.CurrentThread.CurrentUICulture.Name)
                {
                    case "sr-Latn-CS":
                        chartStat.Series["Terapije"].Points.AddXY(0, 0);
                        for (int i = 0; i < 12; i++)
                        {
                            therapyTotal += therapyCount[i];
                            chartStat.Series["Terapije"].Points.AddXY(i + 1, therapyTotal);
                        }
                        break;
                    case "de-DE":
                        chartStat.Series["Therapie"].Points.AddXY(0, 0);
                        for (int i = 0; i < 12; i++)
                        {
                            therapyTotal += therapyCount[i];
                            chartStat.Series["Therapie"].Points.AddXY(i + 1, therapyTotal);
                        }
                        break;
                    default:
                        chartStat.Series["Therapy"].Points.AddXY(0, 0);
                        for (int i = 0; i < 12; i++)
                        {
                            therapyTotal += therapyCount[i];
                            chartStat.Series["Therapy"].Points.AddXY(i + 1, therapyTotal);
                        }
                        break;
                }
            }

            //proveravamo i crtamo Operation
            if (checkBoxChartOperation.Checked)
            {
                switch (Thread.CurrentThread.CurrentUICulture.Name)
                {
                    case "sr-Latn-CS":
                        chartStat.Series["Operacije"].Points.AddXY(0, 0);
                        for (int i = 0; i < 12; i++)
                        {
                            operationTotal += operationCount[i];
                            chartStat.Series["Operacije"].Points.AddXY(i + 1, operationTotal);
                        }
                        break;
                    case "de-DE":
                        chartStat.Series["Operation"].Points.AddXY(0, 0);
                        for (int i = 0; i < 12; i++)
                        {
                            operationTotal += operationCount[i];
                            chartStat.Series["Operation"].Points.AddXY(i + 1, operationTotal);
                        }
                        break;
                    default:
                        chartStat.Series["Operation"].Points.AddXY(0, 0);
                        for (int i = 0; i < 12; i++)
                        {
                            operationTotal += operationCount[i];
                            chartStat.Series["Operation"].Points.AddXY(i + 1, operationTotal);
                        }
                        break;
                }                      
            }

            //proveravamo i crtamo Checkup
            if (checkBoxChartCheckup.Checked)
            {
                switch (Thread.CurrentThread.CurrentUICulture.Name)
                {
                    case "sr-Latn-CS":
                        chartStat.Series["Kontrole"].Points.AddXY(0, 0);
                        for (int i = 0; i < 12; i++)
                        {
                            checkupTotal += checkupCount[i];
                            chartStat.Series["Kontrole"].Points.AddXY(i + 1, checkupTotal);
                        }
                        break;
                    case "de-DE":
                        chartStat.Series["Kontroll"].Points.AddXY(0, 0);
                        for (int i = 0; i < 12; i++)
                        {
                            checkupTotal += checkupCount[i];
                            chartStat.Series["Kontroll"].Points.AddXY(i + 1, checkupTotal);
                        }
                        break;
                    default:
                        chartStat.Series["Check-up"].Points.AddXY(0, 0);
                        for (int i = 0; i < 12; i++)
                        {
                            checkupTotal += checkupCount[i];
                            chartStat.Series["Check-up"].Points.AddXY(i + 1, checkupTotal);
                        }
                        break;
                }                      
            }                                   
        }

        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelStatsTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void iconListDoctorLogout_Click(object sender, EventArgs e)
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

        private void labelListDoctorLogout_Click(object sender, EventArgs e)
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
}

