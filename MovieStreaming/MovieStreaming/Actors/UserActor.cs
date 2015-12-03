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

            ColorConsole.WriteColorLine("Setting initial behaviour to stopped", ConsoleColor.Cyan);
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

            ColorConsole.WriteColorLine("UserActor has now become Playing", ConsoleColor.Cyan);
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(mes => StartPlayingMovie(mes.MovieTitle));
            Receive<StopMovieMessage>(
                mes => ColorConsole.WriteColorLine("Error: cannot stop if nothing is playing", ConsoleColor.Red));

            ColorConsole.WriteColorLine("UserActor has now become Stopped", ConsoleColor.Cyan);
        }

        private void StopPlayingCurrentMovie()
        {
            ColorConsole.WriteColorLine("User has stopped watching " + _currentlyWatching, ConsoleColor.Yellow);
            _currentlyWatching = null;

            Become(Stopped);
        }

        private void StartPlayingMovie(string movieTitle)
        {
            _currentlyWatching = movieTitle;
            ColorConsole.WriteColorLine("User is currently watching " + _currentlyWatching, ConsoleColor.Yellow);

            Become(Playing);
        }

        protected override void PreStart()
        {
            ColorConsole.WriteColorLine("UserActor PreStart", ConsoleColor.Green);
        }

        protected override void PostStop()
        {
            ColorConsole.WriteColorLine("UserActor PostStop", ConsoleColor.Green);
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteColorLine("UserActor PreRestart because: " + reason, ConsoleColor.Green);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteColorLine("UserActor PostRestart because: " + reason, ConsoleColor.Green);
            base.PostRestart(reason);
        }
    }
}