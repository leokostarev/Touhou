using UnityEngine;

namespace Game.Bullet {
    public partial class Bullet {
        public readonly struct BulletAIStraight : IBulletAI {
            private readonly Vector3 direction;

            public BulletAIStraight(Vector3 _direction) {
                direction = _direction;
            }

            public IBulletAI Clone() => this;

            public void OnFixedUpdate(Bullet bullet) {
                bullet.transform.position += direction;
            }
        }
    }
}
