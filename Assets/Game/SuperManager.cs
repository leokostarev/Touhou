using System.Collections.Generic;
using Game.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game {
    public class SuperManager : MonoBehaviour {
        private List<IScriptEvent> scriptList;

        private int nextEventNo;
        private bool isEventActive;
        private IScriptEvent nowEvent;

        private void OnDestroy() {
            foreach (var i in scriptList)
                i.CleanUp();
        }

        private void NextEvent() {
            if (nextEventNo == scriptList.Count) {
                CompleteChapter();
                return;
            }

            isEventActive = true;
            nowEvent = scriptList[nextEventNo];
            nextEventNo++;
            nowEvent.onEnd += NextEvent;
            nowEvent.Begin();
        }

        private void Start() {
            scriptList = ScriptFactory.Get(SharedData.chapterNo);
            NextEvent();
        }

        private void Update() {
            if (isEventActive)
                nowEvent.Update();
        }

        private static void CompleteChapter() {
            SceneManager.LoadScene("MainMenu/MainMenuScene");
        }
    }
}