namespace CSCourseWork
{
    internal partial class ClientForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.nodelinks_listview = new System.Windows.Forms.ListView();
            this.nodeid_column = new System.Windows.Forms.ColumnHeader();
            this.nodeposition_column = new System.Windows.Forms.ColumnHeader();
            this.nodelinks_label = new System.Windows.Forms.Label();
            this.addoperation_button = new System.Windows.Forms.Button();
            this.deleteoperation_button = new System.Windows.Forms.Button();
            this.connectoperation_button = new System.Windows.Forms.Button();
            this.operationstate_label = new System.Windows.Forms.Label();
            this.connectors_listbox = new System.Windows.Forms.ListBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // nodelinks_listview
            // 
            this.nodelinks_listview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nodeid_column,
            this.nodeposition_column});
            this.nodelinks_listview.FullRowSelect = true;
            this.nodelinks_listview.GridLines = true;
            this.nodelinks_listview.Location = new System.Drawing.Point(12, 38);
            this.nodelinks_listview.MultiSelect = false;
            this.nodelinks_listview.Name = "nodelinks_listview";
            this.nodelinks_listview.Size = new System.Drawing.Size(278, 154);
            this.nodelinks_listview.TabIndex = 2;
            this.nodelinks_listview.UseCompatibleStateImageBehavior = false;
            this.nodelinks_listview.View = System.Windows.Forms.View.Details;
            // 
            // nodeid_column
            // 
            this.nodeid_column.Text = "ID Узла";
            // 
            // nodeposition_column
            // 
            this.nodeposition_column.Text = "Координаты";
            this.nodeposition_column.Width = 120;
            // 
            // nodelinks_label
            // 
            this.nodelinks_label.AutoSize = true;
            this.nodelinks_label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nodelinks_label.Location = new System.Drawing.Point(12, 14);
            this.nodelinks_label.Name = "nodelinks_label";
            this.nodelinks_label.Size = new System.Drawing.Size(145, 19);
            this.nodelinks_label.TabIndex = 4;
            this.nodelinks_label.Text = "Подключенные узлы:";
            // 
            // addoperation_button
            // 
            this.addoperation_button.Location = new System.Drawing.Point(305, 12);
            this.addoperation_button.Name = "addoperation_button";
            this.addoperation_button.Size = new System.Drawing.Size(71, 23);
            this.addoperation_button.TabIndex = 5;
            this.addoperation_button.Text = "add";
            this.addoperation_button.UseVisualStyleBackColor = true;
            // 
            // deleteoperation_button
            // 
            this.deleteoperation_button.Location = new System.Drawing.Point(382, 12);
            this.deleteoperation_button.Name = "deleteoperation_button";
            this.deleteoperation_button.Size = new System.Drawing.Size(71, 23);
            this.deleteoperation_button.TabIndex = 6;
            this.deleteoperation_button.Text = "delete";
            this.deleteoperation_button.UseVisualStyleBackColor = true;
            // 
            // connectoperation_button
            // 
            this.connectoperation_button.Location = new System.Drawing.Point(459, 12);
            this.connectoperation_button.Name = "connectoperation_button";
            this.connectoperation_button.Size = new System.Drawing.Size(71, 23);
            this.connectoperation_button.TabIndex = 7;
            this.connectoperation_button.Text = "connect";
            this.connectoperation_button.UseVisualStyleBackColor = true;
            // 
            // operationstate_label
            // 
            this.operationstate_label.AutoSize = true;
            this.operationstate_label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.operationstate_label.Location = new System.Drawing.Point(582, 14);
            this.operationstate_label.Name = "operationstate_label";
            this.operationstate_label.Size = new System.Drawing.Size(227, 19);
            this.operationstate_label.TabIndex = 8;
            this.operationstate_label.Text = "Текущий инструмент: Добавление";
            // 
            // connectors_listbox
            // 
            this.connectors_listbox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.connectors_listbox.FormattingEnabled = true;
            this.connectors_listbox.ItemHeight = 17;
            this.connectors_listbox.Location = new System.Drawing.Point(12, 210);
            this.connectors_listbox.Name = "connectors_listbox";
            this.connectors_listbox.Size = new System.Drawing.Size(278, 123);
            this.connectors_listbox.TabIndex = 9;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(12, 210);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(130, 233);
            this.propertyGrid1.TabIndex = 10;
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(821, 486);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.connectors_listbox);
            this.Controls.Add(this.operationstate_label);
            this.Controls.Add(this.connectoperation_button);
            this.Controls.Add(this.deleteoperation_button);
            this.Controls.Add(this.addoperation_button);
            this.Controls.Add(this.nodelinks_label);
            this.Controls.Add(this.nodelinks_listview);
            this.MaximumSize = new System.Drawing.Size(837, 525);
            this.MinimumSize = new System.Drawing.Size(837, 525);
            this.Name = "ClientForm";
            this.Text = "Редактор карты";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ListView nodelinks_listview;
        private Label nodelinks_label;
        private ColumnHeader nodeid_column;
        private ColumnHeader nodeposition_column;
        private Button addoperation_button;
        private Button deleteoperation_button;
        private Button connectoperation_button;
        private Label operationstate_label;
        private ListBox connectors_listbox;
        private PropertyGrid propertyGrid1;
    }
}