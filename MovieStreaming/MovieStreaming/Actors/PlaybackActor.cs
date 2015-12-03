using Akka.Actor;
using System;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            Context.ActorOf(Props.Create<UserCoordinationActor>(), "UserCoordinator");
            Context.ActorOf(Props.Create<PlaybackStatisticsActor>(), "PlaybackStatistics");
        }

        private static void HandlePlayMovieMessage(PlayMovieMessage message)
        {
            ColorConsole.WriteColorLine(
                $"PlayMovieMessage: {message.MovieTitle} for user: {message.UserId}",
                ConsoleColor.Green);
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