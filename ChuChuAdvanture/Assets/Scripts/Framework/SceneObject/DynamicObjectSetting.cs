using ChuDebug;
using UnityEngine;

namespace ChuChu.Framework.SceneObject
{
    [CreateAssetMenu(menuName = "ChuChu/Object Setting/DynamicObject")]
    [Debug(EDebugType.SceneObject)]
    public class DynamicObjectSetting : ObjectSetting
    {
        internal override EObjectType Type { get { return EObjectType.DynamicSceneObj; } }
    }
}

