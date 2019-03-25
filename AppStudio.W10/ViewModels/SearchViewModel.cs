using System;
using System.Collections.Generic;
using AppStudio.Uwp;
using AppStudio.Uwp.Commands;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AppStudio.Sections;
namespace AppStudio.ViewModels
{
    public class SearchViewModel : PageViewModelBase
    {
        public SearchViewModel() : base()
        {
			Title = "Агрегатор новостей Тулы";
            MySLO1 = ViewModelFactory.NewList(new MySLO1Section());
            MySLO2 = ViewModelFactory.NewList(new MySLO2Section());
            MySLO3 = ViewModelFactory.NewList(new MySLO3Section());
            Section1 = ViewModelFactory.NewList(new Section1Section());
            Section2 = ViewModelFactory.NewList(new Section2Section());
            Section3 = ViewModelFactory.NewList(new Section3Section());
            Section4 = ViewModelFactory.NewList(new Section4Section());
            Section5 = ViewModelFactory.NewList(new Section5Section());
            Section6 = ViewModelFactory.NewList(new Section6Section());
            Section7 = ViewModelFactory.NewList(new Section7Section());
            Section8 = ViewModelFactory.NewList(new Section8Section());
                        
        }

        private string _searchText;
        private bool _hasItems = true;

        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        public bool HasItems
        {
            get { return _hasItems; }
            set { SetProperty(ref _hasItems, value); }
        }

		public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand<string>(
                async (text) =>
                {
                    await SearchDataAsync(text);
                }, SearchViewModel.CanSearch);
            }
        }      
        public ListViewModel MySLO1 { get; private set; }
        public ListViewModel MySLO2 { get; private set; }
        public ListViewModel MySLO3 { get; private set; }
        public ListViewModel Section1 { get; private set; }
        public ListViewModel Section2 { get; private set; }
        public ListViewModel Section3 { get; private set; }
        public ListViewModel Section4 { get; private set; }
        public ListViewModel Section5 { get; private set; }
        public ListViewModel Section6 { get; private set; }
        public ListViewModel Section7 { get; private set; }
        public ListViewModel Section8 { get; private set; }
        public async Task SearchDataAsync(string text)
        {
            this.HasItems = true;
            SearchText = text;
            var loadDataTasks = GetViewModels()
                                    .Select(vm => vm.SearchDataAsync(text));

            await Task.WhenAll(loadDataTasks);
			this.HasItems = GetViewModels().Any(vm => vm.HasItems);
        }

        private IEnumerable<ListViewModel> GetViewModels()
        {
            yield return MySLO1;
            yield return MySLO2;
            yield return MySLO3;
            yield return Section1;
            yield return Section2;
            yield return Section3;
            yield return Section4;
            yield return Section5;
            yield return Section6;
            yield return Section7;
            yield return Section8;
        }
		private void CleanItems()
        {
            foreach (var vm in GetViewModels())
            {
                vm.CleanItems();
            }
        }
		public static bool CanSearch(string text) { return !string.IsNullOrWhiteSpace(text) && text.Length >= 3; }
    }
}
