using System;
using UniRx;
using UnityEngine;
using BaseBullet = Game.Bullet.BaseBullet;
using Object = UnityEngine.Object;


namespace Game.BossAttacks {
    public class Marisa1 : IBossAttack {
        public Action Callback { get; set; }

        private readonly CompositeDisposable _disposable = new();

        private float allTime;
        private float startTime;
        private readonly float frequency;
        private const float cooldown = 1 / 15f;

        private float accumulatedTime;

        public Marisa1(float _frequency = 4f, float _allTime = 15f) {
            frequency = _frequency;
            allTime = _allTime;
        }


        public void CleanUp() => _disposable.Clear();

        public void Begin() {
            Observable.EveryUpdate().Subscribe(_ => Update()).AddTo(_disposable);
            startTime = Time.time + .8f;
        }

        private void Update() {
            accumulatedTime += Time.deltaTime * (.2f + Math.Abs(Mathf.Cos((Time.time - startTime) * frequency)));
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
            foreach (var i in new[] { -.1f, .2f, .5f, .8f, 1.1f }) {
                var b = Object.Instantiate(SharedData.bullet0, Vector3.zero, Quaternion.Euler(0, 0, 270))
                    .GetComponent<BaseBullet>();

                b.transform.position = new Vector3(
                    SharedData.GetPosX(i) + Mathf.Sin((Time.time - startTime) * frequency),
                    SharedData.GetPosY(1.1f)
                );

                b.Radius = .2f;
                b.AI = new BaseBullet.BulletAIStraight(new Vector3(0, 2));
            }
        }
    }
}
