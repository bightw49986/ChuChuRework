using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChuChu.Framework.SceneAndObjectManagement;

namespace ChuChu.Framework.UI
{
    /// <summary>
    /// Base class of all UI components, this will be the controller for the UI element.
    /// </summary>
    public abstract class UIComponent : MonoBehaviour,IManageableObject
    {
        public ObjectFlag ObjectFlag{ get { return ObjectFlag.UI; }}
        public GameObject GameObject { get { return GameObject; } }

        private void Awake()
        {
            CGameEventSystem.UI.ShowAllCalled += Show;
            CGameEventSystem.UI.HideAllCalled += Hide;
        }

        private void OnDestroy()
        {
            CGameEventSystem.UI.ShowAllCalled -= Show;
            CGameEventSystem.UI.HideAllCalled -= Hide;
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Access()
        {

        }

        public void Return()
        {

        }
    }
}

