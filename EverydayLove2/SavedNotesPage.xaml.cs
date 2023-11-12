namespace EverydayLove2;

public partial class SavedNotesPage : ContentPage
{
	public SavedNotesPage(SavedNotesViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}
