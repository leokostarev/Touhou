using System;
using Game.Bullet;
using Game.Events;
using Helpers;
using UnityEngine;
using UniRx;

namespace Game.BossAttacks {
    public class Marisa0 : IBossAttack {
        private static readonly BulletBuilder builder =
            new BulletBuilder(new Bullet.Bullet.BulletAIForward(3))
                .SetRadius(.4f);

        public Action Callback { get; set; }
        private readonly CompositeDisposable _disposable = new();

        private readonly float duration;
        private readonly bool noWait;
        private readonly float deltaDeg;

        private Instant endTime;
        private CooldownTimer fireCooldown;
        private int noOfAttack;
        private Vector3 position;


        // FIXME: remove noWait in favor of wrappers Attacks
        public Marisa0(float duation_ = 8, bool noWait_ = false, float deltaDeg_ = 5.5f, float cooldown_ = .03f) {
            duration = duation_;
            noWait = noWait_;
            deltaDeg = deltaDeg_;
            fireCooldown = new CooldownTimer(cooldown_);
        }

        public void CleanUp() => _disposable.Clear();

        public void Begin() {
            endTime = new Instant(duration);
            position = FightEvent.boss.transform.position;

            Observable.EveryUpdate().Subscribe(_ => Update()).AddTo(_disposable);

            if (noWait) {
                Callback();
            }
        }

        private void Update() {
            fireCooldown.Tick();

            if (endTime.Elapsed) {
                if (!noWait) Callback();
                _disposable.Clear();
                return;
            }

            while (fireCooldown.IsReady()) {
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
