using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChuChu.Framework.Scene;

namespace ChuChu.Framework.UI
{
    /// <summary>
    /// Base class of all UI components, this will be the controller for the UI element.
    /// </summary>
    public abstract class UIComponent : MonoBehaviour,IManageableResource
    {
        public ResourceType ObjectFlag{ get { return ResourceType.UI; }}
        public GameObject GameObject { get { return GameObject; } }

        private void Awake()
        {
            GameEventSystem.Instance_UI.ShowAllCalled += Show;
            GameEventSystem.Instance_UI.HideAllCalled += Hide;
        }

        private void OnDestroy()
        {
            GameEventSystem.Instance_UI.ShowAllCalled -= Show;
            GameEventSystem.Instance_UI.HideAllCalled -= Hide;
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

