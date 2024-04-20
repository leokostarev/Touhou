using System;
using System.Collections.Generic;
using Extensions;
using Game.Bullet;
using Game.Events;
using Game.Saw;
using Helpers;
using UniRx;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


namespace Game.BossAttacks {
    public class Marisa6 : IBossAttack {
        public Action Callback { get; set; }

        private readonly CompositeDisposable _disposable = new();

        private Instant endTime;
        private const float spawnRadius = 10f;

        private bool isAttackFase;
        private int BulletsLast = 200;
        private readonly List<Bullet.Bullet> bullets = new();

        private CooldownTimer fireCooldown = new(.01f);

        public void CleanUp() => _disposable.Clear();

        public void Begin() {
            endTime = new Instant(10f);

            Observable.EveryUpdate().Subscribe(_ => Update()).AddTo(_disposable);
            PreFire();
        }


        private void Update() {
            if (endTime.Elapsed) {
                Callback();
                _disposable.Clear();
                return;
            }

            if (isAttackFase) return;
            fireCooldown.Tick();
            while (fireCooldown.IsReady()) {
                Fire();
            }
        }

        private void PreFire() {
            var BossPos = FightEvent.boss.transform.position;

            var saw = Object.Instantiate(SharedData.saw0, BossPos, default).GetComponent<BaseSaw>();
            saw.lastTime = 2f;
            saw.size = .7f;

            for (var i = 0; i < 360; i += 10) {
                var b =
                    new BulletBuilder(new Bullet.Bullet.BulletAIOrbital(BossPos, 1.5f, -60, i))
                        .SetRadius(.2f)
                        .SetIsFrozen(true)
                        .Build();

                bullets.Add(b);
            }

            if (Player.Instance.transform.position.DistanceTo(BossPos) < 1.7) {
                Player.Instance.ResetPosition();
            }
        }

        private void Fire() {
            BulletsLast--;
            if (BulletsLast <= 0) {
                isAttackFase = true;
                foreach (var i in bullets) i.IsFrozen = false;

                return;
            }

            var BossPos = FightEvent.boss.transform.position;

            var b = new BulletBuilder(
                    new Bullet.Bullet.BulletAIOrbital(
                        BossPos,
                        Random.Range(1.6f, spawnRadius),
                        40,
                        Random.Range(0, 360) // FIXME: should be seeded
                    )
                )
                .SetRadius(.2f)
                .SetIsFrozen(true)
                .Build();

            bullets.Add(b);
        }
    }
}
