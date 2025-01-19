using LINSequencerLib.Sequence;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SequencerUI.Helpers
{
    public  class StepParameterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var parameterList = value as IEnumerable<SequenceStepParamModel>;
            if (parameterList != null)
            {
                string paramName = parameter as string;
                var param = parameterList.FirstOrDefault(p => p.Name == paramName);
                return param?.ParamValue ?? string.Empty;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
