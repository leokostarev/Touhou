using UnityEngine;

namespace Game.Bullet {
    public partial class BaseBullet {
        public struct BulletAIRadius : IBulletAI {
            private readonly Vector3 originPos;
            private readonly float originRadius;
            private readonly float angularSpeed;

            private float nowAngle;

            public BulletAIRadius(Vector3 originPos_, float originRadius_, float angularSpeed_, float nowAngle_) {
                originPos = originPos_;
                originRadius = originRadius_;
                angularSpeed = angularSpeed_;
                nowAngle = nowAngle_;
            }


            public void OnFixedUpdate(BaseBullet bullet) {
                nowAngle += angularSpeed;
                bullet.transform.position = originPos + Quaternion.Euler(0, 0, nowAngle) * Vector3.right * originRadius;
            }
        }
    }
}
