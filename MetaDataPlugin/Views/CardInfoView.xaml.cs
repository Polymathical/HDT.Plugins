﻿using HDT.Plugins.Custom.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace HDT.Plugins.Custom.Controls
{
    /// <summary>
    /// Interaction logic for CardInfoView.xaml
    /// </summary>
    public partial class CardInfoView : UserControl
    {
        public ObservableCollection<CardViewModel> CardInfo { get; set; } = new ObservableCollection<CardViewModel>();

        public CardInfoView()
        {
            InitializeComponent();
        }
    }
}