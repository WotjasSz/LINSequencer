using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequencerUI.Models
{
    public class BoolSwitchOptionModel
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public bool IsSelected { get; set; }
    }
}
