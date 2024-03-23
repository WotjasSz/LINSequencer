using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceBuilderUI.Models
{
    public class ParameterModel
    {
        public EParameterType ParameterType; 
        public string Name { get; set; }
        public string Value { get; set; }

        public ParameterModel(EParameterType type, string name, string value)
        {
            ParameterType = type;
            Name = name;
            Value = value;
        }        
    }
}
