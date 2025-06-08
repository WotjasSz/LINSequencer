using LINSequencerLib.SequenceStep;
using SequencerUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SequencerUI.Helpers
{
    public class StepTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SingleStepTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is StepListItemViewModel)
            {
                return SingleStepTemplate;
            }
            return base.SelectTemplate(item, container);
        }

    }
}
