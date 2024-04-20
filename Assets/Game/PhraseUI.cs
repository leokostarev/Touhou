using Game.Events;
using TMPro;
using UnityEngine;

namespace Game {
    public class PhraseUI : MonoBehaviour {
        [HideInInspector] public float endPosY;

        [HideInInspector] public float beginPosY;


        private void Awake() {
            TalkEvent.text = GetComponent<TextMeshProUGUI>();
            SharedData.PharasePanel = this;
            beginPosY = transform.position.y;
            endPosY = -beginPosY;
        }
    }
}