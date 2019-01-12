using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChuChu.Framework.UI
{
    /// <summary>
    /// Main manager for all UI elements, meanwhile represents the main canvas in the scene which provides basic UI functinality such as Show/Hide all, Fade in/out...etc.
    /// This component and its gameObject(The main canvas) should not be destoryed until the game exit. 
    /// </summary>
    public sealed class CUIManager : MonoBehaviour
    {
        public static CUIManager Instance;
        public static GameObject UIRoot;

        #region MonoBehaviorStuff
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                UIRoot = gameObject;
                DontDestroyOnLoad(UIRoot);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {

        }
        #endregion

        #region Show/Hide all

        public void ShowAll(float sec = 0)
        {
            StartCoroutine(OnShowAllCalled(sec));
        }
        IEnumerator OnShowAllCalled(float sec)
        {
            if (sec > 0)
                yield return new WaitForSeconds(sec);
                CGameEventSystem.UI.OnShowAllCalled();
        }

        public void HideAll(float sec = 0)
        {
            StartCoroutine(OnHideAllCalled(sec));
        }
        IEnumerator OnHideAllCalled(float sec)
        {
            if (sec > 0)
                yield return new WaitForSeconds(sec);
            CGameEventSystem.UI.OnHideAllCalled();
        }

        #endregion
    }
}


