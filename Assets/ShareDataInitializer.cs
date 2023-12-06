using UnityEngine;

public class ShareDataInitializer : MonoBehaviour {
    [SerializeField] private GameObject bullet0;
    [SerializeField] private GameObject laser0;
    [SerializeField] private GameObject saw0;
    [SerializeField] private GameObject boss;
    

    private void Awake() {
        ShareData.bullet0 = bullet0;
        ShareData.laser0 = laser0;
        ShareData.saw0 = saw0;
        ShareData.boss = boss;
    }
}