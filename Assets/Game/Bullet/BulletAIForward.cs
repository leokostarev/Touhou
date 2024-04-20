namespace Game.Bullet {
    public partial class Bullet {
        public readonly struct BulletAIForward : IBulletAI {
            // TODO: unify with BulletAIStraight
            private readonly float speed;

            public BulletAIForward(float _speed) {
                speed = _speed;
            }

            public IBulletAI Clone() => this;

            public void OnFixedUpdate(Bullet bullet) {
                var transform1 = bullet.transform;

                transform1.position += transform1.right * speed;
            }
        }
    }
}
