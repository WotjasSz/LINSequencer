using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceBuilderUI.Models
{
    public class ParameterModel
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public ParameterModel(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
