using System;
using System.Collections.Generic;
using Game.BossAttacks;


namespace Game {
    public static class BossPhaseFactory {
        public static List<IBossAttack> Get(string name, int phase) {
            return (name, phase) switch {
                ("Marisa", -1) => GetTest(),
                ("Marisa", 0) => GetMarisa0(),
                _ => throw new Exception("ДАУН ЧТО ТЫ ТВОРИШЬ")
            };
        }

        private static List<IBossAttack> GetTest() => new() {
            new BossMove((.5f, .7f), 1),
            new Marisa6(),
            new BossWait(10f)
        };

        private static List<IBossAttack> GetMarisa0() => new() {
            new BossMove((.5f, .6f), 2),
            new BossWait(.2f),
            new Marisa0(),

            new BossMove((.5f, .9f), 1),
            new BossWait(1),
            new Marisa1(),

            new BossMove((.5f, .9f), 1),
            new BossWait(1),
            new Marisa2(),

            new BossMove((.5f, .8f), 0),
            new BossWait(1.5f),
            new Marisa0(10, true, cooldown: .2f, deltaDeg: 170f),

            new BossMove((.5f, .2f), .5f),
            new BossWait(.5f),
            new Marisa0(9, cooldown: .2f, deltaDeg: 170f),

            new BossWait(4)
        };
    }
}