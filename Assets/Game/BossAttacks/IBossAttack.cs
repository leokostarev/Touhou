using System;

namespace Game.BossAttacks {
    public interface IBossAttack {
        public Action Callback { get; set; }

        public void Begin();

        public void CleanUp() {
        }
    }
}
