using System.Collections;
using System.Collections.Generic;
using ChuChu.Framework.SceneAndObjectManagement;
using UnityEngine;


namespace ChuChu.Framework.FX
{
    /// <summary>
    /// Base class for any kind of FX, provides method that co-work with Unity animator. 
    /// <para></para>
    /// Should define every event that includes in the whole lifetime of the FX so the FX system will automaticlly subscribe them.
    /// <para></para>
    /// Remember to serialize the child class so that you can manage the callbacks in the inspector.
    /// </summary>
    public class FXController : MonoBehaviour,IManageableObject
    {
        public List<FXController> SubControllers;

        public ObjectFlag ObjectFlag { get { return ObjectFlag.FX; }}
        public GameObject GameObject { get { return gameObject; }}

        public void FindSubControllers()
        {
            if (SubControllers == null) SubControllers = new List<FXController>();
            var controllers = GetComponentsInChildren<FXController>();
            if (controllers.Length <= 0) { Debug.LogWarning(name + " loads a gameobject that doesn't have any FX Controller."); return; }
            foreach (var c in controllers)
            {
                if (SubControllers.Contains(c) == false)
                    SubControllers.Add(c);
            }
        }

        public void Access()
        {

        }

        public void Return()
        {

        }
    }
}

