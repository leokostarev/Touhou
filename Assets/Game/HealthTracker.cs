using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Game {
    public class HealthTracker : MonoBehaviour {
        [SerializeField] private List<GameObject> lifeStars;
        [SerializeField] private Player player;

        public void Start() {
            player.lifes.Subscribe(value => {
                for (var i = 0; i < lifeStars.Count; i++) lifeStars[i].SetActive(i < value);
            }).AddTo(this);
        }
    }
}