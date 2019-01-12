using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ChuChu.Framework.FX
{
    public abstract class FXEvent : ScriptableObject
    {
        [SerializeField]List<FXController> FXElements;

        public abstract void OnInstantiated();
        public abstract void OnDepose();
    }
}

