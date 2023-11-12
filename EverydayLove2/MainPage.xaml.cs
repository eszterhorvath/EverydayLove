namespace EverydayLove2;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel vm)
	{
		BindingContext = vm;

		InitializeComponent();
	}

    void OnHeartTapped(object sender, TappedEventArgs e)
    {
		if (BindingContext is MainViewModel vm)
		{
			vm.Liked = !vm.Liked;
		}
    }
}


