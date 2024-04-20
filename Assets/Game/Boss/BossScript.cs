using System;
using System.Collections.Generic;
using Game.BossAttacks;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Boss {
    public class BossScript : MonoBehaviour {
        [SerializeField] private Image healthBar;
        private List<IBossAttack> attacks;
        private int noAttack;

        public FloatReactiveProperty lifes = new(10000);
        private float maxLifes;

        public event Action onAttacksEnd;
        private IBossAttack nowAttack;

        public void Init(string bossName, int phase) {
            maxLifes = lifes.Value;
            transform.position = SharedData.getPos(.5f, 1.2f);
            attacks = BossPhaseFactory.Get(bossName, phase);
            NextAttack();

            lifes.Subscribe(v => { healthBar.fillAmount = v / maxLifes; }).AddTo(this);
        }


        public void CleanUp() {
            foreach (var i in attacks) {
                i.CleanUp();
            }

            if (!gameObject.IsDestroyed()) Destroy(gameObject);
        }

        private void NextAttack() {
            if (noAttack == attacks.Count) {
                onAttacksEnd?.Invoke();
                Destroy(gameObject);
                return;
            }

            nowAttack = attacks[noAttack];
            noAttack += 1;
            nowAttack.Callback = NextAttack;
            nowAttack.Begin();
        }
    }
}
