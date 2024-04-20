namespace Game.Bullet {
    public interface IBulletAI {
        IBulletAI Clone();
        void OnFixedUpdate(BaseBullet bullet);
    }
}
