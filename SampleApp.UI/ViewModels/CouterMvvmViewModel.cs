using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SampleApp.UI.Models;

namespace SampleApp.UI.ViewModels
{
    public partial class CouterMvvmViewModel : ObservableObject
    {
        private readonly CountModel _model;

        public CouterMvvmViewModel()
        {
            _model = new CountModel();
            _count = _model.Value;
        }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(DecrementCommand))]
        private int _count;

        [RelayCommand]
        private void Increment()
        {
            _model.Increment();
            Count = _model.Value;
        }

        private bool CanDecrement() => Count > 0;

        [RelayCommand(CanExecute = nameof(CanDecrement))]
        private void Decrement()
        {
            _model.Decrement();
            Count = _model.Value;
        }
    }
}
