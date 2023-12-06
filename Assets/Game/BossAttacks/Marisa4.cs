using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


namespace Game.BossAttacks {
    public class Marisa4 : AbsBossAttack {
        private readonly CompositeDisposable _disposable = new();

        private float allTime = 25f;
        private float cooldown = .5f;

        private float accumulatedTime;

        public override void CleanUp() => _disposable.Clear();

        public Marisa4(float difficulty = 1f) {
            cooldown /= difficulty;
        }

        public override void Begin() {
            Observable.EveryUpdate().Subscribe(_ => Update()).AddTo(_disposable);
            Random.InitState(7);
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

        private static void Fire() {
            var t = Random.value;
            var pos = Random.value switch {
                < .25f => ShareData.getPos(0, t),
                < .5f => ShareData.getPos(1, t),
                < .75f => ShareData.getPos(t, 0),
                _ => ShareData.getPos(t, 1)
            };
            var dirD = Random.onUnitSphere / 4;
            dirD.z = 0;
            var direction = (Player.instance.transform.position - pos + dirD).normalized;

            var b = Object.Instantiate(ShareData.bullet0, pos, default).GetComponent<Prefabs.BaseBullet>();

            var ratio = Random.value;
            b.radius = Mathf.Lerp(.15f, .25f, ratio);
            b.Direction = direction * Mathf.Lerp(2f, 1f, ratio);
        }
    }
}