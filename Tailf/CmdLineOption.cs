using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Tailf
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple=false)]
    public class OptionAttribute:Attribute
    {
        public OptionAttribute(string flag)
        {
            Flag = flag;
        }
        public string Flag { get; set; }
        public bool Optional { get; set; }
        public string Default { get; set; }
        public string ShortHelp { get; set; }
        public string LongHelp { get; set; }
        internal bool Given { get; set; }
        PropertyInfo pi;
        internal void SetPropertyInfo(PropertyInfo pi)
        {
            this.pi = pi;
        }
        internal PropertyInfo GetPropertyInfo()
        {
            return pi;
        }
    }
}
