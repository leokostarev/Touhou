using System;
using System.Collections.Generic;
using Game.Bullet;
using Game.Events;
using Game.Saw;
using UniRx;
using UnityEngine;
using BaseBullet = Game.Bullet.BaseBullet;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


namespace Game.BossAttacks {
    public class Marisa6 : IBossAttack {
        public Action Callback { get; set; }

        private readonly CompositeDisposable _disposable = new();

        private float allTime = 10f;
        private const float spawnRadius = 10f;
        private readonly float cooldown = .01f;

        private bool isAttackFase;
        private int BulletsLast = 200;
        private readonly List<BaseBullet> bullets = new();

        private float accumulatedTime;

        public void CleanUp() => _disposable.Clear();

        public Marisa6(float difficulty = 1f) {
            cooldown /= difficulty;
        }

        public void Begin() {
            Observable.EveryUpdate().Subscribe(_ => Update()).AddTo(_disposable);
            Random.InitState(18);
            PreFire();
        }


        private void Update() {
            allTime -= Time.deltaTime;
            if (allTime < 0) {
                Callback();
                _disposable.Clear();
                return;
            }

            if (isAttackFase) return;
            accumulatedTime += Time.deltaTime;
            while (accumulatedTime > cooldown) {
                accumulatedTime -= cooldown;
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
                    new BulletBuilder(new BaseBullet.BulletAIOrbital(BossPos, 1.5f, -60, i))
                        .SetRadius(.2f)
                        .SetIsFrozen(true)
                        .Build();

                bullets.Add(b);
            }

            if ((Player.instance.transform.position - BossPos).magnitude < 1.7) Player.instance.ResetPosition();
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
                    new BaseBullet.BulletAIOrbital(
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
