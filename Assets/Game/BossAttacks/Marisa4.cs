using System;
using Game.Bullet;
using Helpers;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Game.BossAttacks {
    public class Marisa4 : IBossAttack {
        public Action Callback { get; set; }
        private readonly CompositeDisposable _disposable = new();


        private Instant endTime;
        private CooldownTimer fireCooldown = new(.5f);

        public void CleanUp() => _disposable.Clear();


        public void Begin() {
            endTime = new Instant(25);

            Observable.EveryUpdate().Subscribe(_ => Update()).AddTo(_disposable);
            Random.InitState(7);
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

        private static void Fire() {
            var t = Random.value;

            var pos = Random.value switch {
                < .25f => SharedData.GetPos(0, t),
                < .5f => SharedData.GetPos(1, t),
                < .75f => SharedData.GetPos(t, 0),
                _ => SharedData.GetPos(t, 1)
            };

            var dirD = Random.onUnitSphere / 4;
            dirD.z = 0;

            var ratio = Random.value; // FIXME: should be seeded

            var direction = (Player.Instance.transform.position - pos + dirD).normalized
                            * Mathf.Lerp(2f, 1f, ratio);

            new BulletBuilder(new Bullet.Bullet.BulletAIStraight(direction))
                .SetRadius(Mathf.Lerp(.15f, .25f, ratio))
                .SetPosition(pos)
                .Build();
        }
    }
}
