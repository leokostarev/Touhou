using System;
using System.Diagnostics.CodeAnalysis;
using Game.Bullet;
using Game.Events;
using Helpers;
using UniRx;
using UnityEngine;

namespace Game.BossAttacks {
    public class Marisa2 : IBossAttack {
        private static readonly BulletBuilder builder =
            new BulletBuilder(new Bullet.Bullet.BulletAIForward(4))
                .SetRadius(.2f);

        public Action Callback { get; set; }
        private readonly CompositeDisposable _disposable = new();

        private Instant endTime;
        private CooldownTimer fireCooldown = new(.3f);

        private float deltaAngle; // in degrees

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
            for (var angle = 0; angle < 360; angle += 45) {
                var rotation = Quaternion.Euler(0, 0, angle + deltaAngle);

                foreach (var i in GetOffsets(rotation)) {
                    builder
                        .SetPosition(FightEvent.boss.transform.position + i)
                        .SetRotation(rotation)
                        .Build();
                }
            }

            deltaAngle += 11.25f;
        }


        [SuppressMessage("ReSharper", "ReturnTypeCanBeEnumerable.Local")]
        // TODO: benchmark array against IEnumerable
        private static Vector3[] GetOffsets(Quaternion rotation) {
            return new[] {
                rotation * new Vector3(0f, 0f),
                rotation * new Vector3(-.2f, .2f),
                rotation * new Vector3(-.2f, -.2f),
                rotation * new Vector3(-.4f, .4f),
                rotation * new Vector3(-.4f, -.4f),
                rotation * new Vector3(-.6f, .6f),
                rotation * new Vector3(-.6f, -.6f)
            };
        }
    }
}
