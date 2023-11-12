using Newtonsoft.Json;
using SQLite;

namespace EverydayLove2.Notes
{

    public interface INotesRepository
    {
        Task Initialize();
        Task<Note> GetNoteAsync();
        void SaveOrRemoveNote(Note note, bool saved);
        List<Note> GetSavedNotes();
    }

    public class NotesRepository : INotesRepository
    {
        private SQLiteConnection _database;
        private Note _currentNote;

        public async Task Initialize()
        {
            if (_database != null) return;

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, Constants.DatabaseFilename);
            _database = new SQLiteConnection(dbPath, Constants.Flags);
            var tableExist = false;
            try
            {
                var n = _database.Table<Note>().ToList();
                if (n.Count != 0)
                {
                    tableExist = true;
                }
            }
            catch (Exception) { }
            if (!tableExist)
            {
                // create table
                _database.CreateTable<Note>(CreateFlags.AutoIncPK);
                // collect notes
                using var stream = await FileSystem.OpenAppPackageFileAsync("notes.json");
                using var reader = new StreamReader(stream);
                var s = await reader.ReadToEndAsync();
                var notes = JsonConvert.DeserializeObject<List<Note>>(s);
                // add notes to table
                _database.InsertAll(notes);
            }
        }

        public async Task<Note> GetNoteAsync()
        {
            await Initialize();
            if (await ShouldShowNewNote()) // get random new note
            {
                // 1. get all notes
                var all = await GetAllNotes();
                // 2. find smallest count
                var counts = all.Select(n => n.ShownCounter).ToList();
                counts.Sort();
                var smallestCount = counts[0];
                // 3. find possible options to show
                // 3.1. check if there is any note for today
                var possibleNotes = all.Where(n => n.DayToShow == DateTime.Today).ToList();
                if (!possibleNotes.Any())
                {
                    // 3.2 if no specific notes for today, find the ones with smallest count
                    possibleNotes = all.Where(n => n.ShownCounter == smallestCount && string.IsNullOrEmpty(n.Date)).ToList();
                }
                // 4. select random
                var random = new Random();
                var index = random.Next(possibleNotes.Count);
                var note = possibleNotes[index];
                // 5. update its LastShown and ShownCounter properties
                note.LastShown = DateTime.Now;
                note.ShownCounter++;
                // 6. update db
                _database.Update(note, typeof(Note));
                _currentNote = note;
                return note;
            }
            else
            {
                if (_currentNote == null)
                    _currentNote = await GetLastShownNote();

                return _currentNote;
            }
        }

        internal async Task<List<Note>> GetAllNotes()
        {
            await Initialize();
            var table = _database.Table<Note>();
            return table.ToList();
        }

        internal async Task<Note> GetLastShownNote()
        {
            await Initialize();
            var all = await GetAllNotes();
            all.Sort((n1, n2) =>
            {
                if (n1.LastShown == null && n2.LastShown != null)
                {
                    return -1;
                }
                if (n1.LastShown != null && n2.LastShown == null)
                {
                    return 1;
                }
                if (n1.LastShown == null && n2.LastShown == null)
                {
                    return 0;
                }
                return DateTime.Compare(n1.LastShown!.Value, n2.LastShown!.Value);

            });
            all.Reverse();
            return all[0];
        }

        internal async Task<bool> ShouldShowNewNote()
        {
            await Initialize();
            var lastShownNote = await GetLastShownNote();
            var lastShown = lastShownNote.LastShown;
            return lastShown == null || lastShown.Value.Date != DateTime.Today;
        }

        public void SaveOrRemoveNote(Note note, bool saved)
        {
            note.Saved = saved;
            _database.Update(_currentNote, typeof(Note));
        }

        public List<Note> GetSavedNotes()
        {
            return _database.Table<Note>().Where(n => n.Saved).ToList();
        }
    }
}

