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
            this.components = new System.ComponentModel.Container();
            this.projects_listview = new System.Windows.Forms.ListView();
            this.projectname_column = new System.Windows.Forms.ColumnHeader();
            this.projectfile_column = new System.Windows.Forms.ColumnHeader();
            this.datetime_column = new System.Windows.Forms.ColumnHeader();
            this.delete_button = new System.Windows.Forms.Button();
            this.open_button = new System.Windows.Forms.Button();
            this.projects_label = new System.Windows.Forms.Label();
            this.datetime_label = new System.Windows.Forms.Label();
            this.datetime_textbox = new System.Windows.Forms.TextBox();
            this.file_label = new System.Windows.Forms.Label();
            this.file_textbox = new System.Windows.Forms.TextBox();
            this.main_tabcontrol = new System.Windows.Forms.TabControl();
            this.open_tabpage = new System.Windows.Forms.TabPage();
            this.export_tabpage = new System.Windows.Forms.TabPage();
            this.export_button = new System.Windows.Forms.Button();
            this.email_label = new System.Windows.Forms.Label();
            this.email_textbox = new System.Windows.Forms.TextBox();
            this.import_tabpage = new System.Windows.Forms.TabPage();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.filename_label = new System.Windows.Forms.Label();
            this.projectname_textbox = new System.Windows.Forms.TextBox();
            this.filename_textbox = new System.Windows.Forms.TextBox();
            this.projectname_label = new System.Windows.Forms.Label();
            this.importfile_label = new System.Windows.Forms.Label();
            this.importfile_textbox = new System.Windows.Forms.TextBox();
            this.openfile_button = new System.Windows.Forms.Button();
            this.import_button = new System.Windows.Forms.Button();
            this.main_tabcontrol.SuspendLayout();
            this.open_tabpage.SuspendLayout();
            this.export_tabpage.SuspendLayout();
            this.import_tabpage.SuspendLayout();
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
            this.projects_listview.Size = new System.Drawing.Size(416, 155);
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
            this.datetime_column.Width = 120;
            // 
            // delete_button
            // 
            this.delete_button.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.delete_button.Location = new System.Drawing.Point(212, 82);
            this.delete_button.Name = "delete_button";
            this.delete_button.Size = new System.Drawing.Size(188, 31);
            this.delete_button.TabIndex = 5;
            this.delete_button.Text = "Удалить проект";
            this.delete_button.UseVisualStyleBackColor = true;
            // 
            // open_button
            // 
            this.open_button.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.open_button.Location = new System.Drawing.Point(6, 82);
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
            this.projects_label.Size = new System.Drawing.Size(121, 19);
            this.projects_label.TabIndex = 6;
            this.projects_label.Text = "Список проектов:";
            // 
            // datetime_label
            // 
            this.datetime_label.AutoSize = true;
            this.datetime_label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.datetime_label.Location = new System.Drawing.Point(212, 3);
            this.datetime_label.Name = "datetime_label";
            this.datetime_label.Size = new System.Drawing.Size(159, 19);
            this.datetime_label.TabIndex = 22;
            this.datetime_label.Text = "Дата и время создания:";
            // 
            // datetime_textbox
            // 
            this.datetime_textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.datetime_textbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.datetime_textbox.Location = new System.Drawing.Point(212, 25);
            this.datetime_textbox.MaxLength = 40;
            this.datetime_textbox.Name = "datetime_textbox";
            this.datetime_textbox.ReadOnly = true;
            this.datetime_textbox.Size = new System.Drawing.Size(188, 29);
            this.datetime_textbox.TabIndex = 21;
            // 
            // file_label
            // 
            this.file_label.AutoSize = true;
            this.file_label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.file_label.Location = new System.Drawing.Point(6, 3);
            this.file_label.Name = "file_label";
            this.file_label.Size = new System.Drawing.Size(115, 19);
            this.file_label.TabIndex = 20;
            this.file_label.Text = "Название файла:";
            // 
            // file_textbox
            // 
            this.file_textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.file_textbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.file_textbox.Location = new System.Drawing.Point(6, 25);
            this.file_textbox.MaxLength = 20;
            this.file_textbox.Name = "file_textbox";
            this.file_textbox.ReadOnly = true;
            this.file_textbox.Size = new System.Drawing.Size(188, 29);
            this.file_textbox.TabIndex = 18;
            // 
            // main_tabcontrol
            // 
            this.main_tabcontrol.Controls.Add(this.open_tabpage);
            this.main_tabcontrol.Controls.Add(this.export_tabpage);
            this.main_tabcontrol.Controls.Add(this.import_tabpage);
            this.main_tabcontrol.Location = new System.Drawing.Point(12, 192);
            this.main_tabcontrol.Name = "main_tabcontrol";
            this.main_tabcontrol.SelectedIndex = 0;
            this.main_tabcontrol.Size = new System.Drawing.Size(420, 158);
            this.main_tabcontrol.TabIndex = 24;
            // 
            // open_tabpage
            // 
            this.open_tabpage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.open_tabpage.Controls.Add(this.delete_button);
            this.open_tabpage.Controls.Add(this.datetime_label);
            this.open_tabpage.Controls.Add(this.open_button);
            this.open_tabpage.Controls.Add(this.file_textbox);
            this.open_tabpage.Controls.Add(this.datetime_textbox);
            this.open_tabpage.Controls.Add(this.file_label);
            this.open_tabpage.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.open_tabpage.Location = new System.Drawing.Point(4, 24);
            this.open_tabpage.Name = "open_tabpage";
            this.open_tabpage.Padding = new System.Windows.Forms.Padding(3);
            this.open_tabpage.Size = new System.Drawing.Size(412, 130);
            this.open_tabpage.TabIndex = 1;
            this.open_tabpage.Text = "Открытие";
            this.open_tabpage.UseVisualStyleBackColor = true;
            // 
            // export_tabpage
            // 
            this.export_tabpage.Controls.Add(this.export_button);
            this.export_tabpage.Controls.Add(this.email_label);
            this.export_tabpage.Controls.Add(this.email_textbox);
            this.export_tabpage.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.export_tabpage.Location = new System.Drawing.Point(4, 24);
            this.export_tabpage.Name = "export_tabpage";
            this.export_tabpage.Padding = new System.Windows.Forms.Padding(3);
            this.export_tabpage.Size = new System.Drawing.Size(412, 130);
            this.export_tabpage.TabIndex = 0;
            this.export_tabpage.Text = "Экспорт";
            this.export_tabpage.UseVisualStyleBackColor = true;
            // 
            // export_button
            // 
            this.export_button.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.export_button.Location = new System.Drawing.Point(9, 87);
            this.export_button.Name = "export_button";
            this.export_button.Size = new System.Drawing.Size(397, 31);
            this.export_button.TabIndex = 24;
            this.export_button.Text = "Экспорт проекта на почту";
            this.export_button.UseVisualStyleBackColor = true;
            // 
            // email_label
            // 
            this.email_label.AutoSize = true;
            this.email_label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.email_label.Location = new System.Drawing.Point(9, 3);
            this.email_label.Name = "email_label";
            this.email_label.Size = new System.Drawing.Size(155, 19);
            this.email_label.TabIndex = 26;
            this.email_label.Text = "Email для отправления:";
            // 
            // email_textbox
            // 
            this.email_textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.email_textbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.email_textbox.Location = new System.Drawing.Point(9, 25);
            this.email_textbox.MaxLength = 100;
            this.email_textbox.Name = "email_textbox";
            this.email_textbox.Size = new System.Drawing.Size(397, 29);
            this.email_textbox.TabIndex = 25;
            // 
            // import_tabpage
            // 
            this.import_tabpage.Controls.Add(this.import_button);
            this.import_tabpage.Controls.Add(this.openfile_button);
            this.import_tabpage.Controls.Add(this.importfile_label);
            this.import_tabpage.Controls.Add(this.importfile_textbox);
            this.import_tabpage.Controls.Add(this.filename_label);
            this.import_tabpage.Controls.Add(this.projectname_label);
            this.import_tabpage.Controls.Add(this.projectname_textbox);
            this.import_tabpage.Controls.Add(this.filename_textbox);
            this.import_tabpage.Location = new System.Drawing.Point(4, 24);
            this.import_tabpage.Name = "import_tabpage";
            this.import_tabpage.Size = new System.Drawing.Size(412, 130);
            this.import_tabpage.TabIndex = 2;
            this.import_tabpage.Text = "Импорт";
            this.import_tabpage.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // filename_label
            // 
            this.filename_label.AutoSize = true;
            this.filename_label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.filename_label.Location = new System.Drawing.Point(212, 3);
            this.filename_label.Name = "filename_label";
            this.filename_label.Size = new System.Drawing.Size(115, 19);
            this.filename_label.TabIndex = 29;
            this.filename_label.Text = "Название файла:";
            // 
            // projectname_textbox
            // 
            this.projectname_textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.projectname_textbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.projectname_textbox.Location = new System.Drawing.Point(9, 25);
            this.projectname_textbox.MaxLength = 20;
            this.projectname_textbox.Name = "projectname_textbox";
            this.projectname_textbox.Size = new System.Drawing.Size(188, 29);
            this.projectname_textbox.TabIndex = 26;
            // 
            // filename_textbox
            // 
            this.filename_textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.filename_textbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.filename_textbox.Location = new System.Drawing.Point(212, 25);
            this.filename_textbox.MaxLength = 40;
            this.filename_textbox.Name = "filename_textbox";
            this.filename_textbox.Size = new System.Drawing.Size(188, 29);
            this.filename_textbox.TabIndex = 28;
            // 
            // projectname_label
            // 
            this.projectname_label.AutoSize = true;
            this.projectname_label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.projectname_label.Location = new System.Drawing.Point(9, 3);
            this.projectname_label.Name = "projectname_label";
            this.projectname_label.Size = new System.Drawing.Size(127, 19);
            this.projectname_label.TabIndex = 27;
            this.projectname_label.Text = "Название проекта:";
            // 
            // importfile_label
            // 
            this.importfile_label.AutoSize = true;
            this.importfile_label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.importfile_label.Location = new System.Drawing.Point(9, 57);
            this.importfile_label.Name = "importfile_label";
            this.importfile_label.Size = new System.Drawing.Size(155, 19);
            this.importfile_label.TabIndex = 31;
            this.importfile_label.Text = "Путь к файлу импорта:";
            // 
            // importfile_textbox
            // 
            this.importfile_textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.importfile_textbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.importfile_textbox.Location = new System.Drawing.Point(9, 79);
            this.importfile_textbox.MaxLength = 200;
            this.importfile_textbox.Name = "importfile_textbox";
            this.importfile_textbox.ReadOnly = true;
            this.importfile_textbox.Size = new System.Drawing.Size(188, 29);
            this.importfile_textbox.TabIndex = 30;
            // 
            // openfile_button
            // 
            this.openfile_button.BackgroundImage = global::CSCourseWork.Windows.Resourses.FolderIcon;
            this.openfile_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.openfile_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openfile_button.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.openfile_button.Location = new System.Drawing.Point(212, 79);
            this.openfile_button.Name = "openfile_button";
            this.openfile_button.Size = new System.Drawing.Size(30, 29);
            this.openfile_button.TabIndex = 32;
            this.openfile_button.UseVisualStyleBackColor = true;
            // 
            // import_button
            // 
            this.import_button.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.import_button.Location = new System.Drawing.Point(254, 79);
            this.import_button.Name = "import_button";
            this.import_button.Size = new System.Drawing.Size(146, 29);
            this.import_button.TabIndex = 33;
            this.import_button.Text = "Импорт проекта";
            this.import_button.UseVisualStyleBackColor = true;
            // 
            // ProjectOpen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(438, 357);
            this.Controls.Add(this.projects_label);
            this.Controls.Add(this.projects_listview);
            this.Controls.Add(this.main_tabcontrol);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(454, 396);
            this.MinimumSize = new System.Drawing.Size(454, 396);
            this.Name = "ProjectOpen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Открыть проект";
            this.main_tabcontrol.ResumeLayout(false);
            this.open_tabpage.ResumeLayout(false);
            this.open_tabpage.PerformLayout();
            this.export_tabpage.ResumeLayout(false);
            this.export_tabpage.PerformLayout();
            this.import_tabpage.ResumeLayout(false);
            this.import_tabpage.PerformLayout();
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
        private Label datetime_label;
        private TextBox datetime_textbox;
        private Label file_label;
        private TextBox file_textbox;
        private TabControl main_tabcontrol;
        private TabPage open_tabpage;
        private TabPage export_tabpage;
        private Button export_button;
        private TextBox email_textbox;
        private Label email_label;
        private TabPage import_tabpage;
        private Button import_button;
        private Button openfile_button;
        private Label importfile_label;
        private TextBox importfile_textbox;
        private Label filename_label;
        private Label projectname_label;
        private TextBox projectname_textbox;
        private TextBox filename_textbox;
        private ContextMenuStrip contextMenuStrip1;
    }
}