using System;
using UniRx;

namespace Game.BossAttacks {
    public class BossWait : IBossAttack {
        public Action Callback { get; set; }

        private readonly CompositeDisposable _disposable = new();

        private readonly float duration;

        public BossWait(float duration) {
            this.duration = duration;
        }

        public void CleanUp() => _disposable.Clear();


        public void Begin() {
            // FIXME: it's not a good idea to use EveryUpdate here
            Observable.EveryUpdate().Delay(TimeSpan.FromSeconds(duration)).Subscribe(
                _ => {
                    Callback();
                    _disposable.Clear();
                }
            ).AddTo(_disposable);
        }
    }
}
