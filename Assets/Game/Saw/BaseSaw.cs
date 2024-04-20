using UnityEngine;

namespace Game.Saw {
    public class BaseSaw : MonoBehaviour {
        [SerializeField] private AudioClip sound;

        [HideInInspector] public float size = 1;
        [HideInInspector] public float lastTime;
        [HideInInspector] public float growTime = .5f;

        private float accumulatedTime;


        public float speed;

        private void Start() {
            transform.localScale *= size;

            AudioSource.PlayClipAtPoint(sound, default);
        }

        private void Update() {
            lastTime -= Time.deltaTime;

            if (lastTime < 0) Destroy(gameObject);

            accumulatedTime += Time.deltaTime;
            transform.localScale = Vector3.one * (size * Mathf.Clamp01(accumulatedTime / growTime));
        }
    }
}
