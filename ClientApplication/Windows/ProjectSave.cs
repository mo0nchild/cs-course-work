using CSCourseWork.Connected_Services.GraphServiceReference;
using CSCourseWork.NodesControllers;
using GraphServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CSCourseWork.Windows
{
    public partial class ProjectSave : Form
    {
        private const System.Int32 MinCharacterCount = 5;

        protected System.Guid ProfileID { get; private set; } = default;
        protected INodesController Controller { get; private set; } = default!;

        public System.String? ProjectName { get; set; } = default;

        public ProjectSave(System.Guid profile_id, INodesController controller) : base()
        {
            (this.ProfileID, this.Controller) = (profile_id, controller);
            this.InitializeComponent();
            this.Load += delegate (object? sender, EventArgs args) { this.RefreshButtonClick(sender, args); };

            this.refresh_button.Click += new EventHandler(RefreshButtonClick);
            this.save_button.Click += new EventHandler(SaveButtonClick);
            this.edit_button.Click += new EventHandler(EditButtonClick);
        }

        private void RefreshButtonClick(object? sender, EventArgs args)
        {
            if (this.ProjectName == null) return;
            this.name_textbox.Text = this.ProjectName;

            using (var project_dispatcher = new GraphServiceReference.ProjectDispatcherClient())
            {
                if (!project_dispatcher.SetProjectsDirectory(this.ProfileID))
                { MessageBox.Show("Невозможно использовать каталог с проектами", "Ошибка"); return; }

                try {
                    foreach (var project in project_dispatcher.GetProjectsInfo())
                    {
                        if (project.ProjectName == this.ProjectName)
                        {
                            this.datetime_textbox.Text = project.CreateTime.ToString();
                            this.file_textbox.Text = project.FileName;
                        }
                    }
                }
                catch (FaultException<GraphServiceReference.ProjectDispatcherException> error)
                {
                    MessageBox.Show(error.Detail.Message, "Ошибка"); this.Close();
                }
                catch (CommunicationException error) { MessageBox.Show(error.Message, "Ошибка"); this.Close(); }
            }
            this.currentproject_label.Text = $"Текущий проект: {this.ProjectName}";
        }

        private void EditButtonClick(object? sender, EventArgs args)
        {
            if (this.ProjectName == null) return;

            string projectname = this.name_textbox.Text, filename = this.file_textbox.Text;
            if (projectname.Length < MinCharacterCount || filename.Length < MinCharacterCount)
            {
                MessageBox.Show("Неверный формат текстовых полей", "Ошибка"); return;
            }
            var project_info = new GraphServiceReference.ProjectInfo()
            { 
                FileName = filename, ProjectName = projectname, CreateTime = DateTime.Parse(this.datetime_textbox.Text)
            };
            using (var project_dispatcher = new ProjectDispatcherClient())
            {
                if (!project_dispatcher.SetProjectsDirectory(this.ProfileID)) 
                { MessageBox.Show("Невозможно использовать каталог с проектами", "Ошибка"); return; }

                try {
                    project_dispatcher.UpdateProject(this.ProjectName, project_info);
                }
                catch (FaultException<GraphServiceReference.ProjectDispatcherException> error)
                {
                    MessageBox.Show(error.Detail.Message, "Ошибка"); return;
                }
                catch(CommunicationException error) { MessageBox.Show(error.Message, "Ошибка"); return; }
            }
            MessageBox.Show($"Проект \'{project_info.ProjectName}\' успешно сохранён", "Готово");

            this.ProjectName = project_info.ProjectName; this.DialogResult = DialogResult.Continue;
            this.RefreshButtonClick(sender, args);
        }

        private void SaveButtonClick(object? sender, EventArgs args)
        {
            string projectname = this.name_textbox.Text, filename = this.file_textbox.Text;
            if (projectname.Length < MinCharacterCount || filename.Length < MinCharacterCount)
            {
                MessageBox.Show("Неверный формат текстовых полей", "Ошибка"); return;
            }
            var project_info = new GraphServiceReference.ProjectInfo()
            { FileName = filename, ProjectName = projectname, CreateTime = DateTime.Now };

            using (var project_dispatcher = new ProjectDispatcherClient())
            {
                if (!project_dispatcher.SetProjectsDirectory(this.ProfileID)) 
                { MessageBox.Show("Невозможно использовать каталог с проектами", "Ошибка"); return; }
                try {
                    project_dispatcher.CreateProject(project_info);
                    project_dispatcher.PutProjectData(project_info.ProjectName, this.Controller.ConvertToServiceData());
                }
                catch (FaultException<GraphServiceReference.ProjectDispatcherException> error)
                {
                    MessageBox.Show(error.Detail.Message, "Ошибка"); return;
                }
                catch(CommunicationException error) { MessageBox.Show(error.Message, "Ошибка"); return; }
            }
            this.datetime_textbox.Text = project_info.CreateTime.ToString();
            MessageBox.Show($"Проект \'{project_info.ProjectName}\' успешно сохранён", "Готово");

            this.ProjectName = project_info.ProjectName;
            this.DialogResult = DialogResult.OK; this.Close();
        }
    }
}
