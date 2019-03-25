using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Windows.Input;
using AppStudio.Uwp;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Navigation;
using AppStudio.Uwp.Commands;
using AppStudio.DataProviders;

using AppStudio.DataProviders.Menu;
using AppStudio.DataProviders.Facebook;
using AppStudio.DataProviders.Rss;
using AppStudio.DataProviders.YouTube;
using AppStudio.DataProviders.LocalStorage;
using AppStudio.Sections;


namespace AppStudio.ViewModels
{
    public class MainViewModel : PageViewModelBase
    {
        public ListViewModel MySLO { get; private set; }
        public ListViewModel Section1 { get; private set; }
        public ListViewModel Section2 { get; private set; }
        public ListViewModel Section3 { get; private set; }
        public ListViewModel Section4 { get; private set; }
        public ListViewModel Section5 { get; private set; }
        public ListViewModel Section6 { get; private set; }
        public ListViewModel Section7 { get; private set; }
        public ListViewModel Section8 { get; private set; }

        public MainViewModel(int visibleItems) : base()
        {
            Title = "Агрегатор новостей Тулы";
            MySLO = ViewModelFactory.NewList(new MySLOSection());
            Section1 = ViewModelFactory.NewList(new Section1Section(), visibleItems);
            Section2 = ViewModelFactory.NewList(new Section2Section(), visibleItems);
            Section3 = ViewModelFactory.NewList(new Section3Section(), visibleItems);
            Section4 = ViewModelFactory.NewList(new Section4Section(), visibleItems);
            Section5 = ViewModelFactory.NewList(new Section5Section(), visibleItems);
            Section6 = ViewModelFactory.NewList(new Section6Section(), visibleItems);
            Section7 = ViewModelFactory.NewList(new Section7Section(), visibleItems);
            Section8 = ViewModelFactory.NewList(new Section8Section(), visibleItems);

            if (GetViewModels().Any(vm => !vm.HasLocalData))
            {
                Actions.Add(new ActionInfo
                {
                    Command = RefreshCommand,
                    Style = ActionKnownStyles.Refresh,
                    Name = "RefreshButton",
                    ActionType = ActionType.Primary
                });
            }
        }

		#region Commands
		public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    var refreshDataTasks = GetViewModels()
                        .Where(vm => !vm.HasLocalData).Select(vm => vm.LoadDataAsync(true));

                    await Task.WhenAll(refreshDataTasks);
					LastUpdated = GetViewModels().OrderBy(vm => vm.LastUpdated, OrderType.Descending).FirstOrDefault()?.LastUpdated;
                    OnPropertyChanged("LastUpdated");
                });
            }
        }
		#endregion

        public async Task LoadDataAsync()
        {
            var loadDataTasks = GetViewModels().Select(vm => vm.LoadDataAsync());

            await Task.WhenAll(loadDataTasks);
			LastUpdated = GetViewModels().OrderBy(vm => vm.LastUpdated, OrderType.Descending).FirstOrDefault()?.LastUpdated;
            OnPropertyChanged("LastUpdated");
        }

        private IEnumerable<ListViewModel> GetViewModels()
        {
            yield return MySLO;
            yield return Section1;
            yield return Section2;
            yield return Section3;
            yield return Section4;
            yield return Section5;
            yield return Section6;
            yield return Section7;
            yield return Section8;
        }
    }
}
