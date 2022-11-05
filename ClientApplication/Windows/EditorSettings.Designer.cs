namespace CSCourseWork.Windows
{
    public partial class EditorSettings<TNodeController> : System.Windows.Forms.Form
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
            this.accept_button = new System.Windows.Forms.Button();
            this.export_button = new System.Windows.Forms.Button();
            this.properties_label = new System.Windows.Forms.Label();
            this.settinglist_treeview = new System.Windows.Forms.TreeView();
            this.settinglist_label = new System.Windows.Forms.Label();
            this.properties_panel = new System.Windows.Forms.FlowLayoutPanel();
            this.search_textbox = new System.Windows.Forms.TextBox();
            this.search_label = new System.Windows.Forms.Label();
            this.save_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // accept_button
            // 
            this.accept_button.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.accept_button.Location = new System.Drawing.Point(458, 340);
            this.accept_button.Name = "accept_button";
            this.accept_button.Size = new System.Drawing.Size(96, 33);
            this.accept_button.TabIndex = 4;
            this.accept_button.Text = "Принять";
            this.accept_button.UseVisualStyleBackColor = true;
            // 
            // export_button
            // 
            this.export_button.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.export_button.Location = new System.Drawing.Point(266, 341);
            this.export_button.Name = "export_button";
            this.export_button.Size = new System.Drawing.Size(86, 33);
            this.export_button.TabIndex = 5;
            this.export_button.Text = "Загрузить";
            this.export_button.UseVisualStyleBackColor = true;
            // 
            // properties_label
            // 
            this.properties_label.AutoSize = true;
            this.properties_label.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.properties_label.Location = new System.Drawing.Point(266, 56);
            this.properties_label.Name = "properties_label";
            this.properties_label.Size = new System.Drawing.Size(126, 15);
            this.properties_label.TabIndex = 3;
            this.properties_label.Text = "Доступные свойства: ";
            // 
            // settinglist_treeview
            // 
            this.settinglist_treeview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.settinglist_treeview.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.settinglist_treeview.Location = new System.Drawing.Point(12, 28);
            this.settinglist_treeview.Name = "settinglist_treeview";
            this.settinglist_treeview.Size = new System.Drawing.Size(242, 346);
            this.settinglist_treeview.TabIndex = 6;
            // 
            // settinglist_label
            // 
            this.settinglist_label.AutoSize = true;
            this.settinglist_label.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.settinglist_label.Location = new System.Drawing.Point(12, 9);
            this.settinglist_label.Name = "settinglist_label";
            this.settinglist_label.Size = new System.Drawing.Size(166, 15);
            this.settinglist_label.TabIndex = 7;
            this.settinglist_label.Text = "Список настроек редактора: ";
            // 
            // properties_panel
            // 
            this.properties_panel.AutoScroll = true;
            this.properties_panel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.properties_panel.BackColor = System.Drawing.Color.White;
            this.properties_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.properties_panel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.properties_panel.Location = new System.Drawing.Point(266, 74);
            this.properties_panel.Margin = new System.Windows.Forms.Padding(5);
            this.properties_panel.Name = "properties_panel";
            this.properties_panel.Padding = new System.Windows.Forms.Padding(5);
            this.properties_panel.Size = new System.Drawing.Size(288, 260);
            this.properties_panel.TabIndex = 8;
            this.properties_panel.WrapContents = false;
            // 
            // search_textbox
            // 
            this.search_textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.search_textbox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.search_textbox.Location = new System.Drawing.Point(266, 28);
            this.search_textbox.Name = "search_textbox";
            this.search_textbox.Size = new System.Drawing.Size(288, 25);
            this.search_textbox.TabIndex = 9;
            // 
            // search_label
            // 
            this.search_label.AutoSize = true;
            this.search_label.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.search_label.Location = new System.Drawing.Point(266, 10);
            this.search_label.Name = "search_label";
            this.search_label.Size = new System.Drawing.Size(166, 15);
            this.search_label.TabIndex = 10;
            this.search_label.Text = "Список настроек редактора: ";
            // 
            // save_button
            // 
            this.save_button.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.save_button.Location = new System.Drawing.Point(358, 341);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(94, 32);
            this.save_button.TabIndex = 11;
            this.save_button.Text = "Сохранить";
            this.save_button.UseVisualStyleBackColor = true;
            // 
            // EditorSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(569, 386);
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.search_label);
            this.Controls.Add(this.search_textbox);
            this.Controls.Add(this.properties_panel);
            this.Controls.Add(this.settinglist_label);
            this.Controls.Add(this.settinglist_treeview);
            this.Controls.Add(this.accept_button);
            this.Controls.Add(this.export_button);
            this.Controls.Add(this.properties_label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EditorSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройка редактора";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button accept_button;
        private Button export_button;
        private Label properties_label;
        private TreeView settinglist_treeview;
        private Label settinglist_label;
        private FlowLayoutPanel properties_panel;
        private TextBox search_textbox;
        private Label search_label;
        private Button save_button;
    }
}