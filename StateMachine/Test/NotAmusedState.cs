using System;
using Game.Shared.Core;

namespace Game.Shared.Utility.StateMachine.Test
{
    public class NotAmusedState : IState<PlayerTest>
    {
        public PlayerTest Parent { get; set; }

        public void Start()
        {
        }

        public void Stop()
        {
        }

        public void Update(CoreTime coreTime)
        {
            Console.WriteLine("*Doesn't look amused*");
        }
    }
}