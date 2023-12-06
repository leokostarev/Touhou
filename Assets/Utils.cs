using UnityEngine;

public static class RandomH {
    public static float plusMinus => Random.value > .5f ? -1 : 1;
}