using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CSCourseWork.EditorComponents
{
    [EditorConfiguration.EditorConfigTypeAttribute]
    public struct EditorScale
    {
        public static System.Int32 MinLimit = 10, MaxLimit = 100;

        private System.Int32 min = default, max = default;

        [EditorConfiguration.EditorConfigPropertyAttribute("min")]
        public System.Int32 Min
        { get => this.min; set => this.min = this.RangeValidate(value) ? value : MinLimit; }

        [EditorConfiguration.EditorConfigPropertyAttribute("max")]
        public System.Int32 Max
        { get => this.max; set => this.max = this.RangeValidate(value) ? value : MaxLimit; }

        private System.Boolean RangeValidate(int value) => (value <= MaxLimit && value >= MinLimit);

        public EditorScale(int min, int max) => (this.Min, this.Max) = (min, max);
        public EditorScale() : this(10, 100) { }
    }

    [EditorConfiguration.EditorConfigTypeAttribute]
    public struct EditorColor
    {
        private System.Int32 red = default, green = default, blue = default;

        [EditorConfiguration.EditorConfigPropertyAttribute("red")]
        public System.Int32 RColorValue
        { get => this.red; set => this.red = this.ColorValidate(value) ? value : 0; }

        [EditorConfiguration.EditorConfigPropertyAttribute("green")]
        public System.Int32 GColorValue 
        { get => this.green; set => this.green = this.ColorValidate(value) ? value : 0; }

        [EditorConfiguration.EditorConfigPropertyAttribute("blue")]
        public System.Int32 BColorValue
        { get => this.blue; set => this.blue = this.ColorValidate(value) ? value : 0; }

        public EditorColor(int r_color, int g_color, int b_color)
        { (this.RColorValue, this.GColorValue, this.BColorValue) = (r_color, g_color, b_color); }

        public EditorColor() : this(default(int), default(int), default(int)) { }

        private System.Boolean ColorValidate(int value) => (value <= 255 && value >= 0);

        public static implicit operator System.Drawing.Color(EditorColor color) 
            => Color.FromArgb(color.RColorValue, color.GColorValue, color.BColorValue);
    }

    [EditorConfiguration.EditorConfigTypeAttribute]
    public struct EditorFontFamily
    {
        [EditorConfiguration.EditorConfigPropertyAttribute("font")]
        public System.String FontFamily { get; set; } = string.Empty;

        public EditorFontFamily() => this.FontFamily = new("Arial");
    }

    [EditorConfiguration.EditorConfigTypeAttribute]
    public struct EditorTestType
    {
        [EditorConfiguration.EditorConfigPropertyAttribute("text")]
        public System.String Text { get; set; } = string.Empty;

        [EditorConfiguration.EditorConfigPropertyAttribute("range")]
        public EditorComponents.EditorScale Range { get; set; } = new();

        public EditorTestType(string text, int min, int max) 
            => (this.Text, this.Range) = (text, new(min, max));

        public EditorTestType() : this("", 0, 100) { }
    }
}
