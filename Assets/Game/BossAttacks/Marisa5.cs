using Game.Events;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


namespace Game.BossAttacks {
    public class Marisa5 : AbsBossAttack {
        private readonly CompositeDisposable _disposable = new();

        private float lastTime = 10f;
        private int stacks = 0;
        private const float cooldown = 1f;

        private float accumulatedTime;

        public override void CleanUp() => _disposable.Clear();

        public override void Begin() {
            Observable.EveryUpdate().Subscribe(_ => Update()).AddTo(_disposable);
            Random.InitState(3);
        }


        private void Update() {
            accumulatedTime += Time.deltaTime;
            lastTime -= Time.deltaTime;
            if (lastTime < 0) {
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
            foreach (var rot in new[] { 0, 45, 90, 135, 180, 225, 270, 315 }) {
                var laser = Object.Instantiate(
                        ShareData.laser0,
                        FightEvent.boss.transform.position,
                        Quaternion.Euler(0, 0, rot + stacks * 15))
                    .GetComponent<Prefabs.BaseLaser>();

                laser.speed = 2f;
                laser.lendth = 1.5f;
            }

            stacks++;
        }
    }
}