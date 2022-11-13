namespace CSCourseWork.Windows
{
    partial class ProjectSave
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
            this.name_textbox = new System.Windows.Forms.TextBox();
            this.file_textbox = new System.Windows.Forms.TextBox();
            this.save_button = new System.Windows.Forms.Button();
            this.edit_button = new System.Windows.Forms.Button();
            this.name_label = new System.Windows.Forms.Label();
            this.file_label = new System.Windows.Forms.Label();
            this.datetime_label = new System.Windows.Forms.Label();
            this.datetime_textbox = new System.Windows.Forms.TextBox();
            this.refresh_button = new System.Windows.Forms.Button();
            this.currentproject_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // name_textbox
            // 
            this.name_textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.name_textbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.name_textbox.Location = new System.Drawing.Point(12, 31);
            this.name_textbox.MaxLength = 20;
            this.name_textbox.Name = "name_textbox";
            this.name_textbox.Size = new System.Drawing.Size(186, 29);
            this.name_textbox.TabIndex = 0;
            // 
            // file_textbox
            // 
            this.file_textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.file_textbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.file_textbox.Location = new System.Drawing.Point(204, 31);
            this.file_textbox.MaxLength = 20;
            this.file_textbox.Name = "file_textbox";
            this.file_textbox.Size = new System.Drawing.Size(188, 29);
            this.file_textbox.TabIndex = 1;
            // 
            // save_button
            // 
            this.save_button.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.save_button.Location = new System.Drawing.Point(12, 214);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(186, 31);
            this.save_button.TabIndex = 2;
            this.save_button.Text = "Сохранить проект";
            this.save_button.UseVisualStyleBackColor = true;
            // 
            // edit_button
            // 
            this.edit_button.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.edit_button.Location = new System.Drawing.Point(204, 214);
            this.edit_button.Name = "edit_button";
            this.edit_button.Size = new System.Drawing.Size(188, 31);
            this.edit_button.TabIndex = 3;
            this.edit_button.Text = "Изменить проект";
            this.edit_button.UseVisualStyleBackColor = true;
            // 
            // name_label
            // 
            this.name_label.AutoSize = true;
            this.name_label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.name_label.Location = new System.Drawing.Point(12, 9);
            this.name_label.Name = "name_label";
            this.name_label.Size = new System.Drawing.Size(127, 19);
            this.name_label.TabIndex = 5;
            this.name_label.Text = "Название проекта:";
            // 
            // file_label
            // 
            this.file_label.AutoSize = true;
            this.file_label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.file_label.Location = new System.Drawing.Point(204, 9);
            this.file_label.Name = "file_label";
            this.file_label.Size = new System.Drawing.Size(115, 19);
            this.file_label.TabIndex = 6;
            this.file_label.Text = "Название файла:";
            // 
            // datetime_label
            // 
            this.datetime_label.AutoSize = true;
            this.datetime_label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.datetime_label.Location = new System.Drawing.Point(12, 63);
            this.datetime_label.Name = "datetime_label";
            this.datetime_label.Size = new System.Drawing.Size(159, 19);
            this.datetime_label.TabIndex = 16;
            this.datetime_label.Text = "Дата и время создания:";
            // 
            // datetime_textbox
            // 
            this.datetime_textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.datetime_textbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.datetime_textbox.Location = new System.Drawing.Point(12, 85);
            this.datetime_textbox.MaxLength = 40;
            this.datetime_textbox.Name = "datetime_textbox";
            this.datetime_textbox.ReadOnly = true;
            this.datetime_textbox.Size = new System.Drawing.Size(346, 29);
            this.datetime_textbox.TabIndex = 15;
            // 
            // refresh_button
            // 
            this.refresh_button.BackgroundImage = global::CSCourseWork.Windows.Resourses.RefreshIcon;
            this.refresh_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.refresh_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refresh_button.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.refresh_button.Location = new System.Drawing.Point(364, 85);
            this.refresh_button.Name = "refresh_button";
            this.refresh_button.Size = new System.Drawing.Size(28, 29);
            this.refresh_button.TabIndex = 17;
            this.refresh_button.UseVisualStyleBackColor = true;
            // 
            // currentproject_label
            // 
            this.currentproject_label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.currentproject_label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.currentproject_label.Location = new System.Drawing.Point(12, 126);
            this.currentproject_label.Name = "currentproject_label";
            this.currentproject_label.Size = new System.Drawing.Size(380, 23);
            this.currentproject_label.TabIndex = 18;
            this.currentproject_label.Text = "Текущий проект: ";
            // 
            // ProjectSave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(404, 257);
            this.Controls.Add(this.currentproject_label);
            this.Controls.Add(this.refresh_button);
            this.Controls.Add(this.datetime_label);
            this.Controls.Add(this.datetime_textbox);
            this.Controls.Add(this.file_label);
            this.Controls.Add(this.name_label);
            this.Controls.Add(this.edit_button);
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.file_textbox);
            this.Controls.Add(this.name_textbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(420, 296);
            this.MinimumSize = new System.Drawing.Size(420, 296);
            this.Name = "ProjectSave";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Сохранить проект";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox name_textbox;
        private TextBox file_textbox;
        private Button save_button;
        private Button edit_button;
        private Label name_label;
        private Label file_label;
        private Label datetime_label;
        private TextBox datetime_textbox;
        private Button refresh_button;
        private Label currentproject_label;
    }
}