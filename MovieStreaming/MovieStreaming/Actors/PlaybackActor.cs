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
            ColorConsole.WriteYellowLine($"PlayMovieMessage: {message.MovieTitle} for user: {message.UserId}");
        }

        protected override void PreStart()
        {
            ColorConsole.WriteGreenLine("PlaybackActor PreStart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteGreenLine("PlaybackActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteGreenLine("PlaybackActor PreRestart because: " + reason);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteGreenLine("PlaybackActor PostRestart because: " + reason);
            base.PostRestart(reason);
        }
    }
}