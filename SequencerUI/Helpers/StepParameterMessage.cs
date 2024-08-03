using CommunityToolkit.Mvvm.Messaging.Messages;
using LINSequencerLib.Sequence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequencerUI.Helpers
{
    public sealed class StepParameterMessage : ValueChangedMessage<SequenceStepModel>
    {
        public StepParameterMessage(SequenceStepModel value) : base(value)
        {
            
        }
    }
}
