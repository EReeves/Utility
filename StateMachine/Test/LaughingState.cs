using System;

namespace Game.Shared.Utility.StateMachine.Test
{
    public class LaughingState : IState<PlayerTest>
    {
        public PlayerTest Parent { get; set; }

        public void Start() => Console.WriteLine("*Smirk*");
        public void Stop() => Console.WriteLine("*Stops laughing*");

        public void Update()
        {
            Console.WriteLine("*Laugh*");
            if (Parent.HeardBadJoke)
            {
                Parent.States.ChangeState<NotAmusedState>();
            }
        }   
    }
}