using System;
using Helpers;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Game {
    public class Player : MonoBehaviour {
        [SerializeField] private GameObject bullet;
        [SerializeField] private float playerSpeed;
        [SerializeField] private float shieldDuration;

        public static Player Instance { get; private set; }

        public readonly IntReactiveProperty lifes = new(5);
        // private const int maxLives = 5;

        private Instant shieldTill;
        public int score;

        public bool IsActive {
            set {
                gameObject.SetActive(value);
                ResetPosition();
            }
        }

        public event Action interactEvent;

        private CooldownTimer fireCooldown = new(.1f);


        private PlayerInput playerInput;
        private InputAction moveAction;
        private InputAction slowdownAction;
        // private InputAction bombAction;

        private void Start() {
            Instance = this;
            playerInput = GetComponent<PlayerInput>();

            moveAction = playerInput.actions["move"];
            slowdownAction = playerInput.actions["slowdown"];
            // bombAction = playerInput.actions["bomb"];

            IsActive = true;
        }

        private void Update() {
            UpdateMovement();
            ClipCoords();

            FireLogic();
        }

        private void FireLogic() {
            fireCooldown.Tick();

            while (fireCooldown.IsReady()) {
                var deltas = new[] {
                    new Vector3(-.3f, .1f),
                    new Vector3(-.15f, .3f),
                    new Vector3(.15f, .3f),
                    new Vector3(.3f, .1f)
                };
                foreach (var delta in deltas) Instantiate(bullet, transform.position + delta, default);
            }
        }

        private void UpdateMovement() {
            var isSlowDown = slowdownAction.ReadValue<float>() > .5f;
            var factor = moveAction.ReadValue<Vector2>().normalized * (Time.deltaTime * playerSpeed);
            if (isSlowDown) factor /= 2;
            transform.position += new Vector3(factor.x, factor.y, 0);
        }

        private void ClipCoords() {
            var pos = transform.position;
            if (pos.x < SharedData.x_min) pos.x = SharedData.x_min;
            if (pos.x > SharedData.x_max) pos.x = SharedData.x_max;
            if (pos.y < SharedData.y_min) pos.y = SharedData.y_min;
            if (pos.y > SharedData.y_max) pos.y = SharedData.y_max;
            transform.position = pos;
        }

        public void ResetPosition() {
            transform.position = SharedData.GetPos(.5f, .1f);
        }

        public void Hit() {
            if (shieldTill.Elapsed) return;
            shieldTill = new Instant(shieldDuration);
            lifes.Value--;

            ResetPosition();

            if (lifes.Value <= 0) {
                SceneManager.LoadScene("GameOver/GameOverScene");
            }
        }

        private void OnInteract() {
            interactEvent?.Invoke();
        }
    }
}
