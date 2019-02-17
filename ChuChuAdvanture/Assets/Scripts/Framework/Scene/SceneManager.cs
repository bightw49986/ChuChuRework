using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using ChuChu.Framework.UI;

namespace ChuChu.Framework.Scene
{
    public enum ChuChuScene {None = 0,Title,Loading,Game}

    /// <summary>
    /// Manager for scene management.
    /// </summary>
    public sealed class SceneManager
    {
        #region Singleton
        private static readonly SceneManager _INSTANCE = new SceneManager();
        public static SceneManager Instance { get { return _INSTANCE; } }
        #endregion

        #region Members
        private SceneSettingConfiguration m_SceneConfig;
        #endregion

        #region Properties
        public string ActiveScene { get { return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name; } }
        public ChuChuScene CurrentScene { get { return GetSceneByName(ActiveScene); } }
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
        public void LoadScene(ChuChuScene scene)
        {
            SceneSettings sceneSetting = LoadSceneSetting(scene);
            if (Main.Instance.IsFirstEnterApp)
            {
                //讀UI
                if (sceneSetting.UISetting != null && sceneSetting.UISetting.IsEmpty == false)
                {
                    foreach (var obj in sceneSetting.UISetting.ObjectSets)
                    {
                        for (int i = 0; i < obj.Amount - 1; i++)
                        {
                            GameObject uiManager;
                            GameObject go;
                            if (uiManager = GameObject.Find(StringTable.UI.RootCanvasPath))
                                go = UnityEngine.Object.Instantiate(obj.Prefab.GameObject, uiManager.transform);
                            else
                            {
                                go = UnityEngine.Object.Instantiate(obj.Prefab.GameObject, UIManager.Instance.transform);
                            }
                            go.SetActive(obj.ActiveOnCreate);
                        }
                    }
                }
                //讀動態物件
                //讀特效
                if (sceneSetting.FXSetting != null && sceneSetting.FXSetting.IsEmpty == false)
                {
                    foreach (var obj in sceneSetting.FXSetting.ObjectSets)
                    {
                        for (int i = 0; i < obj.Amount - 1; i++)
                        {
                        }
                    }
                }
                //讀怪物
                //讀玩家
                //讀Boss
                //啟動其他東東
            }
            else
            {
                //比較當前場景
                //計算工作
                //批次讀取
            }

        }
        #endregion

        #region Private Methods
        private SceneSettings LoadSceneSetting(ChuChuScene scene)
        {
            switch (scene)
            {
                case ChuChuScene.Title:
                    return m_SceneConfig.Title;
                case ChuChuScene.Loading:
                    return m_SceneConfig.Loading;
                case ChuChuScene.Game:
                    return m_SceneConfig.Game;

                default:
                    return m_SceneConfig.Default;
            }
        }

        public ChuChuScene GetSceneByName(string name)
        {
            foreach (var e in (ChuChuScene[])Enum.GetValues(typeof(ChuChuScene)))
            {
                if (e.ToString() == name)
                {
                    return e;
                }
            }
            return ChuChuScene.None;
        }
        #endregion




    }
}
