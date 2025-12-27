using Application.Services;
using Application.Common;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Ui.Avalonia.ViewModels;

public sealed class SampleEditorViewModel : INotifyPropertyChanged
{
    private readonly ISampleService _service;
    private Guid _id;
    private string _name = string.Empty;
    private string? _description;
    private string _status = string.Empty;

    public Guid Id { get => _id; set { _id = value; OnPropertyChanged(); OnPropertyChanged(nameof(IdString)); } }
    public string IdString
    {
        get => _id.ToString();
        set
        {
            if (Guid.TryParse(value, out var g))
            {
                _id = g;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Id));
            }
        }
    }
    public string Name { get => _name; set { _name = value; OnPropertyChanged(); Save.RaiseCanExecuteChanged(); } }
    public string? Description { get => _description; set { _description = value; OnPropertyChanged(); } }
    public string Status { get => _status; private set { _status = value; OnPropertyChanged(); } }

    public AsyncCommand Load { get; }
    public AsyncCommand Save { get; }

    public SampleEditorViewModel(ISampleService service)
    {
        _service = service;
        Load = new AsyncCommand(async ct =>
        {
            var result = await _service.GetAsync(Id, ct);
            if (!result.IsSuccess)
            {
                Status = result.Code == "not_found" ? "Not found" : "Error";
                return;
            }
            var e = result.Value!;
            Name = e.Name;
            Description = e.Description;
            Status = "Loaded";
        });

        Save = new AsyncCommand(async ct =>
        {
            // Try update existing
            var update = await _service.UpdateAsync(Id, Name, Description, ct);
            if (update.IsSuccess)
            {
                Status = "Saved";
                return;
            }

            if (update.Code == "validation")
            {
                Status = "Invalid input";
                return;
            }

            // If not found, create new
            var create = await _service.CreateAsync(Name, Description, ct);
            if (create.IsSuccess)
            {
                Id = create.Value!.Id; // Update Id to newly created entity
                Status = "Saved";
                return;
            }

            Status = "Error";
        }, canExecute: () => !string.IsNullOrWhiteSpace(Name));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
