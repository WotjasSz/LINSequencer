using LINSequencerLib.Sequence;

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
