//---------------------------------------------------------------------------
//
// <copyright file="Bootstrap.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>8/30/2016 2:02:12 PM</createdOn>
//
//---------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store;
using Windows.Storage;
using Windows.UI.Notifications;

using AppStudio.Uwp;
using AppStudio.Uwp.Controls;

using AppStudio.Services;

namespace AppStudio
{
    static class Bootstrap
    {
        private static readonly Guid APP_ID = new Guid("f0e0dfae-fcf7-4827-b2b7-f87cf37b2734");

		public static void Init()
        {
			InitializeTelemetry();
			InitializeTiles();

			BitmapCache.ClearCacheAsync(TimeSpan.FromHours(48)).FireAndForget();
		}

		private static void InitializeTiles()
        {
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily.ToLower() != "windows.iot")
            {
                var init = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.TilesInitialized];
                if (init == null || (init is bool && !(bool)init))
                {
                    var tileUpdater = TileUpdateManager.CreateTileUpdaterForApplication();
                    tileUpdater.EnableNotificationQueue(true);
                    ApplicationData.Current.LocalSettings.Values[LocalSettingNames.TilesInitialized] = true;
                }
            }
        }

		private static void InitializeTelemetry()
        {
            var init = !string.IsNullOrEmpty(ApplicationData.Current.LocalSettings.Values[LocalSettingNames.DeviceType] as string);
            if (!init)
            {
                string deviceType = IsOnPhoneExecution() ? LocalSettingNames.PhoneValue : LocalSettingNames.WindowsValue;
                ApplicationData.Current.LocalSettings.Values[LocalSettingNames.DeviceType] = deviceType;
                ApplicationData.Current.LocalSettings.Values[LocalSettingNames.StoreId] = ValidateStoreId();
            }
        }

        private static bool IsOnPhoneExecution()
        {
            var qualifiers = Windows.ApplicationModel.Resources.Core.ResourceContext.GetForCurrentView().QualifierValues;
            return (qualifiers.ContainsKey("DeviceFamily") && qualifiers["DeviceFamily"] == "Mobile");
        }

        private static Guid ValidateStoreId()
        {
            try
            {
                Guid storeId = CurrentApp.AppId;
                if (storeId != Guid.Empty && storeId != APP_ID)
                {
                    return storeId;
                }

                return Guid.Empty;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }
    }
}