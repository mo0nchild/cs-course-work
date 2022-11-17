using GraphServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace CSCourseWork.Windows
{
    public partial class ProjectOpen : Form
    {
        public GraphServiceReference.NodeData[]? NodeList { get; set; } = default;
        public System.String? ProjectName { get; set; } = default;

        protected System.Guid ProfileID { get; private set; } = default;
        protected virtual System.Int32 MinCharacter { get; private set; } = 5;

        public ProjectOpen(System.Guid profile_id) : base()
        {
            this.InitializeComponent(); this.ProfileID = profile_id;
            this.Load += delegate (object? sender, EventArgs args) { this.UpdateProjectListView(); };

            this.delete_button.Click += new EventHandler(DeleteButtonClick);
            this.open_button.Click += new EventHandler(OpenButtonClick);
            this.export_button.Click += new EventHandler(ExportButtonClick);

            this.openfile_button.Click += new EventHandler(OpenfileButtonClick);
            this.import_button.Click += new EventHandler(ImportButtonClick);

            this.projects_listview.SelectedIndexChanged += new EventHandler(Projects_listview_SelectedIndexChanged);
            this.projects_listview.DoubleClick += new EventHandler(OpenButtonClick);
        }

        private void ImportButtonClick(object? sender, EventArgs args)
        {
            string projectname = this.projectname_textbox.Text, filename = this.filename_textbox.Text,
                importfile = this.importfile_textbox.Text;

            if (projectname.Length < MinCharacter || filename.Length < MinCharacter) return;
            using (var project_dispatcher = new ProjectDispatcherClient())
            {
                if (!project_dispatcher.SetProjectsDirectory(this.ProfileID))
                { MessageBox.Show("Невозможно использовать каталог с проектами", "Ошибка"); return; }
                try {
                    project_dispatcher.ImportProject(projectname, new TransferData() { FromPath = importfile, ToPath = filename });
                }
                catch (FaultException<ProjectDispatcherException> error)
                {
                    MessageBox.Show(error.Detail.Message, "Ошибка"); return;
                }
                catch (CommunicationException error) { MessageBox.Show(error.Message, "Ошибка"); return; }
            }
            MessageBox.Show("Проект успешно импортирован", "Готово");
            this.UpdateProjectListView();
        }

        private void OpenfileButtonClick(object? sender, EventArgs args)
        {
            using (var openfile_dialog = new OpenFileDialog() { Filter = "(*.graphproj) |*.graphproj| (*.json) |*.json" })
            {
                if (openfile_dialog.ShowDialog() != DialogResult.OK) { return; }
                this.importfile_textbox.Text = openfile_dialog.FileName;
            }
        }

        private void ExportButtonClick(object? sender, EventArgs args)
        {
            if (this.projects_listview.SelectedItems.Count <= 0) return;

            var loading_project = this.projects_listview.SelectedItems[0].Text;
            var target_email = this.email_textbox.Text;

            if (!Regex.IsMatch(target_email, @"^[\w.]+@(?:gmail|mail).(?:ru|com)$"))
            { MessageBox.Show("Неверный формат email почты", "Ошибка"); return; }

            using (var project_dispatcher = new ProjectDispatcherClient())
            {
                var transfer_data = new TransferData() { FromPath = $"{this.ProfileID}",ToPath = target_email };
                if (!project_dispatcher.SetProjectsDirectory(this.ProfileID))
                {
                    MessageBox.Show("Невозможно использовать каталог с проектами", "Ошибка"); return; 
                }
                try {project_dispatcher.ExportProject(loading_project, transfer_data); }
                catch (FaultException<ProjectDispatcherException> error)
                {
                    MessageBox.Show(error.Detail.Message, "Ошибка"); return;
                }
                catch (CommunicationException error) { MessageBox.Show(error.Message, "Ошибка"); return; }
            }
            MessageBox.Show("Проект экспортирован на указанную почту", "Готово");
        }

        private void Projects_listview_SelectedIndexChanged(object? sender, EventArgs args)
        {
            if (this.projects_listview.SelectedItems.Count <= 0) return;

            this.file_textbox.Text = this.projects_listview.SelectedItems[0].SubItems[1].Text;
            this.datetime_textbox.Text = this.projects_listview.SelectedItems[0].SubItems[2].Text;
        }

        private void UpdateProjectListView()
        {
            ProjectInfo[]? projects_list = default;
            using (var project_dispatcher = new ProjectDispatcherClient())
            {
                try { 
                    if (project_dispatcher.SetProjectsDirectory(this.ProfileID)) 
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
                    var listview_row = new ListViewItem(new string[] { project.ProjectName, project.FileName, 
                        project.CreateTime.ToString() });

                    this.projects_listview.Items.Add(listview_row);
                }
            }
        }

        private void OpenButtonClick(object? sender, EventArgs args)
        {
            if (this.projects_listview.SelectedItems.Count <= 0) return;
            var loading_project = this.projects_listview.SelectedItems[0].Text;

            using (var project_dispatcher = new ProjectDispatcherClient())
            {
                try {
                    if (project_dispatcher.SetProjectsDirectory(this.ProfileID))
                    { this.NodeList = project_dispatcher.TakeProjectData(loading_project); }
                }
                catch (FaultException<ProjectDispatcherException> error)
                {
                    MessageBox.Show(error.Detail.Message, "Ошибка"); this.UpdateProjectListView(); return;
                }
                catch (CommunicationException error) 
                { 
                    MessageBox.Show(error.Message, "Ошибка"); this.UpdateProjectListView(); return;
                }
            }
            MessageBox.Show($"Проект {loading_project} загружен", "Готово");

            this.ProjectName = loading_project; this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void DeleteButtonClick(object? sender, EventArgs args)
        {
            if (this.projects_listview.SelectedItems.Count <= 0) return;
            var deleted_project = this.projects_listview.SelectedItems[0].Text;

            if (MessageBox.Show("Вы уверены?", "Подтвердение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) 
                != DialogResult.Yes) return;

            using (var project_dispatcher = new ProjectDispatcherClient())
            {
                try {
                    if (project_dispatcher.SetProjectsDirectory(this.ProfileID))
                    { project_dispatcher.DeleteProject(deleted_project, true); }
                }
                catch (FaultException<ProjectDispatcherException> error)
                {
                    MessageBox.Show(error.Detail.Message, "Ошибка"); return;
                }
                catch (CommunicationException error) { MessageBox.Show(error.Message, "Ошибка"); return; }
            }
            MessageBox.Show($"Проект {deleted_project} удалён", "Готово");
            this.ProjectName = null; this.UpdateProjectListView();
        }
    }
}
