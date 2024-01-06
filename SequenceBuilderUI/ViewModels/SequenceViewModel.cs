using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceBuilderUI.ViewModels
{
    public class SequenceViewModel : Conductor<Screen>.Collection.OneActive
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _windowManager;

        private EditorFeatureViewModel _editorFeatureVm;

        public EditorFeatureViewModel EditorFeatureVm
        {
            get { return _editorFeatureVm; }
            set 
            { 
                Set(ref _editorFeatureVm, value); 
            }
        }


        public SequenceViewModel(IEventAggregator eventAggregator, IWindowManager windowManager, EditorFeatureViewModel editorFeatureViewModel)
        {
            _eventAggregator = eventAggregator;
            _windowManager = windowManager;
            _editorFeatureVm = editorFeatureViewModel;

            EditorFeatureVm.ConductWith(this);
        }
    }
}
