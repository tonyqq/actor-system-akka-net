using Akka.Actor;

namespace MovieStreaming.Actors
{
    using System;

    using MovieStreaming.Messages;

    public class PlaybackActor : UntypedActor
    {
        public PlaybackActor()
        {
            Console.WriteLine("Creating a PlaybackActor");
        }
        protected override void OnReceive(object message)
        {
            var movieMessage = message as PlayMovieMessage;
            if (movieMessage != null)
            {
                var mes = movieMessage;
                Console.WriteLine("Received movie title: " + mes.MovieTitle);
                Console.WriteLine("Received user Id:  " + mes.UserId);
            }
            else
            {
                Unhandled(message);
            }
        }
    }
}