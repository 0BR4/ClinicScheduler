namespace DiplomskiPlanerKlinike
{
    partial class FormLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLogin));
            this.label1 = new System.Windows.Forms.Label();
            this.labelUserName = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.panelSelectLanguage = new System.Windows.Forms.Panel();
            this.buttonGerman = new System.Windows.Forms.Button();
            this.buttonEnglish = new System.Windows.Forms.Button();
            this.buttonSerbian = new System.Windows.Forms.Button();
            this.buttonSelectLanguage = new System.Windows.Forms.Button();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.panelLoginTitleBar = new System.Windows.Forms.Panel();
            this.labelLoginExit = new System.Windows.Forms.Label();
            this.iconExit = new FontAwesome.Sharp.IconPictureBox();
            this.labelTitlebarLogin = new System.Windows.Forms.Label();
            this.iconLogin = new FontAwesome.Sharp.IconPictureBox();
            this.labelLoginCredentials = new System.Windows.Forms.Label();
            this.labelLoginWelcome = new System.Windows.Forms.Label();
            this.labelLoginLanguage = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonShowPassword = new System.Windows.Forms.Button();
            this.buttonHidePassword = new System.Windows.Forms.Button();
            this.panelSelectLanguage.SuspendLayout();
            this.panelLoginTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconLogin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // labelUserName
            // 
            resources.ApplyResources(this.labelUserName, "labelUserName");
            this.labelUserName.Name = "labelUserName";
            // 
            // labelPassword
            // 
            resources.ApplyResources(this.labelPassword, "labelPassword");
            this.labelPassword.Name = "labelPassword";
            // 
            // textBoxUserName
            // 
            resources.ApplyResources(this.textBoxUserName, "textBoxUserName");
            this.textBoxUserName.BackColor = System.Drawing.Color.White;
            this.textBoxUserName.ForeColor = System.Drawing.Color.Black;
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxUserName_KeyDown);
            // 
            // textBoxPassword
            // 
            resources.ApplyResources(this.textBoxPassword, "textBoxPassword");
            this.textBoxPassword.BackColor = System.Drawing.Color.White;
            this.textBoxPassword.ForeColor = System.Drawing.Color.Black;
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.UseSystemPasswordChar = true;
            this.textBoxPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxPassword_KeyDown);
            // 
            // panelSelectLanguage
            // 
            resources.ApplyResources(this.panelSelectLanguage, "panelSelectLanguage");
            this.panelSelectLanguage.Controls.Add(this.buttonGerman);
            this.panelSelectLanguage.Controls.Add(this.buttonEnglish);
            this.panelSelectLanguage.Controls.Add(this.buttonSerbian);
            this.panelSelectLanguage.Controls.Add(this.buttonSelectLanguage);
            this.panelSelectLanguage.Name = "panelSelectLanguage";
            // 
            // buttonGerman
            // 
            resources.ApplyResources(this.buttonGerman, "buttonGerman");
            this.buttonGerman.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.buttonGerman.FlatAppearance.BorderSize = 0;
            this.buttonGerman.ForeColor = System.Drawing.Color.White;
            this.buttonGerman.Name = "buttonGerman";
            this.buttonGerman.UseVisualStyleBackColor = false;
            this.buttonGerman.Click += new System.EventHandler(this.buttonGerman_Click);
            // 
            // buttonEnglish
            // 
            resources.ApplyResources(this.buttonEnglish, "buttonEnglish");
            this.buttonEnglish.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.buttonEnglish.FlatAppearance.BorderSize = 0;
            this.buttonEnglish.ForeColor = System.Drawing.Color.White;
            this.buttonEnglish.Name = "buttonEnglish";
            this.buttonEnglish.UseVisualStyleBackColor = false;
            this.buttonEnglish.Click += new System.EventHandler(this.buttonEnglish_Click);
            // 
            // buttonSerbian
            // 
            resources.ApplyResources(this.buttonSerbian, "buttonSerbian");
            this.buttonSerbian.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.buttonSerbian.FlatAppearance.BorderSize = 0;
            this.buttonSerbian.ForeColor = System.Drawing.Color.White;
            this.buttonSerbian.Name = "buttonSerbian";
            this.buttonSerbian.UseVisualStyleBackColor = false;
            this.buttonSerbian.Click += new System.EventHandler(this.buttonSerbian_Click);
            // 
            // buttonSelectLanguage
            // 
            resources.ApplyResources(this.buttonSelectLanguage, "buttonSelectLanguage");
            this.buttonSelectLanguage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.buttonSelectLanguage.FlatAppearance.BorderSize = 0;
            this.buttonSelectLanguage.ForeColor = System.Drawing.Color.White;
            this.buttonSelectLanguage.Name = "buttonSelectLanguage";
            this.buttonSelectLanguage.UseVisualStyleBackColor = false;
            this.buttonSelectLanguage.Click += new System.EventHandler(this.buttonSelectLanguage_Click);
            // 
            // buttonLogin
            // 
            resources.ApplyResources(this.buttonLogin, "buttonLogin");
            this.buttonLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.buttonLogin.FlatAppearance.BorderSize = 0;
            this.buttonLogin.ForeColor = System.Drawing.Color.White;
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.UseVisualStyleBackColor = false;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // buttonClear
            // 
            resources.ApplyResources(this.buttonClear, "buttonClear");
            this.buttonClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.buttonClear.FlatAppearance.BorderSize = 0;
            this.buttonClear.ForeColor = System.Drawing.Color.White;
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.UseVisualStyleBackColor = false;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // panelLoginTitleBar
            // 
            resources.ApplyResources(this.panelLoginTitleBar, "panelLoginTitleBar");
            this.panelLoginTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(225)))), ((int)(((byte)(192)))));
            this.panelLoginTitleBar.Controls.Add(this.labelLoginExit);
            this.panelLoginTitleBar.Controls.Add(this.iconExit);
            this.panelLoginTitleBar.Controls.Add(this.labelTitlebarLogin);
            this.panelLoginTitleBar.Controls.Add(this.iconLogin);
            this.panelLoginTitleBar.Name = "panelLoginTitleBar";
            this.panelLoginTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelLoginTitleBar_MouseDown);
            // 
            // labelLoginExit
            // 
            resources.ApplyResources(this.labelLoginExit, "labelLoginExit");
            this.labelLoginExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.labelLoginExit.Name = "labelLoginExit";
            this.labelLoginExit.Click += new System.EventHandler(this.labelLoginExit_Click);
            // 
            // iconExit
            // 
            resources.ApplyResources(this.iconExit, "iconExit");
            this.iconExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(225)))), ((int)(((byte)(192)))));
            this.iconExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.iconExit.IconChar = FontAwesome.Sharp.IconChar.SignOutAlt;
            this.iconExit.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.iconExit.IconSize = 34;
            this.iconExit.Name = "iconExit";
            this.iconExit.TabStop = false;
            this.iconExit.Click += new System.EventHandler(this.iconExit_Click);
            // 
            // labelTitlebarLogin
            // 
            resources.ApplyResources(this.labelTitlebarLogin, "labelTitlebarLogin");
            this.labelTitlebarLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.labelTitlebarLogin.Name = "labelTitlebarLogin";
            // 
            // iconLogin
            // 
            resources.ApplyResources(this.iconLogin, "iconLogin");
            this.iconLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(225)))), ((int)(((byte)(192)))));
            this.iconLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.iconLogin.IconChar = FontAwesome.Sharp.IconChar.SignInAlt;
            this.iconLogin.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.iconLogin.Name = "iconLogin";
            this.iconLogin.TabStop = false;
            // 
            // labelLoginCredentials
            // 
            resources.ApplyResources(this.labelLoginCredentials, "labelLoginCredentials");
            this.labelLoginCredentials.Name = "labelLoginCredentials";
            // 
            // labelLoginWelcome
            // 
            resources.ApplyResources(this.labelLoginWelcome, "labelLoginWelcome");
            this.labelLoginWelcome.Name = "labelLoginWelcome";
            // 
            // labelLoginLanguage
            // 
            resources.ApplyResources(this.labelLoginLanguage, "labelLoginLanguage");
            this.labelLoginLanguage.Name = "labelLoginLanguage";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Image = global::DiplomskiPlanerKlinike.Properties.Resources.login1;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // buttonShowPassword
            // 
            resources.ApplyResources(this.buttonShowPassword, "buttonShowPassword");
            this.buttonShowPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(195)))), ((int)(((byte)(108)))));
            this.buttonShowPassword.Name = "buttonShowPassword";
            this.buttonShowPassword.UseVisualStyleBackColor = false;
            this.buttonShowPassword.Click += new System.EventHandler(this.buttonShowPassword_Click);
            // 
            // buttonHidePassword
            // 
            resources.ApplyResources(this.buttonHidePassword, "buttonHidePassword");
            this.buttonHidePassword.Name = "buttonHidePassword";
            this.buttonHidePassword.UseVisualStyleBackColor = true;
            this.buttonHidePassword.Click += new System.EventHandler(this.buttonHidePassword_Click);
            // 
            // FormLogin
            // 
            resources.ApplyResources(this, "$this");
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.labelLoginLanguage);
            this.Controls.Add(this.labelLoginWelcome);
            this.Controls.Add(this.labelLoginCredentials);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panelLoginTitleBar);
            this.Controls.Add(this.buttonShowPassword);
            this.Controls.Add(this.buttonHidePassword);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.panelSelectLanguage);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxUserName);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelUserName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormLogin";
            this.panelSelectLanguage.ResumeLayout(false);
            this.panelLoginTitleBar.ResumeLayout(false);
            this.panelLoginTitleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconLogin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelUserName;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Panel panelSelectLanguage;
        private System.Windows.Forms.Button buttonSerbian;
        private System.Windows.Forms.Button buttonSelectLanguage;
        private System.Windows.Forms.Button buttonEnglish;
        private System.Windows.Forms.Button buttonGerman;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonHidePassword;
        private System.Windows.Forms.Button buttonShowPassword;
        private System.Windows.Forms.Panel panelLoginTitleBar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelLoginCredentials;
        private System.Windows.Forms.Label labelLoginWelcome;
        private System.Windows.Forms.Label labelLoginLanguage;
        private FontAwesome.Sharp.IconPictureBox iconLogin;
        private System.Windows.Forms.Label labelTitlebarLogin;
        private System.Windows.Forms.Label labelLoginExit;
        private FontAwesome.Sharp.IconPictureBox iconExit;
    }
}

