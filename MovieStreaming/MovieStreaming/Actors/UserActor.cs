using System;
using Akka.Actor;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    public class UserActor : ReceiveActor
    {
        private string _currentlyWatching;
        private readonly int userId;

        public UserActor(int userId)
        {
            this.userId = userId;
            ColorConsole.WriteColorLine("Setting initial behaviour to stopped", ConsoleColor.Yellow);
            Stopped();
        }

        private void Playing()
        {
            Receive<PlayMovieMessage>(
                mes =>
                ColorConsole.WriteColorLine(
                    "Error: cannot start playing another movie before stoping existing one.",
                    ConsoleColor.Red));
            Receive<StopMovieMessage>(mes => StopPlayingCurrentMovie());

            ColorConsole.WriteColorLine($"UserActor {userId} has now become Playing", ConsoleColor.Yellow);
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(mes => StartPlayingMovie(mes.MovieTitle));
            Receive<StopMovieMessage>(
                mes => ColorConsole.WriteColorLine("Error: cannot stop if nothing is playing", ConsoleColor.Yellow));

            ColorConsole.WriteColorLine($"UserActor {userId} has now become Stopped", ConsoleColor.Yellow);
        }

        private void StopPlayingCurrentMovie()
        {
            ColorConsole.WriteColorLine($"User {userId} has stopped watching " + _currentlyWatching, ConsoleColor.Yellow);
            _currentlyWatching = null;

            Become(Stopped);
        }

        private void StartPlayingMovie(string movieTitle)
        {
            _currentlyWatching = movieTitle;
            ColorConsole.WriteColorLine($"User {userId} is currently watching " + _currentlyWatching, ConsoleColor.Yellow);

            Context.ActorSelection("/user/Playback/PlaybackStatistics/MoviePlayCounter")
                .Tell(new IncrementPlayCountMessage(movieTitle));
            Become(Playing);
        }

        protected override void PreStart()
        {
            ColorConsole.WriteColorLine($"UserActor {userId} PreStart", ConsoleColor.Yellow);
        }

        protected override void PostStop()
        {
            ColorConsole.WriteColorLine($"UserActor {userId} PostStop", ConsoleColor.Yellow);
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteColorLine($"UserActor {userId} PreRestart because: " + reason, ConsoleColor.Yellow);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteColorLine($"UserActor {userId} PostRestart because: " + reason, ConsoleColor.Yellow);
            base.PostRestart(reason);
        }
    }
}