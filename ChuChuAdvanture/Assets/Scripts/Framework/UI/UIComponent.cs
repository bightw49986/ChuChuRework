using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ChuChu.Framework.UI
{
    /// <summary>
    /// Base class of all UI components, this will be the controller for the UI element.
    /// </summary>
    public abstract class UIComponent : MonoBehaviour
    {
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
    }
}

