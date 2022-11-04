﻿namespace CSCourseWork.Windows
{
    partial class EditorSettings
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
            this.editor_colordialog = new System.Windows.Forms.ColorDialog();
            this.accept_button = new System.Windows.Forms.Button();
            this.export_button = new System.Windows.Forms.Button();
            this.properties_label = new System.Windows.Forms.Label();
            this.settinglist_treeview = new System.Windows.Forms.TreeView();
            this.settinglist_label = new System.Windows.Forms.Label();
            this.properties_panel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // accept_button
            // 
            this.accept_button.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.accept_button.Location = new System.Drawing.Point(412, 340);
            this.accept_button.Name = "accept_button";
            this.accept_button.Size = new System.Drawing.Size(142, 33);
            this.accept_button.TabIndex = 4;
            this.accept_button.Text = "Принять";
            this.accept_button.UseVisualStyleBackColor = true;
            // 
            // export_button
            // 
            this.export_button.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.export_button.Location = new System.Drawing.Point(266, 341);
            this.export_button.Name = "export_button";
            this.export_button.Size = new System.Drawing.Size(142, 33);
            this.export_button.TabIndex = 5;
            this.export_button.Text = "Загрузить";
            this.export_button.UseVisualStyleBackColor = true;
            // 
            // properties_label
            // 
            this.properties_label.AutoSize = true;
            this.properties_label.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.properties_label.Location = new System.Drawing.Point(266, 9);
            this.properties_label.Name = "properties_label";
            this.properties_label.Size = new System.Drawing.Size(126, 15);
            this.properties_label.TabIndex = 3;
            this.properties_label.Text = "Доступные свойства: ";
            // 
            // settinglist_treeview
            // 
            this.settinglist_treeview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.properties_panel.Location = new System.Drawing.Point(266, 28);
            this.properties_panel.Name = "properties_panel";
            this.properties_panel.Size = new System.Drawing.Size(288, 300);
            this.properties_panel.TabIndex = 8;
            this.properties_panel.WrapContents = false;
            // 
            // EditorSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(569, 386);
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

        private ColorDialog editor_colordialog;
        private Button accept_button;
        private Button export_button;
        private Label properties_label;
        private TreeView settinglist_treeview;
        private Label settinglist_label;
        private FlowLayoutPanel properties_panel;
    }
}