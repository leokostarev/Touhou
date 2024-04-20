using UnityEngine;

public class ShareDataInitializer : MonoBehaviour {
    [SerializeField] private GameObject bullet0;
    [SerializeField] private GameObject laser0;
    [SerializeField] private GameObject saw0;
    [SerializeField] private GameObject boss;
    

    private void Awake() {
        SharedData.bullet0 = bullet0;
        SharedData.laser0 = laser0;
        SharedData.saw0 = saw0;
        SharedData.boss = boss;
    }
}