using System;
using Game.Bullet;
using UniRx;
using UnityEngine;
using BaseBullet = Game.Bullet.BaseBullet;
using Random = UnityEngine.Random;


namespace Game.BossAttacks {
    public class Marisa4 : IBossAttack {
        public Action Callback { get; set; }

        private readonly CompositeDisposable _disposable = new();

        private float allTime = 25f;
        private readonly float cooldown = .5f;

        private float accumulatedTime;

        public void CleanUp() => _disposable.Clear();

        public Marisa4(float difficulty = 1f) {
            cooldown /= difficulty;
        }

        public void Begin() {
            Observable.EveryUpdate().Subscribe(_ => Update()).AddTo(_disposable);
            Random.InitState(7);
        }


        private void Update() {
            accumulatedTime += Time.deltaTime;
            allTime -= Time.deltaTime;
            if (allTime < 0) {
                Callback();
                _disposable.Clear();
                return;
            }

            while (accumulatedTime > cooldown) {
                accumulatedTime -= cooldown;
                Fire();
            }
        }

        private static void Fire() {
            var t = Random.value;

            var pos = Random.value switch {
                < .25f => SharedData.GetPos(0, t),
                < .5f => SharedData.GetPos(1, t),
                < .75f => SharedData.GetPos(t, 0),
                _ => SharedData.GetPos(t, 1)
            };

            var dirD = Random.onUnitSphere / 4;
            dirD.z = 0;

            var ratio = Random.value; // FIXME: should be seeded

            var direction = (Player.instance.transform.position - pos + dirD).normalized
                            * Mathf.Lerp(2f, 1f, ratio);

            new BulletBuilder(new BaseBullet.BulletAIStraight(direction))
                .SetRadius(Mathf.Lerp(.15f, .25f, ratio))
                .SetPosition(pos)
                .Build();
        }
    }
}
