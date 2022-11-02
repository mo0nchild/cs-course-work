using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSCourseWork.Windows
{
    internal partial class AuthForm : Form
    {
        public AuthForm()
        {
            this.InitializeComponent();
            this.filepath_button.FlatAppearance.BorderSize = default(int);

            this.auth_checkbox.CheckedChanged += new EventHandler(CheckboxCheckedChanged);
            this.reg_checkbox.CheckedChanged += new EventHandler(CheckboxCheckedChanged);

            this.filepath_button.Click += new EventHandler(FilepathButtonClick);
            this.login_button.Click += new EventHandler(LoginButtonClick);
            this.register_button.Click += new EventHandler(RegisterButtonClick);
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
            
        }

        private void LoginButtonClick(object? sender, EventArgs args)
        {
            
        }

        private void FilepathButtonClick(object? sender, EventArgs args)
        {
            
        }
    }
}
