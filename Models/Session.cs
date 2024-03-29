﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastCast.Models
{
    public class Session
    {
        public Guid SessionId { get; set; }

        public string SessionCode { get; set; }

        public string SessionName { get; set; }

        public int InitiatorId { get; set; }

        // List of possible participants
        [NotMapped]
        public List<int> Participants { get; set; }

        // timer in seconds
        public int Timer { get; set; }

        // Google Form Question
        public string FormId { get; set; }

        public bool IsLive { get; set; }

        [Range(-90,90)]
        public float Latitude { get; set; }
        [Range(-180,180)]
        public float Longitude { get; set; }

        public float Radius { get; set; }
    }
}
