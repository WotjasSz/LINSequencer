using LINSequencerLib.Sequence;
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
    class StepParameterDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate BoolTemplate { get; set; }
        public DataTemplate StringTemplate { get; set; }
        public DataTemplate IntTemplate { get; set; }        

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            //if(item is SequenceStepParamModel)
            //{
            //    if ((item as SequenceStepParamModel).ParamType == "System.Boolean")
            //    {
            //        return BoolTemplate;
            //    }
            //    else if ((item as SequenceStepParamModel).ParamType == "System.String")
            //    {
            //        return StringTemplate;
            //    }
            //    else if ((item as SequenceStepParamModel).ParamType == "System.Int32")
            //    {
            //        return IntTemplate;
            //    }                
            //}            

            if (item is BoolParameterViewModel)
            {
                return BoolTemplate;
            }
            if (item is StringParameterViewModel)
            {
                return StringTemplate;
            }
            if (item is IntParameterViewModel)
            {
                return IntTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
