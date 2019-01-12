using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ChuChu.Framework.SceneManagement
{
    public enum ChuChuScene {None = 0,Title,Loading,Game}

    /// <summary>
    /// Manager for scene management.
    /// </summary>
    public sealed class CSceneManager
    {
        private static readonly CSceneManager _INSTANCE = new CSceneManager();
        public static CSceneManager Instance { get { return _INSTANCE; } }
        public string CurrentScene { get { return SceneManager.GetActiveScene().name; } }
        private SceneSetting mDefaultSceneSetting;

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

        void LoadScene(ChuChuScene scene)
        {
            SceneSetting sceneSetting = LoadSceneSetting(scene);
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
