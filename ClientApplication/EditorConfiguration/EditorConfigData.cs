using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static CSCourseWork.EditorConfiguration.EditorConfigData;

namespace CSCourseWork.EditorConfiguration
{
    public sealed class EditorConfigData : System.Object, IEnumerable<EditorConfigProperties>
    {
        public record struct EditorConfigParameters(object ParamValue, System.Type ParamType);
        public record struct EditorConfigProperties(System.Type Type, object? Value)
        { public List<EditorConfigParameters> Parameters { get; set; } = new List<EditorConfigParameters>(); }

        public List<string> UsingNamespaces { get; private set; } = new List<string>();
        public List<EditorConfigProperties> Properties { get; private set; }

        public IEnumerator<EditorConfigProperties> GetEnumerator() => this.Properties.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
