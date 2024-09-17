namespace DiplomskiPlanerKlinike
{
    partial class FormSablonLab
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSablonLab));
            this.buttonLabClear = new System.Windows.Forms.Button();
            this.buttonLabConfirm = new System.Windows.Forms.Button();
            this.textBoxLabJMBG = new System.Windows.Forms.TextBox();
            this.labelLabJMBG = new System.Windows.Forms.Label();
            this.labelLabDoctor = new System.Windows.Forms.Label();
            this.textBoxLabDoctor = new System.Windows.Forms.TextBox();
            this.textBoxLabDate = new System.Windows.Forms.TextBox();
            this.textBoxLabTime = new System.Windows.Forms.TextBox();
            this.textBoxLabProblem = new System.Windows.Forms.TextBox();
            this.textBoxLabPatient = new System.Windows.Forms.TextBox();
            this.labelLabDate = new System.Windows.Forms.Label();
            this.labelLabTime = new System.Windows.Forms.Label();
            this.labelLabProblem = new System.Windows.Forms.Label();
            this.labelLabPatient = new System.Windows.Forms.Label();
            this.panelLabTitleBar = new System.Windows.Forms.Panel();
            this.labelLabExit = new System.Windows.Forms.Label();
            this.iconLabExit = new FontAwesome.Sharp.IconPictureBox();
            this.labelTitlebarLab = new System.Windows.Forms.Label();
            this.iconMedical = new FontAwesome.Sharp.IconPictureBox();
            this.panelLabTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconLabExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconMedical)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonLabClear
            // 
            resources.ApplyResources(this.buttonLabClear, "buttonLabClear");
            this.buttonLabClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.buttonLabClear.FlatAppearance.BorderSize = 0;
            this.buttonLabClear.ForeColor = System.Drawing.Color.White;
            this.buttonLabClear.Name = "buttonLabClear";
            this.buttonLabClear.UseVisualStyleBackColor = false;
            this.buttonLabClear.Click += new System.EventHandler(this.buttonLabClear_Click);
            // 
            // buttonLabConfirm
            // 
            resources.ApplyResources(this.buttonLabConfirm, "buttonLabConfirm");
            this.buttonLabConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.buttonLabConfirm.FlatAppearance.BorderSize = 0;
            this.buttonLabConfirm.ForeColor = System.Drawing.Color.White;
            this.buttonLabConfirm.Name = "buttonLabConfirm";
            this.buttonLabConfirm.UseVisualStyleBackColor = false;
            this.buttonLabConfirm.Click += new System.EventHandler(this.buttonLabConfirm_Click);
            // 
            // textBoxLabJMBG
            // 
            resources.ApplyResources(this.textBoxLabJMBG, "textBoxLabJMBG");
            this.textBoxLabJMBG.Name = "textBoxLabJMBG";
            // 
            // labelLabJMBG
            // 
            resources.ApplyResources(this.labelLabJMBG, "labelLabJMBG");
            this.labelLabJMBG.Name = "labelLabJMBG";
            // 
            // labelLabDoctor
            // 
            resources.ApplyResources(this.labelLabDoctor, "labelLabDoctor");
            this.labelLabDoctor.Name = "labelLabDoctor";
            // 
            // textBoxLabDoctor
            // 
            resources.ApplyResources(this.textBoxLabDoctor, "textBoxLabDoctor");
            this.textBoxLabDoctor.Name = "textBoxLabDoctor";
            this.textBoxLabDoctor.ReadOnly = true;
            // 
            // textBoxLabDate
            // 
            resources.ApplyResources(this.textBoxLabDate, "textBoxLabDate");
            this.textBoxLabDate.Name = "textBoxLabDate";
            this.textBoxLabDate.ReadOnly = true;
            // 
            // textBoxLabTime
            // 
            resources.ApplyResources(this.textBoxLabTime, "textBoxLabTime");
            this.textBoxLabTime.Name = "textBoxLabTime";
            this.textBoxLabTime.ReadOnly = true;
            // 
            // textBoxLabProblem
            // 
            resources.ApplyResources(this.textBoxLabProblem, "textBoxLabProblem");
            this.textBoxLabProblem.Name = "textBoxLabProblem";
            // 
            // textBoxLabPatient
            // 
            resources.ApplyResources(this.textBoxLabPatient, "textBoxLabPatient");
            this.textBoxLabPatient.Name = "textBoxLabPatient";
            // 
            // labelLabDate
            // 
            resources.ApplyResources(this.labelLabDate, "labelLabDate");
            this.labelLabDate.Name = "labelLabDate";
            // 
            // labelLabTime
            // 
            resources.ApplyResources(this.labelLabTime, "labelLabTime");
            this.labelLabTime.Name = "labelLabTime";
            // 
            // labelLabProblem
            // 
            resources.ApplyResources(this.labelLabProblem, "labelLabProblem");
            this.labelLabProblem.Name = "labelLabProblem";
            // 
            // labelLabPatient
            // 
            resources.ApplyResources(this.labelLabPatient, "labelLabPatient");
            this.labelLabPatient.Name = "labelLabPatient";
            // 
            // panelLabTitleBar
            // 
            resources.ApplyResources(this.panelLabTitleBar, "panelLabTitleBar");
            this.panelLabTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(225)))), ((int)(((byte)(192)))));
            this.panelLabTitleBar.Controls.Add(this.labelLabExit);
            this.panelLabTitleBar.Controls.Add(this.iconLabExit);
            this.panelLabTitleBar.Controls.Add(this.labelTitlebarLab);
            this.panelLabTitleBar.Controls.Add(this.iconMedical);
            this.panelLabTitleBar.Name = "panelLabTitleBar";
            this.panelLabTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelLabTitleBar_MouseDown);
            // 
            // labelLabExit
            // 
            resources.ApplyResources(this.labelLabExit, "labelLabExit");
            this.labelLabExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.labelLabExit.Name = "labelLabExit";
            this.labelLabExit.Click += new System.EventHandler(this.labelLabExit_Click);
            // 
            // iconLabExit
            // 
            resources.ApplyResources(this.iconLabExit, "iconLabExit");
            this.iconLabExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(225)))), ((int)(((byte)(192)))));
            this.iconLabExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.iconLabExit.IconChar = FontAwesome.Sharp.IconChar.SignOutAlt;
            this.iconLabExit.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.iconLabExit.IconSize = 34;
            this.iconLabExit.Name = "iconLabExit";
            this.iconLabExit.TabStop = false;
            this.iconLabExit.Click += new System.EventHandler(this.iconLabExit_Click);
            // 
            // labelTitlebarLab
            // 
            resources.ApplyResources(this.labelTitlebarLab, "labelTitlebarLab");
            this.labelTitlebarLab.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.labelTitlebarLab.Name = "labelTitlebarLab";
            // 
            // iconMedical
            // 
            resources.ApplyResources(this.iconMedical, "iconMedical");
            this.iconMedical.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(225)))), ((int)(((byte)(192)))));
            this.iconMedical.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.iconMedical.IconChar = FontAwesome.Sharp.IconChar.ClinicMedical;
            this.iconMedical.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.iconMedical.IconSize = 34;
            this.iconMedical.Name = "iconMedical";
            this.iconMedical.TabStop = false;
            // 
            // FormSablonLab
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelLabTitleBar);
            this.Controls.Add(this.buttonLabClear);
            this.Controls.Add(this.buttonLabConfirm);
            this.Controls.Add(this.textBoxLabJMBG);
            this.Controls.Add(this.labelLabJMBG);
            this.Controls.Add(this.labelLabDoctor);
            this.Controls.Add(this.textBoxLabDoctor);
            this.Controls.Add(this.textBoxLabDate);
            this.Controls.Add(this.textBoxLabTime);
            this.Controls.Add(this.textBoxLabProblem);
            this.Controls.Add(this.textBoxLabPatient);
            this.Controls.Add(this.labelLabDate);
            this.Controls.Add(this.labelLabTime);
            this.Controls.Add(this.labelLabProblem);
            this.Controls.Add(this.labelLabPatient);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormSablonLab";
            this.Load += new System.EventHandler(this.FormSablonLab_Load);
            this.panelLabTitleBar.ResumeLayout(false);
            this.panelLabTitleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconLabExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconMedical)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonLabClear;
        private System.Windows.Forms.Button buttonLabConfirm;
        private System.Windows.Forms.TextBox textBoxLabJMBG;
        private System.Windows.Forms.Label labelLabJMBG;
        private System.Windows.Forms.Label labelLabDoctor;
        private System.Windows.Forms.TextBox textBoxLabDoctor;
        private System.Windows.Forms.TextBox textBoxLabDate;
        private System.Windows.Forms.TextBox textBoxLabTime;
        private System.Windows.Forms.TextBox textBoxLabProblem;
        private System.Windows.Forms.TextBox textBoxLabPatient;
        private System.Windows.Forms.Label labelLabDate;
        private System.Windows.Forms.Label labelLabTime;
        private System.Windows.Forms.Label labelLabProblem;
        private System.Windows.Forms.Label labelLabPatient;
        private System.Windows.Forms.Panel panelLabTitleBar;
        private System.Windows.Forms.Label labelTitlebarLab;
        private FontAwesome.Sharp.IconPictureBox iconMedical;
        private System.Windows.Forms.Label labelLabExit;
        private FontAwesome.Sharp.IconPictureBox iconLabExit;
    }
}