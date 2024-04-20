using System;
using Game.Events;
using Game.Laser;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


namespace Game.BossAttacks {
    public class Marisa5 : IBossAttack {
        public Action Callback { get; set; }

        private readonly CompositeDisposable _disposable = new();

        private float lastTime = 10f;
        private int stacks;
        private const float cooldown = 1f;

        private float accumulatedTime;

        public void CleanUp() => _disposable.Clear();

        public void Begin() {
            Observable.EveryUpdate().Subscribe(_ => Update()).AddTo(_disposable);
            Random.InitState(3);
        }


        private void Update() {
            accumulatedTime += Time.deltaTime;
            lastTime -= Time.deltaTime;
            if (lastTime < 0) {
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
