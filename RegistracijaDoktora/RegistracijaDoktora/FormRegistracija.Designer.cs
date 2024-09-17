namespace RegistracijaDoktora
{
    partial class FormRegistracija
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonRegisterDoctor = new System.Windows.Forms.Button();
            this.labelDoctorName = new System.Windows.Forms.Label();
            this.labelDoctorUsername = new System.Windows.Forms.Label();
            this.labelDoctorPassword = new System.Windows.Forms.Label();
            this.labelDoctorSpecialization = new System.Windows.Forms.Label();
            this.labelDoctorSurgery = new System.Windows.Forms.Label();
            this.textBoxDoctorName = new System.Windows.Forms.TextBox();
            this.textBoxDoctorUsername = new System.Windows.Forms.TextBox();
            this.textBoxDoctorPassword = new System.Windows.Forms.TextBox();
            this.textBoxDoctorSpecialization = new System.Windows.Forms.TextBox();
            this.checkBoxDoctorSurgery = new System.Windows.Forms.CheckBox();
            this.panelRegistrationTitleBar = new System.Windows.Forms.Panel();
            this.labelLogout = new System.Windows.Forms.Label();
            this.iconLogout = new FontAwesome.Sharp.IconPictureBox();
            this.labelTitlebarRegistration = new System.Windows.Forms.Label();
            this.iconRegistration = new FontAwesome.Sharp.IconPictureBox();
            this.labelExit = new System.Windows.Forms.Label();
            this.iconExit = new FontAwesome.Sharp.IconPictureBox();
            this.panelRegistrationTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconLogout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconRegistration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconExit)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonRegisterDoctor
            // 
            this.buttonRegisterDoctor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.buttonRegisterDoctor.FlatAppearance.BorderSize = 0;
            this.buttonRegisterDoctor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRegisterDoctor.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRegisterDoctor.ForeColor = System.Drawing.Color.White;
            this.buttonRegisterDoctor.Location = new System.Drawing.Point(228, 254);
            this.buttonRegisterDoctor.Name = "buttonRegisterDoctor";
            this.buttonRegisterDoctor.Size = new System.Drawing.Size(128, 32);
            this.buttonRegisterDoctor.TabIndex = 0;
            this.buttonRegisterDoctor.Text = "Register Doctor";
            this.buttonRegisterDoctor.UseVisualStyleBackColor = false;
            this.buttonRegisterDoctor.Click += new System.EventHandler(this.buttonRegisterDoctor_Click);
            // 
            // labelDoctorName
            // 
            this.labelDoctorName.AutoSize = true;
            this.labelDoctorName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDoctorName.Location = new System.Drawing.Point(30, 78);
            this.labelDoctorName.Name = "labelDoctorName";
            this.labelDoctorName.Size = new System.Drawing.Size(48, 14);
            this.labelDoctorName.TabIndex = 1;
            this.labelDoctorName.Text = "Name:";
            // 
            // labelDoctorUsername
            // 
            this.labelDoctorUsername.AutoSize = true;
            this.labelDoctorUsername.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDoctorUsername.Location = new System.Drawing.Point(30, 111);
            this.labelDoctorUsername.Name = "labelDoctorUsername";
            this.labelDoctorUsername.Size = new System.Drawing.Size(76, 14);
            this.labelDoctorUsername.TabIndex = 2;
            this.labelDoctorUsername.Text = "Username:";
            // 
            // labelDoctorPassword
            // 
            this.labelDoctorPassword.AutoSize = true;
            this.labelDoctorPassword.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDoctorPassword.Location = new System.Drawing.Point(30, 147);
            this.labelDoctorPassword.Name = "labelDoctorPassword";
            this.labelDoctorPassword.Size = new System.Drawing.Size(74, 14);
            this.labelDoctorPassword.TabIndex = 3;
            this.labelDoctorPassword.Text = "Password:";
            // 
            // labelDoctorSpecialization
            // 
            this.labelDoctorSpecialization.AutoSize = true;
            this.labelDoctorSpecialization.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDoctorSpecialization.Location = new System.Drawing.Point(30, 181);
            this.labelDoctorSpecialization.Name = "labelDoctorSpecialization";
            this.labelDoctorSpecialization.Size = new System.Drawing.Size(98, 14);
            this.labelDoctorSpecialization.TabIndex = 4;
            this.labelDoctorSpecialization.Text = "Specialization:";
            // 
            // labelDoctorSurgery
            // 
            this.labelDoctorSurgery.AutoSize = true;
            this.labelDoctorSurgery.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDoctorSurgery.Location = new System.Drawing.Point(30, 216);
            this.labelDoctorSurgery.Name = "labelDoctorSurgery";
            this.labelDoctorSurgery.Size = new System.Drawing.Size(61, 14);
            this.labelDoctorSurgery.TabIndex = 5;
            this.labelDoctorSurgery.Text = "Surgery:";
            // 
            // textBoxDoctorName
            // 
            this.textBoxDoctorName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDoctorName.Location = new System.Drawing.Point(158, 75);
            this.textBoxDoctorName.Name = "textBoxDoctorName";
            this.textBoxDoctorName.Size = new System.Drawing.Size(161, 22);
            this.textBoxDoctorName.TabIndex = 6;
            // 
            // textBoxDoctorUsername
            // 
            this.textBoxDoctorUsername.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDoctorUsername.Location = new System.Drawing.Point(158, 108);
            this.textBoxDoctorUsername.Name = "textBoxDoctorUsername";
            this.textBoxDoctorUsername.Size = new System.Drawing.Size(161, 22);
            this.textBoxDoctorUsername.TabIndex = 7;
            // 
            // textBoxDoctorPassword
            // 
            this.textBoxDoctorPassword.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDoctorPassword.Location = new System.Drawing.Point(158, 144);
            this.textBoxDoctorPassword.Name = "textBoxDoctorPassword";
            this.textBoxDoctorPassword.Size = new System.Drawing.Size(161, 22);
            this.textBoxDoctorPassword.TabIndex = 8;
            // 
            // textBoxDoctorSpecialization
            // 
            this.textBoxDoctorSpecialization.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDoctorSpecialization.Location = new System.Drawing.Point(158, 178);
            this.textBoxDoctorSpecialization.Name = "textBoxDoctorSpecialization";
            this.textBoxDoctorSpecialization.Size = new System.Drawing.Size(161, 22);
            this.textBoxDoctorSpecialization.TabIndex = 9;
            // 
            // checkBoxDoctorSurgery
            // 
            this.checkBoxDoctorSurgery.AutoSize = true;
            this.checkBoxDoctorSurgery.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxDoctorSurgery.Location = new System.Drawing.Point(158, 215);
            this.checkBoxDoctorSurgery.Name = "checkBoxDoctorSurgery";
            this.checkBoxDoctorSurgery.Size = new System.Drawing.Size(156, 18);
            this.checkBoxDoctorSurgery.TabIndex = 10;
            this.checkBoxDoctorSurgery.Text = "Can perform surgery";
            this.checkBoxDoctorSurgery.UseVisualStyleBackColor = true;
            // 
            // panelRegistrationTitleBar
            // 
            this.panelRegistrationTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(225)))), ((int)(((byte)(192)))));
            this.panelRegistrationTitleBar.Controls.Add(this.labelExit);
            this.panelRegistrationTitleBar.Controls.Add(this.iconExit);
            this.panelRegistrationTitleBar.Controls.Add(this.labelLogout);
            this.panelRegistrationTitleBar.Controls.Add(this.iconLogout);
            this.panelRegistrationTitleBar.Controls.Add(this.labelTitlebarRegistration);
            this.panelRegistrationTitleBar.Controls.Add(this.iconRegistration);
            this.panelRegistrationTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRegistrationTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelRegistrationTitleBar.Name = "panelRegistrationTitleBar";
            this.panelRegistrationTitleBar.Size = new System.Drawing.Size(394, 43);
            this.panelRegistrationTitleBar.TabIndex = 46;
            this.panelRegistrationTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelRegistrationTitleBar_MouseDown);
            // 
            // labelLogout
            // 
            this.labelLogout.AutoSize = true;
            this.labelLogout.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.labelLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.labelLogout.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelLogout.Location = new System.Drawing.Point(1434, 13);
            this.labelLogout.Name = "labelLogout";
            this.labelLogout.Size = new System.Drawing.Size(70, 17);
            this.labelLogout.TabIndex = 3;
            this.labelLogout.Text = "Log Out";
            // 
            // iconLogout
            // 
            this.iconLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(225)))), ((int)(((byte)(192)))));
            this.iconLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.iconLogout.IconChar = FontAwesome.Sharp.IconChar.SignOutAlt;
            this.iconLogout.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.iconLogout.IconSize = 34;
            this.iconLogout.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.iconLogout.Location = new System.Drawing.Point(1390, 9);
            this.iconLogout.Name = "iconLogout";
            this.iconLogout.Size = new System.Drawing.Size(42, 34);
            this.iconLogout.TabIndex = 2;
            this.iconLogout.TabStop = false;
            // 
            // labelTitlebarRegistration
            // 
            this.labelTitlebarRegistration.AutoSize = true;
            this.labelTitlebarRegistration.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.labelTitlebarRegistration.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.labelTitlebarRegistration.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelTitlebarRegistration.Location = new System.Drawing.Point(40, 13);
            this.labelTitlebarRegistration.Name = "labelTitlebarRegistration";
            this.labelTitlebarRegistration.Size = new System.Drawing.Size(159, 17);
            this.labelTitlebarRegistration.TabIndex = 1;
            this.labelTitlebarRegistration.Text = "Doctor Registration";
            // 
            // iconRegistration
            // 
            this.iconRegistration.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(225)))), ((int)(((byte)(192)))));
            this.iconRegistration.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.iconRegistration.IconChar = FontAwesome.Sharp.IconChar.UserMd;
            this.iconRegistration.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.iconRegistration.IconSize = 34;
            this.iconRegistration.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.iconRegistration.Location = new System.Drawing.Point(3, 9);
            this.iconRegistration.Name = "iconRegistration";
            this.iconRegistration.Size = new System.Drawing.Size(42, 34);
            this.iconRegistration.TabIndex = 0;
            this.iconRegistration.TabStop = false;
            // 
            // labelExit
            // 
            this.labelExit.AutoSize = true;
            this.labelExit.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.labelExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.labelExit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelExit.Location = new System.Drawing.Point(345, 13);
            this.labelExit.Name = "labelExit";
            this.labelExit.Size = new System.Drawing.Size(37, 17);
            this.labelExit.TabIndex = 5;
            this.labelExit.Text = "Exit";
            this.labelExit.Click += new System.EventHandler(this.labelExit_Click);
            // 
            // iconExit
            // 
            this.iconExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(225)))), ((int)(((byte)(192)))));
            this.iconExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.iconExit.IconChar = FontAwesome.Sharp.IconChar.SignOutAlt;
            this.iconExit.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.iconExit.IconSize = 34;
            this.iconExit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.iconExit.Location = new System.Drawing.Point(309, 9);
            this.iconExit.Name = "iconExit";
            this.iconExit.Size = new System.Drawing.Size(42, 34);
            this.iconExit.TabIndex = 4;
            this.iconExit.TabStop = false;
            this.iconExit.Click += new System.EventHandler(this.iconExit_Click);
            // 
            // FormRegistracija
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 298);
            this.Controls.Add(this.panelRegistrationTitleBar);
            this.Controls.Add(this.checkBoxDoctorSurgery);
            this.Controls.Add(this.textBoxDoctorSpecialization);
            this.Controls.Add(this.textBoxDoctorPassword);
            this.Controls.Add(this.textBoxDoctorUsername);
            this.Controls.Add(this.textBoxDoctorName);
            this.Controls.Add(this.labelDoctorSurgery);
            this.Controls.Add(this.labelDoctorSpecialization);
            this.Controls.Add(this.labelDoctorPassword);
            this.Controls.Add(this.labelDoctorUsername);
            this.Controls.Add(this.labelDoctorName);
            this.Controls.Add(this.buttonRegisterDoctor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormRegistracija";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Doctor Registration";
            this.Load += new System.EventHandler(this.FormRegistracija_Load);
            this.panelRegistrationTitleBar.ResumeLayout(false);
            this.panelRegistrationTitleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconLogout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconRegistration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconExit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRegisterDoctor;
        private System.Windows.Forms.Label labelDoctorName;
        private System.Windows.Forms.Label labelDoctorUsername;
        private System.Windows.Forms.Label labelDoctorPassword;
        private System.Windows.Forms.Label labelDoctorSpecialization;
        private System.Windows.Forms.Label labelDoctorSurgery;
        private System.Windows.Forms.TextBox textBoxDoctorName;
        private System.Windows.Forms.TextBox textBoxDoctorUsername;
        private System.Windows.Forms.TextBox textBoxDoctorPassword;
        private System.Windows.Forms.TextBox textBoxDoctorSpecialization;
        private System.Windows.Forms.CheckBox checkBoxDoctorSurgery;
        private System.Windows.Forms.Panel panelRegistrationTitleBar;
        private System.Windows.Forms.Label labelLogout;
        private FontAwesome.Sharp.IconPictureBox iconLogout;
        private System.Windows.Forms.Label labelTitlebarRegistration;
        private FontAwesome.Sharp.IconPictureBox iconRegistration;
        private System.Windows.Forms.Label labelExit;
        private FontAwesome.Sharp.IconPictureBox iconExit;
    }
}

