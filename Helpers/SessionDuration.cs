using Microsoft.AspNetCore.SignalR;
using FastCast.Hubs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastCast
{

    public class SessionDuration : System.Timers.Timer
    {
        private static ConcurrentDictionary<string, SessionDuration> _durations = new ConcurrentDictionary<string, SessionDuration>();

        private readonly IHubContext<SessionHub> _hubContext;

        public SessionDuration(IHubContext<SessionHub> hubContext)
            : base(1000) // 1 second!
        {
            _hubContext = hubContext;
        }

        public static ConcurrentDictionary<string, SessionDuration> Durations { get => _durations; }

        public IHubContext<SessionHub> HubContext => _hubContext;

    }
}
