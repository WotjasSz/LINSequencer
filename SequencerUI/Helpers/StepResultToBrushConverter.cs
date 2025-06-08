using LINSequencerLib.SequenceStep;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace SequencerUI.Helpers
{
    public class StepResultToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                EStepResult.Success => Brushes.Green,
                EStepResult.Positive => Brushes.LightGreen,
                EStepResult.Negative => Brushes.Orange,
                EStepResult.Timeout => Brushes.Purple,
                EStepResult.Jump => Brushes.Blue,
                EStepResult.Failure => Brushes.Red,
                _ => Brushes.Gray // Default case for None or unrecognized result
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static Brush GetBrush(string key)
        {
            return Application.Current.Resources.Contains(key)
                ? (Brush)Application.Current.Resources[key]
                : Brushes.Gray; // If the key does not exist, return a default color
        }
    }
}
