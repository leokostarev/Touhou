using UnityEngine;

namespace Game.Bullet {
    public partial class BaseBullet {
        public readonly struct BulletAIForward : IBulletAI {
            private readonly float speed;

            public BulletAIForward(float _speed) {
                speed = _speed;
            }

            public void OnFixedUpdate(BaseBullet bullet) {
                var transform1 = bullet.transform;

                transform1.position += transform1.right * speed;
            }
        }
    }
}
