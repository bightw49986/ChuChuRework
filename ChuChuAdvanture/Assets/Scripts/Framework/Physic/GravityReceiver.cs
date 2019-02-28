using System.Collections;
using System.Collections.Generic;
using ChuDebug;
using UnityEngine;

namespace ChuChu.Framework.Physics
{
    [Debug(EDebugType.Physics)]
    public class GravityReceiver : MonoBehaviour, IGravityReceiver
    {
        #region Members
        public bool AutoRegister = true;

        public float GravityMutiplyer = 1f;
        public bool Override;
        public Vector3 GravityOverride = Vector3.zero;
        #endregion

        #region Properties
        /// <summary>
        /// Gravity applys on this gameObject.
        /// </summary>
        public Vector3 Gravity { get;private set; }
        #endregion

        #region MonoBehaviors
        private void OnEnable()
        {
            if(AutoRegister)
            {
                GravitySystem.Instance.Register(this);
            }
        }
        private void OnDisable()
        {
            GravitySystem.Instance.UnRegister(this);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Receives the gravity. This is the callback function which must be register in the GravitySystem's "ApplyGravity" method.
        /// </summary>
        /// <param name="currentGravity">Current gravity.</param>
        public void ReceiveGravity(Vector3 currentGravity)
        {
            Gravity = Override ? GravityOverride : currentGravity * GravityMutiplyer;
        }

        /// <summary>
        /// Use this method to set a different amount of gravity only applys to this receiver.
        /// <para></para>
        /// (Call the "ResetGravity" function to reset to default.)
        /// </summary>
        /// <param name="newGravity">New gravity.</param>
        public void OverrideGravity(Vector3 newGravity)
        {
            GravityOverride = newGravity;
            Override = true;
        }

        /// <summary>
        /// Resets the gravity to the same as the value given by GravitySystem.
        /// </summary>
        public void ResetGravity()
        {
            Override = false;
            GravityOverride = Vector3.zero;
        }
        #endregion
    }
}
