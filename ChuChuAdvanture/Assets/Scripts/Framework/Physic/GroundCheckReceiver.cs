using System.Collections;
using System.Collections.Generic;
using ChuDebug;
using UnityEngine;

namespace ChuChu.Framework.Physics
{
    [Debug(EDebugType.Physics)]
    public class GroundCheckReceiver : MonoBehaviour, IGroundCheckReceiver
    {
        #region Members
        [SerializeField] private bool m_bAutoApply = true;
        [SerializeField] private readonly float m_fRaycastHeight = 1f;
        [SerializeField] private readonly float m_fTolerance = 0.01f;
        #endregion

        #region Properties
        public GroundCheckResult Result { get; set; }

        public bool AutoApplyOnFixedUpdate { get { return m_bAutoApply; } set { m_bAutoApply = value; } }

        public LayerMask GroundLayer { get { return GroundCheckSystem.Instance.DefaultGroundLayer; } }

        public Vector3 GroundCheckPoint { get { return transform.position + Vector3.up * m_fRaycastHeight; } }

        public float RaycastLength { get; private set; }
        #endregion

        #region Public Methods
        public bool RegisterGroundCheck()
        {
            return GroundCheckSystem.Instance.Register(this);
        }

        public bool UnRegisterGroundCheck()
        {
            return GroundCheckSystem.Instance.UnRegister(this);
        }
        #endregion

        #region MonoBehaviors
        private void Start()
        {
            RaycastLength = m_fRaycastHeight + m_fTolerance;
        }
        private void OnEnable()
        {
            if (AutoApplyOnFixedUpdate)
            {
                GroundCheckSystem.Instance.Register(this);
            }
        }
        private void OnDisable()
        {
            GroundCheckSystem.Instance.UnRegister(this);
        }
        #endregion
    }
}

