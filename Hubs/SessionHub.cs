using Microsoft.AspNetCore.SignalR;
using FastCast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using System.Diagnostics;


namespace FastCast.Hubs
{
    public interface ISessionClient
    {
        Task EngageSession(string sessionCode);
        Task HaltSession(string sessionCode);
        Task ShowDurationLeft(string timeLeft);
        Task StopTimer();
        Task UserHasJoined();
        Task UserLeft();
    }

    public class SessionHub : Hub<ISessionClient>
    {
        private SessionDuration _duration;

        public SessionHub(SessionDuration sessionDuration)
        {
            _duration = sessionDuration;
        }

        public async Task StartTimer(string sessionCode, int durationInSeconds)
        {
            _duration = SessionDuration.Durations.GetOrAdd(Context.ConnectionId, _duration) as SessionDuration;
            var endTime = DateTime.Now.AddSeconds(durationInSeconds);
            _duration.Elapsed += (sender, e) => UpdateDuration(sender, e, sessionCode, endTime);
            _duration.Interval = 1000;
            _duration.Enabled = true;
        }

        static void UpdateDuration(object sender, System.Timers.ElapsedEventArgs e, string sessionCode, DateTime endTime)
        {
            var _duration = (SessionDuration)sender;
            var currentTime = DateTime.Now;
            var remainingTime = endTime - currentTime;

            _duration.HubContext.Clients.Group(sessionCode).SendAsync(
                "ShowDurationLeft",
                new
                {
                    Remaining = Math.Ceiling(remainingTime.TotalSeconds)
                }
            );
        }

        public async Task StopTimer(string sessionCode, int durationInSeconds)
        {
            _duration = SessionDuration.Durations.GetOrAdd(Context.ConnectionId, _duration) as SessionDuration;
            // TODO: Maybe I should find a way to better dispose of it
            //_duration.Elapsed -= (sender, e) => UpdateDuration(sender, e, sessionCode, DateTime.Now.AddSeconds(durationInSeconds));
            _duration.Enabled = false;

            #pragma warning disable CA2007
            await Clients.OthersInGroup(sessionCode).StopTimer();
        }

        public async Task JoinSession(string sessionCode)
        {
            Debug.WriteLine("Someone joined a session!");
            // Need to implement logic to check if session has started // Can check that on the front end....
            await Groups.AddToGroupAsync(Context.ConnectionId, sessionCode);
            await Clients.Group(sessionCode).UserHasJoined();
        }

        public async Task ExitSession(string sessionCode)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, sessionCode);
            await Clients.Group(sessionCode).UserLeft();
        }

        public async Task StartSession(string sessionCode)
        {
            #pragma warning disable CA2007
            await Clients.All.EngageSession(sessionCode);
        }

        public async Task StopSession(string sessionCode)
        {
            #pragma warning disable CA2007
            await Clients.All.HaltSession(sessionCode);
        }
    }
}
