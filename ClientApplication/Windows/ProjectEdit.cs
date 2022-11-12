using GraphServiceReference;
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
    public partial class ProjectEdit : Form
    {
        private System.Guid ProfileId { get; set; } = default;
        public System.String ProjectName { get; set; } = default!;

        public ProjectEdit(System.Guid profile_id, string project_name)
        {
            this.InitializeComponent();
            (this.ProjectName, this.ProfileId) = (project_name, profile_id);

            this.save_button.Click += new EventHandler(SaveButtonClick);
            this.Load += new EventHandler(ProjectEditLoad);
        }

        private void ProjectEditLoad(object? sender, EventArgs args)
        {
            using (var profile_controller = new ProfileControllerClient())
            {
                try
                {

                }
                catch { }
            }
        }

        private void SaveButtonClick(object? sender, EventArgs args)
        {
            
        }
    }
}
