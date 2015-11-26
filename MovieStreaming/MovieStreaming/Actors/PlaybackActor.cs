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

            Receive<PlayMovieMessage>(HandlePlayMovieMessage, message => message.UserId == 42);
        }

        private void HandlePlayMovieMessage(PlayMovieMessage message)
        {
            Console.WriteLine("Received movie title: " + message.MovieTitle);
            Console.WriteLine("Received user Id:  " + message.UserId);
        }
    }
}