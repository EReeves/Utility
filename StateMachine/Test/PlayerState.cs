using System;
using Microsoft.Xna.Framework;

namespace Game.Shared.Utility.StateMachine.Test
{
    public class PlayerState : StateMachine<PlayerTest>
    {
        public bool HeardBadJoke = true;
        
 
        public PlayerState(PlayerTest parentIn) : base(parentIn){}
    }
}