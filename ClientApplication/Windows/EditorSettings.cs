using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

using CSCourseWork.EditorComponents;
using CSCourseWork.EditorConfiguration;

namespace CSCourseWork.Windows
{
    public partial class EditorSettings<TNodeController> : System.Windows.Forms.Form
         where TNodeController : NodesControllers.INodesControllerWithConnectors, new()
    {
        public delegate Control TypeSettingHandler(EditorConfiguration.EditorConfigProperty property);

        private EditorComponentBase<TNodeController> EditorInstance { get; set; } = default!;
        private IEditorConfigProvider ConfigProvider { get; set; } = default!;

        private Dictionary<string, EditorConfigProperty> SettingsBuffer { get; set; } = new();

        private static Hashtable TypeHandlers = new Hashtable() 
        {
            ["EditorScale"] = new TypeSettingHandler(EditorScaleSettingHandler),
            ["EditorColor"] = new TypeSettingHandler(EditorColorSettingHandler),
            ["EditorFontFamily"] = new TypeSettingHandler(EditorFontFamilySettingHandler),
            ["Int32"] = new TypeSettingHandler(Int32SettingHandler),
        };

        public EditorSettings(EditorComponentBase<TNodeController> editor, IEditorConfigProvider config_provider)
        {
            (this.EditorInstance, this.ConfigProvider) = (editor, config_provider);
            this.InitializeComponent(); this.SettingListInitialize(string.Empty);

            this.settinglist_treeview.NodeMouseClick += SettinglistTreeviewNodeMouseClick;
            this.search_textbox.TextChanged += SearchTextboxTextChanged;

            this.accept_button.Click += new EventHandler(AcceptButtonClick);
            this.export_button.Click += new EventHandler(ExportButtonClick);
        }

        private void SearchTextboxTextChanged(object? sender, System.EventArgs args)
            => this.SettingListInitialize(this.search_textbox.Text);
        
        private void ExportButtonClick(object? sender, System.EventArgs args)
        {
            EditorConfiguration.EditorConfigProvider export_config;
            using (var configfile_dialog = new OpenFileDialog())
            {
                configfile_dialog.InitialDirectory = Directory.GetCurrentDirectory();
                configfile_dialog.Filter = "configuration files (*.config)|*.config";

                if (configfile_dialog.ShowDialog() != DialogResult.OK) return;
                export_config = new EditorConfigProvider(configfile_dialog.FileName);
            }
            List<EditorConfiguration.EditorConfigProperty> export_properies = default!;

            try { export_properies = export_config.TakeConfig(); }
            catch(System.Exception error)
            {
                MessageBox.Show($"Невозможно загрузить конфигурацию: {error.Message}", "Ошибка"); return;
            }
            foreach (var export_property in export_properies)
            {
                string? setting_key = default;
                foreach (var setting in this.SettingsBuffer)
                {
                    if (setting.Value.Name == export_property.Name &&
                        setting.Value.Type == export_property.Type) { setting_key = setting.Key; }
                }
                if (setting_key != null)
                { this.SettingsBuffer[setting_key].Value = export_property.Value; }
            }
            this.SettingListInitialize(string.Empty);
        }

        private void AcceptButtonClick(object? sender, System.EventArgs args)
        {
            foreach (var item in this.SettingsBuffer)
            {
                try { this.ConfigProvider.PutConfigProperty(item.Value.Name, item.Value.Value); } 
                catch { continue; }
            }
            this.DialogResult = DialogResult.OK;
        }

        private static Control Int32SettingHandler(EditorConfigProperty property)
        {
            var int_control = new NumericUpDown() { Value = (int)property.Value, 
                Minimum = 1, Maximum = 100, Width = 75, Font = new Font("Segoe UI", 10) };

            int_control.ValueChanged += delegate { property.Value = (int)int_control.Value; };
            return int_control;
        }

        private static Control EditorFontFamilySettingHandler(EditorConfigProperty property)
        {
            var font_control = new ComboBox() { DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 200, Font = new Font("Segoe UI", 10) };

            foreach (var fontfamily in FontFamily.Families) font_control.Items.Add(fontfamily.Name);
            font_control.Text = ((EditorFontFamily)property.Value).FontFamily;

            font_control.SelectedValueChanged += delegate (object? sender, EventArgs args)
            {
                var font_value = new EditorFontFamily() { FontFamily = font_control.Text };
                property.Value = font_value;
            };
            return (font_control);
        }

        private static Control EditorScaleSettingHandler(EditorConfigProperty property)
        {
            var scale_value = (EditorScale)property.Value;

            var min_control = new NumericUpDown() { Name = "min", Value = scale_value.Min, 
                Minimum = 10, Maximum = 100, Width = 75, Font = new Font("Segoe UI", 10) };

            var max_control = new NumericUpDown() { Name = "max", Value = scale_value.Max,
                Minimum = 10, Maximum = 100, Width = 75, Font = new Font("Segoe UI", 10) };

            var scale_handler = delegate (object? sender, EventArgs args)
            {
                var control = (sender as NumericUpDown)!;

                if (control.Name == "min") scale_value.Min = (int)min_control.Value;
                else if (control.Name == "max") scale_value.Max = (int)max_control.Value;
                else return;

                property.Value = scale_value;
            };

            min_control.ValueChanged += new EventHandler(scale_handler);
            max_control.ValueChanged += new EventHandler(scale_handler);

            var controls_group = new FlowLayoutPanel()
            { WrapContents = false, FlowDirection = FlowDirection.LeftToRight, AutoSize = true };

            controls_group.Controls.Add(min_control);
            controls_group.Controls.Add(max_control);

            return controls_group;
        }

        private static Control EditorColorSettingHandler(EditorConfigProperty property)
        {
            var color_control = new Button() { Text = string.Empty, Font = new Font("Segoe UI", 10),
                BackColor = ((EditorColor)property.Value), Size = new(75, 30) };

            color_control.Click += delegate (object? sender, EventArgs args)
            {
                var color_dialog = new ColorDialog() { FullOpen = true };
                if (color_dialog.ShowDialog() != DialogResult.OK) return;

                color_control.BackColor = color_dialog.Color;
                property.Value = new EditorColor(color_dialog.Color.R, color_dialog.Color.G, color_dialog.Color.B);
            };
            return color_control;
        }

        private void SettinglistTreeviewNodeMouseClick(object? sender, TreeNodeMouseClickEventArgs args)
        {
            this.properties_panel.Controls.Clear();

            EditorConfiguration.EditorConfigProperty node_property;
            if (args.Node.Level == 0)
            {
                foreach (TreeNode node in args.Node.Nodes)
                {
                    node_property = this.SettingsBuffer[node.Text];
                    ControlsPanelAdd(node.Text, ref node_property); 
                }
                return;
            }
            node_property = this.SettingsBuffer[args.Node.Text];
            ControlsPanelAdd(args.Node.Text, ref node_property);

            void ControlsPanelAdd(string name, ref EditorConfigProperty property_config) 
            {
                this.properties_panel.Controls.Add(new Label() { Text = $"{name}:", AutoSize = true });

                var type_setting = (TypeHandlers[property_config.Type.Name] as TypeSettingHandler)?.Invoke(property_config);
                this.properties_panel.Controls.Add(type_setting);
            }
        }

        private void SettingListInitialize(string find_setting = "")
        {
            this.settinglist_treeview.Nodes.Clear();
            this.properties_panel.Controls.Clear();

            this.settinglist_treeview.Nodes.Add("Прочее");
            var search_pattern = ($"[\\w ]*{find_setting}[\\w ]*");

            foreach (var properties in this.EditorInstance.GetType().GetProperties())
            {
                if (properties.PropertyType.GetCustomAttribute<EditorConfigTypeAttribute>(true) == null
                    && (properties.GetCustomAttribute<EditorConfigTargetAttribute>(true) == null)) continue;

                var setting_attribute = properties.GetCustomAttribute<EditorSettingsAttribute>(true);
                var buffer_value = properties.GetValue(this.EditorInstance);

                if (setting_attribute == null || buffer_value == null) continue;
                var buffer = new EditorConfigProperty(properties.Name, buffer_value);

                if (this.SettingsBuffer.ContainsKey(setting_attribute.SettingName) != true)
                {
                    this.SettingsBuffer.Add(setting_attribute.SettingName, buffer);
                }
                var selected_section = (setting_attribute.SettingSection == string.Empty) ? 0 : default(int?);
                for (int index = 1; index < this.settinglist_treeview.Nodes.Count; index++)
                {
                    if (this.settinglist_treeview.Nodes[index].Text == setting_attribute.SettingSection) 
                    { selected_section = index; break; }
                }

                var search_check = Regex.IsMatch(setting_attribute.SettingName, search_pattern, RegexOptions.IgnoreCase);
                if (selected_section != null)
                {
                    var name = setting_attribute.SettingName;
                    if (search_check) this.settinglist_treeview.Nodes[selected_section.Value].Nodes.Add(name);
                    continue;
                }
                this.settinglist_treeview.Nodes.Add(setting_attribute.SettingSection);
                if (search_check != true) continue;

                var last_index = this.settinglist_treeview.Nodes.Count - 1;
                this.settinglist_treeview.Nodes[last_index].Nodes.Add(setting_attribute.SettingName);
            }
            this.settinglist_treeview.ExpandAll();
        }
    }
}
