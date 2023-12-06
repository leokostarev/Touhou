using System;
using DG.Tweening;
using Game.Events;
using UnityEngine;

namespace Game.BossAttacks {
    public class BossMove : AbsBossAttack {
        private Vector3 targetPos;
        private float duration;

        public BossMove(Vector3 targetPos, float duration) {
            this.targetPos = targetPos;
            this.duration = duration;
        }

        public BossMove((float, float) pos, float duration) {
            this.targetPos = ShareData.getPos(pos.Item1, pos.Item2);
            this.duration = duration;
        }

        public override void Begin() {
            FightEvent.boss.transform.DOMove(targetPos, duration).OnComplete(() => callback());
        }
    }
}