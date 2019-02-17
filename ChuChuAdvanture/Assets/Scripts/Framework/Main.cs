using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChuChu.Framework.Scene;


namespace ChuChu.Framework
{
    /// <summary>
    /// The main class for the app, entry point for all systems.
    /// </summary>
    public class Main : MonoBehaviour
    {
        public static Main Instance;

        public bool IsFirstEnterApp { get { return mIsFirstEnterApp; } }
        private bool mIsFirstEnterApp = true;

        private void Awake()
        {
            //Singleton.
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            InitializeAllSystems();
        }

        private void InitializeAllSystems()
        {
            SceneManager sceneManager = new SceneManager(); //SceneManager
            sceneManager.Init();
            GameEventSystem gameEventSystem = new GameEventSystem(); //GameEventSystem
            gameEventSystem.Init();
            GameObject uiManager = Resources.Load(StringTable.UI.RootCanvasPath) as GameObject; //UIManager
            Instantiate(uiManager);
            mIsFirstEnterApp = false;
        }
    }
}


