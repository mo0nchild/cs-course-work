namespace CSCourseWork.Windows
{
    partial class ProjectOpen
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
            this.projects_listview = new System.Windows.Forms.ListView();
            this.projectname_column = new System.Windows.Forms.ColumnHeader();
            this.projectfile_column = new System.Windows.Forms.ColumnHeader();
            this.datetime_column = new System.Windows.Forms.ColumnHeader();
            this.delete_button = new System.Windows.Forms.Button();
            this.open_button = new System.Windows.Forms.Button();
            this.projects_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // projects_listview
            // 
            this.projects_listview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.projectname_column,
            this.projectfile_column,
            this.datetime_column});
            this.projects_listview.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.projects_listview.FullRowSelect = true;
            this.projects_listview.GridLines = true;
            this.projects_listview.Location = new System.Drawing.Point(12, 31);
            this.projects_listview.MultiSelect = false;
            this.projects_listview.Name = "projects_listview";
            this.projects_listview.Size = new System.Drawing.Size(394, 249);
            this.projects_listview.TabIndex = 0;
            this.projects_listview.UseCompatibleStateImageBehavior = false;
            this.projects_listview.View = System.Windows.Forms.View.Details;
            // 
            // projectname_column
            // 
            this.projectname_column.Text = "Название проекта";
            this.projectname_column.Width = 140;
            // 
            // projectfile_column
            // 
            this.projectfile_column.Text = "Название файла";
            this.projectfile_column.Width = 160;
            // 
            // datetime_column
            // 
            this.datetime_column.Text = "Дата и время";
            this.datetime_column.Width = 100;
            // 
            // delete_button
            // 
            this.delete_button.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.delete_button.Location = new System.Drawing.Point(218, 307);
            this.delete_button.Name = "delete_button";
            this.delete_button.Size = new System.Drawing.Size(188, 31);
            this.delete_button.TabIndex = 5;
            this.delete_button.Text = "Удалить проект";
            this.delete_button.UseVisualStyleBackColor = true;
            // 
            // open_button
            // 
            this.open_button.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.open_button.Location = new System.Drawing.Point(12, 307);
            this.open_button.Name = "open_button";
            this.open_button.Size = new System.Drawing.Size(188, 31);
            this.open_button.TabIndex = 4;
            this.open_button.Text = "Открыть проект";
            this.open_button.UseVisualStyleBackColor = true;
            // 
            // projects_label
            // 
            this.projects_label.AutoSize = true;
            this.projects_label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.projects_label.Location = new System.Drawing.Point(12, 9);
            this.projects_label.Name = "projects_label";
            this.projects_label.Size = new System.Drawing.Size(127, 19);
            this.projects_label.TabIndex = 6;
            this.projects_label.Text = "Название проекта:";
            // 
            // ProjectOpen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(418, 349);
            this.Controls.Add(this.projects_label);
            this.Controls.Add(this.delete_button);
            this.Controls.Add(this.open_button);
            this.Controls.Add(this.projects_listview);
            this.MaximumSize = new System.Drawing.Size(434, 388);
            this.MinimumSize = new System.Drawing.Size(434, 388);
            this.Name = "ProjectOpen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ProjectOpen";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListView projects_listview;
        private Button delete_button;
        private Button open_button;
        private Label projects_label;
        private ColumnHeader projectname_column;
        private ColumnHeader projectfile_column;
        private ColumnHeader datetime_column;
    }
}