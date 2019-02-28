using ChuDebug;
using UnityEngine;

namespace ChuChu.Framework.SceneObject
{
    [CreateAssetMenu(menuName = "ChuChu/Object Setting/FX")]
    [Debug(EDebugType.SceneObject)]
    public class FXSetting : ObjectSetting
    {
        internal override EObjectType Type { get { return EObjectType.FX; } }
    }
}

