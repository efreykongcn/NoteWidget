// Copyright (c) Efrey Kong. All Rights Reserved.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Web.WebView2.Core;
using NLog;

namespace NoteWidgetAddIn.RibbonCommand.Markdown
{
    /// <summary>
    /// 
    /// </summary>
    public partial class WebBrowserWindow : Window
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public WebBrowserWindow()
        {
            InitializeComponent();
            Loaded += PreviewWindow_Loaded;
            Closed += WebBrowserWindow_Closed;
            webBrowser.NavigationStarting += WebBrowser_NavigationStarting;
            webBrowser.NavigationCompleted += WebBrowser_NavigationCompleted;
        }

        private void WebBrowserWindow_Closed(object sender, EventArgs e)
        {
            SaveWindowPropertiesIfRequired();
        }

        private async void PreviewWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await InitBrowser();
            RefreshBrowser();
        }
        private void WebBrowser_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            var url = e.Uri?.ToString().ToLower();
            if (url != null && (url.StartsWith("http://") || url.StartsWith("https://")))
            {
                ProcessStartInfo psi = new ProcessStartInfo();

                psi.FileName = url;
                psi.UseShellExecute = true;

                try
                {
                    Process.Start(psi);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    _logger.Error(ex);
                }
                finally
                {
                    e.Cancel = true;
                }
            }
        }
        private void WebBrowser_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            Title = webBrowser.CoreWebView2.DocumentTitle;
        }
        
        public bool BrowserInitialized { get; private set; }
        /// <summary>
        /// Browser will navigate to html content if not empty
        /// </summary>
        public string BrowserHtmlContent { get; set; }
        /// <summary>
        /// Browser will Navigate to url if HtmlContent is empty
        /// </summary>
        public Uri Url { get; set; }
        
        private async Task InitBrowser()
        {
            try
            {
                var tempDir = Path.Combine(Path.GetTempPath(), Assembly.GetExecutingAssembly().GetName().Name);
                CoreWebView2EnvironmentOptions options = null;
                var webView2Environment = await CoreWebView2Environment.CreateAsync(null, tempDir, options);

                await webBrowser.EnsureCoreWebView2Async(webView2Environment);
                webBrowser.CoreWebView2.SetVirtualHostNameToFolderMapping(PathHelper.MappedVirtualHostName, PathHelper.GetWidgetRootPath(), CoreWebView2HostResourceAccessKind.Allow);
                BrowserInitialized = true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }
        public async void RefreshBrowser()
        {
            try
            {
                if (!string.IsNullOrEmpty(BrowserHtmlContent))
                {
                    webBrowser.NavigateToString(BrowserHtmlContent);
                }
                else if (Url != null)
                {
                    webBrowser.CoreWebView2.Navigate(Url.AbsoluteUri);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            await Task.Yield();
        }

        #region Remember me
        private string _settingPropertyIdentifier;
        /// <summary>
        /// Remember window's position, size, browser's zooom factor etc.
        /// </summary>

        public string RememberMeIdentifier 
        { 
            get
            {
                return _settingPropertyIdentifier;
            }
            set
            {
                _settingPropertyIdentifier = value;
                RestoreWindowPropertiesIfRequired();
            }
        }
        private void RestoreWindowPropertiesIfRequired()
        {
            if (!string.IsNullOrEmpty(RememberMeIdentifier))
            {
                Width = GetPropertyValue("Width") ?? Width;
                Height = GetPropertyValue("Height") ?? Height;
                Left = GetPropertyValue("Left") ?? Left;
                Top = GetPropertyValue("Top") ?? Top;
                webBrowser.ZoomFactor = GetPropertyValue("ZoomFactor") ?? webBrowser.ZoomFactor;
            }
        }
        private double? GetPropertyValue(string propertyLocalName)
        {
            var propertyName = GetPropertyName(propertyLocalName);
            if (Properties.Settings.Default.Properties.OfType<SettingsProperty>().Any(p => p.Name == propertyName))
            {
                if (double.TryParse(Properties.Settings.Default.Properties[propertyName].DefaultValue?.ToString(), out var result))
                {
                    return result;
                }
            }
            return null;
        }
        private void SaveWindowPropertiesIfRequired()
        {
            if (!string.IsNullOrEmpty(RememberMeIdentifier))
            {
                var settings = Properties.Settings.Default;
                SaveProperty(settings, "Width", Width);
                SaveProperty(settings, "Height", Height);
                SaveProperty(settings, "Left", Left);
                SaveProperty(settings, "Top", Top);
                SaveProperty(settings, "ZoomFactor", webBrowser.ZoomFactor);
                settings.Save();

                var path = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
                _logger.Debug(path);
            }
        }
        private void SaveProperty(Properties.Settings settings, string propertyLocalName, object propertyValue)
        {
            var propertyName = GetPropertyName(propertyLocalName);
            if (settings.Properties.OfType<SettingsProperty>().Any(p => p.Name == propertyName))
            {
                settings.Properties[propertyName].DefaultValue = propertyValue.ToString();
            }
            else
            {
                var p = new SettingsProperty(propertyName);
                p.DefaultValue = propertyValue.ToString();
                settings.Properties.Add(p);
            }
        }
        private string GetPropertyName(string propertyLocalName)
        {
            return $"{RememberMeIdentifier}_{propertyLocalName}";
        }
        #endregion
    }
}
