using LINSequencerLib.Sequence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SequencerUI.Helpers
{
    public class OptionDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate BoolTemplate { get; set; }
        public DataTemplate BoolSwitchTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is ParamOptionBool)
            {
                if ((item as ParamOptionBool).Type is EParamOptionType.Bool)
                {
                    return BoolTemplate;
                }
                else if ((item as ParamOptionBool).Type is EParamOptionType.BoolSwitch)
                {
                    return BoolSwitchTemplate;
                }
            }
            return base.SelectTemplate(item, container);
        }
    }
}
