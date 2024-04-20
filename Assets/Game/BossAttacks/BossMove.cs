using System;
using DG.Tweening;
using Game.Events;
using UnityEngine;

namespace Game.BossAttacks {
    public class BossMove : IBossAttack {
        public Action Callback { get; set; }

        private readonly Vector3 targetPos;
        private readonly float duration;

        public BossMove(Vector3 targetPos, float duration) {
            this.targetPos = targetPos;
            this.duration = duration;
        }

        public BossMove((float, float) pos, float duration_) {
            targetPos = SharedData.GetPos(pos.Item1, pos.Item2);
            duration = duration_;
        }

        public void Begin() {
            FightEvent.boss.transform.DOMove(targetPos, duration).OnComplete(() => Callback());
        }
    }
}
