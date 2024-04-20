using System;
using Game.Events;
using UnityEngine;
using Object = UnityEngine.Object;
using UniRx;
using BaseBullet = Game.Bullet.BaseBullet;

namespace Game.BossAttacks {
    public class Marisa0 : IBossAttack {
        public Action Callback { get; set; }
        private readonly CompositeDisposable _disposable = new();


        private float remainingTime;

        private readonly float deltaDeg;
        private readonly float cooldown;

        private int noOfAttack;
        private float accumulatedTime;
        private readonly bool noWait;
        private Vector3 position;


        public Marisa0(float duation = 8, bool _noWait = false, float deltaDeg = 5.5f, float cooldown = .03f) {
            remainingTime = duation;
            noWait = _noWait;
            this.cooldown = cooldown;
            this.deltaDeg = deltaDeg;
        }

        public void CleanUp() => _disposable.Clear();

        public void Begin() {
            position = FightEvent.boss.transform.position;
            Observable.EveryUpdate().Subscribe(_ => Update()).AddTo(_disposable);
            if (noWait) Callback();
        }

        private void Update() {
            accumulatedTime += Time.deltaTime;
            remainingTime -= Time.deltaTime;

            if (remainingTime < 0) {
                if (!noWait) Callback();
                _disposable.Clear();
                return;
            }

            while (accumulatedTime > cooldown) {
                accumulatedTime -= cooldown;
                Fire();
            }
        }

        private void Fire() {
            var b = Object.Instantiate(
                SharedData.bullet0,
                position,
                Quaternion.Euler(0, 0, deltaDeg * noOfAttack)
            ).GetComponent<BaseBullet>();

            b.Radius = .4f;
            b.AI = new BaseBullet.BulletAIForward(3);
            noOfAttack++;
        }
    }
}
