using System;
using System.Globalization;
using SQLite;

namespace EverydayLove2.Notes
{
    [Table("Notes")]
    public class Note
    {
        private string _date;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Sender { get; set; }

        public string NoteText { get; set; }

        public string? ImagePath { get; set; }

        public DateTime? DayToShow { get; set; }

        public string? Date
        {
            get => _date;
            set
            {
                if (_date == value) return;
                _date = value;

                if (DateTime.TryParseExact(_date, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                        out var dayToShow))
                {
                    DayToShow = dayToShow;
                }
            }
        }

        [Ignore]
        public bool HasImage => !string.IsNullOrEmpty(ImagePath);

        public int ShownCounter { get; set; }

        public DateTime? LastShown { get; set; }
    }
}

