using System;

namespace Game.BossAttacks {
    public abstract class AbsBossAttack {
        internal Action callback;
        public abstract void Begin();

        public virtual void CleanUp() {
        }

        public virtual void AddCallback(Action onAttackEnd) {
            callback = onAttackEnd;
        }
    }
}