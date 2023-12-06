using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


namespace Game.BossAttacks {
    public class Marisa3 : AbsBossAttack {
        private readonly CompositeDisposable _disposable = new();

        private float allTime = 10f;
        private const float cooldown = .02f;
        
        private float accumulatedT;
        private const float AngleRange = 80f;

        private float accumulatedTime;

        public override void CleanUp() => _disposable.Clear();

        public override void Begin() {
            Observable.EveryUpdate().Subscribe(_ => Update()).AddTo(_disposable);
            Random.InitState(3);
        }
        

        private void Update() {
            accumulatedTime += Time.deltaTime;
            accumulatedT += Time.deltaTime;
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
            var pongetT = Mathf.PingPong(accumulatedT, 1);
            var rotation = Mathf.Lerp(-AngleRange, AngleRange, pongetT) - 90;


            var b = Object.Instantiate(
                    ShareData.bullet0,
                    ShareData.getPos(pongetT / 4 + .375f + Random.value / 4 - .125f, 1.1f),
                    Quaternion.Euler(0, 0, rotation))
                .GetComponent<Prefabs.BaseBullet>();
            var ratio = Random.value;
            b.radius = 0.1f + .2f * ratio;
            b.speed = 5 - 3 * ratio;
        }
    }
}