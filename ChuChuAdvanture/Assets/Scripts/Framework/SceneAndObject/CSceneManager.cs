using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using ChuChu.Framework.UI;

namespace ChuChu.Framework.SceneAndObjectManagement
{
    public enum ChuChuScene {None = 0,Title,Loading,Game}

    /// <summary>
    /// Manager for scene management.
    /// </summary>
    public sealed class CSceneManager
    {
        #region Singleton
        private static readonly CSceneManager _INSTANCE = new CSceneManager();
        public static CSceneManager Instance { get { return _INSTANCE; } }
        #endregion

        public string CurrentScene { get { return SceneManager.GetActiveScene().name; } }
        private SceneSetting mDefaultSceneSetting;
        private bool mIsFirstLoading = true;

        #region Life cycle and event listening stuff
        public void Init()
        {
            mDefaultSceneSetting = Resources.Load(StringTable.SceneSettingPath.Default) as SceneSetting;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void Release()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            CGameEventSystem.SceneLoading.OnSceneLoaded(scene);
        }
        #endregion

        void LoadScene(ChuChuScene scene)
        {
            SceneSetting sceneSetting = LoadSceneSetting(scene);
            if(mIsFirstLoading)
            {
                //讀UI
                if(sceneSetting.UIConfig.IsEmpty ==false)
                {
                    foreach (var obj in sceneSetting.UIConfig.ObjectSets)
                    {
                        for (int i = 0; i < obj.Amount - 1; i++)
                        {
                            GameObject parent;
                            GameObject go;
                            if (parent = GameObject.Find(obj.Parent))
                            go = UnityEngine.Object.Instantiate(obj.Prefab.GameObject,parent.transform);
                            else
                            {
                                go = UnityEngine.Object.Instantiate(obj.Prefab.GameObject, CUIManager.UIRoot.transform);
                            }
                            go.SetActive(obj.ActiveOnCreate);
                        }
                    }
                }
                //讀動態物件
                //讀特效
                if(sceneSetting.FXConfig.IsEmpty == false)
                {
                    foreach(var obj in sceneSetting.FXConfig.ObjectSets)
                    {
                        for (int i = 0; i < obj.Amount - 1; i++)
                        {
                            GameObject parent;
                            GameObject go;

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

        SceneSetting LoadSceneSetting(ChuChuScene scene)
        {
            switch (scene)
            {
                case ChuChuScene.Title:
                    return Resources.Load(StringTable.SceneSettingPath.Title) as SceneSetting;
                case ChuChuScene.Loading:
                    return Resources.Load(StringTable.SceneSettingPath.Loading) as SceneSetting;
                case ChuChuScene.Game:
                    return Resources.Load(StringTable.SceneSettingPath.Game) as SceneSetting;

                default:
                    return mDefaultSceneSetting;
            }
        }
    }
}
