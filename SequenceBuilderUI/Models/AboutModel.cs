using LINSequencerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceBuilderUI.Models
{
    public class AboutModel
    {
        public string Title { get; set; } = "LIN sequencer";
        public string Version { get; set; } = "1.0";
        public string Author { get; set; } = "Sztuk Wojciech";
        public string DllVersion { get; set; } = LinSequencer.DllVersion;
        public string WrapperVersion { get; set; } = LinSequencer.WrapperVersion;
    }
}
