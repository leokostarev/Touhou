using Game.Events;
using UnityEngine;

namespace Game {
    public class DialogPicture : MonoBehaviour {
        [SerializeField] private string picName;
        [SerializeField] public Vector3 endPos;
        [HideInInspector] public Vector3 basePos;


        private void Awake() {
            TalkEvent.pictures.Add(picName, this);
            basePos = transform.position;
        }

        private void OnDestroy() {
            TalkEvent.pictures.Clear(); // на всякий))))))
        }
    }
}