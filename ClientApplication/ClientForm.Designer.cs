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
            this.nodelinks_label = new System.Windows.Forms.Label();
            this.addoperation_button = new System.Windows.Forms.Button();
            this.deleteoperation_button = new System.Windows.Forms.Button();
            this.connectoperation_button = new System.Windows.Forms.Button();
            this.operationstate_label = new System.Windows.Forms.Label();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.test = new System.Windows.Forms.Button();
            this.SuspendLayout();
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
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(12, 210);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(253, 233);
            this.propertyGrid1.TabIndex = 10;
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(12, 46);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(253, 148);
            this.treeView1.TabIndex = 11;
            // 
            // test
            // 
            this.test.Location = new System.Drawing.Point(12, 451);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(75, 23);
            this.test.TabIndex = 12;
            this.test.Text = "button1";
            this.test.UseVisualStyleBackColor = true;
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(821, 486);
            this.Controls.Add(this.test);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.operationstate_label);
            this.Controls.Add(this.connectoperation_button);
            this.Controls.Add(this.deleteoperation_button);
            this.Controls.Add(this.addoperation_button);
            this.Controls.Add(this.nodelinks_label);
            this.MaximumSize = new System.Drawing.Size(837, 525);
            this.MinimumSize = new System.Drawing.Size(837, 525);
            this.Name = "ClientForm";
            this.Text = "Редактор карты";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label nodelinks_label;
        private Button addoperation_button;
        private Button deleteoperation_button;
        private Button connectoperation_button;
        private Label operationstate_label;
        private PropertyGrid propertyGrid1;
        private TreeView treeView1;
        private Button test;
    }
}