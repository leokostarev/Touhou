using System;
using System.Collections.Generic;
using Game.Events;
using static Game.Events.TalkEvent.DialogMove;

namespace Game {
    public static class ScriptFactory {
        public static List<IScriptEvent> Get(int n) => n switch {
            -1 => ScriptTest(),
            1 => Script1(),
            2 => throw new Exception("ПИШИ СЮЖЕТ"),
            3 => throw new Exception("ПИШИ СЮЖЕТ"),
            4 => throw new Exception("ПИШИ СЮЖЕТ"),
            666 => ScriptExtra(),
            _ => throw new Exception("ТЫ ДАУНИЩЕ")
        };

        private static List<IScriptEvent> ScriptTest() => new() {
            new WaitEvent(1),
            // new TalkEvent("Reimu", "Отдай пиво!", panelMove: ShowAndHide),
            new FightEvent("Marisa", -1)
        };

        private static List<IScriptEvent> Script1() => new() {
            new WaitEvent(1),

            new TalkEvent("Reimu", "Отдай пиво!", panelMove: Show),
            new TalkEvent("Marisa", "Неееееет!!!!!"),
            new TalkEvent("Reimu", "Тогда я тебя заставлю", characterMove: Show),
            new TalkEvent("Reimu", "Ты у меня попляшешь", characterMove: Hide),
            new TalkEvent("Marisa", "А ты попробуй", panelMove: Hide),

            new FightEvent("Marisa", 0),

            new TalkEvent("Reimu", "Ахахаха какая ты изичная", panelMove: Show),
            new TalkEvent("Marisa", "Хнык хнык"),
            new TalkEvent("Reimu", "Ладно, хоть ты и украла у меня пиво, я с тобой поделюсь"),
            new TalkEvent("Marisa", "Спасибо"),
            new TalkEvent("Reimu", "Держи", panelMove: Hide)
        };

        private static List<IScriptEvent> ScriptExtra() => new() {
            new WaitEvent(1),

            new TalkEvent("Cirno", "Я была на вечеринке у Рейму, но потерялась(((", panelMove: Show),
            new TalkEvent("", "Тем временем в храме Хакурей:"),
            new TalkEvent("Reimu", "Куда-то делась Чирно. Хммм. Она явно перебрала."),
            new TalkEvent("Marisa", "Надо пойти её искать. А то она потеряется насовсем."),
            new TalkEvent("", "Через час"),
            new TalkEvent("Reimu", "Ты что еще за черт?"),

            new TalkEvent("Ben", ".", characterMove: Show),
            new TalkEvent("Ben", "...", characterMove: NoMove),
            new TalkEvent("Ben", ".....", characterMove: NoMove),
            new TalkEvent("Ben", "Убью каждого", characterMove: NoMove),
            new TalkEvent("Ben", "Я сама неотвратимость", characterMove: Hide),
            new TalkEvent("Sergionis", "Я нашел тебя Бен! Это просто жесть, СУПРИМ БАНДА!!!"),
            new TalkEvent("Ben", "В этот раз тебе не уйти"),
            new TalkEvent("Sergionis", "Давай попробуй, у меня твоя кукла вуду"),

            new TalkEvent("Marisa", "Чё это было"),
            new TalkEvent("Reimu", "В последнее время в нашем лесу стали появлятся странные сумасшедшие"),
            new TalkEvent("Marisa", "Ну нафиг"),
            new TalkEvent("Reimu", "Просто забей, пойдём дальше")

            // new FightEvent("")
        };
    }
}
