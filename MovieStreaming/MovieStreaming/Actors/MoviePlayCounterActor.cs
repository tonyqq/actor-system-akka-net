using Akka.Actor;
using System.Collections.Generic;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    using System;

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

            ColorConsole.WriteColorLine(
                $"MoviePlayCounterActor '{mes.MovieTitle}' has been watched {_moviePlayCounts[mes.MovieTitle]}",
                ConsoleColor.Magenta);
        }
    }
}