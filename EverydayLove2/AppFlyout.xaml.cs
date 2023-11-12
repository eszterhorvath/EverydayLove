namespace EverydayLove2;

public partial class AppFlyout : FlyoutPage
{
    public AppFlyout(MainPage page)
    {
        InitializeComponent();
        this.Detail = new NavigationPage(page);

        flyoutPage.collectionView.SelectionChanged += OnSelectionChanged;
    }

    void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is FlyoutPageItem item)
        {
            var page = Handler.MauiContext.Services.GetServices(item.TargetType).First();
            if (page is SavedNotesPage snp)
            {
                Detail = new NavigationPage(snp);
            }
            else if (page is MainPage mp)
            {
                Detail = new NavigationPage(mp);
            }
            else if (page is SettingsPage sp)
            {
                Detail = new NavigationPage(sp);
            }
            IsPresented = false;
        }
    }
}
