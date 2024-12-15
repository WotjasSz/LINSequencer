using Caliburn.Micro;

namespace SequenceBuilderUI.ViewModels
{
    public class EditorFeatureViewModel : Conductor<Screen>.Collection.AllActive
    {
        private AvailableFunctionViewModel _functionVM;
        private PropertiesViewModel _propertiesVm;

        public PropertiesViewModel PropertiesVm
        {
            get { return _propertiesVm; }
            set
            {
                Set(ref _propertiesVm, value);
            }
        }

        public AvailableFunctionViewModel FunctionVm
        {
            get { return _functionVM; }
            set
            {
                Set(ref _functionVM, value);
            }
        }

        public EditorFeatureViewModel(AvailableFunctionViewModel availableFunctionViewModel, PropertiesViewModel propertiesViewModel)
        {
            _propertiesVm = propertiesViewModel;
            _functionVM = availableFunctionViewModel;

            FunctionVm.ConductWith(this);
            PropertiesVm.ConductWith(this);
        }
    }
}
