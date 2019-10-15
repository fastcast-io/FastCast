using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastCast.Models
{
    public class Session
    {
        public Guid SessionId { get; set; }

        public string SessionCode { get; set; }

        public int InitiatorId { get; set; }

        // List of possible participants
        public List<int> Participants { get; set; }

        // timer in seconds
        public int Timer { get; set; }

        // Google Form Question
        public int FormId { get; set; }
    }
}
