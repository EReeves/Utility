using System.Runtime.Serialization;

namespace Game.Shared.Utility.StateMachine.Test
{
    public class PlayerTest
    {
        public readonly StateMachine<PlayerTest> States;
        public bool HeardBadJoke = true;
        
        public PlayerTest()
        {
             States = new StateMachine<PlayerTest>(this);
             States.ChangeState<LaughingState>();
        }

        public void Update()
        {
            States.Update();
        }
        
    }
}