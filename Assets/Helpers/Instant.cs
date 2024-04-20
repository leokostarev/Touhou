using System.Runtime.CompilerServices;
using UnityEngine;

namespace Helpers {
    public readonly struct Instant {
        private readonly float time;

        public bool Elapsed => Time.time >= time;

        public float ElapsedBy => Time.time - time;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Instant(float delta) {
            time = Time.time + delta;
        }
    }
}
