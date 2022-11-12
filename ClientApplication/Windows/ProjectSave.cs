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

namespace CSCourseWork.Windows
{
    public partial class ProjectSave : Form
    {
        protected System.Guid ProfileID { get; private set; } = default;
        protected INodesController Controller { get; private set; } = default!;
        protected System.String ProjectsPath { get; private set; } = default!;

        public System.String? ProjectName { get; set; } = default;

        public ProjectSave(System.Guid profile_id, INodesController controller)
        {
            this.InitializeComponent(); 
            (this.ProfileID, this.Controller) = (profile_id, controller);

            this.save_button.Click += new EventHandler(SaveButtonClick);
            this.email_button.Click += new EventHandler(EmailButtonClick);
            this.Load += new EventHandler(ProjectSaveLoad);
        }

        private void ProjectSaveLoad(object? sender, EventArgs args)
        {
            using (var profile_contoller = new GraphServiceReference.ProfileControllerClient())
            {
                try { this.ProjectsPath = profile_contoller.ReadProfile(this.ProfileID).ProjectsPath; }
                catch (FaultException<GraphServiceReference.ProfileControllerException> error)
                {
                    MessageBox.Show(error.Detail.Message, "Ошибка"); this.Close();
                }
                catch (CommunicationException error) { MessageBox.Show(error.Message, "Ошибка"); this.Close(); }
            }
            if (this.ProjectName == null) return;

            this.name_textbox.Text = this.ProjectName;
            using (var project_dispatcher = new GraphServiceReference.ProjectDispatcherClient())
            {
                if (!project_dispatcher.SetProjectsDirectory(this.ProjectsPath))
                { MessageBox.Show("Невозможно использовать каталог с проектами", "Ошибка"); return; }
                try {
                    foreach (var project in project_dispatcher.GetProjectsInfo())
                    { if (project.ProjectName == this.ProjectName) this.file_textbox.Text = project.FileName; }
                }
                catch (FaultException<GraphServiceReference.ProjectDispatcherException> error)
                {
                    MessageBox.Show(error.Detail.Message, "Ошибка"); this.Close();
                }
                catch (CommunicationException error) { MessageBox.Show(error.Message, "Ошибка"); this.Close(); }
            }
        }

        private void EmailButtonClick(object? sender, EventArgs args)
        {
            
        }

        private void SaveButtonClick(object? sender, EventArgs args)
        {
            var project_info = new GraphServiceReference.ProjectInfo()
            { FileName = this.file_textbox.Text, ProjectName = this.name_textbox.Text, CreateTime = DateTime.Now };

            using (var project_dispatcher = new ProjectDispatcherClient())
            {
                if (!project_dispatcher.SetProjectsDirectory(this.ProjectsPath)) 
                { MessageBox.Show("Невозможно использовать каталог с проектами", "Ошибка"); return; }
                try {
                    if (this.ProjectName == null) project_dispatcher.CreateProject(project_info);
                    project_dispatcher.PutProjectData(project_info.ProjectName, this.Controller.ConvertToServiceData());
                }
                catch (FaultException<GraphServiceReference.ProjectDispatcherException> error)
                {
                    MessageBox.Show(error.Detail.Message, "Ошибка"); return;
                }
                catch(CommunicationException error) { MessageBox.Show(error.Message, "Ошибка"); return; }
            }
            MessageBox.Show($"Проект \'{project_info.ProjectName}\' успешно сохранён", "Готово");

            this.ProjectName = project_info.ProjectName;
            this.DialogResult = DialogResult.OK; this.Close();
        }
    }
}
