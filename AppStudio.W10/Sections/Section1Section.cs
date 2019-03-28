using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.Facebook;
using AppStudio.Uwp;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Commands;

using AppStudio.Navigation;
using AppStudio.ViewModels;

namespace AppStudio.Sections
{
    public class Section1Section : Section<FacebookSchema>
    {
		private FacebookDataProvider _dataProvider;

		public Section1Section()
		{
			_dataProvider = new FacebookDataProvider(new FacebookOAuthTokens
			{
				AppId = "1158179577528273",
                AppSecret = Vault.Facebook
			});
		}

		public override async Task<IEnumerable<FacebookSchema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new FacebookDataConfig
            {
                UserId = "222343777914236"
            };
            return await _dataProvider.LoadDataAsync(config, MaxRecords);
        }

        public override async Task<IEnumerable<FacebookSchema>> GetNextPageAsync()
        {
            return await _dataProvider.LoadMoreDataAsync();
        }

        public override bool HasMorePages
        {
            get
            {
                return _dataProvider.HasMoreItems;
            }
        }

        public override ListPageConfig<FacebookSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<FacebookSchema>
                {
                    Title = "Правительство ТО",

                    Page = typeof(Pages.Section1ListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.PublishDate.ToString(DateTimeFormat.FullLongDate);
                        viewModel.SubTitle = item.Summary.ToSafeString();
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
						return NavInfo.FromPage<Pages.Section1DetailPage>(true);
                    }
                };
            }
        }

        public override DetailPageConfig<FacebookSchema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, FacebookSchema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = item.Author.ToSafeString();
                    viewModel.Title = item.PublishDate.ToString(DateTimeFormat.ShortDate);
                    viewModel.Description = item.Content.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    viewModel.Content = null;
					viewModel.Source = item.FeedUrl;
                });

                var actions = new List<ActionConfig<FacebookSchema>>
                {
                    ActionConfig<FacebookSchema>.Link("Go To Source", (item) => item.FeedUrl.ToSafeString()),
                };

                return new DetailPageConfig<FacebookSchema>
                {
                    Title = "Правительство ТО",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
