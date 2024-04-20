using System;
using Game.Bullet;
using UniRx;
using UnityEngine;
using BaseBullet = Game.Bullet.BaseBullet;
using Random = UnityEngine.Random;


namespace Game.BossAttacks {
    public class Marisa3 : IBossAttack {
        public Action Callback { get; set; }

        private readonly CompositeDisposable _disposable = new();

        private float allTime = 10f;
        private const float cooldown = .02f;

        private float accumulatedT;
        private const float AngleRange = 80f;

        private float accumulatedTime;

        public void CleanUp() => _disposable.Clear();

        public void Begin() {
            Observable.EveryUpdate().Subscribe(_ => Update()).AddTo(_disposable);
            Random.InitState(3);
        }


        private void Update() {
            accumulatedTime += Time.deltaTime;
            accumulatedT += Time.deltaTime;
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

        private void Fire() {
            var pongetT = Mathf.PingPong(accumulatedT, 1);
            var rotation = Mathf.Lerp(-AngleRange, AngleRange, pongetT) - 90;

            var ratio = Random.value; // FIXME: should be seeded

            new BulletBuilder(new BaseBullet.BulletAIForward(5 - 3 * ratio))
                .SetRadius(0.1f + .2f * ratio)
                .SetRotation(Quaternion.Euler(0, 0, rotation))
                .SetPosition(SharedData.GetPos(pongetT / 4 + .375f + Random.value / 4 - .125f, 1.1f))
                .Build();
        }
    }
}
