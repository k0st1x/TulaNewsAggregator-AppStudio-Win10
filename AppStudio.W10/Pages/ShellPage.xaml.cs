using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Media.Imaging;

using AppStudio.Uwp;
using AppStudio.Uwp.Controls;
using AppStudio.Uwp.Navigation;

using AppStudio.Navigation;

namespace AppStudio.Pages
{
    public sealed partial class ShellPage : Page
    {
        public static ShellPage Current { get; private set; }

        public ShellControl ShellControl
        {
            get { return shell; }
        }

        public Frame AppFrame
        {
            get { return frame; }
        }

        public ShellPage()
        {
            InitializeComponent();

            this.DataContext = this;
            ShellPage.Current = this;

            this.SizeChanged += OnSizeChanged;
            if (SystemNavigationManager.GetForCurrentView() != null)
            {
                SystemNavigationManager.GetForCurrentView().BackRequested += ((sender, e) =>
                {
                    if (SupportFullScreen && ShellControl.IsFullScreen)
                    {
                        e.Handled = true;
                        ShellControl.ExitFullScreen();
                    }
                    else if (NavigationService.CanGoBack())
                    {
                        NavigationService.GoBack();
                        e.Handled = true;
                    }
                });
				
                NavigationService.Navigated += ((sender, e) =>
                {
                    if (NavigationService.CanGoBack())
                    {
                        SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                    }
                    else
                    {
                        SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
                    }
                });
            }
        }

		public bool SupportFullScreen { get; set; }

		#region NavigationItems
        public ObservableCollection<NavigationItem> NavigationItems
        {
            get { return (ObservableCollection<NavigationItem>)GetValue(NavigationItemsProperty); }
            set { SetValue(NavigationItemsProperty, value); }
        }

        public static readonly DependencyProperty NavigationItemsProperty = DependencyProperty.Register("NavigationItems", typeof(ObservableCollection<NavigationItem>), typeof(ShellPage), new PropertyMetadata(new ObservableCollection<NavigationItem>()));
        #endregion

		protected override void OnNavigatedTo(NavigationEventArgs e)
        {
#if DEBUG
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size { Width = 320, Height = 500 });
#endif
            NavigationService.Initialize(typeof(ShellPage), AppFrame);
			NavigationService.NavigateToPage<HomePage>(e);

            InitializeNavigationItems();

            Bootstrap.Init();
        }		        
		
		#region Navigation
        private void InitializeNavigationItems()
        {
            NavigationItems.Add(AppNavigation.NodeFromAction(
				"Home",
                "Home",
                (ni) => NavigationService.NavigateToRoot(),
                AppNavigation.IconFromSymbol(Symbol.Home)));

            var menuMySLOItems = new List<NavigationItem>();

            menuMySLOItems.Add(AppNavigation.NodeFromAction(
				"a5340184-ea6e-4264-a23f-e38f7a3ae912",
                "MySLO Лента",
				AppNavigation.ActionFromPage("MySLO1ListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/myslo_32-1.png")) }));

            menuMySLOItems.Add(AppNavigation.NodeFromAction(
				"a5340184-ea6e-4264-a23f-e38f7a3ae912",
                "MySLO Блоги",
				AppNavigation.ActionFromPage("MySLO2ListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/myslo_32-3.png")) }));

            menuMySLOItems.Add(AppNavigation.NodeFromAction(
				"a5340184-ea6e-4264-a23f-e38f7a3ae912",
                "MySLO Город",
				AppNavigation.ActionFromPage("MySLO3ListPage"),
                null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/myslo_32-2.png")) }));

            NavigationItems.Add(AppNavigation.NodeFromSubitems(
				"a5340184-ea6e-4264-a23f-e38f7a3ae912",
                "MySLO",                
                menuMySLOItems,
				AppNavigation.IconFromGlyph("\ue10c")));            

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"1c18af74-0545-4e83-ac04-9ab121ef7993",
                "Правительство ТО",                
                AppNavigation.ActionFromPage("Section1ListPage"),
				null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/ПравительствоТО-32.png")) }));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"b088b34f-0d4e-470e-91b2-70c4292e07af",
                "Тульские Новости",                
                AppNavigation.ActionFromPage("Section2ListPage"),
				null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/tulanews_50.png")) }));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"89841871-8085-4b86-af40-169b0ac768f1",
                "Молодой Коммунар",                
                AppNavigation.ActionFromPage("Section3ListPage"),
				null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/mk_50.png")) }));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"16f5894f-30f2-49ad-85e4-6e6d7f2c4898",
                "Россия 1",                
                AppNavigation.ActionFromPage("Section4ListPage"),
				null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/россия1_32.png")) }));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"ebad5931-1832-4308-bfe2-e069f97dbfa5",
                "Центр 71",                
                AppNavigation.ActionFromPage("Section5ListPage"),
				null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/центр71_32-3.png")) }));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"1a748fc4-95dd-4ebb-8c8f-377e05b8b92d",
                "Следственный Комитет",                
                AppNavigation.ActionFromPage("Section6ListPage"),
				null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/sled_com_32.png")) }));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"d9cd81b6-ae15-44f8-8d32-5d7594a5a01b",
                "ТелеТула",                
                AppNavigation.ActionFromPage("Section7ListPage"),
				null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/teletula_50-2.png")) }));

            NavigationItems.Add(AppNavigation.NodeFromAction(
				"36ce0555-3281-4038-9541-974782d43adf",
                "Первый Тульский",                
                AppNavigation.ActionFromPage("Section8ListPage"),
				null, new Image() { Source = new BitmapImage(new Uri("ms-appx:///Assets/DataImages/первый_тульский_32.png")) }));

            NavigationItems.Add(NavigationItem.Separator);

            NavigationItems.Add(AppNavigation.NodeFromControl(
				"About",
                "NavigationPaneAbout".StringResource(),
                new AboutPage(),
                AppNavigation.IconFromImage(new Uri("ms-appx:///Assets/about.png"))));
        }        
        #endregion        

		private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateDisplayMode(e.NewSize.Width);
        }

        private void UpdateDisplayMode(double? width = null)
        {
            if (width == null)
            {
                width = Window.Current.Bounds.Width;
            }
            this.ShellControl.DisplayMode = width > 640 ? SplitViewDisplayMode.CompactOverlay : SplitViewDisplayMode.Overlay;
            this.ShellControl.CommandBarVerticalAlignment = width > 640 ? VerticalAlignment.Top : VerticalAlignment.Bottom;
        }

        private async void OnKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.F11)
            {
                if (SupportFullScreen)
                {
                    await ShellControl.TryEnterFullScreenAsync();
                }
            }
            else if (e.Key == Windows.System.VirtualKey.Escape)
            {
                if (SupportFullScreen && ShellControl.IsFullScreen)
                {
                    ShellControl.ExitFullScreen();
                }
                else
                {
                    NavigationService.GoBack();
                }
            }
        }
    }
}
