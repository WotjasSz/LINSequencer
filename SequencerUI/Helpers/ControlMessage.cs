using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequencerUI.Helpers
{    
    public class ControlMessage
    {
        public object ControlInstance;
        public EControlMessage CtrlMessage;
        public string Message;

        public ControlMessage(object controlInstance, string message)
        {
            ControlInstance = controlInstance;
            Message = message;
            CtrlMessage = EControlMessage.None;
        }

        public ControlMessage(object controlInstance, EControlMessage cntrlmessage)
        {
            ControlInstance = controlInstance;
            Message = string.Empty;
            CtrlMessage = cntrlmessage;
        }

        public ControlMessage(object controlInstance, EControlMessage cntrlmessage, string message)
        {
            ControlInstance = controlInstance;
            Message = message;
            CtrlMessage = cntrlmessage;
        }
    }
}
