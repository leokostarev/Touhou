using System;

namespace Game.Events {
    public interface IScriptEvent {
        public event Action onEnd;
        public void Begin();
        public void Update();

        public void CleanUp();

    }
}