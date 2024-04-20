using System;
using Game.Boss;
using Object = UnityEngine.Object;

namespace Game.Events {
    public class FightEvent : IScriptEvent {
        private readonly string bossName;
        private readonly int bossPhase;

        public static BossScript boss;
        public static bool IsBossSet;
        public event Action onEnd;

        public FightEvent(string name, int phase) {
            bossName = name;
            bossPhase = phase;
        }

        public void CleanUp() {
            Player.Instance.IsActive = true;
            boss.CleanUp();
        }

        public void Begin() {
            boss = Object.Instantiate(SharedData.boss).GetComponent<BossScript>();
            boss.Init(bossName, bossPhase);
            Player.Instance.IsActive = true;
            IsBossSet = true;
            boss.onAttacksEnd += () => {
                Player.Instance.IsActive = false;

                IsBossSet = false;
                boss.CleanUp();
                onEnd?.Invoke();
            };
        }

        public void Update() {
        }
    }
}
