using GraphServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSCourseWork.Windows
{
    internal partial class AuthForm : Form
    {
        private const System.Int32 MinCharacter = 5;

        public AuthForm()
        {
            this.InitializeComponent();
            this.filepath_button.FlatAppearance.BorderSize = default(int);

            this.auth_checkbox.CheckedChanged += new EventHandler(CheckboxCheckedChanged);
            this.reg_checkbox.CheckedChanged += new EventHandler(CheckboxCheckedChanged);

            this.filepath_button.Click += new EventHandler(FilepathButtonClick);
            this.login_button.Click += new EventHandler(LoginButtonClick);
            this.register_button.Click += new EventHandler(RegisterButtonClick);

            this.skip_linklabel.Click += delegate
            {
                var client_form = new ClientForm(null);
                client_form.FormClosed += (sender, args) => this.Show();

                client_form.Show(); this.Hide();
            };
        }

        private void CheckboxCheckedChanged(object? sender, EventArgs args)
        {
            var checkbox_instance = sender as CheckBox;
            if (checkbox_instance == null) return;

            switch (checkbox_instance.Name) 
            {
                case "auth_checkbox": this.auth_password_textbox.PasswordChar 
                        = (this.auth_password_textbox.PasswordChar == '*') ? default : '*'; break;

                case "reg_checkbox": this.reg_password_textbox.PasswordChar
                        = (this.reg_password_textbox.PasswordChar == '*') ? default : '*'; break;

                default: MessageBox.Show("Выбранный флаг не найден", "Ошибка"); return;
            }
        }

        private void RegisterButtonClick(object? sender, EventArgs args)
        {
            string username = this.reg_name_textbox.Text, password = this.reg_password_textbox.Text,
                email = this.reg_email_textbox.Text;

            var email_validate = Regex.IsMatch(this.reg_email_textbox.Text, @"^\w+@(?:gmail|mail).(?:ru|com)$");
            if (username.Length < MinCharacter || password.Length < MinCharacter || !email_validate)
            { 
                MessageBox.Show("Неверный формат текстовых полей", "Ошибка"); 
            }
            System.Guid? profile_id = default!;
            using (var register = new GraphServiceReference.ProfileControllerClient())
            {
                try {
                    profile_id = register.Registration(new ProfileData()
                    {
                        Email = email, Password = password, UserName = username, 
                        ProjectsPath = this.reg_filepath_textbox.Text
                    });
                }
                catch (FaultException<GraphServiceReference.ProfileControllerException> error) 
                { MessageBox.Show(error.Detail.Message, "Ошибка"); return; }

                catch (CommunicationException error) { MessageBox.Show(error.Message, "Ошибка"); return; }
            }
            MessageBox.Show("Учётная запись была создана", "Готово");

            var client_form = new ClientForm(profile_id.Value);
            client_form.FormClosed += (sender, args) => this.Show();

            client_form.Show(); this.Hide();
        }

        private void LoginButtonClick(object? sender, EventArgs args)
        {
            string username = this.auth_name_textbox.Text, password = this.auth_password_textbox.Text;
            System.Guid? profile_id = default!;

            using (var authorize = new GraphServiceReference.ProfileControllerClient())
            {
                try { profile_id = authorize.Authorization(username, password); }
                catch (FaultException<GraphServiceReference.ProfileControllerException> error) 
                { 
                    MessageBox.Show(error.Detail.Message, "Ошибка"); return; 
                }
                catch (CommunicationException error) { MessageBox.Show(error.Message, "Ошибка"); return; }
            }

            if (profile_id == null) { MessageBox.Show("Невозвожно зайти в профиль", "Ошибка"); return; }
            var client_form = new ClientForm(profile_id.Value);

            client_form.FormClosed += (sender, args) => this.Show();
            client_form.Show(); this.Hide();
        }

        private void FilepathButtonClick(object? sender, EventArgs args)
        {
            using (var filedialog = new FolderBrowserDialog()) 
            {
                var result = filedialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(filedialog.SelectedPath))
                {
                    this.reg_filepath_textbox.Text = filedialog.SelectedPath;
                }
            }
        }
    }
}
