using System;

namespace EverydayLove2
{
    public static class Constants
    {
        public const string DatabaseFilename = "EverydayLoveDB.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create;
            // enable multi-threaded database access
            //SQLite.SQLiteOpenFlags.SharedCache;
    }
}

