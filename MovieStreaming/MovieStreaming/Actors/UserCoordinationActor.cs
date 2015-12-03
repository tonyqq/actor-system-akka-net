using System;
using System.Collections.Generic;

using Akka.Actor;

using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    public class UserCoordinationActor: ReceiveActor
    {
        private readonly Dictionary<int, IActorRef> _users;

        public UserCoordinationActor()
        {
            _users = new Dictionary<int, IActorRef>();

            Receive<PlayMovieMessage>(
                mes =>
                    {
                        CreateChildUserIfNotExists(mes.UserId);
                        IActorRef childActorRef = _users[mes.UserId];
                        childActorRef.Tell(mes);
                    });

            Receive<StopMovieMessage>(
                mes =>
                    {
                        CreateChildUserIfNotExists(mes.UserId);
                        IActorRef childActorRef = _users[mes.UserId];
                        childActorRef.Tell(mes);
                    });
        }

        private void CreateChildUserIfNotExists(int userId)
        {
            if (!_users.ContainsKey(userId))
            {
                IActorRef newChildActorRef = Context.ActorOf(Props.Create(() => new UserActor(userId)), "User" + userId);
                _users.Add(userId, newChildActorRef);
                ColorConsole.WriteColorLine(
                    $"UserCoordinationActor created new child UserActor for {userId} (Total Users: {_users.Count})",
                    ConsoleColor.Cyan);
            }
        }

        protected override void PreStart()
        {
            ColorConsole.WriteColorLine("UserCoordinationActor PreStart", ConsoleColor.Cyan);
        }

        protected override void PostStop()
        {
            ColorConsole.WriteColorLine("UserCoordinationActor PostStop", ConsoleColor.Cyan);
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteColorLine("UserCoordinationActor PreRestart because: " + reason, ConsoleColor.Cyan);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteColorLine("UserCoordinationActor PostRestart because: " + reason, ConsoleColor.Cyan);
            base.PostRestart(reason);
        }
    }
}