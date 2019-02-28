using ChuDebug;
using UnityEngine;

namespace ChuChu.Framework.SceneObject
{
    [CreateAssetMenu(menuName = "ChuChu/Object Setting/UI")]
    [Debug(EDebugType.SceneObject)]
    public class UISetting : ObjectSetting
    {
        internal override EObjectType Type { get { return EObjectType.UI; } }
    }
}

