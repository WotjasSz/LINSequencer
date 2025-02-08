using CommunityToolkit.Mvvm.Messaging;
using SequencerUI.Helpers;
using SequencerUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SequencerUI.Resources.Controls
{
    public class CustomTextBox : TextBox
    {
        private readonly IMessenger _messenger;

        public static readonly DependencyProperty CustomNameProperty =
        DependencyProperty.Register("CustomName", typeof(string), typeof(CustomTextBox), new PropertyMetadata(string.Empty));

        public string CustomName
        {
            get { return (string)GetValue(CustomNameProperty); }
            set { SetValue(CustomNameProperty, value); }
        }

        public CustomTextBox()
        {
            _messenger = ServiceLocator.GetService<IMessenger>();
            this.PreviewTextInput += CustomTextBox_PreviewTextInput;
        }

        private void CustomTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {            
            if (e.Text == "<")
            {
                ControlMessage message = new ControlMessage(this, EControlMessage.ShowPopup);
                _messenger.Send(new GenericMessage<ControlMessage>(message));
            }
            else if (e.Text == ">")
            {
                ControlMessage message = new ControlMessage(this, EControlMessage.HidePopup);
                _messenger.Send(new GenericMessage<ControlMessage>(message));
            }            
        }
    }
}
