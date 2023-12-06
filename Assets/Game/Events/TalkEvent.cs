using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.Events {
    public class TalkEvent : IScriptEvent {
        public enum DialogMove {
            NoMove,
            Show,
            Hide,
            ShowAndHide
        }


        private const float transitionTime = .4f;

        private readonly string name;
        private readonly string phrase;
        private readonly DialogMove characterMove;
        private readonly DialogMove panelMove;
        public event Action onEnd;

        private float endAt;
        private bool doEnd;
        private Tween _tween;

        public static readonly Dictionary<string, DialogPicture> pictures = new();
        public static TextMeshProUGUI text;

        public TalkEvent(string name, string phrase, DialogMove characterMove = DialogMove.ShowAndHide,
            DialogMove panelMove = DialogMove.NoMove) {
            this.name = name;
            this.phrase = phrase;
            this.characterMove = characterMove;
            this.panelMove = panelMove;
        }


        public void CleanUp() {
        }

        public void Begin() {
            if (characterMove is DialogMove.Show or DialogMove.ShowAndHide && name != "")
                _tween = pictures[name].transform.DOMove(pictures[name].endPos, transitionTime);
            text.text = phrase;
            Player.instance.interactEvent += End;
            if (panelMove is DialogMove.Show or DialogMove.ShowAndHide)
                EnablePanel();
        }

        private void End() {
            _tween?.Kill();
            Player.instance.interactEvent -= End;
            if (characterMove is DialogMove.Hide or DialogMove.ShowAndHide && name != "")
                pictures[name].transform.DOMove(pictures[name].basePos, transitionTime);
            if (panelMove is DialogMove.Hide or DialogMove.ShowAndHide)
                DiasblePanel();
            doEnd = true;
            endAt = Time.time + transitionTime;
        }

        public void Update() {
            if (doEnd && Time.time > endAt)
                onEnd!.Invoke();
        }

        private static void EnablePanel() =>
            ShareData.PharasePanel.transform.DOMoveY(ShareData.PharasePanel.endPosY, transitionTime);

        private static void DiasblePanel() =>
            ShareData.PharasePanel.transform.DOMoveY(ShareData.PharasePanel.beginPosY, transitionTime);
    }
}