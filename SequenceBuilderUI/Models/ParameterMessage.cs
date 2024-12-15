using LINSequencerLib.Sequence;

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
