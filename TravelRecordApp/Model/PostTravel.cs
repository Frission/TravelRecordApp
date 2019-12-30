using System;
using System.Collections.Generic;
using System.Text;

namespace TravelRecordApp.Model
{
    public class TravelPost
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement, SQLite.Unique]
        public int Id { get; set; }

        [SQLite.MaxLength(500)]
        public string TravelExperience { get; set; }
    }
}
