using System;
using Game.Shared.Core;

namespace Game.Shared.Utility.StateMachine.Test
{
    public class LaughingState : IState<PlayerTest>
    {
        public PlayerTest Parent { get; set; }

        public void Start()
        {
            Console.WriteLine("*Smirk*");
        }

        public void Stop()
        {
            Console.WriteLine("*Stops laughing*");
        }

        public void Update(CoreTime coreTime)
        {
            Console.WriteLine("*Laugh*");
            if (Parent.States.HeardBadJoke) Parent.States.ChangeState<NotAmusedState>();
        }
    }
}