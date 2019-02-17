using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChuChu.Framework
{
    //All game event are defined here.
    public sealed class GameEventSystem
    {
        #region Singleton

        public static GeneralGameEvent Instance_General { get { return _General; } }
        private static readonly GeneralGameEvent _General = new GeneralGameEvent();

        public static SceneLoadingEvent Instance_SceneLoading { get { return _SceneLoading; } }
        private static readonly SceneLoadingEvent _SceneLoading = new SceneLoadingEvent();

        public static GravityAndGroundCheckCallback Instance_GravityAndGroundCheck { get { return _GravityAndGroundCheck; } }
        private static readonly GravityAndGroundCheckCallback _GravityAndGroundCheck = new GravityAndGroundCheckCallback();

        public static UISystemEvent Instance_UI { get { return _UI; } }
        private static readonly UISystemEvent _UI = new UISystemEvent();

        #endregion
        #region General game event
        public sealed class GeneralGameEvent
        {
            public event Action AppInited; //Triggered when app start
            public void OnAppInited() { if (AppInited != null) AppInited();Debug.Log("[Event]: Game Initialized."); }

            public event Action BeginLoading; //Triggered by clicking 開始遊戲
            public void OnBeginLoading() { if (BeginLoading != null) BeginLoading(); Debug.Log("[Event]: Begin Loading..."); }

            public event Action FinishedLoading; //Triggered by finish loading game scene 90 %
            public void OnFinishedLoading() { if (FinishedLoading != null) FinishedLoading(); Debug.Log("[Event]: Finish Loading."); }

            public event Action EnteredGameScene; //Triggered by clicking 按任意鍵繼續
            public void OnEnteredGameScene() { if (EnteredGameScene != null) EnteredGameScene(); Debug.Log("[Event]: Entered main scene."); }

            public event Action OpeningAnimFinished; //Triggered when entry animation finished
            public void OnOpeningAnimFinished() { if (OpeningAnimFinished != null) OpeningAnimFinished(); Debug.Log("[Event]: Game started."); }

            public event Action PlayerDied; //Triggered when user died
            public void OnPlayerDied() { if (PlayerDied != null) PlayerDied(); Debug.Log("[Event]: Game failed."); }

            public event Action BossDoorTriggered; //Triggered by touching the boss door
            public void OnBossDoorTriggered() { if (BossDoorTriggered != null) BossDoorTriggered(); Debug.Log("[Event]: Enter boss room."); }

            public event Action BossFightStarted; //Triggered when boss fight started.
            public void OnBossFightStarted() { if (BossFightStarted != null) BossFightStarted(); Debug.Log("[Event]: Boss fight started."); }

            public event Action BossDied; //Triggered when boss be killed
            public void OnBossDied() { if (BossDied != null) BossDied(); Debug.Log("[Event]: Victoryed."); }

            public event Action VictoryAnimFinished; //Triggered when ending animation finished
            public void OnVictoryAnimFinished() { if (VictoryAnimFinished != null) VictoryAnimFinished(); Debug.Log("[Event]: Ending animation done."); }

            public event Action BackToTitle; //Triggered when back to title
            public void OnBackToMenu() { if (BackToTitle != null) BackToTitle(); Debug.Log("[Event]: Back to title."); }
        }
        #endregion

        #region Load scene event
        public sealed class SceneLoadingEvent
        {
            public event Action<UnityEngine.SceneManagement.Scene> SceneLoaded;
            public void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene) { if (SceneLoaded != null) SceneLoaded(scene); Debug.Log("[Event][Load scene]: Scene has changed to: " + scene.name); }
        }
        #endregion

        #region Gravity and GroundCheck
        public sealed class GravityAndGroundCheckCallback
        {
            public event Action GrounCheckBegan;
            public void OnGrounCheckBegan() { if (GrounCheckBegan != null) GrounCheckBegan(); }
            public event Action GroundCheckStopped;
            public void OnGroundCheckStop() { if (GroundCheckStopped != null) GroundCheckStopped(); }
            public event Action GroundCheckApplyed;
            public void OnGroundCheckApplyed() { if (GroundCheckApplyed != null) GroundCheckApplyed(); }
            public event Action<Vector3> GravityApplyed;
            public void OnGravityApplyed(Vector3 gravity) { if (GravityApplyed != null) GravityApplyed(gravity); }
        }
        #endregion

        #region UI system event
        public sealed class UISystemEvent
        {
            public event Action ShowAllCalled;
            public void OnShowAllCalled() { if (ShowAllCalled != null) ShowAllCalled(); }
            public event Action HideAllCalled;
            public void OnHideAllCalled() { if (HideAllCalled != null) HideAllCalled(); }
        }
        #endregion

        public void Init()
        {
            Instance_General.OnAppInited();
        }
    }
}


