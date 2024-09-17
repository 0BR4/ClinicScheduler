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
    public partial class FormListaDoktora : Form, IListaDoktoraView
    {
        //implementacije interfejsa
        public event EventHandler GetAllDoctors;

        private List<Doctor> doctorsList = new List<Doctor>();
        public List<Doctor> DoctorsList
        {
            get
            {
                return doctorsList;
            }
            set
            {
                doctorsList = value;
            }
        }

        public FormListaDoktora()
        {
            InitializeComponent();
        }

        private FormPlanerKlinike mainForm = null;

        public FormListaDoktora(Form callingForm)
        {
            mainForm = callingForm as FormPlanerKlinike;
            InitializeComponent();
            ClinicRepository clinicRepository = new ClinicRepository();
            new ListaDoktoraPresenter(this, clinicRepository);

            //sklanjam difoltni TitleBar
            this.Text = String.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        DataTable dtable;
        DataView dv;

        private void FormListaDoktora_Load(object sender, EventArgs e)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentUICulture;

            GetAllDoctors.Invoke(this, EventArgs.Empty);
            dtable = new DataTable();

            switch (Thread.CurrentThread.CurrentUICulture.Name)
            {
                case "sr-Latn-CS":
                    using (var reader = ObjectReader.Create(DoctorsList, "Id", "Name", "Specialization", "Surgery"))
                    {
                        dtable.Load(reader);
                        dtable.Columns["Id"].ColumnName = "ID";
                        dtable.Columns["Name"].ColumnName = "Ime";
                        dtable.Columns["Specialization"].ColumnName = "Specijalizacija";
                        dtable.Columns["Surgery"].ColumnName = "Hirurg";
                    }
                    break;
                case "de-DE":
                    using (var reader = ObjectReader.Create(DoctorsList, "Id", "Name", "Specialization", "Surgery"))
                    {
                        dtable.Load(reader);
                        dtable.Columns["Id"].ColumnName = "ID";
                        dtable.Columns["Specialization"].ColumnName = "Spezialisierung";
                        dtable.Columns["Surgery"].ColumnName = "Chirurg";
                    }
                    break;
                default:
                    using (var reader = ObjectReader.Create(DoctorsList, "Id", "Name", "Specialization", "Surgery"))
                    {
                        dtable.Load(reader);
                        dtable.Columns["Id"].ColumnName = "ID";
                        dtable.Columns["Surgery"].ColumnName = "Surgeon";
                    }
                    break;
            }

            dataGridViewDoctors.DataSource = dtable;
            dv = dtable.DefaultView;

            //resize za ime
            DataGridViewColumn columnName = dataGridViewDoctors.Columns[1];
            columnName.Width = 180;
        }

        private void buttonSelectPagesListDoc_Click(object sender, EventArgs e)
        {
            if (panelSelectPagesListDoc.Height == 164)
            {
                panelSelectPagesListDoc.Height = 41;
            }
            else
            {
                panelSelectPagesListDoc.Height = 164;
            }
        }

        private void buttonSchedulerListDoc_Click(object sender, EventArgs e)
        {
            this.Dispose();
            mainForm.Show();
        }

        private void buttonListPatientsListDoc_Click(object sender, EventArgs e)
        {
            FormListaPacijenata formListPat = new FormListaPacijenata(this.mainForm);
            formListPat.Show();
            this.Dispose();
        }

        private void buttonClinicStatisticListDoc_Click(object sender, EventArgs e)
        {
            FormStatistika formStat = new FormStatistika(this.mainForm);
            formStat.Show();
            this.Dispose();
        }

        private void textBoxListDocName_TextChanged(object sender, EventArgs e)
        {
            checkBoxSurgery.Checked = false;

            switch (Thread.CurrentThread.CurrentUICulture.Name)
            {
                case "sr-Latn-CS":
                    dv.RowFilter = "Ime LIKE '" + textBoxListDocName.Text + "%'";
                    break;
                case "de-DE":
                    dv.RowFilter = "Name LIKE '" + textBoxListDocName.Text + "%'";
                    break;
                default:
                    dv.RowFilter = "Name LIKE '" + textBoxListDocName.Text + "%'";
                    break;
            }

            dataGridViewDoctors.DataSource = dv;
        }

        private void textBoxListDocSpec_TextChanged(object sender, EventArgs e)
        {
            checkBoxSurgery.Checked = false;

            switch (Thread.CurrentThread.CurrentUICulture.Name)
            {
                case "sr-Latn-CS":
                    dv.RowFilter = "Specijalizacija LIKE '" + textBoxListDocSpec.Text + "%'";
                    break;
                case "de-DE":
                    dv.RowFilter = "Spezialisierung LIKE '" + textBoxListDocSpec.Text + "%'";
                    break;
                default:
                    dv.RowFilter = "Specialization LIKE '" + textBoxListDocSpec.Text + "%'";
                    break;
            }

            dataGridViewDoctors.DataSource = dv;
        }

        private void textBoxListDocID_TextChanged(object sender, EventArgs e)
        {
            checkBoxSurgery.Checked = false;

            switch (Thread.CurrentThread.CurrentUICulture.Name)
            {
                case "sr-Latn-CS":
                    dv.RowFilter = string.Format("CONVERT(ID, System.String) like '%{0}%'", textBoxListDocID.Text);
                    break;
                case "de-DE":
                    dv.RowFilter = string.Format("CONVERT(ID, System.String) like '%{0}%'", textBoxListDocID.Text);
                    break;
                default:
                    dv.RowFilter = string.Format("CONVERT(ID, System.String) like '%{0}%'", textBoxListDocID.Text);
                    break;
            }

            dataGridViewDoctors.DataSource = dv;
        }

        private void checkBoxSurgery_CheckedChanged(object sender, EventArgs e)
        {
            if (textBoxListDocName.Text.Equals("") && textBoxListDocID.Text.Equals("") && textBoxListDocSpec.Text.Equals(""))
            {
                switch (Thread.CurrentThread.CurrentUICulture.Name)
                {
                    case "sr-Latn-CS":
                        dv.RowFilter = string.Format("CONVERT(Hirurg, System.String) like '%{0}%'", Convert.ToInt32(checkBoxSurgery.Checked));
                        break;
                    case "de-DE":
                        dv.RowFilter = string.Format("CONVERT(Chirurg, System.String) like '%{0}%'", Convert.ToInt32(checkBoxSurgery.Checked));
                        break;
                    default:
                        dv.RowFilter = string.Format("CONVERT(Surgeon, System.String) like '%{0}%'", Convert.ToInt32(checkBoxSurgery.Checked));
                        break;
                }

                dataGridViewDoctors.DataSource = dv;
            }
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

        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelListDoctorTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
