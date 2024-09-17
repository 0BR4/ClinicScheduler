using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using RegistracijaDoktora.Data;
using RegistracijaDoktora.Views;
using RegistracijaDoktora.EventArgsSub;
using RegistracijaDoktora.Presenters;
using RegistracijaDoktora.Models;

namespace RegistracijaDoktora
{
    public partial class FormRegistracija : Form, IRegistracijaView
    {
        //implementacije interfejsa
        public event EventHandler<DoctorLogin> GetActiveDoctor;
        public event EventHandler<DoctorLogin> CreateDoctor;

        private Doctor myActiveDoctor = new Doctor();
        public Doctor MyActiveDoctor
        {
            get
            {
                return myActiveDoctor;
            }
            set
            {
                myActiveDoctor = value;
            }
        }

        private Doctor doctorToAdd = new Doctor();
        public Doctor DoctorToAdd
        {
            get
            {
                return doctorToAdd;
            }
            set
            {
                doctorToAdd = value;
            }
        }

        //nastavak implementacije forme
        public FormRegistracija()
        {
            InitializeComponent();
            ClinicRepository clinicRepository = new ClinicRepository();
            new RegistracijaPresenter(this, clinicRepository);

            //sklanjam difoltni TitleBar
            this.Text = String.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        private void FormRegistracija_Load(object sender, EventArgs e)
        {

        }

        private void buttonRegisterDoctor_Click(object sender, EventArgs e)
        {
            //pozovi getDoctor u MyActiveDoctor da vidis jel ima takav sa username i password
            DoctorLogin dl = new DoctorLogin(textBoxDoctorUsername.Text, textBoxDoctorPassword.Text);
            GetActiveDoctor.Invoke(this, dl);

            if(MyActiveDoctor.Id == 0)
            {
                //ako nema onda mozemo da kreiramo novog
                Doctor doctor = new Doctor
                {
                    Name = textBoxDoctorName.Text,
                    Username = textBoxDoctorUsername.Text,
                    Password = textBoxDoctorPassword.Text,
                    Specialization = textBoxDoctorSpecialization.Text,
                    Surgery = checkBoxDoctorSurgery.Checked ? 1 : 0,

                };

                CreateDoctor.Invoke(this, new DoctorLogin(doctor));

                MessageBox.Show("Successfully created a new user!");
            }
            else
            {
                MessageBox.Show("There is already a user with those credentials!\nPlease create new credentials!");
            }
        }

        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelRegistrationTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void iconExit_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                Application.Exit();

            }
            else
            {
                this.Show();
            }
        }

        private void labelExit_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                Application.Exit();

            }
            else
            {
                this.Show();
            }
        }
    }
}
