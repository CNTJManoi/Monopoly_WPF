namespace Monopoly.ViewModel;

public class MainWindowViewModel : BasicViewModel
{
    private BasicViewModel _viewModel;

    public MainWindowViewModel()
    {
        var mw = new MenuViewModel(this);
        CurrentContentVM = mw;
    }

    public BasicViewModel CurrentContentVM
    {
        get => _viewModel;
        set
        {
            if (_viewModel != value)
            {
                _viewModel = value;
                RaisePropertyChanged("CurrentContentVM");
            }
        }
    }

    public void GoToViewModel(BasicViewModel bvm)
    {
        CurrentContentVM = bvm;
    }
}