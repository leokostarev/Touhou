using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu {
    public class PlayButton : MonoBehaviour {
        public void Click() {
            SceneManager.LoadScene("StageSelector/StageSelectorScene");
        }
    }
}