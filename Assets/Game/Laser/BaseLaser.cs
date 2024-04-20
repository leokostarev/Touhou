using System;

using UnityEngine;

namespace Prefabs {
    public class BaseLaser : MonoBehaviour {
        private enum LaserAI {
            Monotone
        }

        [SerializeField] private AudioClip clickSound;

        [HideInInspector] public float lendth = 1f;
        [HideInInspector] public float width = 1f;
        private LaserAI aiType = LaserAI.Monotone;

        #region optional (depends on AI type)

        // Monotone
        public float speed;

        #endregion


        private void Start() {
            var transformLocalScale = transform.localScale;
            transformLocalScale.x *= lendth;
            transformLocalScale.y *= width;
            transform.localScale = transformLocalScale;

            AudioSource.PlayClipAtPoint(clickSound, default);
        }

        private void FixedUpdate() {
            switch (aiType) {
                case LaserAI.Monotone:
                    GoByMonotone();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var pos = transform.position;

            if (!ShareData.isInBounds(pos, 4)) Destroy(gameObject);

            // if ((Player.instance.transform.position - pos).sqrMagnitude * 1.7 < radius * radius)
            //     Player.instance.Hit(); TODO!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }

        private void GoByMonotone() {
            // NO TIME.DELTATIME BECAUSE FIXED UPDATE
            var transform1 = transform;
            var pos = transform1.position;
            pos += transform1.right * (speed / 50);
            transform1.position = pos;
        }
    }
}