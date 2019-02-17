using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChuChu.Framework.Scene
{
    [CreateAssetMenu(menuName = "ChuChu/Scene/SceneSettingConfiguration")]
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


