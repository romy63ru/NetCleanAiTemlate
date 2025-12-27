using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ui.Avalonia.ViewModels;

public sealed class AsyncCommand : ICommand
{
    private readonly Func<CancellationToken, Task> _execute;
    private readonly Func<bool>? _canExecute;
    private CancellationTokenSource _cts = new();

    public AsyncCommand(Func<CancellationToken, Task> execute, Func<bool>? canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true;
    public event EventHandler? CanExecuteChanged;
    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

    public async void Execute(object? parameter)
    {
        var prev = _cts;
        _cts = new CancellationTokenSource();
        prev.Cancel();
        try
        {
            await _execute(_cts.Token).ConfigureAwait(false);
        }
        catch
        {
            // swallow to avoid crashing UI; log in API/Infra per rules
        }
    }

    public Task ExecuteAsync() => _execute(_cts.Token);
}
