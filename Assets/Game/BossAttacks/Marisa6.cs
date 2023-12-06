using System.Collections.Generic;
using Game.Events;
using Prefabs;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


namespace Game.BossAttacks {
    public class Marisa6 : AbsBossAttack {
        private readonly CompositeDisposable _disposable = new();

        private float allTime = 10f;
        private float spawnRadius = 10f;
        private float cooldown = .01f;

        private bool isAttackFase;
        private int BulletsLast = 200;
        private List<BaseBullet> bullets = new();

        private float accumulatedTime;

        public override void CleanUp() => _disposable.Clear();

        public Marisa6(float difficulty = 1f) {
            cooldown /= difficulty;
        }

        public override void Begin() {
            Observable.EveryUpdate().Subscribe(_ => Update()).AddTo(_disposable);
            Random.InitState(18);
            PreFire();
        }


        private void Update() {
            allTime -= Time.deltaTime;
            if (allTime < 0) {
                callback();
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

            var saw = Object.Instantiate(ShareData.saw0, BossPos, default).GetComponent<BaseSaw>();
            saw.lastTime = 2f;
            saw.size = .7f;

            for (var i = 0; i < 360; i += 10) {
                var b = Object.Instantiate(ShareData.bullet0).GetComponent<BaseBullet>();
                bullets.Add(b);

                b.IsFrozen = true;
                b.radius = .2f;
                b.speed = -60;
                b.SetOriginAndRadius(BossPos, 1.5f, i);
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

            var b = Object.Instantiate(ShareData.bullet0).GetComponent<BaseBullet>();
            bullets.Add(b);

            b.IsFrozen = true;
            b.radius = .2f;
            b.speed = 40;
            b.SetOriginAndRadius(BossPos, Random.Range(1.6f, spawnRadius));
        }
    }
}