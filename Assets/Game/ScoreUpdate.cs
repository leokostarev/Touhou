using Game;
using TMPro;
using UnityEngine;

public class ScoreUpdate : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI text;


    private void Update() {
        text.text = Player.instance.score.ToString();
    }
}