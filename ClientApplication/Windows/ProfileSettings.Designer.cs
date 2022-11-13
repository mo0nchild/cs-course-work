namespace CSCourseWork.Windows
{
    partial class ProfileSettings
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
            this.password_textbox = new System.Windows.Forms.TextBox();
            this.username_textbox = new System.Windows.Forms.TextBox();
            this.projectpath_textbox = new System.Windows.Forms.TextBox();
            this.projectpath_button = new System.Windows.Forms.Button();
            this.email_textbox = new System.Windows.Forms.TextBox();
            this.save_button = new System.Windows.Forms.Button();
            this.delete_button = new System.Windows.Forms.Button();
            this.username_label = new System.Windows.Forms.Label();
            this.password_label = new System.Windows.Forms.Label();
            this.projectpath_label = new System.Windows.Forms.Label();
            this.email_label = new System.Windows.Forms.Label();
            this.load_button = new System.Windows.Forms.Button();
            this.password_checkbox = new System.Windows.Forms.CheckBox();
            this.emailkey_label = new System.Windows.Forms.Label();
            this.emailkey_textbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // password_textbox
            // 
            this.password_textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.password_textbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.password_textbox.Location = new System.Drawing.Point(216, 35);
            this.password_textbox.MaxLength = 20;
            this.password_textbox.Name = "password_textbox";
            this.password_textbox.Size = new System.Drawing.Size(190, 29);
            this.password_textbox.TabIndex = 0;
            // 
            // username_textbox
            // 
            this.username_textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.username_textbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.username_textbox.Location = new System.Drawing.Point(12, 35);
            this.username_textbox.MaxLength = 20;
            this.username_textbox.Name = "username_textbox";
            this.username_textbox.Size = new System.Drawing.Size(190, 29);
            this.username_textbox.TabIndex = 1;
            // 
            // projectpath_textbox
            // 
            this.projectpath_textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.projectpath_textbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.projectpath_textbox.Location = new System.Drawing.Point(12, 107);
            this.projectpath_textbox.MaxLength = 200;
            this.projectpath_textbox.Name = "projectpath_textbox";
            this.projectpath_textbox.ReadOnly = true;
            this.projectpath_textbox.Size = new System.Drawing.Size(359, 29);
            this.projectpath_textbox.TabIndex = 2;
            // 
            // projectpath_button
            // 
            this.projectpath_button.BackColor = System.Drawing.Color.White;
            this.projectpath_button.BackgroundImage = global::CSCourseWork.Windows.Resourses.FolderIcon;
            this.projectpath_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.projectpath_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.projectpath_button.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.projectpath_button.Location = new System.Drawing.Point(377, 106);
            this.projectpath_button.Name = "projectpath_button";
            this.projectpath_button.Size = new System.Drawing.Size(29, 29);
            this.projectpath_button.TabIndex = 3;
            this.projectpath_button.UseVisualStyleBackColor = false;
            // 
            // email_textbox
            // 
            this.email_textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.email_textbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.email_textbox.Location = new System.Drawing.Point(12, 161);
            this.email_textbox.MaxLength = 100;
            this.email_textbox.Name = "email_textbox";
            this.email_textbox.Size = new System.Drawing.Size(190, 29);
            this.email_textbox.TabIndex = 4;
            // 
            // save_button
            // 
            this.save_button.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.save_button.Location = new System.Drawing.Point(12, 304);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(394, 29);
            this.save_button.TabIndex = 5;
            this.save_button.Text = "Сохранить настройки";
            this.save_button.UseVisualStyleBackColor = true;
            // 
            // delete_button
            // 
            this.delete_button.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.delete_button.Location = new System.Drawing.Point(216, 269);
            this.delete_button.Name = "delete_button";
            this.delete_button.Size = new System.Drawing.Size(190, 29);
            this.delete_button.TabIndex = 6;
            this.delete_button.Text = "Удалить профиль";
            this.delete_button.UseVisualStyleBackColor = true;
            // 
            // username_label
            // 
            this.username_label.AutoSize = true;
            this.username_label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.username_label.Location = new System.Drawing.Point(12, 13);
            this.username_label.Name = "username_label";
            this.username_label.Size = new System.Drawing.Size(128, 19);
            this.username_label.TabIndex = 7;
            this.username_label.Text = "Имя пользователя:";
            // 
            // password_label
            // 
            this.password_label.AutoSize = true;
            this.password_label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.password_label.Location = new System.Drawing.Point(216, 13);
            this.password_label.Name = "password_label";
            this.password_label.Size = new System.Drawing.Size(59, 19);
            this.password_label.TabIndex = 8;
            this.password_label.Text = "Пароль:";
            // 
            // projectpath_label
            // 
            this.projectpath_label.AutoSize = true;
            this.projectpath_label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.projectpath_label.Location = new System.Drawing.Point(12, 85);
            this.projectpath_label.Name = "projectpath_label";
            this.projectpath_label.Size = new System.Drawing.Size(179, 19);
            this.projectpath_label.TabIndex = 9;
            this.projectpath_label.Text = "Пользовательский каталог:";
            // 
            // email_label
            // 
            this.email_label.AutoSize = true;
            this.email_label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.email_label.Location = new System.Drawing.Point(12, 139);
            this.email_label.Name = "email_label";
            this.email_label.Size = new System.Drawing.Size(183, 19);
            this.email_label.TabIndex = 10;
            this.email_label.Text = "Адрес электронной почты: ";
            // 
            // load_button
            // 
            this.load_button.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.load_button.Location = new System.Drawing.Point(12, 269);
            this.load_button.Name = "load_button";
            this.load_button.Size = new System.Drawing.Size(190, 29);
            this.load_button.TabIndex = 11;
            this.load_button.Text = "Загрузить профиль";
            this.load_button.UseVisualStyleBackColor = true;
            // 
            // password_checkbox
            // 
            this.password_checkbox.AutoSize = true;
            this.password_checkbox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.password_checkbox.Location = new System.Drawing.Point(216, 70);
            this.password_checkbox.Name = "password_checkbox";
            this.password_checkbox.Size = new System.Drawing.Size(135, 23);
            this.password_checkbox.TabIndex = 12;
            this.password_checkbox.Text = "Показать пароль";
            this.password_checkbox.UseVisualStyleBackColor = true;
            // 
            // emailkey_label
            // 
            this.emailkey_label.AutoSize = true;
            this.emailkey_label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.emailkey_label.Location = new System.Drawing.Point(216, 139);
            this.emailkey_label.Name = "emailkey_label";
            this.emailkey_label.Size = new System.Drawing.Size(192, 19);
            this.emailkey_label.TabIndex = 14;
            this.emailkey_label.Text = "Пароль электронной почты: ";
            // 
            // emailkey_textbox
            // 
            this.emailkey_textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.emailkey_textbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.emailkey_textbox.Location = new System.Drawing.Point(216, 161);
            this.emailkey_textbox.MaxLength = 100;
            this.emailkey_textbox.Name = "emailkey_textbox";
            this.emailkey_textbox.Size = new System.Drawing.Size(190, 29);
            this.emailkey_textbox.TabIndex = 13;
            // 
            // ProfileSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(418, 349);
            this.Controls.Add(this.emailkey_label);
            this.Controls.Add(this.emailkey_textbox);
            this.Controls.Add(this.password_checkbox);
            this.Controls.Add(this.load_button);
            this.Controls.Add(this.email_label);
            this.Controls.Add(this.projectpath_label);
            this.Controls.Add(this.password_label);
            this.Controls.Add(this.username_label);
            this.Controls.Add(this.delete_button);
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.email_textbox);
            this.Controls.Add(this.projectpath_button);
            this.Controls.Add(this.projectpath_textbox);
            this.Controls.Add(this.username_textbox);
            this.Controls.Add(this.password_textbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(434, 388);
            this.MinimumSize = new System.Drawing.Size(434, 388);
            this.Name = "ProfileSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройка профиля";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox password_textbox;
        private TextBox username_textbox;
        private TextBox projectpath_textbox;
        private Button projectpath_button;
        private TextBox email_textbox;
        private Button save_button;
        private Button delete_button;
        private Label username_label;
        private Label password_label;
        private Label projectpath_label;
        private Label email_label;
        private Button load_button;
        private CheckBox password_checkbox;
        private Label emailkey_label;
        private TextBox emailkey_textbox;
    }
}