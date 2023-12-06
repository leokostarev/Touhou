using System;
using UniRx;

namespace Game.BossAttacks {
    public class BossWait : AbsBossAttack {
        private readonly CompositeDisposable _disposable = new();

        private readonly float duration;

        public BossWait(float duration) {
            this.duration = duration;
        }

        public override void CleanUp() => _disposable.Clear();

        public override void AddCallback(Action onAttackEnd) {
            callback = onAttackEnd;
        }

        public override void Begin() {
            Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(duration)).Subscribe(_ => {
                callback();
                _disposable.Clear();
            }).AddTo(_disposable);
        }
    }
}