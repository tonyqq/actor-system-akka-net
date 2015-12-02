using System;
using Akka.Actor;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    public class UserActor : ReceiveActor
    {
        private string _currentlyWatching;

        public UserActor()
        {
            Console.WriteLine("Creating a UserActor");

            ColorConsole.WriteCyanLine("Setting initial behaviour to stopped");
            Stopped();
        }

        private void Playing()
        {
            Receive<PlayMovieMessage>(
                mes =>
                ColorConsole.WriteRedLine("Error: cannot start playing another movie before stoping existing one."));
            Receive<StopMovieMessage>(mes => StopPlayingCurrentMovie());

            ColorConsole.WriteCyanLine("UserActor has now become Playing");
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(mes => StartPlayingMovie(mes.MovieTitle));
            Receive<StopMovieMessage>(mes => ColorConsole.WriteRedLine("Error: cannot stop if nothing is playing"));

            ColorConsole.WriteCyanLine("UserActor has now become Stopped");
        }

        private void StopPlayingCurrentMovie()
        {
            ColorConsole.WriteYellowLine("User has stopped watching " + _currentlyWatching);
            _currentlyWatching = null;

            Become(Stopped);
        }

        private void StartPlayingMovie(string movieTitle)
        {
            _currentlyWatching = movieTitle;
            ColorConsole.WriteYellowLine("User is currently watching " + _currentlyWatching);

            Become(Playing);
        }

        protected override void PreStart()
        {
            ColorConsole.WriteGreenLine("UserActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteGreenLine("UserActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteGreenLine("UserActor PreRestart because: " + reason);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteGreenLine("UserActor PostRestart because: " + reason);
            base.PostRestart(reason);
        }
    }
}