namespace EverydayLove2;

public partial class SettingsPage : ContentPage
{
    public SettingsPage(SettingsViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }
}
