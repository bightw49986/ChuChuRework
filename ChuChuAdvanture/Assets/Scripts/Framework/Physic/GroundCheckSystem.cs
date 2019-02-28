using System.Collections;
using System.Collections.Generic;
using ChuDebug;
using UnityEngine;

namespace ChuChu.Framework.Physics
{
    public interface IGroundCheckReceiver
    {
        GroundCheckResult Result { get; set; }
        bool AutoApplyOnFixedUpdate { get; set; }

        LayerMask GroundLayer { get; }
        Vector3 GroundCheckPoint { get; }
        float RaycastLength { get; }

        bool RegisterGroundCheck();
        bool UnRegisterGroundCheck();
    }

    /// <summary>
    /// Informations of ground check result.
    /// </summary>
    public struct GroundCheckResult
    {
        public bool IsGrounded;
        public Vector3 GroundPosition;
        public RaycastHit HitInfo;
        public EGroundCheckResultType ResultType;  
    }

    /// <summary>
    /// Type of ground check raycast results.
    /// </summary>
    public enum EGroundCheckResultType
     {
        /// <summary>
        /// The raycast hit nothing, which may indicates the object is currently at a wierd position where there is no ground can be found.
        /// </summary>
        Nothing,
        /// <summary>
        /// The first raycast hit the ground, which means the object is grounded.
        /// </summary>
        HitOn1stAttempt,
        /// <summary>
        /// The second raycast hit the ground, which means the object is not grounded and the raycast found the ground position beneath the object's position.
        /// </summary>
        HitOn2ndAttempt 
     }

    [Debug(EDebugType.Physics)]
    public class GroundCheckSystem : MonoBehaviour
    {
        public static GroundCheckSystem Instance;

        #region Members and Properties
        public bool Activated { get { return m_Activated; } private set { m_Activated = value; } }
        [SerializeField] private bool m_Activated;

        public LayerMask DefaultGroundLayer { get { return m_DefaultGroundLayer; } private set { m_DefaultGroundLayer = value; } }
        [SerializeField] private LayerMask m_DefaultGroundLayer;

        private HashSet<IGroundCheckReceiver> m_Receivers = new HashSet<IGroundCheckReceiver>();
        private Coroutine m_Execution;
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

        private void FixedUpdate()
        {
            if (Activated == false && m_Execution != null)
            {
                Debug.LogWarning("Check your code(The activation is off but execution isn't null.");
                Stop();
            }
            else if (Activated == true && m_Execution == null)
            {
                Debug.LogWarning("Check your code(The activation is on but execution is still null.");
                Begin();
            }
        }

        private void OnDisable()
        {
            UnRegisterAll();
        }
        #endregion

        #region Public Methods
        public bool Begin()
        {
            if (Activated == true || m_Execution != null)
            {
                Debug.LogWarning("Starting execution failed: already running.");
                return false;
            }
            FindReceiverAndRegister();
            Activated = true;
            m_Execution = StartCoroutine(Execution());
            GameEventSystem.Instance_GravityAndGroundCheck.OnGrounCheckBegan();
            return true;
        }

        public bool Stop()
        {
            if (Activated == false || m_Execution == null)
            {
                Debug.LogWarning("Stopping execution failed : not running.");
                return false;
            }
            Activated = false;
            StopCoroutine(m_Execution);
            m_Execution = null;
            GameEventSystem.Instance_GravityAndGroundCheck.OnGroundCheckStop();
            return true;
        }

        public bool Register(IGroundCheckReceiver reciever)
        {
            if (reciever == null)
            {
                Debug.LogError("The receiver you passed in is currently null.");
                return false;
            }
            if (m_Receivers == null)
            {
                m_Receivers = new HashSet<IGroundCheckReceiver>();
                Debug.Log("Re-created a new set of receivers.");
            }
            else if (m_Receivers.Contains(reciever))
            {
                Debug.LogWarning("GroundCheck System already has a same receiver.");
                return false;
            }
            m_Receivers.Add(reciever);
            return true;
        }

        public bool UnRegister(IGroundCheckReceiver reciever)
        {
            if (reciever == null)
            {
                Debug.LogError("The receiver you passed in is null.");
                return false;
            }
            if (m_Receivers == null)
            {
                Debug.LogWarning("The receivers set is currently null.");
                return false;
            }
            if (m_Receivers.Contains(reciever) == false)
            {
                Debug.LogWarning("Can't find the specific reveiver to unregister.");
                return false;
            }
            m_Receivers.Remove(reciever);
            return true;
        }

        public void UnRegisterAll()
        {
            Debug.Log("GroundCheckSystem shutted down, unregistering all receivers...");
            foreach(var receiver in m_Receivers)
            {
                UnRegister(receiver);
            }
            m_Receivers.Clear();
            m_Receivers = null;
        }

        public void GroundCheckCast(IGroundCheckReceiver receiver)
        {
            if(receiver == null)
            {
                Debug.LogError("receiver can't be null!!");
                return;
            }
            Ray ray = new Ray(receiver.GroundCheckPoint, Vector3.down);
            RaycastHit hitInfo;
            if (UnityEngine.Physics.Raycast(ray, out hitInfo, receiver.RaycastLength, receiver.GroundLayer))
            {
                receiver.Result = new GroundCheckResult { IsGrounded = true, HitInfo = hitInfo, GroundPosition = hitInfo.point, ResultType = EGroundCheckResultType.HitOn1stAttempt };
            }
            else if (UnityEngine.Physics.Raycast(ray, out hitInfo, 1000f, receiver.GroundLayer))
            {
                receiver.Result = new GroundCheckResult { IsGrounded = false, HitInfo = hitInfo, GroundPosition = hitInfo.point, ResultType = EGroundCheckResultType.HitOn2ndAttempt };
            }
            else
            {
                receiver.Result = new GroundCheckResult { IsGrounded = false, HitInfo = hitInfo, GroundPosition = Vector3.zero, ResultType = EGroundCheckResultType.Nothing };
            }
        }
        #endregion

        #region Private Methods
        private void FindReceiverAndRegister()
        {
            var receivers = FindObjectsOfType<GroundCheckReceiver>();
            if (receivers != null)
            {
                foreach (var receiver in receivers)
                {
                    receiver.RegisterGroundCheck();
                }
            }
        }

        private IEnumerator Execution()
        {
            while (Activated == true)
            {
                CastRaysForReceivers();
                yield return new WaitForFixedUpdate();
            }
            Stop();
            yield break;
        }

        private void CastRaysForReceivers()
        {
            foreach (var receiver in m_Receivers)
            {
                if (receiver == null || receiver.AutoApplyOnFixedUpdate == false) continue;
                GroundCheckCast(receiver);
            }
            GameEventSystem.Instance_GravityAndGroundCheck.OnGroundCheckApplyed();
        }
    }
    #endregion
}


