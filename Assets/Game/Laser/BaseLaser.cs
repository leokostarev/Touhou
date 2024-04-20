using UnityEngine;

namespace Game.Laser {
    public class BaseLaser : MonoBehaviour {
        [SerializeField] private AudioClip sound;

        [HideInInspector] public float lendth = 1f;
        [HideInInspector] public float width = 1f;

        public float speed;


        private void Start() {
            var transform_ = transform;
            var transformLocalScale = transform_.localScale;
            transformLocalScale.x *= lendth;
            transformLocalScale.y *= width;
            transform_.localScale = transformLocalScale;

            AudioSource.PlayClipAtPoint(sound, default);
        }

        private void FixedUpdate() {
            var transform1 = transform;
            var pos = transform1.position;
            pos += transform1.right * (speed / 50);
            transform1.position = pos;


            if (!SharedData.IsInBounds(pos, 4)) Destroy(gameObject);

            // if ((Player.instance.transform.position - pos).sqrMagnitude * 1.7 < radius * radius)
            //     Player.instance.Hit(); TODO: hit player
        }
    }
}
