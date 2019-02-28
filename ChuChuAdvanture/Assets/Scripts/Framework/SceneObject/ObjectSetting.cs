using System.Collections.Generic;
using UnityEngine;
using System;
using ChuDebug;

namespace ChuChu.Framework.SceneObject
{
    /// <summary>
    /// Serializable dictionary structure for the manageable objects.
    /// </summary>
    [Serializable]
    [Debug(EDebugType.SceneObject)]
    public class ObjectSet
    {
        public IManageableResource Prefab;
        [Min(1)] public int Amount = 1;
        public bool ActiveOnCreate;
    }

    public abstract class ObjectSetting : ScriptableObject
    {
        internal abstract EObjectType Type { get; }
        public List<ObjectSet> ObjectSets;
        public bool IsEmpty { get { return ObjectSets.Count <= 0; } }
    }
}

