using Akka.Actor;
using MovieStreaming.Actors;
using MovieStreaming.Messages;
using System;

namespace MovieStreaming
{
    class Program
    {
        private static ActorSystem movieStreamingActorSystem;

        static void Main(string[] args)
        {
            movieStreamingActorSystem = ActorSystem.Create("movieStreamingActorSystem");
            Console.WriteLine("Actor system created");

            Props playabackActorProps = Props.Create<PlaybackActor>();

            IActorRef playbackActorRef = movieStreamingActorSystem.ActorOf(playabackActorProps, "PlaybackActor");

            playbackActorRef.Tell(new PlayMovieMessage("Akka.NET: The Movie", 42));

            Console.ReadLine();

            movieStreamingActorSystem.Shutdown();
        }
    }
}
