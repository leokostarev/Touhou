using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameOver {
    public class MainSceneButton : MonoBehaviour {
        public void Click() {
            SceneManager.LoadScene("MainMenu/MainMenuScene");
        }
    }
}