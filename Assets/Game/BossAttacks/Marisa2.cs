using System;
using System.Collections.Generic;
using System.Linq;
using Game.Events;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.BossAttacks {
    public class Marisa2 : AbsBossAttack {
        private readonly CompositeDisposable _disposable = new();
       
        private float allTime = 10f;
        private const float cooldown = .3f;

        private float deltaAngle;

        private float accumulatedTime;
        
        public override void CleanUp() => _disposable.Clear();

        public override void Begin() {
            Observable.EveryUpdate().Subscribe(_ => Update()).AddTo(_disposable);
        }

        private void Update() {
            accumulatedTime += Time.deltaTime;
            allTime -= Time.deltaTime;
            if (allTime < 0) {
                callback();
                _disposable.Clear();
                return;
            }

            while (accumulatedTime > cooldown) {
                accumulatedTime -= cooldown;
                Fire();
            }
        }

        private void Fire() {
            for (double angle = deltaAngle; angle < 2 * Math.PI + deltaAngle; angle += 1d / 4 * Math.PI) {
                foreach (var i in GetPositions(angle)) {
                    var b = Object.Instantiate(
                            ShareData.bullet0,
                            FightEvent.boss.transform.position + new Vector3(i.Item1, i.Item2),
                            Quaternion.Euler(0, 0, (float)(angle / Math.PI) * 180))
                        .GetComponent<Prefabs.BaseBullet>();

                    b.radius = .2f;
                    b.speed = 4;
                }
            }

            deltaAngle += (float)Math.PI / 16;
        }

        private static IEnumerable<(float, float)> GetPositions(double angle) {
            return new[] {
                (0f, 0f),
                (-.2f, .2f), (-.2f, -.2f),
                (-.4f, .4f), (-.4f, -.4f),
                (-.6f, .6f), (-.6f, -.6f),
            }.Select(i => (
                    (float)(i.Item1 * Math.Cos(angle) - i.Item2 * Math.Sin(angle)),
                    (float)(i.Item1 * Math.Sin(angle) + i.Item2 * Math.Cos(angle))
                )
            );
        }
    }
}