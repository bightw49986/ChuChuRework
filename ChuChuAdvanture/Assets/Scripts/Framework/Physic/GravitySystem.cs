using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ChuChu.Framework.Physic
{
    /// <summary>
    /// Register to GravitySystem then when the gravity applys, this object will receive callback.
    /// <para></para>
    /// Remember: you have to implement your gravity ignoring check manually, the gravity system won't do this for each receiver.  
    /// </summary>
    public interface IGravityReceiver
    {
        void ReceiveGravity(Vector3 currentGravity);
    }

    /// <summary>
    /// System that accept IGravityReceiver and triggers the gravity event 
    /// </summary>
    public sealed class GravitySystem : MonoBehaviour
    {
        public static GravitySystem Instance; //Singleton

        public Vector3 Gravity { get { return m_Gravity; } private set { m_Gravity = value; } }
        [SerializeField] private Vector3 m_Gravity = new Vector3(0,-10,0);
        public bool Activated { get { return m_Activated; }private set { m_Activated = value; if (m_Activated) GameEventSystem.Instance_GravityAndGroundCheck.GroundCheckApplyed += ApplyGravity; } }
        [SerializeField] private bool m_Activated;

        private HashSet<IGravityReceiver> m_Receivers = new HashSet<IGravityReceiver>();

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

        private void OnEnable()
        {
            if(Activated)
            GameEventSystem.Instance_GravityAndGroundCheck.GroundCheckApplyed += ApplyGravity;
        }

        private void OnDisable()
        {
            GameEventSystem.Instance_GravityAndGroundCheck.GroundCheckApplyed -= ApplyGravity;
            UnRegisterAll();
        }
        #endregion

        #region Public Methods
        public void Register(IGravityReceiver receiver)
        {
            if (m_Receivers.Contains(receiver)) return;
            m_Receivers.Add(receiver);
            GameEventSystem.Instance_GravityAndGroundCheck.GravityApplyed += receiver.ReceiveGravity;
        }

        public void UnRegister(IGravityReceiver receiver)
        {
            if (m_Receivers.Contains(receiver) == false) return;
            m_Receivers.Remove(receiver);
            GameEventSystem.Instance_GravityAndGroundCheck.GravityApplyed -= receiver.ReceiveGravity;
        }

        public void UnRegisterAll()
        {
            if (GameEventSystem.Instance_GravityAndGroundCheck != null)
            {
                foreach (var receiver in m_Receivers)
                {
                    GameEventSystem.Instance_GravityAndGroundCheck.GravityApplyed -= receiver.ReceiveGravity;
                }
            }
            m_Receivers.Clear();
        }
        #endregion

        #region Private Methods
        private void ApplyGravity()
        {
            if (!m_Activated || m_Receivers.Count <= 0) return;
            GameEventSystem.Instance_GravityAndGroundCheck.OnGravityApplyed(Gravity);
        }
        #endregion
    }
}


