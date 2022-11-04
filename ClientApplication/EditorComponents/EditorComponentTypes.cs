using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCourseWork.EditorComponents
{
    [EditorConfiguration.EditorConfigTypeAttribute]
    public struct EditorScale
    {
        [EditorConfiguration.EditorConfigPropertyAttribute("min")]
        public System.Int32 Min { get; set; } = default(int);

        [EditorConfiguration.EditorConfigPropertyAttribute("max")]
        public System.Int32 Max { get; set; } = default(int);

        public EditorScale(int min, int max) => (this.Min, this.Max) = (min, max);
        public EditorScale() : this(default(int), default(int)) { }
    }

    [EditorConfiguration.EditorConfigTypeAttribute]
    public struct EditorColor
    {
        [EditorConfiguration.EditorConfigPropertyAttribute("red")]
        public System.Int32 RColorValue { get; set; } = default(int);

        [EditorConfiguration.EditorConfigPropertyAttribute("green")]
        public System.Int32 GColorValue { get; set; } = default(int);

        [EditorConfiguration.EditorConfigPropertyAttribute("blue")]
        public System.Int32 BColorValue { get; set; } = default(int);

        public EditorColor(int r_color, int g_color, int b_color)
        { (this.RColorValue, this.GColorValue, this.BColorValue) = (r_color, g_color, b_color); }

        public EditorColor() : this(default(int), default(int), default(int)) { }

        public System.Drawing.Color ConvertToColor()
        { return Color.FromArgb(this.RColorValue, this.GColorValue, this.BColorValue); }
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

        public EditorTestType(string text, int min, int max) => (this.Text, this.Range) = (text, new(min, max));

        public EditorTestType() : this("", 0, 100) { }
    }
}
