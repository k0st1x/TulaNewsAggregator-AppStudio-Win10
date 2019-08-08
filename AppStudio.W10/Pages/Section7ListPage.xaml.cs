//---------------------------------------------------------------------------
//
// <copyright file="Section7ListPage.xaml.cs" company="Microsoft">
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
    public sealed partial class Section7ListPage : Page
    {
	    public ListViewModel ViewModel { get; set; }
        public Section7ListPage()
        {
			ViewModel = ViewModelFactory.NewList(new Section7Section());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
            Microsoft.AppCenter.Analytics.Analytics.TrackEvent(this.GetType().FullName);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
			ShellPage.Current.ShellControl.SelectItem("d9cd81b6-ae15-44f8-8d32-5d7594a5a01b");
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
