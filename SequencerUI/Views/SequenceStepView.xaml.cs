﻿using LINSequencerLib.Sequence;
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
    /// Interaction logic for SequenceStepView.xaml
    /// </summary>
    public partial class SequenceStepView : UserControl
    {
        public SequenceStepView()
        {
            InitializeComponent();
            //this.DataContext = this;
            //SequenceStepViewModel seq = new SequenceStepViewModel(this);
        }
    }
}