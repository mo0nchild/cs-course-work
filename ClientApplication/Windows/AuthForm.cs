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
    public class CheckerTokenSourse : System.Object
    {
        private Dictionary<string, CheckerToken> TokensList { get; set; } = new();

        public CheckerTokenSourse() : base() { }

        public sealed class CheckerToken : System.Object
        { 
            public System.Boolean TokenValue { get; set; } = default!;
            public System.Guid TokenID { get; set; } = default!;
        }

        public CheckerToken TakeToken(string name)
        {
            if (this.TokensList.ContainsKey(name)) return this.TokensList[name];
            var token_instance = new CheckerToken() { TokenID = Guid.NewGuid(), TokenValue = default };

            this.TokensList.Add(name, token_instance); return token_instance;
        }

        public System.Boolean CheckAllToken()
        {
            foreach (KeyValuePair<string, CheckerToken> item in this.TokensList)
            {
                if (item.Value.TokenValue == default(bool)) return false;
            }
            return true;
        }
    }

    internal partial class AuthForm : Form
    {
        protected virtual System.Int32 MinCharacter { get; private set; } = 5;
        protected CheckerTokenSourse CheckerSourse { get; private set; } = new();

        public AuthForm() : base()
        {
            this.InitializeComponent(); this.InitializeTextboxToken();
            this.filepath_button.FlatAppearance.BorderSize = default(int);

            this.auth_checkbox.CheckedChanged += new EventHandler(CheckboxCheckedChanged);
            this.reg_checkbox.CheckedChanged += new EventHandler(CheckboxCheckedChanged);

            this.filepath_button.Click += new EventHandler(FilepathButtonClick);
            this.login_button.Click += new EventHandler(LoginButtonClick);
            this.register_button.Click += new EventHandler(RegisterButtonClick);

            this.reg_emailkey_textbox.TextChanged += new EventHandler(Reg_emailkey_textbox_TextChanged);
            this.reg_password_textbox.TextChanged += new EventHandler(Reg_password_textbox_TextChanged);

            this.reg_email_textbox.TextChanged += new EventHandler(Reg_email_textbox_TextChanged);
            this.reg_name_textbox.TextChanged += new EventHandler(Reg_name_textbox_TextChanged);

            this.skip_linklabel.Click += delegate(object? sender, EventArgs args)
            {
                var client_form = new ClientForm(null);
                client_form.FormClosed += (object? sender, FormClosedEventArgs args) => this.Show();

                client_form.Show(); this.Hide();
            };
            this.Load += new EventHandler(AuthForm_Load);
        }

        private void InitializeTextboxToken()
        {
            this.CheckerSourse.TakeToken(this.reg_password_textbox.Name);
            this.CheckerSourse.TakeToken(this.reg_name_textbox.Name);

            this.CheckerSourse.TakeToken(this.reg_email_textbox.Name);
            this.CheckerSourse.TakeToken(this.reg_emailkey_textbox.Name);
        }

        private void FormItemChecker(TextBox textbox, Panel colorpanel, bool expression)
        {
            var token = this.CheckerSourse.TakeToken(textbox.Name);
            colorpanel.BackColor = (token.TokenValue = expression) ? Color.GreenYellow : Color.Salmon;
        }

        private void Reg_password_textbox_TextChanged(object? sender, EventArgs args)
        {
            var password = this.reg_password_textbox.Text;
            this.FormItemChecker(this.reg_password_textbox, this.reg_password_panel, 
                password.Length >= MinCharacter);
        }

        private void Reg_name_textbox_TextChanged(object? sender, EventArgs args)
        {
            var username = this.reg_name_textbox.Text;
            this.FormItemChecker(this.reg_name_textbox, this.reg_name_panel, username.Length >= MinCharacter);
        }

        private void Reg_email_textbox_TextChanged(object? sender, EventArgs args)
        {
            var emailname = this.reg_email_textbox.Text;
            this.FormItemChecker(this.reg_email_textbox, this.reg_email_panel,
                Regex.IsMatch(emailname, @"^[\w.]+@(?:gmail|mail).(?:ru|com)$"));
        }

        private void Reg_emailkey_textbox_TextChanged(object? sender, EventArgs args)
        {
            var emailkey = this.reg_emailkey_textbox.Text;
            this.FormItemChecker(this.reg_emailkey_textbox, this.reg_emailkey_panel,
                emailkey.Length >= MinCharacter);
        }

        private void AuthForm_Load(object? sender, EventArgs args)
        {
            using (var controller = new GraphServiceReference.ProfileControllerClient())
            {
                this.auth_name_combobox.Items.Clear();

                try { foreach (var item in controller.GetProfilesName()) this.auth_name_combobox.Items.Add(item); }
                catch (FaultException<GraphServiceReference.ProfileControllerException> error)
                {
                    MessageBox.Show(error.Detail.Message, "Ошибка"); return;
                }
                catch (CommunicationException error) { MessageBox.Show(error.Message, "Ошибка"); return; }
            }
        }

        private void CheckboxCheckedChanged(object? sender, EventArgs args)
        {
            var checkbox_instance = sender as CheckBox;
            if (checkbox_instance == null) return;

            switch (checkbox_instance.Name)
            {
                case "auth_checkbox":
                    this.auth_password_textbox.PasswordChar
                        = (this.auth_password_textbox.PasswordChar == '*') ? default : '*'; break;

                case "reg_checkbox":
                    this.reg_password_textbox.PasswordChar
                        = (this.reg_password_textbox.PasswordChar == '*') ? default : '*'; break;

                default: MessageBox.Show("Выбранный флаг не найден", "Ошибка"); return;
            }
        }

        private void RegisterButtonClick(object? sender, EventArgs args)
        {
            if (!this.CheckerSourse.CheckAllToken())
            { MessageBox.Show("Неверный формат текстовых полей", "Ошибка"); return; }

            System.Guid? profile_id = default!;
            using (var register = new GraphServiceReference.ProfileControllerClient())
            {
                try {
                    profile_id = register.Registration(new ProfileData()
                    {
                        EmailName = this.reg_email_textbox.Text,
                        Password = this.reg_password_textbox.Text,
                        UserName = this.reg_name_textbox.Text,
                        EmailKey = this.reg_emailkey_textbox.Text,
                        ProjectsPath = this.reg_filepath_textbox.Text
                    });
                }
                catch (FaultException<GraphServiceReference.ProfileControllerException> error)
                { MessageBox.Show(error.Detail.Message, "Ошибка"); return; }

                catch (CommunicationException error) { MessageBox.Show(error.Message, "Ошибка"); return; }
            }
            MessageBox.Show("Учётная запись была создана", "Готово");
            this.auth_name_combobox.Items.Add(this.reg_name_textbox.Text);

            var client_form = new ClientForm(profile_id.Value);
            client_form.FormClosed += (sender, args) => this.Show();

            client_form.Show(); this.Hide();
        }

        private void LoginButtonClick(object? sender, EventArgs args)
        {
            string username = this.auth_name_combobox.Text, password = this.auth_password_textbox.Text;
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

    //internal partial class AuthForm : Form
    //{
    //    protected virtual System.Int32 MinCharacter { get; private set; } = 5;

    //    public AuthForm() : base()
    //    {
    //        this.InitializeComponent();
    //        this.filepath_button.FlatAppearance.BorderSize = default(int);

    //        this.auth_checkbox.CheckedChanged += new EventHandler(CheckboxCheckedChanged);
    //        this.reg_checkbox.CheckedChanged += new EventHandler(CheckboxCheckedChanged);

    //        this.filepath_button.Click += new EventHandler(FilepathButtonClick);
    //        this.login_button.Click += new EventHandler(LoginButtonClick);
    //        this.register_button.Click += new EventHandler(RegisterButtonClick);

    //        this.skip_linklabel.Click += delegate
    //        {
    //            var client_form = new ClientForm(null);
    //            client_form.FormClosed += (sender, args) => this.Show();

    //            client_form.Show(); this.Hide();
    //        };
    //        this.Load += AuthForm_Load;
    //    }

    //    private void AuthForm_Load(object? sender, EventArgs e)
    //    {
    //        using (var controller = new GraphServiceReference.ProfileControllerClient())
    //        {
    //            this.auth_name_combobox.Items.Clear();

    //            try { foreach (var item in controller.GetProfilesName()) this.auth_name_combobox.Items.Add(item); }
    //            catch (FaultException<GraphServiceReference.ProfileControllerException> error)
    //            { 
    //                MessageBox.Show(error.Detail.Message, "Ошибка"); return; 
    //            }
    //            catch (CommunicationException error) { MessageBox.Show(error.Message, "Ошибка"); return; }
    //        }
    //    }

    //    private void CheckboxCheckedChanged(object? sender, EventArgs args)
    //    {
    //        var checkbox_instance = sender as CheckBox;
    //        if (checkbox_instance == null) return;

    //        switch (checkbox_instance.Name) 
    //        {
    //            case "auth_checkbox": this.auth_password_textbox.PasswordChar 
    //                    = (this.auth_password_textbox.PasswordChar == '*') ? default : '*'; break;

    //            case "reg_checkbox": this.reg_password_textbox.PasswordChar
    //                    = (this.reg_password_textbox.PasswordChar == '*') ? default : '*'; break;

    //            default: MessageBox.Show("Выбранный флаг не найден", "Ошибка"); return;
    //        }
    //    }

    //    private void RegisterButtonClick(object? sender, EventArgs args)
    //    {
    //        string username = this.reg_name_textbox.Text, password = this.reg_password_textbox.Text,
    //            emailname = this.reg_email_textbox.Text, emailkey = this.reg_emailkey_textbox.Text;

    //        var email_validate = Regex.IsMatch(emailname, @"^[\w.]+@(?:gmail|mail).(?:ru|com)$");

    //        if (username.Length < MinCharacter || password.Length < MinCharacter || emailkey.Length < MinCharacter
    //            || !email_validate) { MessageBox.Show("Неверный формат текстовых полей", "Ошибка"); return; }

    //        System.Guid? profile_id = default!;
    //        using (var register = new GraphServiceReference.ProfileControllerClient())
    //        {
    //            try {
    //                profile_id = register.Registration(new ProfileData()
    //                {
    //                    EmailName = emailname, Password = password, UserName = username, EmailKey = emailkey,
    //                    ProjectsPath = this.reg_filepath_textbox.Text
    //                });
    //            }
    //            catch (FaultException<GraphServiceReference.ProfileControllerException> error) 
    //            { MessageBox.Show(error.Detail.Message, "Ошибка"); return; }

    //            catch (CommunicationException error) { MessageBox.Show(error.Message, "Ошибка"); return; }
    //        }
    //        MessageBox.Show("Учётная запись была создана", "Готово");
    //        this.auth_name_combobox.Items.Add(username);

    //        var client_form = new ClientForm(profile_id.Value);
    //        client_form.FormClosed += (sender, args) => this.Show();

    //        client_form.Show(); this.Hide();
    //    }

    //    private void LoginButtonClick(object? sender, EventArgs args)
    //    {
    //        string username = this.auth_name_combobox.Text, password = this.auth_password_textbox.Text;
    //        System.Guid? profile_id = default!;

    //        using (var authorize = new GraphServiceReference.ProfileControllerClient())
    //        {
    //            try { profile_id = authorize.Authorization(username, password); }
    //            catch (FaultException<GraphServiceReference.ProfileControllerException> error) 
    //            { 
    //                MessageBox.Show(error.Detail.Message, "Ошибка"); return; 
    //            }
    //            catch (CommunicationException error) { MessageBox.Show(error.Message, "Ошибка"); return; }
    //        }

    //        if (profile_id == null) { MessageBox.Show("Невозвожно зайти в профиль", "Ошибка"); return; }
    //        var client_form = new ClientForm(profile_id.Value);

    //        client_form.FormClosed += (sender, args) => this.Show();
    //        client_form.Show(); this.Hide();
    //    }

    //    private void FilepathButtonClick(object? sender, EventArgs args)
    //    {
    //        using (var filedialog = new FolderBrowserDialog()) 
    //        {
    //            var result = filedialog.ShowDialog();
    //            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(filedialog.SelectedPath))
    //            {
    //                this.reg_filepath_textbox.Text = filedialog.SelectedPath;
    //            }
    //        }
    //    }
    //}
}
