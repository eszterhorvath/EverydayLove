namespace EverydayLove2;

public partial class App : Application
{
	public App(MainPage mainPage)
	{
		InitializeComponent();

        MainPage = new AppFlyout(mainPage);
    }
}

