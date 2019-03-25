//---------------------------------------------------------------------------
//
// <copyright file="Section2ListPage.xaml.cs" company="Microsoft">
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
    public sealed partial class Section2ListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public Section2ListPage()
        {
			ViewModel = ViewModelFactory.NewList(new Section2Section());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
            Microsoft.HockeyApp.HockeyClient.Current.TrackEvent(this.GetType().FullName);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("b088b34f-0d4e-470e-91b2-70c4292e07af");
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
