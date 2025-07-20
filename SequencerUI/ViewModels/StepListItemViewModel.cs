using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using LINSequencerLib.Sequence;
using LINSequencerLib.SequenceStep;
using LINSequencerLib.SupportingFiles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SequencerUI.ViewModels
{
    public partial class StepListItemViewModel : ObservableObject
    {
        private readonly IMessenger _messenger;

        [ObservableProperty]
        private ISeqStep<object>? _sequenceStep;

        [ObservableProperty]
        private int _id;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string? _output;

        [ObservableProperty]
        private EStepResult _result;

        [ObservableProperty]
        private string _stepName;

        public StepListItemViewModel(IMessenger messenger, ISeqStep<object> sequenceStep)
        {
            SequenceStep = sequenceStep;
            Id = sequenceStep.Id;
            Name = sequenceStep.Name;
            StepName = sequenceStep.StepName;
            Result = sequenceStep.Result;
            Output = "";

            sequenceStep.StepResultChanged += OnStepResultChanged;
        }

        private void OnStepResultChanged(object? sender, EventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(() =>
            {
                if (sender is ISeqStep<object> step)
                {
                    Result = step.Result;
                    if (step.Output is byte[] bytes)
                    {
                        Output = bytes.ByteArrayToHexString();
                    }
                    else
                    {
                        Output = step.Output?.ToString() ?? string.Empty;
                    }
                }
            });
        }
    }
}
