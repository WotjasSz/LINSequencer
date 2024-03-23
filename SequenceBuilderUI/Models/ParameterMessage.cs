using LINSequencerLib.Sequence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceBuilderUI.Models
{
    public class ParameterMessage
    {
        public object Sender { get; set; }
        public SequenceStepModel SequenceStep { get; set; }

        public ParameterMessage(object sender, SequenceStepModel model) 
        {
            Sender = sender;
            SequenceStep = model;
        }
    }    
}
