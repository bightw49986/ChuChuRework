using System;
using System.Collections.Generic;
using UnityEngine;

namespace ChuChu.Framework.SceneManagement
{
    /// <summary>
    /// Store what should be prepare while loading the scene.
    /// </summary>
    [CreateAssetMenu(menuName = "ChuChu/Scene/SceneSetting")]
    public class SceneSetting : ScriptableObject
    {
        public ChuChuScene ID;
        public List<ObjectConfig> UIConfig;
        public List<ObjectConfig> DynamicSceneObjectConfig;
        public bool ApplyGravity;

        private void OnEnable()
        {
            foreach (var e in (ChuChuScene[])Enum.GetValues(typeof(ChuChuScene)))
            {
                if(e.ToString()==name)
                {
                    ID = e;
                    return;
                }
            }
        }
    }


    [Serializable]
    public struct ObjectConfig
    {
        public GameObject Object;
        public int Amount;
    }
}
