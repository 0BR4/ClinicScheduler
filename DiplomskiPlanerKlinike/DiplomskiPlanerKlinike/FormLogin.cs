using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;
using DiplomskiPlanerKlinike.Views;
using DiplomskiPlanerKlinike.Models;
using DiplomskiPlanerKlinike.EventArgsSubclass;
using DiplomskiPlanerKlinike.Presenters;
using DiplomskiPlanerKlinike.Data;

namespace DiplomskiPlanerKlinike
{
    public partial class FormLogin : Form, ILoginView
    {
        //ovde eventovi
        public event EventHandler<DoctorLogin> GetActiveDoctor;

        public Doctor myActiveDoctor = new Doctor();

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

        public FormLogin()
        {
            InitializeComponent();
            ClinicRepository clinicRepository = new ClinicRepository();
            new LoginPresenter(this, clinicRepository);

            //sklanjam difoltni TitleBar
            this.Text = String.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        public void buttonSelectLanguage_Click(object sender, EventArgs e)
        {
            if (panelSelectLanguage.Height == 108)
            {
                panelSelectLanguage.Height = 27;
            }
            else
            {
                panelSelectLanguage.Height = 108;
            }
        }

        public void buttonSerbian_Click(object sender, EventArgs e)
        {
            MessageBoxManager.Unregister();
            MessageBoxManager.Yes = "Da";
            MessageBoxManager.No = "Ne";
            MessageBoxManager.Register();

            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("sr-Latn-CS");
            this.Controls.Clear();
            InitializeComponent();
        }

        public void buttonEnglish_Click(object sender, EventArgs e)
        {
            MessageBoxManager.Unregister();
            MessageBoxManager.Yes = "Yes";
            MessageBoxManager.No = "No";
            MessageBoxManager.Register();

            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            this.Controls.Clear();
            InitializeComponent();
        }

        public void buttonGerman_Click(object sender, EventArgs e)
        {
            MessageBoxManager.Unregister();
            MessageBoxManager.Yes = "Ja";
            MessageBoxManager.No = "Nein";
            MessageBoxManager.Register();

            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de-DE");
            this.Controls.Clear();
            InitializeComponent();
        }

        public void buttonLogin_Click(object sender, EventArgs e)
        {
            GetActiveDoctor.Invoke(this, new DoctorLogin(textBoxUserName.Text, textBoxPassword.Text));

            ActiveDoctor.id = MyActiveDoctor.Id;
            ActiveDoctor.surgery = MyActiveDoctor.Surgery;
            ActiveDoctor.specialization = MyActiveDoctor.Specialization;
            ActiveDoctor.name = MyActiveDoctor.Name;
            ActiveDoctor.username = MyActiveDoctor.Username;
            ActiveDoctor.password = MyActiveDoctor.Password;

            MyActiveDoctor = new Doctor();

            FormPlanerKlinike formPlanerKlinike = new FormPlanerKlinike();
            formPlanerKlinike.Show();
            this.Hide();           
        }

        public void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxUserName.Clear();
            textBoxPassword.Clear();
            textBoxUserName.Focus();
        }

        //enter je login
        public void textBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonLogin_Click(this, new EventArgs());
            }
        }

        //enter je login
        public void textBoxUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonLogin_Click(this, new EventArgs());
            }
        }

        //prikazi sifru
        public void buttonShowPassword_Click(object sender, EventArgs e)
        {
            buttonHidePassword.BringToFront();
            textBoxPassword.UseSystemPasswordChar = false;
        }

        //sakrij sifru
        public void buttonHidePassword_Click(object sender, EventArgs e)
        {
            buttonShowPassword.BringToFront();
            textBoxPassword.UseSystemPasswordChar = true;
             
        }

        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelLoginTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void iconExit_Click(object sender, EventArgs e)
        {
            DialogResult res;
            switch (Thread.CurrentThread.CurrentUICulture.Name)
            {
                case "sr-Latn-CS":
                    res = MessageBox.Show("Da li ste sigurni da želite da izađete?", "Izađi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    break;
                case "de-DE":
                    res = MessageBox.Show("Sie sind sicher, dass Sie beenden wollen?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    break;
                default:
                    res = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    break;
            }
            if (res == DialogResult.Yes)
            {
                Application.Exit();

            }
            else
            {
                this.Show();
            }
        }

        private void labelLoginExit_Click(object sender, EventArgs e)
        {
            DialogResult res;
            switch (Thread.CurrentThread.CurrentUICulture.Name)
            {
                case "sr-Latn-CS":
                    res = MessageBox.Show("Da li ste sigurni da želite da izađete?", "Izađi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    break;
                case "de-DE":
                    res = MessageBox.Show("Sie sind sicher, dass Sie beenden wollen?", "Beenden", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    break;
                default:
                    res = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    break;
            }
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

    public static class ActiveDoctor
    {
        public static int id;
        public static String username;
        public static String password;
        public static String name;
        public static String specialization;
        public static int surgery;
    }
}
