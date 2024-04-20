using Game;
using UnityEngine;

public static class SharedData {
    public static int chapterNo = -1;

    public const float x_min = .5f;
    public const float y_min = .5f;
    public const float x_max = 7.5f;
    public const float y_max = 8.5f;

    public static GameObject bullet0;
    public static GameObject laser0;
    public static GameObject saw0;
    public static GameObject boss;
    
    public static PhraseUI PharasePanel;

    public static void RLog(object obj, float t = 15f) {
        if (Random.value < 1 / t) Debug.Log(obj);
    }


    public static bool IsInBounds(Vector3 pos, float padding = 0) {
        return pos.x > x_min - padding &&
               pos.x < x_max + padding &&
               pos.y > y_min - padding &&
               pos.y < y_max + padding;
    }

    public static Vector3 GetPos(float x, float y) {
        return new Vector3(
            Mathf.Lerp(x_min, x_max, x),
            Mathf.Lerp(y_min, y_max, y)
        );
    }

    public static float GetPosX(float t) {
        return Mathf.Lerp(x_min, x_max, t);
    }

    public static float GetPosY(float t) {
        return Mathf.Lerp(y_min, x_max, t);
    }
}