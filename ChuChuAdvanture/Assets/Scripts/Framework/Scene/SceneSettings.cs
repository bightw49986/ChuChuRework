using System;
using System.Collections.Generic;
using UnityEngine;

namespace ChuChu.Framework.Scene
{
    public enum ResourceType
    {
        Other = 0,
        UI,
        DynamicObject,
        FX,
        Monster
    }

    public interface IManageableResource
    {
        ResourceType ObjectFlag { get; }
        GameObject GameObject { get; }

        void Access();
        void Return();
    }

    /// <summary>
    /// Store what should be prepare while loading the scene.
    /// </summary>
    [CreateAssetMenu(menuName = "ChuChu/Scene/SceneSettings")]
    public class SceneSettings : ScriptableObject
    {
        [Header("Assign Scene")]
        [Tooltip("Assign specific Scene to load.")]public ChuChuScene SceneID; 

        [Header("Configurations")]
        [Tooltip("Assign specific UIConfig to load.")] public UISetting UISetting; //UI物件在讀取後就被放置在指定的Canvas (暫時不考慮未來是否UI亂飛會換Canvas)
        [Tooltip("Assign specific DynamicObjectConfig to load.")] public DynamicObjectSetting DynamicObjectSetting; //動態物件讀取後統一放在Manager的物件下當子物件
        [Tooltip("Assign specific FXConfig to load.")] public FXSetting FXSetting; //特效觸發前統一放在同一物件上，觸發後需歸還
        [Tooltip("Assign specific MonsterConfig to load.")] public NPCSetting NPCSetting; //怪物由Manager管理，死亡後需reset回初始狀(用Entity pattern 使怪物讀取設定值之後再存成prefab)

        [Header("System usage")]
        [Tooltip("Does this scene use Gravity system?")]public bool Physic;
        [Tooltip("Does player appears in this scene?")] public bool Player;
        [Tooltip("Does this scene have monsters?")] public bool NPC;
        [Tooltip("Does this scene has any FX usage?")] public bool FXSystem;
        [Tooltip("Does this scene has any Dynamic object usage?")] public bool DynamicObject;

        private void OnEnable()
        {
#if UNITY_EDITOR
            AutoGetID();
#endif
        }

        private void AutoGetID() //簡單的自動找Scene功能，如果你取名時就符合場景名稱，會自動把ID設過去
        {
            foreach (var e in (ChuChuScene[])Enum.GetValues(typeof(ChuChuScene)))
            {
                if (e.ToString() == name)
                {
                    SceneID = e;
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Serializable dictionary structure for the manageable objects.
    /// </summary>
    [Serializable]
    public class ObjectSet
    {
        public IManageableResource Prefab;
        [Min(1)]public int Amount = 1;
        public bool ActiveOnCreate;
    }

    public class ObjectSetting : ScriptableObject
    {
        internal ResourceType ObjectFlag = ResourceType.Other;
        public List<ObjectSet> ObjectSets;
        public bool IsEmpty { get { return ObjectSets.Count <= 0; } }
    }

    [CreateAssetMenu(menuName = "ChuChu/ObjectConfig/UI")]
    public class UISetting : ObjectSetting
    {
        internal new ResourceType ObjectFlag = ResourceType.UI;
    }

    [CreateAssetMenu(menuName = "ChuChu/ObjectConfig/DynamicObject")]
    public class DynamicObjectSetting : ObjectSetting
    {
        internal new ResourceType ObjectFlag = ResourceType.DynamicObject;
    }

    [CreateAssetMenu(menuName = "ChuChu/ObjectConfig/FX")]
    public class FXSetting : ObjectSetting
    {
        internal new ResourceType ObjectFlag = ResourceType.FX;
    }

    [CreateAssetMenu(menuName = "ChuChu/ObjectConfig/Monster")]
    public class NPCSetting : ObjectSetting
    {
        internal new ResourceType ObjectFlag = ResourceType.Monster;
    }
}

