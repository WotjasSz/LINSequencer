using Caliburn.Micro;
using LINSequencerLib;
using LINSequencerLib.Sequence;
using LINSequencerLib.SupportingFiles;
using SequenceBuilderUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SequenceBuilderUI.ViewModels
{
    public class SequenceViewModel : Conductor<Screen>.Collection.OneActive, IHandle<SequenceMessage>
    {
        #region Fields
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _windowManager;

        private EditorFeatureViewModel _editorFeatureVm;

        private SequenceModel _sequence;
        private string _name;
        private string _description;
        private DateTime _creationDate;
        private BindableCollection<string> _sdfList;
        private string _sdfName;
        private BindableCollection<SequenceStepModel> _stepList;
        private SequenceStepModel _selectedItem;

        public SequenceStepModel SelectedItem
        {
            get { return _selectedItem; }
            set 
            { 
                Set(ref _selectedItem, value);
                _eventAggregator.PublishOnUIThreadAsync(_selectedItem);
            }
        }

        #endregion

        #region Properties
        public EditorFeatureViewModel EditorFeatureVm
        {
            get { return _editorFeatureVm; }
            set 
            { 
                Set(ref _editorFeatureVm, value); 
            }
        }

        public SequenceModel Sequence
        {
            get { return _sequence; }
            set 
            {
                Set(ref _sequence, value);
                Name = _sequence.Name;
                Description = _sequence.Description;
                CreationDate = _sequence.CreationDate;
                SdfName = _sequence.SdfName;
                StepList = new BindableCollection<SequenceStepModel>(_sequence.StepList);
            }
        }

        public string Name
        {
            get { return _name; }
            set 
            { 
                Set(ref _name, value);
                _sequence.Name = value;
            }
        }             

        public string Description
        {
            get { return _description; }
            set 
            { 
                Set(ref _description, value);
                _sequence.Description = value;
            }
        }

        public DateTime CreationDate
        {
            get { return _creationDate; }
            set 
            { 
                Set(ref _creationDate, value);
            }
        }        

        public BindableCollection<string> SdfList
        {
            get { return _sdfList; }
            set 
            { 
                Set(ref _sdfList, value); 
            }
        }

        public string SdfName
        {
            get { return _sdfName; }
            set 
            { 
                Set(ref _sdfName, value);
                _sequence.SdfName = value;
            }
        }

        public BindableCollection<SequenceStepModel> StepList
        {
            get { return _stepList; }
            set 
            {
                Set(ref _stepList, value);
            }
        }


        #endregion

        public SequenceViewModel(IEventAggregator eventAggregator, IWindowManager windowManager, EditorFeatureViewModel editorFeatureViewModel)
        {
            _eventAggregator = eventAggregator;
            _windowManager = windowManager;
            _editorFeatureVm = editorFeatureViewModel;

            EditorFeatureVm.ConductWith(this);

            _eventAggregator.SubscribeOnPublishedThread(this);
        }

        public Task HandleAsync(SequenceMessage message, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                if (message.Sender != this) 
                {
                    Sequence = message.Sequence;                    
                }
                
             });
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            List<string> list = new List<string>(LinSequencer.SdfList.Select(x => x.Name).ToList());
            return Task.Run(() => SdfList = new BindableCollection<string>(list));
        }
    }
}
