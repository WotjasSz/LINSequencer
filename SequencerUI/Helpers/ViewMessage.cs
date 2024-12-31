using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequencerUI.Helpers
{
    public class ViewMessage
    {
        public EViewMode Mode;
        public object View;

        public ViewMessage(EViewMode mode, object seq)
        {
            Mode = mode;
            View = seq;
        }
    }
}
