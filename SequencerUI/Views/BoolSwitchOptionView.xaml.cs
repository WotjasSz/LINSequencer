using LINSequencerLib.Sequence;
using SequencerUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SequencerUI.Views
{
    /// <summary>
    /// Interaction logic for BoolSwitchOptionView.xaml
    /// </summary>
    public partial class BoolSwitchOptionView : UserControl
    {
        public BoolSwitchOptionView()
        {
            InitializeComponent();
            this.DataContextChanged += BoolSwitchOptionView_DataContextChanged;
        }

        private void BoolSwitchOptionView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.DataContext is ParamOptionBoolSwitch)
            {
                var vm = new BoolSwitchOptionViewModel((ParamOptionBoolSwitch)this.DataContext);

                this.DataContext = vm;
            }

            
        }
    }
}
