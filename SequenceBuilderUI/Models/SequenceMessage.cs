using LINSequencerLib.Sequence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceBuilderUI.Models
{
    public class SequenceMessage
    {
        public object Sender;
        public SequenceModel Sequence;

        public SequenceMessage(object sender, SequenceModel sequence)
        {
            Sender = sender;
            Sequence = sequence;
        }
    }
}
