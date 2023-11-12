namespace EverydayLove2
{
	public class SettingsViewModel : ViewModelBase
    {
        private BackgroundImage _home;
        private BackgroundImage _saved;

        public SettingsViewModel()
        {
            BackgroundImages = new List<BackgroundImage>
            {
                new("background.jpg"),
                new("background2.jpg"),
                new("background3.jpg"),
                new("background4.jpg")
            };

            _home = BackgroundImages.Single(bi => bi.Path == Settings.HomeBackground);
            _saved = BackgroundImages.Single(bi => bi.Path == Settings.SavedBackground);
        }

        public BackgroundImage HomeBackground
        {
            get => _home;
            set
            {
                if (_home == value)
                    return;

                _home = value;

                Settings.HomeBackground = value.Path;
                Settings.UpdateSettings();

                OnPropertyChanged();
            }
        }

        public BackgroundImage SavedBackground
        {
            get => _saved;
            set
            {
                if (_saved == value)
                    return;

                _saved = value;

                Settings.SavedBackground = value.Path;
                Settings.UpdateSettings();

                OnPropertyChanged();
            }
        }

        public List<BackgroundImage> BackgroundImages { get; }
    }

    public class BackgroundImage
    {
        public BackgroundImage(string path)
        {
            Path = path;
        }

        public string Path { get; set; }

        public override bool Equals(object obj)
        {
            return obj is BackgroundImage b && b.Path == Path;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

