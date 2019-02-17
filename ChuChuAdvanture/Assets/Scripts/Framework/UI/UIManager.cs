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
    public sealed class UIManager : MonoBehaviour
    {
        public static UIManager Instance;


        #region MonoBehaviorStuff
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
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
        IEnumerator OnShowAllCalled(float waitSec)
        {
            if (waitSec > 0)
                yield return new WaitForSeconds(waitSec);
                GameEventSystem.Instance_UI.OnShowAllCalled();
        }

        public void HideAll(float sec = 0)
        {
            StartCoroutine(OnHideAllCalled(sec));
        }
        IEnumerator OnHideAllCalled(float waitSec)
        {
            if (waitSec > 0)
                yield return new WaitForSeconds(waitSec);
            GameEventSystem.Instance_UI.OnHideAllCalled();
        }

        #endregion
    }
}


