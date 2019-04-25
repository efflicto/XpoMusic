﻿using ModernSpotifyUWP.Classes;
using ModernSpotifyUWP.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace ModernSpotifyUWP.Controls
{
    public sealed partial class ProxyConfiguration : UserControl
    {
        public ProxyConfiguration()
        {
            this.InitializeComponent();
        }

        private void PortTextBox_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // Save config
            LocalConfiguration.IsCustomProxyEnabled = true;
            LocalConfiguration.CustomProxyType = ViewModel.ProxyType;
            LocalConfiguration.CustomProxyAddress = ViewModel.ProxyAddress;
            LocalConfiguration.CustomProxyPort = ViewModel.ProxyPort;

            ContentDialog cd = new ContentDialog
            {
                Title = "Restart Xpotify",
                Content = "We need to restart Xpotify in order to apply proxy settings.",
                RequestedTheme = ElementTheme.Dark,
                IsPrimaryButtonEnabled = true,
                PrimaryButtonText = "Restart Now",
                IsSecondaryButtonEnabled = true,
                SecondaryButtonText = "Restart Later",
            };
            cd.PrimaryButtonClick += (s, args) =>
            {
                PackageHelper.RestartApp();
            };

            await cd.ShowAsync();
        }

        private void RestartApp_Click(object sender, RoutedEventArgs e)
        {
            PackageHelper.RestartApp();
        }
    }
}
