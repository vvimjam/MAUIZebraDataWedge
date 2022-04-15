using MAUIZebraSample_UI.ViewModels;

namespace MAUIZebraSample_UI;

public partial class MainView : ContentPage
{
    private readonly MainViewModel _viewModel;

    public MainView(MainViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        this.BindingContext = _viewModel;
    }

}

