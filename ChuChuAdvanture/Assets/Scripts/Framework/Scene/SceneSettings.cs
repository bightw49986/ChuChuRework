using System;
using UnityEngine;
using ChuChu.Framework.SceneObject;
using ChuDebug;

namespace ChuChu.Framework.Scene
{
    /// <summary>
    /// Store what should be prepare while loading the scene.
    /// </summary>
    [CreateAssetMenu(menuName = "ChuChu/Scene/SceneSettings")]
    [Debug(EDebugType.Scene)]
    public class SceneSettings : ScriptableObject
    {
        [Header("Assigned Scene")]
        [Tooltip("Assign specific Scene to load.")]public EScene SceneID; 

        [Header("Configurations")]
        [Tooltip("Assign specific UIConfig to load.")] public UISetting UISetting; //UI物件在讀取後就被放置在指定的Canvas (暫時不考慮未來是否UI亂飛會換Canvas)
        [Tooltip("Assign specific DynamicObject Setting to load.")] public DynamicObjectSetting DynamicObjectSetting; //動態物件讀取後統一放在Manager的物件下當子物件
        [Tooltip("Assign specific FXConfig to load.")] public FXSetting FXSetting; //特效觸發前統一放在同一物件上，觸發後需歸還
        [Tooltip("Assign specific MonsterConfig to load.")] public NPCSetting NPCSetting; //怪物由Manager管理，死亡後需reset回初始狀(用Entity pattern 使怪物讀取設定值之後再存成prefab)

        [Header("System Requirement")]
        [Tooltip("Does this scene require Physic system?")]public bool Physic;
        [Tooltip("Does player appear in this scene?")] public bool Player;
        [Tooltip("Does this scene have any NPC?")] public bool NPC;
        [Tooltip("Does this scene have any FX usage?")] public bool FXSystem;
        [Tooltip("Does this scene require any Dynamic object?")] public bool DynamicObject;

        private void OnEnable()
        {
#if UNITY_EDITOR
            AutoGetID();
#endif
        }
        private void AutoGetID() //簡單的自動找Scene功能，如果你取名時就符合場景名稱，會自動把ID設過去
        {
            foreach (var e in (EScene[])Enum.GetValues(typeof(EScene)))
            {
                if (e.ToString() == name)
                {
                    SceneID = e;
                    return;
                }
            }
        }
    }


}

