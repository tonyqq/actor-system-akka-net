using Akka.Actor;
using System.Collections.Generic;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    using System;

    using MovieStreaming.Exceptions;

    public class MoviePlayCounterActor : ReceiveActor
    {
        private readonly Dictionary<string, int> _moviePlayCounts;

        public MoviePlayCounterActor()
        {
            _moviePlayCounts = new Dictionary<string, int>();

            Receive<IncrementPlayCountMessage>(mes => HandleIncrementMessage(mes));
        }

        private void HandleIncrementMessage(IncrementPlayCountMessage mes)
        {
            if (_moviePlayCounts.ContainsKey(mes.MovieTitle))
            {
                _moviePlayCounts[mes.MovieTitle]++;
            }
            else
            {
                _moviePlayCounts.Add(mes.MovieTitle, 1);
            }

            // simulated bugs
            if (_moviePlayCounts[mes.MovieTitle] > 3)
            {
                throw new SimulatedCorruptStateException();
            }

            if (mes.MovieTitle == "Terrible movie")
            {
                throw new SimulatedTerribleMovieException();
            }

            ColorConsole.WriteColorLine(
                $"MoviePlayCounterActor '{mes.MovieTitle}' has been watched {_moviePlayCounts[mes.MovieTitle]}",
                ConsoleColor.Magenta);
        }

        protected override void PreStart()
        {
            ColorConsole.WriteColorLine("MoviePlayCounterActor PreStart", ConsoleColor.Magenta);
        }

        protected override void PostStop()
        {
            ColorConsole.WriteColorLine("MoviePlayCounterActor PostStop", ConsoleColor.Magenta);
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteColorLine("MoviePlayCounterActor PreRestart because: " + reason, ConsoleColor.Magenta);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteColorLine("MoviePlayCounterActor PostRestart because: " + reason, ConsoleColor.Magenta);
            base.PostRestart(reason);
        }
    }
}