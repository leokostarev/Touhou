using UnityEngine;

namespace Game.Bullet {
    public class BulletBuilder {
        private readonly IBulletAI ai;
        private float radius;
        private Vector3 position;
        private Quaternion rotation;
        private bool isFrozen;

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

        public BulletBuilder SetIsFrozen(bool isFrozen_) {
            isFrozen = isFrozen_;
            return this;
        }

        public Bullet Build() {
            // TODO: use a pool
            // FIXME: make prefab configurable
            var bullet = Object.Instantiate(SharedData.bullet0, position, rotation).GetComponent<Bullet>();

            bullet.Radius = radius;
            bullet.AI = ai.Clone();
            bullet.IsFrozen = isFrozen;

            return bullet;
        }
    }
}
