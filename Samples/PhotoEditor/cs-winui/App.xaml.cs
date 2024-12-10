// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using PhotoEditor.Services;
using PhotoEditor.ViewModels;
using System;
using Microsoft.Extensions.DependencyInjection;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace PhotoEditor
{
    public partial class App : Application
    {
        public IServiceProvider Services { get; }

        public App()
        {
            Services = ConfigureServices();
            this.InitializeComponent();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Register ViewModels
            services.AddTransient<MainPageViewModel>();
            services.AddTransient<DetailPageViewModel>();
            services.AddTransient<ImageFileViewModel>();

            // Register other services
            services.AddSingleton<ImageService>();

            return services.BuildServiceProvider();
        }
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            Window = new MainWindow();
            Window.Activate();
            WindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(Window);
        }
        public static MainWindow Window { get; private set; }
        public static IntPtr WindowHandle { get; private set; }
    }
}