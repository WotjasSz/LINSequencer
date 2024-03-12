﻿using Caliburn.Micro;
using LINSequencerLib;
using LINSequencerLib.Sequence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void DragSelectedFunction(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView) 
            {
                ListView draggedItem = (ListView)sender;
                DragDrop.DoDragDrop(draggedItem, draggedItem.DataContext, DragDropEffects.Copy);
            }
        }

        //public void MoveSelectedFunction(object sender, MouseEventArgs e)
        //{
        //    // Zacznij operację przeciągania, gdy użytkownik przesunie mysz podczas naciśnięcia lewego przycisku myszy
        //    if (e.LeftButton == MouseButtonState.Pressed)
        //    {
        //        Point position = e.GetPosition(null);
        //        //Vector diff = _startPoint - position;

        //        if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
        //            Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
        //        {
        //            if ((sender as ListView).Items.CurrentItem != null)
        //            {
        //                //string item = (string)listBox1.ItemContainerGenerator.ItemFromContainer(listBoxItem);
        //                //DataObject dragData = new DataObject("myFormat", item);
        //                //DragDrop.DoDragDrop(listBoxItem, dragData, DragDropEffects.Move);
        //            }
        //        }
        //    }
        //}        

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() => SequenceFunction = new BindableCollection<SeqFunction>(LinSequencer.FunctionList));
        }
    }
}
