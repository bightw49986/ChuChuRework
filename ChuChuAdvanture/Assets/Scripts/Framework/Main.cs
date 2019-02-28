using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChuChu.Framework.Scene;
using ChuChu.Framework.UI;
using ChuDebug;


namespace ChuChu.Framework
{
    /// <summary>
    /// The main class for the app, entry point for all systems.
    /// </summary>
    [Debug(EDebugType.Main)]
    public class Main : MonoBehaviour
    {
        #region Members
        [HideInInspector] public static Main Instance;
        [SerializeField][Tooltip("Assign scene config to tell SceneManager what setting should it apply for every scene.")]private SceneSettingConfiguration m_SceneConfig;
        #endregion

        #region Properties
        public bool IsFirstEnterApp { get; private set; }
        #endregion

        #region MonoBehaviors
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
        private void Start()
        {
            UIManager.ScreenFadeIn(() => { });
        }
        #endregion

        #region Public Methods

        #endregion

        #region Private Methods
        private void InitializeAllSystems()
        {
            InitSceneManager();
            InitGameEventSystem();
            InitUIManager();
            IsFirstEnterApp = false;
        }

        //Init SceneManager
        private bool InitSceneManager()
        {
            if (m_SceneConfig == null)
            {
                m_SceneConfig = Resources.Load(StringTable.SceneConfig.DefaultConfig) as SceneSettingConfiguration;
                if (m_SceneConfig == null)
                {
                    Debug.LogError("Can't load default scene config!! Check if path was changed.");
                    return false;
                }
            }
            SceneManager sceneManager = new SceneManager(); //SceneManager
            sceneManager.Init(m_SceneConfig);
            return true;
        }

        //Init GameEventSystem
        private bool InitGameEventSystem()
        {
            GameEventSystem gameEventSystem = new GameEventSystem(); //GameEventSystem
            gameEventSystem.Init();
            return true;
        }

        //Init UIManager
        private bool InitUIManager()
        {
            GameObject uiManager = Resources.Load(StringTable.UI.UIManager) as GameObject; //UIManager
            Instantiate(uiManager);
            return true;
        }

        #endregion
    }
}


