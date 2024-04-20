using Extensions;
using UnityEngine;

namespace Game.Bullet {
    public partial class BaseBullet : MonoBehaviour {
        [SerializeField] private AudioClip clickSound;

        public float Radius { get; set; }

        public IBulletAI AI { get; set; }
        public bool IsFrozen { get; set; }


        private void Start() {
            transform.localScale = new Vector3(Radius, Radius, Radius) * 5; // FIXME: magic number

            AudioSource.PlayClipAtPoint(clickSound, default);
        }


        private void FixedUpdate() {
            if (IsFrozen) return;

            AI.OnFixedUpdate(this);

            var pos = transform.position;

            // FIXME: move deletion logic to bulletAI
            if (!SharedData.isInBounds(pos, 4)) {
                // FIXME: magic number
                Destroy(gameObject);
            }

            if (Player.instance.transform.position.DistanceTo(pos) * 1.3 < Radius) {
                // FIXME: magic number
                Player.instance.Hit();
            }
        }
    }
}
