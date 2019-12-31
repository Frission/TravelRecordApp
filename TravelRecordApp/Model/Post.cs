using System;
using System.Collections.Generic;
using System.Text;

namespace TravelRecordApp.Model
{
    public class Post
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement, SQLite.Unique]
        public int Id { get; set; }

        [SQLite.MaxLength(500)]
        public string TravelExperience { get; set; }

        public string VenueName { get; set; }
        
        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int Distance { get; set; }
    }
}
