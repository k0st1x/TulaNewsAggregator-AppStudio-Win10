//---------------------------------------------------------------------------
//
// <copyright file="Section8ListPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>8/30/2016 2:02:12 PM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.DataProviders.YouTube;
using AppStudio.Sections;
using AppStudio.ViewModels;
using AppStudio.Uwp;

namespace AppStudio.Pages
{
    public sealed partial class Section8ListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public Section8ListPage()
        {
			ViewModel = ViewModelFactory.NewList(new Section8Section());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
            Microsoft.AppCenter.Analytics.Analytics.TrackEvent(this.GetType().FullName);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("36ce0555-3281-4038-9541-974782d43adf");
			ShellPage.Current.ShellControl.SetCommandBar(commandBar);
			if (e.NavigationMode == NavigationMode.New)
            {
				await this.ViewModel.LoadDataAsync();
                this.ScrollToTop();
			}
            base.OnNavigatedTo(e);
        }

    }
}
