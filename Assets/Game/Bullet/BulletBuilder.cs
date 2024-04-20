using UnityEngine;

namespace Game.Bullet {
    public class BulletBuilder {
        private readonly IBulletAI ai;
        private float radius;
        private Vector3 position;
        private Quaternion rotation;

        public BulletBuilder(IBulletAI ai_) {
            ai = ai_;
        }

        public BulletBuilder SetRadius(float radius_) {
            radius = radius_;
            return this;
        }

        public BulletBuilder SetPosition(Vector3 position_) {
            position = position_;
            return this;
        }

        public BulletBuilder SetRotation(Quaternion rotation_) {
            rotation = rotation_;
            return this;
        }

        public BaseBullet Build() {
            // TODO: use a pool
            // FIXME: make prefab configurable
            var bullet = Object.Instantiate(SharedData.bullet0, position, rotation).GetComponent<BaseBullet>();

            bullet.Radius = radius;
            bullet.AI = ai;

            return bullet;
        }
    }
}
