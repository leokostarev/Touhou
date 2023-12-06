using UnityEngine;

namespace Prefabs {
    public class BaseSaw : MonoBehaviour {
        private enum SawAI {
            Grow
        }

        [SerializeField] private AudioClip clickSound;

        [HideInInspector] public float size = 1;
        [HideInInspector] public float lastTime;
        [HideInInspector] public float growTime = .5f;
        private SawAI aiType = SawAI.Grow;

        private float accumulatedTime;

        #region optional (depends on AI type)

        // Monotone
        public float speed;

        #endregion


        private void Start() {
            transform.localScale *= size;

            AudioSource.PlayClipAtPoint(clickSound, default);
        }

        private void Update() {
            lastTime -= Time.deltaTime;

            if (lastTime < 0) Destroy(gameObject);

            accumulatedTime += Time.deltaTime;
            transform.localScale = Vector3.one * (size * Mathf.Clamp01(accumulatedTime / growTime));
        }
    }
}