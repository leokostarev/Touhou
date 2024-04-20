using System.Runtime.CompilerServices;
using UnityEngine;

namespace Helpers {
    public struct CooldownTimer {
        private float accumulatedTime;
        private readonly float cooldown;

        public CooldownTimer(float cooldown_, float accumulatedTime_ = 0) {
            accumulatedTime = accumulatedTime_;
            cooldown = cooldown_;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Tick() {
            // TODO: get rid of explicit method, storing previous execution time (maybe)
            accumulatedTime += Time.deltaTime;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void TickNotLinear(float timeScale) {
            accumulatedTime += Time.deltaTime * timeScale;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsReady() {
            if (accumulatedTime < cooldown) {
                return false;
            }

            accumulatedTime -= cooldown;
            return true;
        }
    }
}
