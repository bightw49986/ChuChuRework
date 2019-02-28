using ChuDebug;
using UnityEngine;

namespace ChuChu.Framework.SceneObject
{
    [CreateAssetMenu(menuName = "ChuChu/Object Setting/NPC")]
    [Debug(EDebugType.SceneObject)]
    public class NPCSetting : ObjectSetting
    {
        internal override EObjectType Type { get { return EObjectType.NPC; } }
    }
}

