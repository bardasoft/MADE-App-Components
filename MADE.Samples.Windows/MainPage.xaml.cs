﻿namespace MADE.Samples.Windows
{
    using MADE.App.Views.Navigation.ViewModels;

    public sealed partial class MainPage : MADE.App.Views.Navigation.Pages.MvvmPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = new PageViewModel();
        }
    }
}