using EverydayLove2.Notes;

namespace EverydayLove2
{
	public class SavedNotesViewModel : ViewModelBase
    {
        private readonly INotesRepository _notesRepository;

        public SavedNotesViewModel(INotesRepository notesRepository)
        {
            _notesRepository = notesRepository;
            SavedNotes = _notesRepository.GetSavedNotes();
        }

        public List<Note> SavedNotes { get; }
	}
}

