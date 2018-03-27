using System;
using System.Collections.Generic;
using Game.Shared.Core;

namespace Game.Shared.Utility.StateMachine
{
    public class StateMachine<T>
    {
        private readonly T parent;
        private readonly Dictionary<Type, IState<T>> states = new Dictionary<Type, IState<T>>();

        protected StateMachine(T parentIn)
        {
            parent = parentIn;
        }

        public IState<T> Current { get; private set; }

        public IState<T> ChangeState<Q>() where Q : IState<T>, new()
        {
            var type = typeof(Q);
            if (!states.TryGetValue(type, out var state))
            {
                state = new Q {Parent = parent};
                states.Add(type, state);
            }

            Current?.Stop();
            Current = state;
            Current?.Start();

            return Current;
        }

        public IState<T> GetState<Q>() where Q : IState<T>, new()
        {
            var type = typeof(Q);
            if (states.TryGetValue(type, out var state)) return state;

            //Doesn't exist, create it.
            state = new Q {Parent = parent};
            states.Add(type, state);
            return state;
        }

        public void Update(CoreTime gameTime)
        {
            Current?.Update(gameTime);
        }
    }
}