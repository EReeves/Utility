using System.Runtime.Serialization;

namespace Game.Shared.Utility.StateMachine.Test
{
    public class PlayerTest
    {
        public readonly PlayerState States;
        
        
        public PlayerTest()
        {
             States = new PlayerState(this);
             States.ChangeState<LaughingState>();
        }

        public void Update()
        {
            States.Update();
        }
        
    }
}