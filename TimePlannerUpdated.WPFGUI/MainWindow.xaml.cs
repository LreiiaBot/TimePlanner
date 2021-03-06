﻿using System.Windows;
using TimePlannerUpdated.Default;
using TimePlannerUpdated.WPFGUI.Pages;

namespace TimePlannerUpdated.WPFGUI
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            frameAutoAdd.Navigate(new AutoAddPage(((MainViewModel)DataContext).SelectedList));
        }
    }
}
