using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.YouTube;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Commands;
using AppStudio.Uwp;
using System.Linq;

using AppStudio.Navigation;
using AppStudio.ViewModels;

namespace AppStudio.Sections
{
    public class Section7Section : Section<YouTubeSchema>
    {
		private YouTubeDataProvider _dataProvider;
		
		public Section7Section()
		{
			_dataProvider = new YouTubeDataProvider(new YouTubeOAuthTokens
			{
				ApiKey = Vault.YouTube
			});
		}

		public override async Task<IEnumerable<YouTubeSchema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new YouTubeDataConfig
            {
                QueryType = YouTubeQueryType.Channels,
                Query = @"teletula71",
            };
            return await _dataProvider.LoadDataAsync(config, MaxRecords);
        }

        public override async Task<IEnumerable<YouTubeSchema>> GetNextPageAsync()
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

        public override ListPageConfig<YouTubeSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<YouTubeSchema>
                {
                    Title = "ТелеТула",

                    Page = typeof(Pages.Section7ListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
						viewModel.Header = item.Title.ToSafeString();
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = item.Summary.ToSafeString();
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());

						viewModel.GroupBy = item.Title.SafeType();

						viewModel.OrderBy = item.Title;
                    },
					OrderType = OrderType.Ascending,
                    DetailNavigation = (item) =>
                    {
						return NavInfo.FromPage<Pages.Section7DetailPage>(true);
                    }
                };
            }
        }

        public override DetailPageConfig<YouTubeSchema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, YouTubeSchema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = item.Title.ToSafeString();
                    viewModel.Title = null;
                    viewModel.Description = item.Summary.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(null);
                    viewModel.Content = item.EmbedHtmlFragment;
					viewModel.Source = item.ExternalUrl;
                });

                var actions = new List<ActionConfig<YouTubeSchema>>
                {
                    ActionConfig<YouTubeSchema>.Link("Go To Source", (item) => item.ExternalUrl.ToSafeString()),
                };

                return new DetailPageConfig<YouTubeSchema>
                {
                    Title = "ТелеТула",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
