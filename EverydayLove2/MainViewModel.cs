using System.ComponentModel;
using System.Runtime.CompilerServices;
using EverydayLove2.Notes;
using EverydayLove2.PushNotification;

namespace EverydayLove2
{
	public class MainViewModel : INotifyPropertyChanged
    {
        private readonly INotesRepository _notesRepository;
        private Note _note;

        public MainViewModel()
        {
            _notesRepository = new NotesRepository();

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
                OnPropertyChanged();
            }
        }

        public async Task LoadNote()
        {
            Note = await _notesRepository.GetNoteAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

