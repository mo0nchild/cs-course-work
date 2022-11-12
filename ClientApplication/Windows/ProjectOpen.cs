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
    public partial class ProjectOpen : Form
    {
        public GraphServiceReference.NodeData[]? NodeList { get; set; } = default;
        public System.String? ProjectName { get; set; } = default;

        protected System.Guid ProfileID { get; private set; } = default;
        protected System.String ProfilePath { get; private set; } = default!;

        public ProjectOpen(System.Guid profile_id)
        {
            this.InitializeComponent(); this.ProfileID = profile_id;
            this.Load += new EventHandler(ProjectOpenLoad);

            this.delete_button.Click += new EventHandler(DeleteButtonClick);
            this.open_button.Click += new EventHandler(OpenButtonClick);

            this.projects_listview.DoubleClick += delegate (object? sender, EventArgs args) 
            { this.OpenButtonClick(this, args); };
        }

        private void UpdateProjectListView()
        {
            ProjectInfo[]? projects_list = default;
            using (var project_dispatcher = new ProjectDispatcherClient())
            {
                try { 
                    if (project_dispatcher.SetProjectsDirectory(this.ProfilePath)) 
                    { projects_list = project_dispatcher.GetProjectsInfo(); }
                }
                catch (FaultException<ProjectDispatcherException> error)
                {
                    MessageBox.Show(error.Detail.Message, "Ошибка"); return;
                }
                catch (CommunicationException error) { MessageBox.Show(error.Message, "Ошибка"); return; }
            }
            if(projects_list != null)
            {
                this.projects_listview.Items.Clear();
                foreach (var project in projects_list)
                {
                    var listview_row = new ListViewItem(new string[] { 
                        project.ProjectName, project.FileName, project.CreateTime.ToString() });

                    this.projects_listview.Items.Add(listview_row);
                }
            }
        }

        private void ProjectOpenLoad(object? sender, EventArgs args)
        {
            using (var profile_controller = new ProfileControllerClient())
            {
                try { this.ProfilePath = profile_controller.ReadProfile(this.ProfileID).ProjectsPath; }
                catch (FaultException<ProfileControllerException> error)
                {
                    MessageBox.Show(error.Detail.Message, "Ошибка"); this.Close();
                }
                catch(CommunicationException error) { MessageBox.Show(error.Message, "Ошибка"); this.Close(); }
            }
            this.UpdateProjectListView();
        }

        private void OpenButtonClick(object? sender, EventArgs args)
        {
            if (this.projects_listview.SelectedItems.Count <= 0) return;
            var loading_project = this.projects_listview.SelectedItems[0].Text;

            using (var project_dispatcher = new ProjectDispatcherClient())
            {
                try {
                    if (project_dispatcher.SetProjectsDirectory(this.ProfilePath))
                    { this.NodeList = project_dispatcher.TakeProjectData(loading_project); }
                }
                catch (FaultException<ProjectDispatcherException> error)
                {
                    MessageBox.Show(error.Detail.Message, "Ошибка"); return;
                }
                catch (CommunicationException error) { MessageBox.Show(error.Message, "Ошибка"); return; }
            }

            MessageBox.Show($"Проект {loading_project} загружен", "Готово");
            this.ProjectName = loading_project; this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void DeleteButtonClick(object? sender, EventArgs args)
        {
            if (this.projects_listview.SelectedItems.Count <= 0) return;
            var deleted_project = this.projects_listview.SelectedItems[0].Text;

            using (var project_dispatcher = new ProjectDispatcherClient())
            {
                try {
                    if (project_dispatcher.SetProjectsDirectory(this.ProfilePath))
                    { project_dispatcher.DeleteProject(deleted_project); }
                }
                catch (FaultException<ProjectDispatcherException> error)
                {
                    MessageBox.Show(error.Detail.Message, "Ошибка"); return;
                }
                catch (CommunicationException error) { MessageBox.Show(error.Message, "Ошибка"); return; }
            }
            if (this.ProjectName != null && this.ProjectName == deleted_project) this.ProjectName = null;
            this.UpdateProjectListView();
        }
    }
}
