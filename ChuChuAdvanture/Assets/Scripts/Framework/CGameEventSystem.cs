using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChuChu.Framework
{
    //All game event are defined here.
    public sealed class CGameEventSystem
    {
        #region Singleton for every nested class
        public static CGameEventSystem Instance { get { return _Instance; } }
        private static readonly CGameEventSystem _Instance = new CGameEventSystem();

        public static GeneralGameEvent General { get { return _General; } }
        private static readonly GeneralGameEvent _General = new GeneralGameEvent();

        public static SceneLoadingEvent SceneLoading { get { return _SceneLoading; } }
        private static readonly SceneLoadingEvent _SceneLoading = new SceneLoadingEvent();

        public static GravityAndPositioningCallback GravityAndPositioning { get { return _GravityAndPositioning; } }
        private static readonly GravityAndPositioningCallback _GravityAndPositioning = new GravityAndPositioningCallback();

        public static UISystemEvent UI { get { return _UI; } }
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

            public event Action GameStarted; //Triggered when entry animation finished
            public void OnGameStarted() { if (GameStarted != null) GameStarted(); Debug.Log("[Event]: Game started."); }

            public event Action GameFailed; //Triggered when user died
            public void OnGameFailed() { if (GameFailed != null) GameFailed(); Debug.Log("[Event]: Game failed."); }

            public event Action BossDoorTouched; //Triggered by touching the boss door
            public void OnBossDoorTouched() { if (BossDoorTouched != null) BossDoorTouched(); Debug.Log("[Event]: Enter boss room."); }

            public event Action BossFightStarted; //Triggered when boss fight started.
            public void OnBossFightStarted() { if (BossFightStarted != null) BossFightStarted(); Debug.Log("[Event]: Boss fight started."); }

            public event Action GameVictoryed; //Triggered when boss be killed
            public void OnGameVictoryed() { if (GameVictoryed != null) GameVictoryed(); Debug.Log("[Event]: Victoryed."); }

            public event Action VictoryAnimDone; //Triggered when ending animation finished
            public void OnVictoryAnimDone() { if (VictoryAnimDone != null) VictoryAnimDone(); Debug.Log("[Event]: Ending animation done."); }

            public event Action BackToTitle; //Triggered by clicking 開始遊戲(2nd time)
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

        #region Gravity and positioning callback
        public sealed class GravityAndPositioningCallback
        {
            public event Func<bool> GroundedCheckCalled; //Triggered each frame before apply gravity;
            public void GroundedCheck() { if (GroundedCheckCalled != null) GroundedCheckCalled(); }
            public event Action<float> GravityApplyed; //Triggered each frame before any AI logic begin.
            public void ApplyGravity(float gravity) { if (GravityApplyed != null) GravityApplyed(gravity); }
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
    }
}


