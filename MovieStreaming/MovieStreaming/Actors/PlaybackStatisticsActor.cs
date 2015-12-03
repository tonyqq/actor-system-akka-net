using System;

using Akka.Actor;

namespace MovieStreaming.Actors
{
    public class PlaybackStatisticsActor : ReceiveActor
    {
        public PlaybackStatisticsActor()
        {
            Context.ActorOf(Props.Create<MoviePlayCounterActor>(), "MoviePlayCounter");
        }

        protected override void PreStart()
        {
            ColorConsole.WriteColorLine("PlaybackStatisticsActor PreStart", ConsoleColor.White);
        }

        protected override void PostStop()
        {
            ColorConsole.WriteColorLine("PlaybackStatisticsActor PostStop", ConsoleColor.White);
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteColorLine("PlaybackStatisticsActor PreRestart because: " + reason, ConsoleColor.White);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteColorLine("PlaybackStatisticsActor PostRestart because: " + reason, ConsoleColor.White);
            base.PostRestart(reason);
        }
    }
}