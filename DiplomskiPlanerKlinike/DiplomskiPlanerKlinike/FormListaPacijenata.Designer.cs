namespace DiplomskiPlanerKlinike
{
    partial class FormListaPacijenata
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormListaPacijenata));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelSelectPagesListPat = new System.Windows.Forms.Panel();
            this.buttonClinicStatisticListPat = new System.Windows.Forms.Button();
            this.buttonSelectPagesListPat = new System.Windows.Forms.Button();
            this.buttonListDoctorsListPat = new System.Windows.Forms.Button();
            this.buttonListPatientsScheduler = new System.Windows.Forms.Button();
            this.dataGridViewPatients = new System.Windows.Forms.DataGridView();
            this.labelListPatName = new System.Windows.Forms.Label();
            this.textBoxListPatName = new System.Windows.Forms.TextBox();
            this.textBoxListPatID = new System.Windows.Forms.TextBox();
            this.labelListPatID = new System.Windows.Forms.Label();
            this.checkBoxExam = new System.Windows.Forms.CheckBox();
            this.checkBoxLab = new System.Windows.Forms.CheckBox();
            this.checkBoxTherapy = new System.Windows.Forms.CheckBox();
            this.checkBoxOperation = new System.Windows.Forms.CheckBox();
            this.checkBoxCheckup = new System.Windows.Forms.CheckBox();
            this.panelListPatientTitleBar = new System.Windows.Forms.Panel();
            this.labelListDoctorLogout = new System.Windows.Forms.Label();
            this.labelLogout = new System.Windows.Forms.Label();
            this.labelTitlebarLogin = new System.Windows.Forms.Label();
            this.iconListDoctorLogout = new FontAwesome.Sharp.IconPictureBox();
            this.iconLogout = new FontAwesome.Sharp.IconPictureBox();
            this.iconScheduler = new FontAwesome.Sharp.IconPictureBox();
            this.panelSelectPagesListPat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPatients)).BeginInit();
            this.panelListPatientTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconListDoctorLogout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconLogout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconScheduler)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSelectPagesListPat
            // 
            resources.ApplyResources(this.panelSelectPagesListPat, "panelSelectPagesListPat");
            this.panelSelectPagesListPat.Controls.Add(this.buttonClinicStatisticListPat);
            this.panelSelectPagesListPat.Controls.Add(this.buttonSelectPagesListPat);
            this.panelSelectPagesListPat.Controls.Add(this.buttonListDoctorsListPat);
            this.panelSelectPagesListPat.Controls.Add(this.buttonListPatientsScheduler);
            this.panelSelectPagesListPat.Name = "panelSelectPagesListPat";
            // 
            // buttonClinicStatisticListPat
            // 
            resources.ApplyResources(this.buttonClinicStatisticListPat, "buttonClinicStatisticListPat");
            this.buttonClinicStatisticListPat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.buttonClinicStatisticListPat.FlatAppearance.BorderSize = 0;
            this.buttonClinicStatisticListPat.ForeColor = System.Drawing.Color.White;
            this.buttonClinicStatisticListPat.Name = "buttonClinicStatisticListPat";
            this.buttonClinicStatisticListPat.UseVisualStyleBackColor = false;
            this.buttonClinicStatisticListPat.Click += new System.EventHandler(this.buttonClinicStatisticListPat_Click);
            // 
            // buttonSelectPagesListPat
            // 
            resources.ApplyResources(this.buttonSelectPagesListPat, "buttonSelectPagesListPat");
            this.buttonSelectPagesListPat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.buttonSelectPagesListPat.FlatAppearance.BorderSize = 0;
            this.buttonSelectPagesListPat.ForeColor = System.Drawing.Color.White;
            this.buttonSelectPagesListPat.Name = "buttonSelectPagesListPat";
            this.buttonSelectPagesListPat.UseVisualStyleBackColor = false;
            this.buttonSelectPagesListPat.Click += new System.EventHandler(this.buttonSelectPagesListPat_Click);
            // 
            // buttonListDoctorsListPat
            // 
            resources.ApplyResources(this.buttonListDoctorsListPat, "buttonListDoctorsListPat");
            this.buttonListDoctorsListPat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.buttonListDoctorsListPat.FlatAppearance.BorderSize = 0;
            this.buttonListDoctorsListPat.ForeColor = System.Drawing.Color.White;
            this.buttonListDoctorsListPat.Name = "buttonListDoctorsListPat";
            this.buttonListDoctorsListPat.UseVisualStyleBackColor = false;
            this.buttonListDoctorsListPat.Click += new System.EventHandler(this.buttonListDoctorsListPat_Click);
            // 
            // buttonListPatientsScheduler
            // 
            resources.ApplyResources(this.buttonListPatientsScheduler, "buttonListPatientsScheduler");
            this.buttonListPatientsScheduler.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.buttonListPatientsScheduler.FlatAppearance.BorderSize = 0;
            this.buttonListPatientsScheduler.ForeColor = System.Drawing.Color.White;
            this.buttonListPatientsScheduler.Name = "buttonListPatientsScheduler";
            this.buttonListPatientsScheduler.UseVisualStyleBackColor = false;
            this.buttonListPatientsScheduler.Click += new System.EventHandler(this.buttonListPatientsScheduler_Click);
            // 
            // dataGridViewPatients
            // 
            resources.ApplyResources(this.dataGridViewPatients, "dataGridViewPatients");
            this.dataGridViewPatients.AllowUserToAddRows = false;
            this.dataGridViewPatients.AllowUserToDeleteRows = false;
            this.dataGridViewPatients.AllowUserToOrderColumns = true;
            this.dataGridViewPatients.AllowUserToResizeColumns = false;
            this.dataGridViewPatients.AllowUserToResizeRows = false;
            this.dataGridViewPatients.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewPatients.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewPatients.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewPatients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewPatients.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewPatients.Name = "dataGridViewPatients";
            this.dataGridViewPatients.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewPatients.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewPatients.RowHeadersVisible = false;
            // 
            // labelListPatName
            // 
            resources.ApplyResources(this.labelListPatName, "labelListPatName");
            this.labelListPatName.Name = "labelListPatName";
            // 
            // textBoxListPatName
            // 
            resources.ApplyResources(this.textBoxListPatName, "textBoxListPatName");
            this.textBoxListPatName.Name = "textBoxListPatName";
            this.textBoxListPatName.TextChanged += new System.EventHandler(this.textBoxListDocName_TextChanged);
            // 
            // textBoxListPatID
            // 
            resources.ApplyResources(this.textBoxListPatID, "textBoxListPatID");
            this.textBoxListPatID.Name = "textBoxListPatID";
            this.textBoxListPatID.TextChanged += new System.EventHandler(this.textBoxListPatID_TextChanged);
            // 
            // labelListPatID
            // 
            resources.ApplyResources(this.labelListPatID, "labelListPatID");
            this.labelListPatID.Name = "labelListPatID";
            // 
            // checkBoxExam
            // 
            resources.ApplyResources(this.checkBoxExam, "checkBoxExam");
            this.checkBoxExam.Checked = true;
            this.checkBoxExam.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxExam.Name = "checkBoxExam";
            this.checkBoxExam.UseVisualStyleBackColor = true;
            this.checkBoxExam.CheckedChanged += new System.EventHandler(this.checkBoxChecked_CheckedChanged);
            // 
            // checkBoxLab
            // 
            resources.ApplyResources(this.checkBoxLab, "checkBoxLab");
            this.checkBoxLab.Name = "checkBoxLab";
            this.checkBoxLab.UseVisualStyleBackColor = true;
            this.checkBoxLab.CheckedChanged += new System.EventHandler(this.checkBoxLab_CheckedChanged);
            // 
            // checkBoxTherapy
            // 
            resources.ApplyResources(this.checkBoxTherapy, "checkBoxTherapy");
            this.checkBoxTherapy.Name = "checkBoxTherapy";
            this.checkBoxTherapy.UseVisualStyleBackColor = true;
            this.checkBoxTherapy.CheckedChanged += new System.EventHandler(this.checkBoxTherapy_CheckedChanged);
            // 
            // checkBoxOperation
            // 
            resources.ApplyResources(this.checkBoxOperation, "checkBoxOperation");
            this.checkBoxOperation.Name = "checkBoxOperation";
            this.checkBoxOperation.UseVisualStyleBackColor = true;
            this.checkBoxOperation.CheckedChanged += new System.EventHandler(this.checkBoxOperation_CheckedChanged);
            // 
            // checkBoxCheckup
            // 
            resources.ApplyResources(this.checkBoxCheckup, "checkBoxCheckup");
            this.checkBoxCheckup.Name = "checkBoxCheckup";
            this.checkBoxCheckup.UseVisualStyleBackColor = true;
            this.checkBoxCheckup.CheckedChanged += new System.EventHandler(this.checkBoxCheckup_CheckedChanged);
            // 
            // panelListPatientTitleBar
            // 
            resources.ApplyResources(this.panelListPatientTitleBar, "panelListPatientTitleBar");
            this.panelListPatientTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(225)))), ((int)(((byte)(192)))));
            this.panelListPatientTitleBar.Controls.Add(this.labelListDoctorLogout);
            this.panelListPatientTitleBar.Controls.Add(this.iconListDoctorLogout);
            this.panelListPatientTitleBar.Controls.Add(this.labelLogout);
            this.panelListPatientTitleBar.Controls.Add(this.iconLogout);
            this.panelListPatientTitleBar.Controls.Add(this.labelTitlebarLogin);
            this.panelListPatientTitleBar.Controls.Add(this.iconScheduler);
            this.panelListPatientTitleBar.Name = "panelListPatientTitleBar";
            this.panelListPatientTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelListPatientTitleBar_MouseDown);
            // 
            // labelListDoctorLogout
            // 
            resources.ApplyResources(this.labelListDoctorLogout, "labelListDoctorLogout");
            this.labelListDoctorLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.labelListDoctorLogout.Name = "labelListDoctorLogout";
            this.labelListDoctorLogout.Click += new System.EventHandler(this.labelListDoctorLogout_Click);
            // 
            // labelLogout
            // 
            resources.ApplyResources(this.labelLogout, "labelLogout");
            this.labelLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.labelLogout.Name = "labelLogout";
            // 
            // labelTitlebarLogin
            // 
            resources.ApplyResources(this.labelTitlebarLogin, "labelTitlebarLogin");
            this.labelTitlebarLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.labelTitlebarLogin.Name = "labelTitlebarLogin";
            // 
            // iconListDoctorLogout
            // 
            resources.ApplyResources(this.iconListDoctorLogout, "iconListDoctorLogout");
            this.iconListDoctorLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(225)))), ((int)(((byte)(192)))));
            this.iconListDoctorLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.iconListDoctorLogout.IconChar = FontAwesome.Sharp.IconChar.SignOutAlt;
            this.iconListDoctorLogout.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.iconListDoctorLogout.IconSize = 34;
            this.iconListDoctorLogout.Name = "iconListDoctorLogout";
            this.iconListDoctorLogout.TabStop = false;
            this.iconListDoctorLogout.Click += new System.EventHandler(this.iconListDoctorLogout_Click);
            // 
            // iconLogout
            // 
            resources.ApplyResources(this.iconLogout, "iconLogout");
            this.iconLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(225)))), ((int)(((byte)(192)))));
            this.iconLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.iconLogout.IconChar = FontAwesome.Sharp.IconChar.SignOutAlt;
            this.iconLogout.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.iconLogout.IconSize = 34;
            this.iconLogout.Name = "iconLogout";
            this.iconLogout.TabStop = false;
            // 
            // iconScheduler
            // 
            resources.ApplyResources(this.iconScheduler, "iconScheduler");
            this.iconScheduler.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(225)))), ((int)(((byte)(192)))));
            this.iconScheduler.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.iconScheduler.IconChar = FontAwesome.Sharp.IconChar.CalendarAlt;
            this.iconScheduler.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.iconScheduler.IconSize = 34;
            this.iconScheduler.Name = "iconScheduler";
            this.iconScheduler.TabStop = false;
            // 
            // FormListaPacijenata
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelListPatientTitleBar);
            this.Controls.Add(this.checkBoxCheckup);
            this.Controls.Add(this.checkBoxOperation);
            this.Controls.Add(this.checkBoxTherapy);
            this.Controls.Add(this.checkBoxLab);
            this.Controls.Add(this.checkBoxExam);
            this.Controls.Add(this.textBoxListPatID);
            this.Controls.Add(this.labelListPatID);
            this.Controls.Add(this.textBoxListPatName);
            this.Controls.Add(this.labelListPatName);
            this.Controls.Add(this.dataGridViewPatients);
            this.Controls.Add(this.panelSelectPagesListPat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormListaPacijenata";
            this.Load += new System.EventHandler(this.FormListaPacijenata_Load);
            this.panelSelectPagesListPat.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPatients)).EndInit();
            this.panelListPatientTitleBar.ResumeLayout(false);
            this.panelListPatientTitleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconListDoctorLogout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconLogout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconScheduler)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panelSelectPagesListPat;
        private System.Windows.Forms.Button buttonSelectPagesListPat;
        private System.Windows.Forms.Button buttonClinicStatisticListPat;
        private System.Windows.Forms.Button buttonListDoctorsListPat;
        private System.Windows.Forms.Button buttonListPatientsScheduler;
        private System.Windows.Forms.DataGridView dataGridViewPatients;
        private System.Windows.Forms.Label labelListPatName;
        private System.Windows.Forms.TextBox textBoxListPatName;
        private System.Windows.Forms.TextBox textBoxListPatID;
        private System.Windows.Forms.Label labelListPatID;
        private System.Windows.Forms.CheckBox checkBoxExam;
        private System.Windows.Forms.CheckBox checkBoxLab;
        private System.Windows.Forms.CheckBox checkBoxTherapy;
        private System.Windows.Forms.CheckBox checkBoxOperation;
        private System.Windows.Forms.CheckBox checkBoxCheckup;
        private System.Windows.Forms.Panel panelListPatientTitleBar;
        private System.Windows.Forms.Label labelListDoctorLogout;
        private FontAwesome.Sharp.IconPictureBox iconListDoctorLogout;
        private System.Windows.Forms.Label labelLogout;
        private FontAwesome.Sharp.IconPictureBox iconLogout;
        private System.Windows.Forms.Label labelTitlebarLogin;
        private FontAwesome.Sharp.IconPictureBox iconScheduler;
    }
}