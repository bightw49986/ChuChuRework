using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ChuDebug;

namespace ChuChu.Framework.FX
{
    /// <summary>
    /// Base class for all kinds of FX, provides method that co-work with Unity animator. 
    /// <para></para>
    /// Should define every event that includes in the whole lifetime of the FX so the FX system will automaticlly subscribe them.
    /// <para></para>
    /// Remember to serialize the child class so that you can manage the callbacks in the inspector.
    /// </summary>
    [Debug(EDebugType.VFX)]
    public class AnimControllerComponent : MonoBehaviour, IManageableResource
    {
        #region Members
        private bool m_bInitialized;
        private Action<AnimControllerComponent> m_Callback;
        #endregion

        #region Properties
        public Dictionary<string,AnimControllerComponent> SubControllers { get; protected set; }
        public Animator Animator { get { return GetComponent<Animator>(); } }
        public EObjectType ObjectTag { get { return EObjectType.FX; } }

        public void Deploy()
        {
            if (m_bInitialized == false && transform.childCount > 0)
            {
                FindSubControllers();
            }
            m_Callback = null;
        }
        #endregion

        #region MonoBehaviors
        protected virtual void OnDisable()
        {
            StopAllCoroutines();
            if (m_Callback != null)
                m_Callback(this);   
            m_Callback = null;
        }
        #endregion

        #region Public Methods
        public void FindSubControllers()
        {
            if (SubControllers == null) SubControllers = new Dictionary<string, AnimControllerComponent>();
            var controllers = GetComponentsInChildren<AnimControllerComponent>();
            if (controllers.Length <= 0) { Debug.LogWarning(name + " loads a gameobject that doesn't have any FX Controller."); return; }
            foreach (var c in controllers)
            {
                if (c != this && SubControllers.ContainsKey(c.name) == false)
                    SubControllers.Add(c.name,c);
            }
            m_bInitialized = true;
        }

        public AnimControllerComponent GetSubController(string controllerObjName)
        {
            if (SubControllers == null)
            {
                Debug.LogError("SubController's dictionary not inited yet!!");
                return null;
            }
            return SubControllers[controllerObjName];
        }

        /// <summary>
        /// Turn to specific state immediately and invoke callback after the first loop of animation.
        /// </summary>
        public void PlayAnimation(string state,Action<AnimControllerComponent> callback = null)
        {
            if (Animator.GetCurrentAnimatorStateInfo(0).IsName(state) == false)
            {
                if (m_Callback != null)
                {
                    m_Callback(this);
                }
                StartCoroutine(_PlayAnim(state));
            }
            if (callback != null)
            m_Callback += callback;
        }

        /// <summary>
        /// Sets the animator trigger and invoke callback after it finish first loop.
        /// </summary>
        public void SetAnimatorTrigger(string trigger, Action<AnimControllerComponent> callback = null)
        {
            StartCoroutine(_SetAnimatorTrigger(trigger));
            if (callback != null)
                m_Callback = callback;
        }

        #endregion

        #region Private Methods
        protected IEnumerator _PlayAnim(string state)
        {
            Animator.Play(state);
            yield return new WaitForEndOfFrame();
            yield return new WaitWhile(() => Animator.GetCurrentAnimatorStateInfo(0).IsName(state) == true  && Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);
            if (m_Callback != null)
            {
                m_Callback(this);
            }
            m_Callback = null;
        }

        protected IEnumerator _SetAnimatorTrigger(string trigger)
        {
            Animator.SetTrigger(trigger);
            yield return new WaitForEndOfFrame();
            yield return new WaitUntil(() => Animator.IsInTransition(0) == true);
            yield return new WaitWhile(() => Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);
            if (m_Callback != null)
            {
                m_Callback(this);
            }
            m_Callback = null;
        }
        #endregion
    }
}

