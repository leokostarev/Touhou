using System;
using Game.Events;
using Game.Laser;
using Helpers;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;


namespace Game.BossAttacks {
    public class Marisa5 : IBossAttack {
        public Action Callback { get; set; }
        private readonly CompositeDisposable _disposable = new();

        private Instant endTime;
        private int stacks;

        private CooldownTimer fireCooldown;

        public void CleanUp() => _disposable.Clear();

        public void Begin() {
            endTime = new Instant(10f);

            Observable.EveryUpdate().Subscribe(_ => Update()).AddTo(_disposable);
        }

        private void Update() {
            fireCooldown.Tick();

            if (endTime.Elapsed) {
                Callback();
                _disposable.Clear();
                return;
            }

            while (fireCooldown.IsReady()) {
                Fire();
            }
        }

        private void Fire() {
            for (var rotation = 0f; rotation < 360f; rotation += 45f) {
                var laser = Object.Instantiate(
                        SharedData.laser0,
                        FightEvent.boss.transform.position,
                        Quaternion.Euler(0, 0, rotation + stacks * 15)
                    )
                    .GetComponent<BaseLaser>();

                laser.speed = 2f;
                laser.lendth = 1.5f;
            }

            stacks++;
        }
    }
}
