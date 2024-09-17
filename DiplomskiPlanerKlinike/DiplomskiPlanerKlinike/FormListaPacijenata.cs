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
using DiplomskiPlanerKlinike.Models;
using DiplomskiPlanerKlinike.Presenters;
using FastMember;

namespace DiplomskiPlanerKlinike
{
    public partial class FormListaPacijenata : Form, IListaPacijenataView
    {
        //implementacije interfejsa
        public event EventHandler GetAllPatients;

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

        public FormListaPacijenata()
        {
            InitializeComponent();
        }

        private FormPlanerKlinike mainForm = null;

        public FormListaPacijenata(Form callingForm)
        {
            mainForm = callingForm as FormPlanerKlinike;
            InitializeComponent();
            ClinicRepository clinicRepository = new ClinicRepository();
            new ListaPacijenataPresenter(this, clinicRepository);

            //sklanjam difoltni TitleBar
            this.Text = String.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        DataTable dtable;
        DataView dv;

        private void FormListaPacijenata_Load(object sender, EventArgs e)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentUICulture;

            GetAllPatients.Invoke(this, EventArgs.Empty);
            dtable = new DataTable();

            switch (Thread.CurrentThread.CurrentUICulture.Name)
            {
                case "sr-Latn-CS":
                    using (var reader = ObjectReader.Create(PatientsList, "Jmbg", "Name", "Phone", "Code", "Checked", "Labed", "Therapied", "Operated", "Controled"))
                    {
                        dtable.Load(reader);
                        dtable.Columns["Jmbg"].ColumnName = "JMBG";
                        dtable.Columns["Name"].ColumnName = "Ime";
                        dtable.Columns["Phone"].ColumnName = "Telefon";
                        dtable.Columns["Code"].ColumnName = "Šifra";
                        dtable.Columns["Checked"].ColumnName = "Pregled";
                        dtable.Columns["Labed"].ColumnName = "Laboratorija";
                        dtable.Columns["Therapied"].ColumnName = "Terapija";
                        dtable.Columns["Operated"].ColumnName = "Operacija";
                        dtable.Columns["Controled"].ColumnName = "Kontrola";
                    }
                    break;
                case "de-DE":                 
                    using (var reader = ObjectReader.Create(PatientsList, "Jmbg", "Name", "Phone", "Code", "Checked", "Labed", "Therapied", "Operated", "Controled"))
                    {
                        dtable.Load(reader);
                        dtable.Load(reader);
                        dtable.Columns["Jmbg"].ColumnName = "ID";
                        dtable.Columns["Phone"].ColumnName = "Telefon";
                        dtable.Columns["Checked"].ColumnName = "Untersuchung";
                        dtable.Columns["Labed"].ColumnName = "Labor";
                        dtable.Columns["Therapied"].ColumnName = "Therapie";
                        dtable.Columns["Operated"].ColumnName = "Operation";
                        dtable.Columns["Controled"].ColumnName = "Kontroll";
                    }
                    break;
                default:
                    using (var reader = ObjectReader.Create(PatientsList, "Jmbg", "Name", "Phone", "Code", "Checked", "Labed", "Therapied", "Operated", "Controled"))
                    {
                        dtable.Load(reader);
                        dtable.Load(reader);
                        dtable.Load(reader);
                        dtable.Columns["Jmbg"].ColumnName = "ID";
                        dtable.Columns["Checked"].ColumnName = "Examination";
                        dtable.Columns["Labed"].ColumnName = "Laboratory";
                        dtable.Columns["Therapied"].ColumnName = "Therapy";
                        dtable.Columns["Operated"].ColumnName = "Operation";
                        dtable.Columns["Controled"].ColumnName = "CheckUp";
                    }
                    break;
            }

            dataGridViewPatients.DataSource = dtable;
            dv = dtable.DefaultView;

            //resize za ime
            DataGridViewColumn columnName = dataGridViewPatients.Columns[1];
            DataGridViewColumn columnPhone = dataGridViewPatients.Columns[2];
            columnName.Width = 140;
            columnPhone.Width = 140;
        }

        private void buttonSelectPagesListPat_Click(object sender, EventArgs e)
        {
            if (panelSelectPagesListPat.Height == 164)
            {
                panelSelectPagesListPat.Height = 41;
            }
            else
            {
                panelSelectPagesListPat.Height = 164;
            }
        }

        private void buttonListPatientsScheduler_Click(object sender, EventArgs e)
        {
            this.Dispose();
            mainForm.Show();
        }

        private void buttonListDoctorsListPat_Click(object sender, EventArgs e)
        {
            FormListaDoktora formListDoc = new FormListaDoktora(this.mainForm);
            formListDoc.Show();
            this.Dispose();
        }

        private void buttonClinicStatisticListPat_Click(object sender, EventArgs e)
        {
            FormStatistika formStat = new FormStatistika(this.mainForm);
            formStat.Show();
            this.Dispose();
        }

        private void textBoxListDocName_TextChanged(object sender, EventArgs e)
        {
            checkBoxExam.Checked = true;
            checkBoxLab.Checked = false;
            checkBoxTherapy.Checked = false;
            checkBoxOperation.Checked = false;
            checkBoxCheckup.Checked = false;

            switch (Thread.CurrentThread.CurrentUICulture.Name)
            {
                case "sr-Latn-CS":
                    dv.RowFilter = "Ime LIKE '" + textBoxListPatName.Text + "%'";
                    break;
                case "de-DE":
                    dv.RowFilter = "Name LIKE '" + textBoxListPatName.Text + "%'";
                    break;
                default:
                    dv.RowFilter = "Name LIKE '" + textBoxListPatName.Text + "%'";
                    break;
            }

            dataGridViewPatients.DataSource = dv;
        }

        private void textBoxListPatID_TextChanged(object sender, EventArgs e)
        {
            checkBoxExam.Checked = true;
            checkBoxLab.Checked = false;
            checkBoxTherapy.Checked = false;
            checkBoxOperation.Checked = false;
            checkBoxCheckup.Checked = false;

            switch (Thread.CurrentThread.CurrentUICulture.Name)
            {
                case "sr-Latn-CS":
                    dv.RowFilter = string.Format("CONVERT(JMBG, System.String) like '%{0}%'", textBoxListPatID.Text);
                    break;
                case "de-DE":
                    dv.RowFilter = string.Format("CONVERT(ID, System.String) like '%{0}%'", textBoxListPatID.Text);
                    break;
                default:
                    dv.RowFilter = string.Format("CONVERT(ID, System.String) like '%{0}%'", textBoxListPatID.Text);
                    break;
            }

            dataGridViewPatients.DataSource = dv;
        }

        private void checkBoxChecked_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxExam.Checked)
            {
                checkBoxLab.Checked = false;
                checkBoxTherapy.Checked = false;
                checkBoxOperation.Checked = false;
                checkBoxCheckup.Checked = false;
                textBoxListPatName.Text = "";
                textBoxListPatID.Text = "";

                switch (Thread.CurrentThread.CurrentUICulture.Name)
                {
                    case "sr-Latn-CS":
                        dv.RowFilter = string.Format("CONVERT(Pregled, System.String) like '%{0}%'", Convert.ToInt32(checkBoxExam.Checked));
                        break;
                    case "de-DE":
                        dv.RowFilter = string.Format("CONVERT(Untersuchung, System.String) like '%{0}%'", Convert.ToInt32(checkBoxExam.Checked));
                        break;
                    default:
                        dv.RowFilter = string.Format("CONVERT(Examination, System.String) like '%{0}%'", Convert.ToInt32(checkBoxExam.Checked));
                        break;
                }

                dataGridViewPatients.DataSource = dv;
            }
        }

        private void checkBoxLab_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLab.Checked)
            {
                checkBoxExam.Checked = false;
                checkBoxTherapy.Checked = false;
                checkBoxOperation.Checked = false;
                checkBoxCheckup.Checked = false;
                textBoxListPatName.Text = "";
                textBoxListPatID.Text = "";

                switch (Thread.CurrentThread.CurrentUICulture.Name)
                {
                    case "sr-Latn-CS":
                        dv.RowFilter = string.Format("CONVERT(Laboratorija, System.String) like '%{0}%'", Convert.ToInt32(checkBoxLab.Checked));
                        break;
                    case "de-DE":
                        dv.RowFilter = string.Format("CONVERT(Labor, System.String) like '%{0}%'", Convert.ToInt32(checkBoxLab.Checked));
                        break;
                    default:
                        dv.RowFilter = string.Format("CONVERT(Laboratory, System.String) like '%{0}%'", Convert.ToInt32(checkBoxLab.Checked));
                        break;
                }

                dataGridViewPatients.DataSource = dv;
            }
        }

        private void checkBoxTherapy_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTherapy.Checked)
            {
                checkBoxLab.Checked = false;
                checkBoxExam.Checked = false;
                checkBoxOperation.Checked = false;
                checkBoxCheckup.Checked = false;
                textBoxListPatName.Text = "";
                textBoxListPatID.Text = "";

                switch (Thread.CurrentThread.CurrentUICulture.Name)
                {
                    case "sr-Latn-CS":
                        dv.RowFilter = string.Format("CONVERT(Terapija, System.String) like '%{0}%'", Convert.ToInt32(checkBoxTherapy.Checked));
                        break;
                    case "de-DE":
                        dv.RowFilter = string.Format("CONVERT(Therapie, System.String) like '%{0}%'", Convert.ToInt32(checkBoxTherapy.Checked));
                        break;
                    default:
                        dv.RowFilter = string.Format("CONVERT(Therapy, System.String) like '%{0}%'", Convert.ToInt32(checkBoxTherapy.Checked));
                        break;
                }

                dataGridViewPatients.DataSource = dv;
            }
        }

        private void checkBoxOperation_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOperation.Checked)
            {
                checkBoxLab.Checked = false;
                checkBoxTherapy.Checked = false;
                checkBoxExam.Checked = false;
                checkBoxCheckup.Checked = false;
                textBoxListPatName.Text = "";
                textBoxListPatID.Text = "";

                switch (Thread.CurrentThread.CurrentUICulture.Name)
                {
                    case "sr-Latn-CS":
                        dv.RowFilter = string.Format("CONVERT(Operacija, System.String) like '%{0}%'", Convert.ToInt32(checkBoxOperation.Checked));
                        break;
                    case "de-DE":
                        dv.RowFilter = string.Format("CONVERT(Operation, System.String) like '%{0}%'", Convert.ToInt32(checkBoxOperation.Checked));
                        break;
                    default:
                        dv.RowFilter = string.Format("CONVERT(Operation, System.String) like '%{0}%'", Convert.ToInt32(checkBoxOperation.Checked));
                        break;
                }

                dataGridViewPatients.DataSource = dv;
            }
        }

        private void checkBoxCheckup_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCheckup.Checked)
            {
                checkBoxLab.Checked = false;
                checkBoxTherapy.Checked = false;
                checkBoxOperation.Checked = false;
                checkBoxExam.Checked = false;
                textBoxListPatName.Text = "";
                textBoxListPatID.Text = "";

                switch (Thread.CurrentThread.CurrentUICulture.Name)
                {
                    case "sr-Latn-CS":
                        dv.RowFilter = string.Format("CONVERT(Kontrola, System.String) like '%{0}%'", Convert.ToInt32(checkBoxCheckup.Checked));
                        break;
                    case "de-DE":
                        dv.RowFilter = string.Format("CONVERT(Kontroll, System.String) like '%{0}%'", Convert.ToInt32(checkBoxCheckup.Checked));
                        break;
                    default:
                        dv.RowFilter = string.Format("CONVERT(CheckUp, System.String) like '%{0}%'", Convert.ToInt32(checkBoxCheckup.Checked));
                        break;
                }

                dataGridViewPatients.DataSource = dv;
            }
        }

        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelListPatientTitleBar_MouseDown(object sender, MouseEventArgs e)
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
