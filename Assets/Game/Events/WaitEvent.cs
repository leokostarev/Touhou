using System;
using UnityEngine;

namespace Game.Events {
    public class WaitEvent : IScriptEvent {
        public event Action onEnd;

        private float endAt;
        private readonly float duration;

        public WaitEvent(float _duration) {
            duration = _duration;
        }


        public void Begin() {
            endAt = Time.time + duration;
        }

        public void Update() {
            if (Time.time > endAt) onEnd?.Invoke();
        }

        public void CleanUp() {
        }
    }
}
