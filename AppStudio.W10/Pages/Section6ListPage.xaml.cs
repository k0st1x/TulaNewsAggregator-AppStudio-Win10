//---------------------------------------------------------------------------
//
// <copyright file="Section6ListPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>8/30/2016 2:02:12 PM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.DataProviders.Rss;
using AppStudio.Sections;
using AppStudio.ViewModels;
using AppStudio.Uwp;

namespace AppStudio.Pages
{
    public sealed partial class Section6ListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public Section6ListPage()
        {
			ViewModel = ViewModelFactory.NewList(new Section6Section());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
            Microsoft.AppCenter.Analytics.Analytics.TrackEvent(this.GetType().FullName);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("1a748fc4-95dd-4ebb-8c8f-377e05b8b92d");
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
