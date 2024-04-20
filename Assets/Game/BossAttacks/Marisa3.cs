using System;
using Game.Bullet;
using Helpers;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Game.BossAttacks {
    public class Marisa3 : IBossAttack {
        public Action Callback { get; set; }
        private readonly CompositeDisposable _disposable = new();

        private Instant endTime;
        private CooldownTimer fireCooldown = new(0.02f);

        private Instant startTime;
        private const float AngleRange = 80f;


        public void CleanUp() => _disposable.Clear();

        public void Begin() {
            endTime = new Instant(10f);
            startTime = new Instant();

            Observable.EveryUpdate().Subscribe(_ => Update()).AddTo(_disposable);
            Random.InitState(3);
        }

        private void Update() {
            fireCooldown.Tick();


            if (endTime.Elapsed) {
                Callback();
                _disposable.Clear();
                return;
            }

            while (fireCooldown.IsReady()) {
                Fire();
            }
        }

        private void Fire() {
            var pongetT = Mathf.PingPong(startTime.ElapsedBy, 1);
            var rotation = Mathf.Lerp(-AngleRange, AngleRange, pongetT) - 90;

            var ratio = Random.value; // FIXME: should be seeded

            new BulletBuilder(new Bullet.Bullet.BulletAIForward(5 - 3 * ratio))
                .SetRadius(0.1f + .2f * ratio)
                .SetRotation(Quaternion.Euler(0, 0, rotation))
                .SetPosition(SharedData.GetPos(pongetT / 4 + .375f + Random.value / 4 - .125f, 1.1f))
                .Build();
        }
    }
}
