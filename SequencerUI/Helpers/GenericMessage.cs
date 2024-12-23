using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequencerUI.Helpers
{
    public sealed class GenericMessage<T>
    {
        public T Content { get; }

        public GenericMessage(T content) 
        {
            Content = content; 
        }
    }
}
