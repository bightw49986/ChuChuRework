using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChuChu.Framework;
using System;

namespace ChuChu.App.Gravity
{
    /// <summary>
    /// Register to GravitySystem then when the gravity applys, this object will recieve callback.  
    /// </summary>
    public interface IGravityReciever
    {
        bool IgnoreGravity { get; }
        bool RegisterGravity();
        bool DeRegisterGravity();
        void ApplyGravity(float currentGravity);
    }

    /// <summary>
    /// System that accept IGravityReciever and triggers the gravity event 
    /// </summary>
    public sealed class GravitySystem : MonoBehaviour
    {
        public static GravitySystem Instance;
        [SerializeField] private float Gravity = 10f;
        HashSet<IGravityReciever> mRecievers = new HashSet<IGravityReciever>();
        bool mApply;

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

        private void Update()
        {
            if (!mApply || mRecievers.Count <= 0) return;
            CGameEventSystem.GravityAndPositioning.ApplyGravity(Gravity);
        }

        public void Register(IGravityReciever reciever)
        {
            if (mRecievers.Contains(reciever)) return;
            mRecievers.Add(reciever);
            CGameEventSystem.GravityAndPositioning.GravityApplyed += reciever.ApplyGravity;
        }

        public void DeRegister(IGravityReciever reciever)
        {
            if (mRecievers.Contains(reciever) == false) return;
            mRecievers.Remove(reciever);
            CGameEventSystem.GravityAndPositioning.GravityApplyed -= reciever.ApplyGravity;
        }

        public void SetGravity(bool onOrOff)
        {
            mApply = onOrOff;
        }

        public void SetGravity(float newGravity)
        {
            Gravity = newGravity;
        }
    }
}


