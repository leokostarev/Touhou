using Game.Events;
using UnityEngine;
using Object = UnityEngine.Object;
using UniRx;

namespace Game.BossAttacks {
    public class Marisa0 : AbsBossAttack {
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

        public override void CleanUp() => _disposable.Clear();

        public override void Begin() {
            position = FightEvent.boss.transform.position;
            Observable.EveryUpdate().Subscribe(_ => Update()).AddTo(_disposable);
            if (noWait)
                callback();
        }

        private void Update() {
            accumulatedTime += Time.deltaTime;
            remainingTime -= Time.deltaTime;

            if (remainingTime < 0) {
                if (!noWait)
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
            var b = Object.Instantiate(
                ShareData.bullet0,
                position,
                Quaternion.Euler(0, 0, deltaDeg * noOfAttack)
            ).GetComponent<Prefabs.BaseBullet>();

            b.radius = .4f;
            b.speed = 3;
            noOfAttack++;
        }
    }
}