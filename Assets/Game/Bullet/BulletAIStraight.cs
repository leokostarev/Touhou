using UnityEngine;

namespace Game.Bullet {
    public partial class BaseBullet {
        public readonly struct BulletAIStraight : IBulletAI {
            private readonly Vector3 direction;

            public BulletAIStraight(Vector3 _direction) {
                direction = _direction;
            }

            public void OnFixedUpdate(BaseBullet bullet) {
                bullet.transform.position += direction;
            }
        }
    }
}
