using System;
using Game;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Prefabs {
    public class BaseBullet : MonoBehaviour {
        private enum BulletAI {
            ByAngle,
            ByDirection,
            ByRadius
        }

        [SerializeField] private AudioClip clickSound;
        [SerializeField] public float radius = .3f;
        private BulletAI aiType = BulletAI.ByAngle;

        public bool IsFrozen { get; set; }

        #region optional (depends on AI type)

        // ByAngle
        public float speed;
        private Vector3 direction;

        // ByDirection
        public Vector3 Direction {
            set {
                aiType = BulletAI.ByDirection;
                direction = value;
            }
        }

        // ByRadius
        private float originRadius;
        private Vector3 originPos;
        private float nowAngle;

        public void SetOriginAndRadius(Vector3 _orgiginPos, float _originRadius, float _nowAngle) {
            aiType = BulletAI.ByRadius;
            originRadius = _originRadius;
            originPos = _orgiginPos;
            nowAngle = _nowAngle;
            UpdateNOW();
        }

        public void SetOriginAndRadius(Vector3 _orgiginPos, float _originRadius) {
            aiType = BulletAI.ByRadius;
            originRadius = _originRadius;
            originPos = _orgiginPos;
            nowAngle = Random.value * 360;
            UpdateNOW();
        }

        #endregion


        private void Start() {
            transform.localScale = new Vector3(radius, radius, radius) * 5;

            AudioSource.PlayClipAtPoint(clickSound, default);
        }

        private void UpdateNOW() {
            var prevIsFrozen = IsFrozen;
            IsFrozen = false;
            FixedUpdate();
            IsFrozen = prevIsFrozen;
        }

        private void FixedUpdate() {
            if (IsFrozen) return;
            switch (aiType) {
                case BulletAI.ByAngle:
                    GoByAngle();
                    break;
                case BulletAI.ByDirection:
                    GoByDirection();
                    break;
                case BulletAI.ByRadius:
                    GoByRadius();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var pos = transform.position;

            if (aiType != BulletAI.ByRadius && !ShareData.isInBounds(pos, 4)) Destroy(gameObject);

            if ((Player.instance.transform.position - pos).sqrMagnitude * 1.7 < radius * radius)
                Player.instance.Hit();
        }

        private void GoByAngle() {
            // NO TIME.DELTATIME BECAUSE FIXED UPDATE
            var transform1 = transform;
            var pos = transform1.position;
            pos += transform1.right * (speed / 50);
            transform1.position = pos;
        }

        private void GoByDirection() {
            transform.position += direction / 50;
        }

        private void GoByRadius() {
            nowAngle += Time.deltaTime * speed;
            transform.position = originPos + Quaternion.Euler(0, 0, nowAngle) * Vector3.right * originRadius;
        }
    }
}