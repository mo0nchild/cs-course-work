using GraphServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSCourseWork.Windows
{
    public partial class ProfileSettings : Form
    {
        private System.Guid ProfileID { get; set; } = default;
        public ProfileSettings(System.Guid profile_id)
        {
            this.InitializeComponent(); this.ProfileID = profile_id;
            this.password_textbox.PasswordChar = '*';

            this.projectpath_button.Click += new EventHandler(ProjectpathButtonClick);
            this.password_checkbox.CheckedChanged += new EventHandler(CheckboxCheckedChanged);

            this.delete_button.Click += new EventHandler(DeleteButtonClick);
            this.save_button.Click += new EventHandler(SaveButtonClick);
            this.load_button.Click += new EventHandler(LoadButtonClick);

            this.LoadButtonClick(this, EventArgs.Empty);
        }

        private void CheckboxCheckedChanged(object? sender, EventArgs args)
        {
            this.password_textbox.PasswordChar = (this.password_textbox.PasswordChar == '*') ? default : '*';
        }

        private void LoadButtonClick(object? sender, EventArgs args)
        {
            ProfileData profile_data = default!;
            using (var profile_contoller = new GraphServiceReference.ProfileControllerClient())
            {
                try { profile_data = profile_contoller.ReadProfile(this.ProfileID); }
                catch (FaultException<GraphServiceReference.ProfileControllerException> error)
                {
                    MessageBox.Show(error.Detail.Message, "Ошибка"); return;
                }
                catch (CommunicationException error) { MessageBox.Show(error.Message, "Ошибка"); return; }
            }
            if (profile_data == null) 
            { MessageBox.Show("Невозможно прочитать данные профиля", "Ошибка"); return; }

            this.username_textbox.Text = profile_data.UserName;
            this.password_textbox.Text = profile_data.Password;
         
            this.projectpath_textbox.Text = profile_data.ProjectsPath;
            this.email_textbox.Text = profile_data.Email;
        }

        private void ProjectpathButtonClick(object? sender, EventArgs args)
        {
            using (var filedialog = new FolderBrowserDialog())
            {
                var result = filedialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(filedialog.SelectedPath))
                {
                    this.projectpath_textbox.Text = filedialog.SelectedPath;
                }
            }
        }

        private void SaveButtonClick(object? sender, EventArgs args)
        {
            var profile_data = new ProfileData()
            {
                UserName = this.username_textbox.Text, Password = this.password_textbox.Text,
                ProjectsPath = this.projectpath_textbox.Text, Email = this.email_textbox.Text
            };
            using (var profile_contoller = new GraphServiceReference.ProfileControllerClient())
            {
                try { profile_contoller.SetupProfile(this.ProfileID, profile_data); }
                catch (FaultException<GraphServiceReference.ProfileControllerException> error)
                {
                    MessageBox.Show(error.Detail.Message, "Ошибка"); return; 
                }
                catch (CommunicationException error) { MessageBox.Show(error.Message, "Ошибка"); return; }
            }
            MessageBox.Show("Данные профиля обновлены", "Готово");

            this.DialogResult = DialogResult.OK; this.Close();
        }

        private void DeleteButtonClick(object? sender, EventArgs args)
        {
            using (var profile_contoller = new GraphServiceReference.ProfileControllerClient())
            {
                try { profile_contoller.DeleteProfile(this.ProfileID); }
                catch (FaultException<GraphServiceReference.ProfileControllerException> error)
                {
                    MessageBox.Show(error.Detail.Message, "Ошибка"); return;
                }
                catch (CommunicationException error) { MessageBox.Show(error.Message, "Ошибка"); return; }
            }
            MessageBox.Show("Профиль удалён", "Готово");

            this.DialogResult = DialogResult.Abort; this.Close();
        }
    }
}
