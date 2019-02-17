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
        private SceneSetting m_DefaultSceneSetting;
        #endregion

        #region Properties
        public string CurrentScene { get { return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name; } }
        #endregion

        #region Life cycle and event stuff
        public void Init()
        {
            m_DefaultSceneSetting = Resources.Load(StringTable.SceneSetting.Default) as SceneSetting;
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
            SceneSetting sceneSetting = LoadSceneSetting(scene);
            if (Main.Instance.IsFirstEnterApp)
            {
                //讀UI
                if (sceneSetting.UIConfig != null && sceneSetting.UIConfig.IsEmpty == false)
                {
                    foreach (var obj in sceneSetting.UIConfig.ObjectSets)
                    {
                        for (int i = 0; i < obj.Amount - 1; i++)
                        {
                            GameObject uiManager;
                            GameObject go;
                            if (uiManager = GameObject.Find(StringTable.UI.RootCanvasPath))
                                go = UnityEngine.Object.Instantiate(obj.Prefab.GameObject, uiManager.transform);
                            else
                            {
                                go = UnityEngine.Object.Instantiate(obj.Prefab.GameObject, UIManager.UIRoot.transform);
                            }
                            go.SetActive(obj.ActiveOnCreate);
                        }
                    }
                }
                //讀動態物件
                //讀特效
                if (sceneSetting.FXConfig != null && sceneSetting.FXConfig.IsEmpty == false)
                {
                    foreach (var obj in sceneSetting.FXConfig.ObjectSets)
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
        private SceneSetting LoadSceneSetting(ChuChuScene scene)
        {
            switch (scene)
            {
                case ChuChuScene.Title:
                    return Resources.Load(StringTable.SceneSetting.Title) as SceneSetting;
                case ChuChuScene.Loading:
                    return Resources.Load(StringTable.SceneSetting.Loading) as SceneSetting;
                case ChuChuScene.Game:
                    return Resources.Load(StringTable.SceneSetting.Game) as SceneSetting;

                default:
                    return m_DefaultSceneSetting;
            }
        }
        #endregion




    }
}
