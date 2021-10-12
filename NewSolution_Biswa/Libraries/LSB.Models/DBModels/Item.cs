using System;
using System.Collections.Generic;

namespace LSB.Models
{
    public class Item
    {
        public string Title { get; set; }
        public string Summary { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }

        public AgeGroup AgeGroup { get; set; }

        public string Language { get; set; }

        public Condition Condition { get; set; }

        public string Picture { get; set; }

        public DateTime PostedDate { get; set; }

        public string OwnerId { get; set; }

        public List<string> Reviews { get; set; }

    }
}