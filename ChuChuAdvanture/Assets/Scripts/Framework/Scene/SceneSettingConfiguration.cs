using System.Collections;
using System.Collections.Generic;
using ChuDebug;
using UnityEngine;

namespace ChuChu.Framework.Scene
{
    [CreateAssetMenu(menuName = "ChuChu/Scene/SceneSettingConfiguration")]
    [Debug(EDebugType.Scene)]
    public class SceneSettingConfiguration : ScriptableObject
    {
        #region Members
        [Header("Scene settings for each scene.")]
        public SceneSettings Title;
        public SceneSettings Loading;
        public SceneSettings Game;

        [Header("Default scene setting for developing scenes.")]
        public SceneSettings Default;
        #endregion
    }
}


