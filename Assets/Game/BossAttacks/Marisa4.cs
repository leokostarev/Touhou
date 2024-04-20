using System;
using UniRx;
using UnityEngine;
using BaseBullet = Game.Bullet.BaseBullet;
using Object = UnityEngine.Object;
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
                < .25f => SharedData.getPos(0, t),
                < .5f => SharedData.getPos(1, t),
                < .75f => SharedData.getPos(t, 0),
                _ => SharedData.getPos(t, 1)
            };
            var dirD = Random.onUnitSphere / 4;
            dirD.z = 0;
            var direction = (Player.instance.transform.position - pos + dirD).normalized;

            var b = Object.Instantiate(SharedData.bullet0, pos, default).GetComponent<BaseBullet>();

            var ratio = Random.value;
            b.Radius = Mathf.Lerp(.15f, .25f, ratio);
            b.AI = new BaseBullet.BulletAIStraight(direction * Mathf.Lerp(2f, 1f, ratio));
        }
    }
}
