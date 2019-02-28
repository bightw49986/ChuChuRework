using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ChuChu.Framework.UI;
using ChuDebug;

namespace ChuChu.Framework.Scene
{
    /// <summary>
    /// Manager for scene management.
    /// </summary>
    [Debug(EDebugType.Scene)]
    public sealed class SceneManager
    {
        #region Singleton
        private static readonly SceneManager _INSTANCE = new SceneManager();
        public static SceneManager Instance { get { return _INSTANCE; } }
        #endregion

        #region Members
        private SceneSettingConfiguration m_SceneConfig;
        private SceneSettings m_CurrentSetting;
        #endregion

        #region Properties
        public string ActiveScene { get { return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name; } }
        public EScene CurrentScene { get { return GetSceneByName(ActiveScene); } }
        #endregion

        #region Life cycle and event stuff
        public void Init(SceneSettingConfiguration config)
        {
            m_SceneConfig = config;
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void Depose()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
        {
            GameEventSystem.Instance_SceneLoading.OnSceneLoaded(scene);
        }
        #endregion

        #region Public Methods
        public void LoadScene(EScene scene)
        {
            SceneSettings newSetting = LoadSceneSetting(scene);
            if (!Main.Instance.IsFirstEnterApp)
            {
                CompareCurrentSettings(newSetting);
            }
            m_CurrentSetting = newSetting;
            CalculateTask(m_CurrentSetting);
            LoadTask();
        }
        public EScene GetSceneByName(string name)
        {
            foreach (var e in (EScene[])Enum.GetValues(typeof(EScene)))
            {
                if (e.ToString() == name)
                {
                    return e;
                }
            }
            return EScene.None;
        }
        #endregion

        #region Private Methods
        private SceneSettings LoadSceneSetting(EScene scene)
        {
            switch (scene)
            {
                case EScene.Title:
                    return m_SceneConfig.Title;
                case EScene.Loading:
                    return m_SceneConfig.Loading;
                case EScene.Game:
                    return m_SceneConfig.Game;

                default:
                    return m_SceneConfig.Default;
            }
        }

        private void CompareCurrentSettings(SceneSettings newSetting)
        {

        }

        private void CalculateTask(SceneSettings newSetting)
        {

        }

        private void LoadTask()
        {

        }
        #endregion
    }
}
