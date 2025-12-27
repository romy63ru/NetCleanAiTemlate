using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Application.Services;
using Ui.Avalonia.Services;
using Ui.Avalonia.ViewModels;

namespace Ui.Avalonia;

public partial class App : global::Avalonia.Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var repo = new InMemorySampleRepository();
            var service = new SampleService(repo);
            var vm = new SampleEditorViewModel(service)
            {
                Id = Guid.NewGuid()
            };
            desktop.MainWindow = new MainWindow
            {
                DataContext = vm
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}