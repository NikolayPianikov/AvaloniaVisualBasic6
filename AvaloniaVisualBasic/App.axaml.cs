using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Fonts;
using AvaloniaVisualBasic.Runtime.Interpreter;
using AvaloniaVisualBasic.VisualDesigner;
using Classic.Avalonia.Theme;
using Classic.CommonControls.Dialogs;
using CommunityToolkit.Mvvm.ComponentModel;
using R3;

namespace AvaloniaVisualBasic;

public partial class App : Application
{
    public override void Initialize()
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            BindingPlugins.DataValidators.RemoveAt(0);

            if (Static.ForceSingleView)
            {
                Static.SingleView = true;
                Static.MainView = new MainView();
                desktop.MainWindow = new ClassicWindow
                {
                    Content = Static.MainView
                };

                if (desktop.MainWindow.DataContext is MainViewViewModel rootViewModel)
                {
                    rootViewModel.ObservePropertyChanged(x => x.Title)
                        .Subscribe(title => desktop.MainWindow.Title = title);
                }
#if DEBUG
                desktop.MainWindow.AttachDevTools();
#endif

                Static.MainView.WindowInitialized();
            }
            else
            {
                var mainWindow = new MainWindow();
                desktop.MainWindow = mainWindow;
                Static.SingleView = false;
                Static.MainView = mainWindow.MainView;
            }
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            Static.SingleView = true;
            singleViewPlatform.MainView = Static.MainView = new MainView();
            Static.MainView.WindowInitialized();
        }

        base.OnFrameworkInitializationCompleted();
    }
}