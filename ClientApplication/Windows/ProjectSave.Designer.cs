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
            this.email_button = new System.Windows.Forms.Button();
            this.name_label = new System.Windows.Forms.Label();
            this.file_label = new System.Windows.Forms.Label();
            this.email_label = new System.Windows.Forms.Label();
            this.email_textbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // name_textbox
            // 
            this.name_textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.name_textbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.name_textbox.Location = new System.Drawing.Point(12, 35);
            this.name_textbox.MaxLength = 20;
            this.name_textbox.Name = "name_textbox";
            this.name_textbox.Size = new System.Drawing.Size(186, 29);
            this.name_textbox.TabIndex = 0;
            // 
            // file_textbox
            // 
            this.file_textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.file_textbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.file_textbox.Location = new System.Drawing.Point(204, 35);
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
            this.save_button.Size = new System.Drawing.Size(188, 31);
            this.save_button.TabIndex = 2;
            this.save_button.Text = "Сохранить проект";
            this.save_button.UseVisualStyleBackColor = true;
            // 
            // email_button
            // 
            this.email_button.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.email_button.Location = new System.Drawing.Point(204, 214);
            this.email_button.Name = "email_button";
            this.email_button.Size = new System.Drawing.Size(188, 31);
            this.email_button.TabIndex = 3;
            this.email_button.Text = "Отправить на почту";
            this.email_button.UseVisualStyleBackColor = true;
            // 
            // name_label
            // 
            this.name_label.AutoSize = true;
            this.name_label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.name_label.Location = new System.Drawing.Point(12, 13);
            this.name_label.Name = "name_label";
            this.name_label.Size = new System.Drawing.Size(127, 19);
            this.name_label.TabIndex = 5;
            this.name_label.Text = "Название проекта:";
            // 
            // file_label
            // 
            this.file_label.AutoSize = true;
            this.file_label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.file_label.Location = new System.Drawing.Point(204, 13);
            this.file_label.Name = "file_label";
            this.file_label.Size = new System.Drawing.Size(115, 19);
            this.file_label.TabIndex = 6;
            this.file_label.Text = "Название файла:";
            // 
            // email_label
            // 
            this.email_label.AutoSize = true;
            this.email_label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.email_label.Location = new System.Drawing.Point(12, 67);
            this.email_label.Name = "email_label";
            this.email_label.Size = new System.Drawing.Size(183, 19);
            this.email_label.TabIndex = 8;
            this.email_label.Text = "Адрес почты для отправки:";
            // 
            // email_textbox
            // 
            this.email_textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.email_textbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.email_textbox.Location = new System.Drawing.Point(12, 89);
            this.email_textbox.MaxLength = 40;
            this.email_textbox.Name = "email_textbox";
            this.email_textbox.Size = new System.Drawing.Size(380, 29);
            this.email_textbox.TabIndex = 7;
            // 
            // ProjectSave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(404, 257);
            this.Controls.Add(this.email_label);
            this.Controls.Add(this.email_textbox);
            this.Controls.Add(this.file_label);
            this.Controls.Add(this.name_label);
            this.Controls.Add(this.email_button);
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
        private Button email_button;
        private Label name_label;
        private Label file_label;
        private Label email_label;
        private TextBox email_textbox;
    }
}