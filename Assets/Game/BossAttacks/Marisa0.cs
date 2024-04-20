using System;
using Game.Bullet;
using Game.Events;
using UnityEngine;
using UniRx;
using BaseBullet = Game.Bullet.BaseBullet;

namespace Game.BossAttacks {
    public class Marisa0 : IBossAttack {
        private static readonly BulletBuilder builder =
            new BulletBuilder(new BaseBullet.BulletAIForward(3))
                .SetRadius(.4f);

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
            builder
                .SetPosition(position)
                .SetRotation(Quaternion.Euler(0, 0, deltaDeg * noOfAttack))
                .Build();

            noOfAttack++;
        }
    }
}
