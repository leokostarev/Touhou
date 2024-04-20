using System;

namespace Game.BossAttacks {
    public interface IBossAttack {
        public Action Callback { set; }

        // Should only be called once, doing the other way is undefined behavior
        public void Begin();

        public void CleanUp() {
        }
    }
}
