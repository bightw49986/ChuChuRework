using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChuChu.Framework.SceneManagement;


namespace ChuChu.Framework
{
    /// <summary>
    /// The main class for the app, entry point for all systems.
    /// </summary>
    public class CMain : MonoBehaviour
    {
        public static CMain Instance;

        public bool IsFirstEnterApp { get { return mIsFirstEnterApp; } }
        private bool mIsFirstEnterApp = true;

        private void Awake()
        {
            //Singleton.
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                var sceneManager = new CSceneManager();
                var gameEventSystem = new CGameEventSystem();
            }
            else
            {
                Destroy(gameObject);
            }
            //Init all system.
            Init();
        }

        private void Init()
        {
            GameObject go = Resources.Load(StringTable.UIStringTable.RootCanvasPath) as GameObject;
            Instantiate(go);
            CSceneManager.Instance.Init();
            CGameEventSystem.General.OnAppInited();
            mIsFirstEnterApp = false;
        }
    }
}


