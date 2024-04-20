using System;
using Game.Bullet;
using Helpers;
using UniRx;
using UnityEngine;


namespace Game.BossAttacks {
    public class Marisa1 : IBossAttack {
        private static readonly BulletBuilder builder =
            new BulletBuilder(new Bullet.Bullet.BulletAIStraight(new Vector3(0, 2)))
                .SetRadius(.2f)
                .SetRotation(Quaternion.Euler(0, 0, 270));

        public Action Callback { get; set; }
        private readonly CompositeDisposable _disposable = new();

        private readonly float frequency;
        private float duration;

        private float startTime; // TODO: change to Instant (maybe)

        private CooldownTimer fireCooldown = new(1 / 15f);


        public Marisa1(float _frequency = 4f, float duration_ = 15f) {
            frequency = _frequency;
            duration = duration_;
        }

        public void CleanUp() => _disposable.Clear();

        public void Begin() {
            startTime = Time.time + .8f;

            Observable.EveryUpdate().Subscribe(_ => Update()).AddTo(_disposable);
        }

        private void Update() {
            fireCooldown.TickNotLinear(.2f + Math.Abs(Mathf.Cos((Time.time - startTime) * frequency)));
            duration -= Time.deltaTime;
            if (duration < 0) {
                Callback();
                _disposable.Clear();
                return;
            }

            while (fireCooldown.IsReady()) {
                Fire();
            }
        }

        private void Fire() {
            foreach (var i in new[] { -.1f, .2f, .5f, .8f, 1.1f }) {
                var pos = SharedData.GetPos(i, 1.1f);

                pos.x += Mathf.Sin((Time.time - startTime) * frequency);

                builder
                    .SetPosition(pos)
                    .Build();
            }
        }
    }
}
