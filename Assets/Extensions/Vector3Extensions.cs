using UnityEngine;

namespace Extensions {
    public static class Vector3Extensions {
        public static float DistanceTo(this Vector3 a, Vector3 b) {
            return (a - b).magnitude;
        }
    }
}
