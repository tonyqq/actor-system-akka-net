using Akka.Actor;
using System;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            Console.WriteLine("Creating a PlaybackActor");

            Receive<PlayMovieMessage>(mes => HandlePlayMovieMessage(mes));
        }

        private static void HandlePlayMovieMessage(PlayMovieMessage message)
        {
            ColorConsole.WriteColorLine(
                $"PlayMovieMessage: {message.MovieTitle} for user: {message.UserId}",
                ConsoleColor.Yellow);
        }

        protected override void PreStart()
        {
            ColorConsole.WriteColorLine("PlaybackActor PreStart", ConsoleColor.Green);
        }

        protected override void PostStop()
        {
            ColorConsole.WriteColorLine("PlaybackActor PostStop", ConsoleColor.Green);
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteColorLine("PlaybackActor PreRestart because: " + reason, ConsoleColor.Green);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteColorLine("PlaybackActor PostRestart because: " + reason, ConsoleColor.Green);
            base.PostRestart(reason);
        }
    }
}