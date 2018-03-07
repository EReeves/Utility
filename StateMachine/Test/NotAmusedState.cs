using System;

namespace Game.Shared.Utility.StateMachine.Test
{
    public class NotAmusedState : IState<PlayerTest>
    {
        public PlayerTest Parent { get; set; }
        public void Start(){}
        public void Stop(){}

        public void Update()
        {
            Console.WriteLine("*Doesn't look amused*");
        }

        
    }
}