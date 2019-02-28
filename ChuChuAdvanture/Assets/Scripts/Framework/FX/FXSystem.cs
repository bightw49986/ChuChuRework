using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChuChu.Framework.FX
{
    public class FXSystem : MonoBehaviour
    {
        #region Members
        [HideInInspector]public static FXSystem Instance;
        #endregion

        #region Properties

        #endregion

        #region MonoBehaviors
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
        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        #endregion

    }
}


