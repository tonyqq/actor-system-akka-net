using Akka.Actor;
using System;

namespace MovieStreaming
{
    using MovieStreaming.Actors;
    using MovieStreaming.Messages;

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
