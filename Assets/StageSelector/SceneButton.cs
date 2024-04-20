using UnityEngine;
using UnityEngine.SceneManagement;

namespace StageSelector {
    public class SceneButton : MonoBehaviour {
        public void Click(int n) {
            SharedData.chapterNo = n;
            SceneManager.LoadScene("Game/GameScene");
        }
    }
}