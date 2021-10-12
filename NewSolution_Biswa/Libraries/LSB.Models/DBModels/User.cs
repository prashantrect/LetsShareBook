using System;
using System.Collections.Generic;
using System.Text;

namespace LSB.Models
{
    public class User
    {
        public string UserId { get; set; }
        public Profile UserProfile { get; set; }

        public List<Item> Postings { get; set; }

        public List<Item> ExpressedInterests { get; set; }

        public List<Item> ReceivedInterests { get; set; }

        public List<Item> Received { get; set; }

    }
}
