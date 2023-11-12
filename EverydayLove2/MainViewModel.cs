using EverydayLove2.Notes;
using EverydayLove2.PushNotification;

namespace EverydayLove2
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INotesRepository _notesRepository;
        private Note _note;
        private bool _liked;

        public MainViewModel(INotesRepository notesRepository)
        {
            _notesRepository = notesRepository;

            Task.Run(async () =>
            {
                await LoadNote();
                await NotificationService.InitializeAsync();
            });
        }

        public Note Note
        {
            get => _note;
            set
            {
                if (_note == value)
                    return;

                _note = value;
                Liked = _note.Saved;
                OnPropertyChanged();
            }
        }

        public bool Liked
        {
            get => _liked;
            set
            {
                if (_liked == value)
                    return;

                _liked = value;
                SaveOrRemoveNote();

                OnPropertyChanged();
            }
        }

        public async Task LoadNote()
        {
            Note = await _notesRepository.GetNoteAsync();
        }

        public void SaveOrRemoveNote()
        {
            Note.Saved = Liked;
            _notesRepository.SaveOrRemoveNote(Note, Liked);
        }
    }
}

