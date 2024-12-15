using Caliburn.Micro;
using LINSequencerLib;
using LINSequencerLib.Sequence;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SequenceBuilderUI.ViewModels
{
    public class AvailableFunctionViewModel : Screen
    {
        private IEventAggregator _eventAggregator;
        private IWindowManager _windowManager;

        private Point _startPosition = new Point();
        private BindableCollection<SeqFunction> _sequenceFunction;
        private SeqFunction _selectedFunction;

        public BindableCollection<SeqFunction> SequenceFunction
        {
            get { return _sequenceFunction; }
            set
            {
                Set(ref _sequenceFunction, value);
            }
        }

        public SeqFunction SelectedFunction
        {
            get { return _selectedFunction; }
            set
            {
                Set(ref _selectedFunction, value);
            }
        }

        public AvailableFunctionViewModel(IEventAggregator eventAggregator, IWindowManager windowManager)
        {
            _eventAggregator = eventAggregator;
            _windowManager = windowManager;
        }

        public void AddSequenceStep()
        {
            _eventAggregator.PublishOnUIThreadAsync(SelectedFunction);
        }

        public void DragSelectedFunction(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is ListView)
            {
                //ListView draggedItem = (ListView)sender;
                //DragDrop.DoDragDrop(draggedItem, draggedItem.DataContext, DragDropEffects.Copy);
                _startPosition = e.GetPosition(null);
            }
        }

        public void FunctionList_SelectionChanged(object sender)
        {
            if (sender is AvailableFunctionViewModel)
            {
                return;
            }
        }

        public void MoveSelectedFunction(object sender, MouseEventArgs e)
        {
            // Zacznij operację przeciągania, gdy użytkownik przesunie mysz podczas naciśnięcia lewego przycisku myszy
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point position = e.GetPosition(null);
                Vector diff = _startPosition - position;

                if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    //Console.WriteLine(e.OriginalSource.GetType());

                    if (e.OriginalSource is TextBlock)
                    {
                        Console.WriteLine((e.OriginalSource as TextBlock).DataContext.GetType());
                        //string item = (string)listBox1.ItemContainerGenerator.ItemFromContainer(listBoxItem);
                        //DataObject dragData = new DataObject("myFormat", item);
                        //DragDrop.DoDragDrop(listBoxItem, dragData, DragDropEffects.Move);
                        //Console.WriteLine((sender as ListView).Items.CurrentItem);
                    }
                }
            }
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() => SequenceFunction = new BindableCollection<SeqFunction>(LinSequencer.FunctionList));
        }
    }
}
