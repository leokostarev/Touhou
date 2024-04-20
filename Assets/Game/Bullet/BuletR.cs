using Game.Events;
using UnityEngine;

namespace Game.Bullet {
    public class BuletR : MonoBehaviour {
        private const float speed = 10f;

        private void Update() {
            transform.position += new Vector3(0, speed * Time.deltaTime);

            if (!FightEvent.IsBossSet || (FightEvent.boss.transform.position - transform.position).magnitude > 1)
                return;
            FightEvent.boss.lifes.Value -= 10;
            Destroy(gameObject);
        }
    }
}
