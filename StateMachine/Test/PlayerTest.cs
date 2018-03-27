using Game.Shared.Core;

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

        public void Update(CoreTime coreTime)
        {
            States.Update(coreTime);
        }
    }
}