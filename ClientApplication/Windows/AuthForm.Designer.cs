namespace CSCourseWork.Windows
{
    internal partial class AuthForm
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
            this.auth_tabcontrol = new System.Windows.Forms.TabControl();
            this.authorization_tabpage = new System.Windows.Forms.TabPage();
            this.auth_checkbox = new System.Windows.Forms.CheckBox();
            this.auth_password_label = new System.Windows.Forms.Label();
            this.auth_name_label = new System.Windows.Forms.Label();
            this.login_button = new System.Windows.Forms.Button();
            this.auth_password_textbox = new System.Windows.Forms.TextBox();
            this.auth_name_textbox = new System.Windows.Forms.TextBox();
            this.registration_tabpage = new System.Windows.Forms.TabPage();
            this.reg_checkbox = new System.Windows.Forms.CheckBox();
            this.register_button = new System.Windows.Forms.Button();
            this.filepath_button = new System.Windows.Forms.Button();
            this.reg_filepath_label = new System.Windows.Forms.Label();
            this.reg_filepath_textbox = new System.Windows.Forms.TextBox();
            this.reg_email_label = new System.Windows.Forms.Label();
            this.reg_email_textbox = new System.Windows.Forms.TextBox();
            this.reg_password_label = new System.Windows.Forms.Label();
            this.reg_password_textbox = new System.Windows.Forms.TextBox();
            this.reg_name_label = new System.Windows.Forms.Label();
            this.reg_name_textbox = new System.Windows.Forms.TextBox();
            this.auth_tabcontrol.SuspendLayout();
            this.authorization_tabpage.SuspendLayout();
            this.registration_tabpage.SuspendLayout();
            this.SuspendLayout();
            // 
            // auth_tabcontrol
            // 
            this.auth_tabcontrol.Controls.Add(this.authorization_tabpage);
            this.auth_tabcontrol.Controls.Add(this.registration_tabpage);
            this.auth_tabcontrol.Location = new System.Drawing.Point(3, 3);
            this.auth_tabcontrol.Name = "auth_tabcontrol";
            this.auth_tabcontrol.SelectedIndex = 0;
            this.auth_tabcontrol.Size = new System.Drawing.Size(413, 345);
            this.auth_tabcontrol.TabIndex = 0;
            // 
            // authorization_tabpage
            // 
            this.authorization_tabpage.Controls.Add(this.auth_checkbox);
            this.authorization_tabpage.Controls.Add(this.auth_password_label);
            this.authorization_tabpage.Controls.Add(this.auth_name_label);
            this.authorization_tabpage.Controls.Add(this.login_button);
            this.authorization_tabpage.Controls.Add(this.auth_password_textbox);
            this.authorization_tabpage.Controls.Add(this.auth_name_textbox);
            this.authorization_tabpage.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.authorization_tabpage.Location = new System.Drawing.Point(4, 24);
            this.authorization_tabpage.Name = "authorization_tabpage";
            this.authorization_tabpage.Padding = new System.Windows.Forms.Padding(3);
            this.authorization_tabpage.Size = new System.Drawing.Size(405, 317);
            this.authorization_tabpage.TabIndex = 0;
            this.authorization_tabpage.Text = "Авторизация";
            this.authorization_tabpage.UseVisualStyleBackColor = true;
            // 
            // auth_checkbox
            // 
            this.auth_checkbox.AutoSize = true;
            this.auth_checkbox.Location = new System.Drawing.Point(60, 145);
            this.auth_checkbox.Name = "auth_checkbox";
            this.auth_checkbox.Size = new System.Drawing.Size(135, 23);
            this.auth_checkbox.TabIndex = 5;
            this.auth_checkbox.Text = "Показать пароль";
            this.auth_checkbox.UseVisualStyleBackColor = true;
            // 
            // auth_password_label
            // 
            this.auth_password_label.AutoSize = true;
            this.auth_password_label.Location = new System.Drawing.Point(60, 88);
            this.auth_password_label.Name = "auth_password_label";
            this.auth_password_label.Size = new System.Drawing.Size(59, 19);
            this.auth_password_label.TabIndex = 4;
            this.auth_password_label.Text = "Пароль:";
            // 
            // auth_name_label
            // 
            this.auth_name_label.AutoSize = true;
            this.auth_name_label.Location = new System.Drawing.Point(60, 25);
            this.auth_name_label.Name = "auth_name_label";
            this.auth_name_label.Size = new System.Drawing.Size(103, 19);
            this.auth_name_label.TabIndex = 3;
            this.auth_name_label.Text = "Имя профиля: ";
            // 
            // login_button
            // 
            this.login_button.BackColor = System.Drawing.Color.White;
            this.login_button.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.login_button.Location = new System.Drawing.Point(60, 234);
            this.login_button.Name = "login_button";
            this.login_button.Size = new System.Drawing.Size(282, 40);
            this.login_button.TabIndex = 2;
            this.login_button.Text = "Войти в профиль";
            this.login_button.UseVisualStyleBackColor = false;
            // 
            // auth_password_textbox
            // 
            this.auth_password_textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.auth_password_textbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.auth_password_textbox.Location = new System.Drawing.Point(60, 110);
            this.auth_password_textbox.MaxLength = 20;
            this.auth_password_textbox.Name = "auth_password_textbox";
            this.auth_password_textbox.PasswordChar = '*';
            this.auth_password_textbox.Size = new System.Drawing.Size(282, 29);
            this.auth_password_textbox.TabIndex = 1;
            // 
            // auth_name_textbox
            // 
            this.auth_name_textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.auth_name_textbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.auth_name_textbox.Location = new System.Drawing.Point(60, 47);
            this.auth_name_textbox.MaxLength = 20;
            this.auth_name_textbox.Name = "auth_name_textbox";
            this.auth_name_textbox.Size = new System.Drawing.Size(282, 29);
            this.auth_name_textbox.TabIndex = 0;
            // 
            // registration_tabpage
            // 
            this.registration_tabpage.Controls.Add(this.reg_checkbox);
            this.registration_tabpage.Controls.Add(this.register_button);
            this.registration_tabpage.Controls.Add(this.filepath_button);
            this.registration_tabpage.Controls.Add(this.reg_filepath_label);
            this.registration_tabpage.Controls.Add(this.reg_filepath_textbox);
            this.registration_tabpage.Controls.Add(this.reg_email_label);
            this.registration_tabpage.Controls.Add(this.reg_email_textbox);
            this.registration_tabpage.Controls.Add(this.reg_password_label);
            this.registration_tabpage.Controls.Add(this.reg_password_textbox);
            this.registration_tabpage.Controls.Add(this.reg_name_label);
            this.registration_tabpage.Controls.Add(this.reg_name_textbox);
            this.registration_tabpage.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.registration_tabpage.Location = new System.Drawing.Point(4, 24);
            this.registration_tabpage.Name = "registration_tabpage";
            this.registration_tabpage.Padding = new System.Windows.Forms.Padding(3);
            this.registration_tabpage.Size = new System.Drawing.Size(405, 317);
            this.registration_tabpage.TabIndex = 1;
            this.registration_tabpage.Text = "Регистрация";
            this.registration_tabpage.UseVisualStyleBackColor = true;
            // 
            // reg_checkbox
            // 
            this.reg_checkbox.AutoSize = true;
            this.reg_checkbox.Location = new System.Drawing.Point(212, 81);
            this.reg_checkbox.Name = "reg_checkbox";
            this.reg_checkbox.Size = new System.Drawing.Size(135, 23);
            this.reg_checkbox.TabIndex = 14;
            this.reg_checkbox.Text = "Показать пароль";
            this.reg_checkbox.UseVisualStyleBackColor = true;
            // 
            // register_button
            // 
            this.register_button.BackColor = System.Drawing.Color.White;
            this.register_button.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.register_button.Location = new System.Drawing.Point(21, 234);
            this.register_button.Name = "register_button";
            this.register_button.Size = new System.Drawing.Size(360, 40);
            this.register_button.TabIndex = 13;
            this.register_button.Text = "Регистрация профиля";
            this.register_button.UseVisualStyleBackColor = false;
            // 
            // filepath_button
            // 
            this.filepath_button.BackColor = System.Drawing.Color.White;
            this.filepath_button.BackgroundImage = global::CSCourseWork.Windows.Resourses.FolderIcon;
            this.filepath_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.filepath_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.filepath_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.filepath_button.Location = new System.Drawing.Point(352, 180);
            this.filepath_button.Name = "filepath_button";
            this.filepath_button.Size = new System.Drawing.Size(29, 29);
            this.filepath_button.TabIndex = 12;
            this.filepath_button.UseVisualStyleBackColor = false;
            // 
            // reg_filepath_label
            // 
            this.reg_filepath_label.AutoSize = true;
            this.reg_filepath_label.Location = new System.Drawing.Point(21, 158);
            this.reg_filepath_label.Name = "reg_filepath_label";
            this.reg_filepath_label.Size = new System.Drawing.Size(180, 19);
            this.reg_filepath_label.TabIndex = 11;
            this.reg_filepath_label.Text = "Пользовательский католог:";
            // 
            // reg_filepath_textbox
            // 
            this.reg_filepath_textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.reg_filepath_textbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.reg_filepath_textbox.Location = new System.Drawing.Point(21, 180);
            this.reg_filepath_textbox.MaxLength = 200;
            this.reg_filepath_textbox.Name = "reg_filepath_textbox";
            this.reg_filepath_textbox.ReadOnly = true;
            this.reg_filepath_textbox.Size = new System.Drawing.Size(325, 29);
            this.reg_filepath_textbox.TabIndex = 10;
            // 
            // reg_email_label
            // 
            this.reg_email_label.AutoSize = true;
            this.reg_email_label.Location = new System.Drawing.Point(21, 101);
            this.reg_email_label.Name = "reg_email_label";
            this.reg_email_label.Size = new System.Drawing.Size(179, 19);
            this.reg_email_label.TabIndex = 9;
            this.reg_email_label.Text = "Адрес электронной почты:";
            // 
            // reg_email_textbox
            // 
            this.reg_email_textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.reg_email_textbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.reg_email_textbox.Location = new System.Drawing.Point(21, 123);
            this.reg_email_textbox.MaxLength = 50;
            this.reg_email_textbox.Name = "reg_email_textbox";
            this.reg_email_textbox.Size = new System.Drawing.Size(360, 29);
            this.reg_email_textbox.TabIndex = 8;
            // 
            // reg_password_label
            // 
            this.reg_password_label.AutoSize = true;
            this.reg_password_label.Location = new System.Drawing.Point(212, 25);
            this.reg_password_label.Name = "reg_password_label";
            this.reg_password_label.Size = new System.Drawing.Size(59, 19);
            this.reg_password_label.TabIndex = 7;
            this.reg_password_label.Text = "Пароль:";
            // 
            // reg_password_textbox
            // 
            this.reg_password_textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.reg_password_textbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.reg_password_textbox.Location = new System.Drawing.Point(212, 47);
            this.reg_password_textbox.MaxLength = 20;
            this.reg_password_textbox.Name = "reg_password_textbox";
            this.reg_password_textbox.PasswordChar = '*';
            this.reg_password_textbox.Size = new System.Drawing.Size(169, 29);
            this.reg_password_textbox.TabIndex = 6;
            // 
            // reg_name_label
            // 
            this.reg_name_label.AutoSize = true;
            this.reg_name_label.Location = new System.Drawing.Point(21, 25);
            this.reg_name_label.Name = "reg_name_label";
            this.reg_name_label.Size = new System.Drawing.Size(99, 19);
            this.reg_name_label.TabIndex = 5;
            this.reg_name_label.Text = "Имя профиля:";
            // 
            // reg_name_textbox
            // 
            this.reg_name_textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.reg_name_textbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.reg_name_textbox.Location = new System.Drawing.Point(21, 47);
            this.reg_name_textbox.MaxLength = 20;
            this.reg_name_textbox.Name = "reg_name_textbox";
            this.reg_name_textbox.Size = new System.Drawing.Size(169, 29);
            this.reg_name_textbox.TabIndex = 4;
            // 
            // AuthForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(418, 349);
            this.Controls.Add(this.auth_tabcontrol);
            this.MaximumSize = new System.Drawing.Size(434, 388);
            this.MinimumSize = new System.Drawing.Size(434, 388);
            this.Name = "AuthForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Авторизация";
            this.auth_tabcontrol.ResumeLayout(false);
            this.authorization_tabpage.ResumeLayout(false);
            this.authorization_tabpage.PerformLayout();
            this.registration_tabpage.ResumeLayout(false);
            this.registration_tabpage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl auth_tabcontrol;
        private TabPage authorization_tabpage;
        private TabPage registration_tabpage;
        private Label auth_password_label;
        private Label auth_name_label;
        private Button login_button;
        private TextBox auth_password_textbox;
        private TextBox auth_name_textbox;
        private CheckBox auth_checkbox;
        private Button filepath_button;
        private Label reg_filepath_label;
        private TextBox reg_filepath_textbox;
        private Label reg_email_label;
        private TextBox reg_email_textbox;
        private Label reg_password_label;
        private TextBox reg_password_textbox;
        private Label reg_name_label;
        private TextBox reg_name_textbox;
        private Button register_button;
        private CheckBox reg_checkbox;
    }
}