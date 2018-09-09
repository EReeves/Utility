using Game.Shared.Core;

namespace Game.Shared.Utility.StateMachine
{
    public interface IState<T>
    {
        T Parent { get; set; }
        void Start();
        void Stop();
        void Update(CoreTime time);
    }
}