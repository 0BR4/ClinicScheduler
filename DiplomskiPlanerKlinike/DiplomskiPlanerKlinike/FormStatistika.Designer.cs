namespace DiplomskiPlanerKlinike
{
    partial class FormStatistika
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStatistika));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.panelSelectPagesStat = new System.Windows.Forms.Panel();
            this.buttonListDoctorsStat = new System.Windows.Forms.Button();
            this.buttonSelectPagesStat = new System.Windows.Forms.Button();
            this.buttonListPatientsStat = new System.Windows.Forms.Button();
            this.buttonSchedulerStat = new System.Windows.Forms.Button();
            this.chartStat = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.buttonChartGenerate = new System.Windows.Forms.Button();
            this.textBoxChartYear = new System.Windows.Forms.TextBox();
            this.labelChartYear = new System.Windows.Forms.Label();
            this.checkBoxChartExam = new System.Windows.Forms.CheckBox();
            this.checkBoxChartLab = new System.Windows.Forms.CheckBox();
            this.checkBoxChartTherapy = new System.Windows.Forms.CheckBox();
            this.checkBoxChartOperation = new System.Windows.Forms.CheckBox();
            this.checkBoxChartCheckup = new System.Windows.Forms.CheckBox();
            this.panelStatsTitleBar = new System.Windows.Forms.Panel();
            this.labelListDoctorLogout = new System.Windows.Forms.Label();
            this.iconListDoctorLogout = new FontAwesome.Sharp.IconPictureBox();
            this.labelLogout = new System.Windows.Forms.Label();
            this.iconLogout = new FontAwesome.Sharp.IconPictureBox();
            this.labelTitlebarLogin = new System.Windows.Forms.Label();
            this.iconScheduler = new FontAwesome.Sharp.IconPictureBox();
            this.panelSelectPagesStat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartStat)).BeginInit();
            this.panelStatsTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconListDoctorLogout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconLogout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconScheduler)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSelectPagesStat
            // 
            resources.ApplyResources(this.panelSelectPagesStat, "panelSelectPagesStat");
            this.panelSelectPagesStat.Controls.Add(this.buttonListDoctorsStat);
            this.panelSelectPagesStat.Controls.Add(this.buttonSelectPagesStat);
            this.panelSelectPagesStat.Controls.Add(this.buttonListPatientsStat);
            this.panelSelectPagesStat.Controls.Add(this.buttonSchedulerStat);
            this.panelSelectPagesStat.Name = "panelSelectPagesStat";
            // 
            // buttonListDoctorsStat
            // 
            resources.ApplyResources(this.buttonListDoctorsStat, "buttonListDoctorsStat");
            this.buttonListDoctorsStat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.buttonListDoctorsStat.FlatAppearance.BorderSize = 0;
            this.buttonListDoctorsStat.ForeColor = System.Drawing.Color.White;
            this.buttonListDoctorsStat.Name = "buttonListDoctorsStat";
            this.buttonListDoctorsStat.UseVisualStyleBackColor = false;
            this.buttonListDoctorsStat.Click += new System.EventHandler(this.buttonListDoctorsStat_Click);
            // 
            // buttonSelectPagesStat
            // 
            resources.ApplyResources(this.buttonSelectPagesStat, "buttonSelectPagesStat");
            this.buttonSelectPagesStat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.buttonSelectPagesStat.FlatAppearance.BorderSize = 0;
            this.buttonSelectPagesStat.ForeColor = System.Drawing.Color.White;
            this.buttonSelectPagesStat.Name = "buttonSelectPagesStat";
            this.buttonSelectPagesStat.UseVisualStyleBackColor = false;
            this.buttonSelectPagesStat.Click += new System.EventHandler(this.buttonSelectPagesStat_Click);
            // 
            // buttonListPatientsStat
            // 
            resources.ApplyResources(this.buttonListPatientsStat, "buttonListPatientsStat");
            this.buttonListPatientsStat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.buttonListPatientsStat.FlatAppearance.BorderSize = 0;
            this.buttonListPatientsStat.ForeColor = System.Drawing.Color.White;
            this.buttonListPatientsStat.Name = "buttonListPatientsStat";
            this.buttonListPatientsStat.UseVisualStyleBackColor = false;
            this.buttonListPatientsStat.Click += new System.EventHandler(this.buttonListPatientsStat_Click);
            // 
            // buttonSchedulerStat
            // 
            resources.ApplyResources(this.buttonSchedulerStat, "buttonSchedulerStat");
            this.buttonSchedulerStat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.buttonSchedulerStat.FlatAppearance.BorderSize = 0;
            this.buttonSchedulerStat.ForeColor = System.Drawing.Color.White;
            this.buttonSchedulerStat.Name = "buttonSchedulerStat";
            this.buttonSchedulerStat.UseVisualStyleBackColor = false;
            this.buttonSchedulerStat.Click += new System.EventHandler(this.buttonSchedulerStat_Click);
            // 
            // chartStat
            // 
            resources.ApplyResources(this.chartStat, "chartStat");
            this.chartStat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(225)))), ((int)(((byte)(192)))));
            this.chartStat.BorderlineColor = System.Drawing.Color.Black;
            chartArea1.Name = "ChartArea1";
            this.chartStat.ChartAreas.Add(chartArea1);
            legend1.Font = new System.Drawing.Font("Verdana", 8.25F);
            legend1.IsTextAutoFit = false;
            legend1.LegendItemOrder = System.Windows.Forms.DataVisualization.Charting.LegendItemOrder.SameAsSeriesOrder;
            legend1.Name = "Legend1";
            legend1.Position.Auto = false;
            legend1.Position.Height = 5F;
            legend1.Position.Width = 20F;
            legend1.Position.X = 80F;
            legend1.Position.Y = 5F;
            legend2.Font = new System.Drawing.Font("Verdana", 8.25F);
            legend2.IsTextAutoFit = false;
            legend2.Name = "Legend2";
            legend2.Position.Auto = false;
            legend2.Position.Height = 5F;
            legend2.Position.Width = 20F;
            legend2.Position.X = 80F;
            legend2.Position.Y = 10F;
            legend3.Font = new System.Drawing.Font("Verdana", 8.25F);
            legend3.IsTextAutoFit = false;
            legend3.Name = "Legend3";
            legend3.Position.Auto = false;
            legend3.Position.Height = 5F;
            legend3.Position.Width = 20F;
            legend3.Position.X = 80F;
            legend3.Position.Y = 15F;
            legend4.Font = new System.Drawing.Font("Verdana", 8.25F);
            legend4.IsTextAutoFit = false;
            legend4.Name = "Legend4";
            legend4.Position.Auto = false;
            legend4.Position.Height = 5F;
            legend4.Position.Width = 20F;
            legend4.Position.X = 80F;
            legend4.Position.Y = 20F;
            legend5.Font = new System.Drawing.Font("Verdana", 8.25F);
            legend5.InterlacedRows = true;
            legend5.IsEquallySpacedItems = true;
            legend5.IsTextAutoFit = false;
            legend5.Name = "Legend5";
            legend5.Position.Auto = false;
            legend5.Position.Height = 5F;
            legend5.Position.Width = 20F;
            legend5.Position.X = 80F;
            legend5.Position.Y = 25F;
            this.chartStat.Legends.Add(legend1);
            this.chartStat.Legends.Add(legend2);
            this.chartStat.Legends.Add(legend3);
            this.chartStat.Legends.Add(legend4);
            this.chartStat.Legends.Add(legend5);
            this.chartStat.Name = "chartStat";
            // 
            // buttonChartGenerate
            // 
            resources.ApplyResources(this.buttonChartGenerate, "buttonChartGenerate");
            this.buttonChartGenerate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.buttonChartGenerate.FlatAppearance.BorderSize = 0;
            this.buttonChartGenerate.ForeColor = System.Drawing.Color.White;
            this.buttonChartGenerate.Name = "buttonChartGenerate";
            this.buttonChartGenerate.UseVisualStyleBackColor = false;
            this.buttonChartGenerate.Click += new System.EventHandler(this.buttonChartGenerate_Click);
            // 
            // textBoxChartYear
            // 
            resources.ApplyResources(this.textBoxChartYear, "textBoxChartYear");
            this.textBoxChartYear.Name = "textBoxChartYear";
            // 
            // labelChartYear
            // 
            resources.ApplyResources(this.labelChartYear, "labelChartYear");
            this.labelChartYear.Name = "labelChartYear";
            // 
            // checkBoxChartExam
            // 
            resources.ApplyResources(this.checkBoxChartExam, "checkBoxChartExam");
            this.checkBoxChartExam.Checked = true;
            this.checkBoxChartExam.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxChartExam.Name = "checkBoxChartExam";
            this.checkBoxChartExam.UseVisualStyleBackColor = true;
            // 
            // checkBoxChartLab
            // 
            resources.ApplyResources(this.checkBoxChartLab, "checkBoxChartLab");
            this.checkBoxChartLab.Checked = true;
            this.checkBoxChartLab.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxChartLab.Name = "checkBoxChartLab";
            this.checkBoxChartLab.UseVisualStyleBackColor = true;
            // 
            // checkBoxChartTherapy
            // 
            resources.ApplyResources(this.checkBoxChartTherapy, "checkBoxChartTherapy");
            this.checkBoxChartTherapy.Checked = true;
            this.checkBoxChartTherapy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxChartTherapy.Name = "checkBoxChartTherapy";
            this.checkBoxChartTherapy.UseVisualStyleBackColor = true;
            // 
            // checkBoxChartOperation
            // 
            resources.ApplyResources(this.checkBoxChartOperation, "checkBoxChartOperation");
            this.checkBoxChartOperation.Checked = true;
            this.checkBoxChartOperation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxChartOperation.Name = "checkBoxChartOperation";
            this.checkBoxChartOperation.UseVisualStyleBackColor = true;
            // 
            // checkBoxChartCheckup
            // 
            resources.ApplyResources(this.checkBoxChartCheckup, "checkBoxChartCheckup");
            this.checkBoxChartCheckup.Checked = true;
            this.checkBoxChartCheckup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxChartCheckup.Name = "checkBoxChartCheckup";
            this.checkBoxChartCheckup.UseVisualStyleBackColor = true;
            // 
            // panelStatsTitleBar
            // 
            resources.ApplyResources(this.panelStatsTitleBar, "panelStatsTitleBar");
            this.panelStatsTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(225)))), ((int)(((byte)(192)))));
            this.panelStatsTitleBar.Controls.Add(this.labelListDoctorLogout);
            this.panelStatsTitleBar.Controls.Add(this.iconListDoctorLogout);
            this.panelStatsTitleBar.Controls.Add(this.labelLogout);
            this.panelStatsTitleBar.Controls.Add(this.iconLogout);
            this.panelStatsTitleBar.Controls.Add(this.labelTitlebarLogin);
            this.panelStatsTitleBar.Controls.Add(this.iconScheduler);
            this.panelStatsTitleBar.Name = "panelStatsTitleBar";
            this.panelStatsTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelStatsTitleBar_MouseDown);
            // 
            // labelListDoctorLogout
            // 
            resources.ApplyResources(this.labelListDoctorLogout, "labelListDoctorLogout");
            this.labelListDoctorLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.labelListDoctorLogout.Name = "labelListDoctorLogout";
            this.labelListDoctorLogout.Click += new System.EventHandler(this.labelListDoctorLogout_Click);
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
            // labelLogout
            // 
            resources.ApplyResources(this.labelLogout, "labelLogout");
            this.labelLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.labelLogout.Name = "labelLogout";
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
            // labelTitlebarLogin
            // 
            resources.ApplyResources(this.labelTitlebarLogin, "labelTitlebarLogin");
            this.labelTitlebarLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.labelTitlebarLogin.Name = "labelTitlebarLogin";
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
            // FormStatistika
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelStatsTitleBar);
            this.Controls.Add(this.checkBoxChartCheckup);
            this.Controls.Add(this.checkBoxChartOperation);
            this.Controls.Add(this.checkBoxChartTherapy);
            this.Controls.Add(this.checkBoxChartLab);
            this.Controls.Add(this.checkBoxChartExam);
            this.Controls.Add(this.labelChartYear);
            this.Controls.Add(this.textBoxChartYear);
            this.Controls.Add(this.buttonChartGenerate);
            this.Controls.Add(this.chartStat);
            this.Controls.Add(this.panelSelectPagesStat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormStatistika";
            this.Load += new System.EventHandler(this.FormStatistika_Load);
            this.panelSelectPagesStat.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartStat)).EndInit();
            this.panelStatsTitleBar.ResumeLayout(false);
            this.panelStatsTitleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconListDoctorLogout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconLogout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconScheduler)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panelSelectPagesStat;
        private System.Windows.Forms.Button buttonSelectPagesStat;
        private System.Windows.Forms.Button buttonSchedulerStat;
        private System.Windows.Forms.Button buttonListDoctorsStat;
        private System.Windows.Forms.Button buttonListPatientsStat;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartStat;
        private System.Windows.Forms.Button buttonChartGenerate;
        private System.Windows.Forms.TextBox textBoxChartYear;
        private System.Windows.Forms.Label labelChartYear;
        private System.Windows.Forms.CheckBox checkBoxChartExam;
        private System.Windows.Forms.CheckBox checkBoxChartLab;
        private System.Windows.Forms.CheckBox checkBoxChartTherapy;
        private System.Windows.Forms.CheckBox checkBoxChartOperation;
        private System.Windows.Forms.CheckBox checkBoxChartCheckup;
        private System.Windows.Forms.Panel panelStatsTitleBar;
        private System.Windows.Forms.Label labelListDoctorLogout;
        private FontAwesome.Sharp.IconPictureBox iconListDoctorLogout;
        private System.Windows.Forms.Label labelLogout;
        private FontAwesome.Sharp.IconPictureBox iconLogout;
        private System.Windows.Forms.Label labelTitlebarLogin;
        private FontAwesome.Sharp.IconPictureBox iconScheduler;
    }
}