using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChuDebug;

namespace ChuChu.Framework.UI
{
    /// <summary>
    /// Main manager for all UI elements, meanwhile represents the main canvas in the scene which provides basic UI functinality such as Show/Hide all, Fade in/out...etc.
    /// This component and its gameObject(The main canvas) should not be destoryed until the game exit. 
    /// </summary>
    [Debug(EDebugType.UI)]
    public sealed class UIManager : MonoBehaviour
    {
        [HideInInspector]public static UIManager Instance;

        #region Members
        private GameObject m_FadingImage;
        private Dictionary<Graphic,List<Action>> m_FadingTasks;
        #endregion

        #region Properties
        private Image FadeImage
        {
            get
            {
                if (m_FadingImage != null)
                {
                    return m_FadingImage.GetComponent<Image>();
                }
                Debug.LogError("You searched Fading Image while its gameObject is null!");
                return null;
            }
        }
        public bool ScreenIsFading { get; private set; }
        #endregion

        #region MonoBehavior
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                m_FadingTasks = new Dictionary<Graphic, List<Action>>();
                m_FadingImage = transform.FindDeepChild(StringTable.UI.FadingImage).gameObject;
                if (m_FadingImage == null)
                {
                    Debug.LogError("Can't find Fading Image!!");
                    return;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void LateUpdate()
        {
        }

        private void OnDestroy()
        {
            
        }
        #endregion

        #region Show/Hide
        public void ShowAll(float sec = 0)
        {
            StartCoroutine(_ShowAll(sec));
        }


        public void HideAll(float sec = 0)
        {
            StartCoroutine(_HideAll(sec));
        }
        IEnumerator _ShowAll(float waitSec)
        {
            if (waitSec > 0)
                yield return new WaitForSeconds(waitSec);
            GameEventSystem.Instance_UI.OnShowAllCalled();
        }

        IEnumerator _HideAll(float waitSec)
        {
            if (waitSec > 0)
                yield return new WaitForSeconds(waitSec);
            GameEventSystem.Instance_UI.OnHideAllCalled();
        }
        #endregion

        #region Screen Fade In/Out
        public static void ScreenFadeIn(Action callback,float duration = 1.5f)
        {
            Instance.StartCoroutine(FadeOut(Instance.FadeImage,callback, duration));
        }

        public static void ScreenFadeIn(Action callback,Color color,float duration = 1.5f)
        {
            Instance.StartCoroutine(FadeOut(Instance.FadeImage, callback, color, duration));
        }

        public static void ScreenFadeOut(Action callback,float duration = 1.5f)
        {
            Instance.StartCoroutine(FadeIn(Instance.FadeImage, callback, duration));
        }

        public static void ScreenFadeOut(Action callback, Color color, float duration = 1.5f)
        {
            Instance.StartCoroutine(FadeIn(Instance.FadeImage, callback, color, duration));
        }

        public static void ScreenFadeOutAndWaitForFadeIn(Func<IEnumerator> waitUntilThisDone, float duration = 1.5f)
        {
            if (Instance == null)
            {
                Debug.LogError("Instance not exist!!");
                return;
            }
            if (Instance.ScreenIsFading == false)
            {
                Instance.StartCoroutine(Instance._ScreenFadeOutAndWaitForFadeIn(waitUntilThisDone, duration));
            }
            else
            {
                Debug.LogError("Screen is currently fading.");
            }
        }
        IEnumerator _ScreenFadeOutAndWaitForFadeIn(Func<IEnumerator> waitUntilThisDone,float duration)
        {
            yield return FadeIn(Instance.FadeImage, duration);
            yield return StartCoroutine(waitUntilThisDone());
            yield return FadeOut(Instance.FadeImage, duration);
        }

        public static void ScreenFadeOutAndWaitForFadeIn(Func<IEnumerator> waitUntilThisDone,Action callback,float duration = 1.5f)
        {
            if (Instance == null)
            {
                Debug.LogError("Instance not exist!!");
                return;
            }
            if (Instance.ScreenIsFading == false)
            {
                Instance.StartCoroutine(Instance._ScreenFadeOutAndWaitForFadeIn(waitUntilThisDone,callback,duration));
            }
            else
            {
                Debug.LogError("Screen is currently fading.");
            }
        }
        IEnumerator _ScreenFadeOutAndWaitForFadeIn(Func<IEnumerator> waitUntilThisDone, Action callback, float duration)
        {
            yield return FadeIn(Instance.FadeImage, duration);
            yield return StartCoroutine(waitUntilThisDone());
            yield return FadeOut(Instance.FadeImage, duration);
            callback();
        }
        #endregion

        #region FadeOut
        public static IEnumerator FadeOut(Graphic graphic, float sec = 1.5f)
        {
            if (Instance == null)
            {
                Debug.LogError("Instance not exist!!");
                yield break;
            }
            if (Instance.m_FadingTasks.ContainsKey(graphic) == false)
            {
                yield return Instance.StartCoroutine(Instance._FadeOut(graphic, sec));
            }
            else
            {
                Debug.LogWarning("重複呼叫Fade Out!! : " + graphic.name);
            }
        }

        public static IEnumerator FadeOut(Graphic graphic, Action callback, float sec = 1.5f)
        {
            if (Instance == null)
            {
                Debug.LogError("Instance not exist!!");
                yield break;
            }
            if (Instance.m_FadingTasks.ContainsKey(graphic) == false)
            {
                yield return Instance.StartCoroutine(Instance._FadeOut(graphic,callback,sec));
            }
            else
            {
                Instance.m_FadingTasks[graphic].Add(callback);
            }
        }

        public static IEnumerator FadeOut(Graphic graphic, Action callback, Color color, float sec = 1.5f)
        {
            if (Instance == null)
            {
                Debug.LogError("Instance not exist!!");
                yield break;
            }
            if (Instance.m_FadingTasks.ContainsKey(graphic) == false)
            {
                yield return Instance.StartCoroutine(Instance._FadeOut(graphic, callback, color, sec));
            }
            else
            {
                Instance.m_FadingTasks[graphic].Add(callback);
                graphic.color = new Color(color.r, color.g, color.b, graphic.color.a);
            }
        }

        IEnumerator _FadeOut(Graphic graphic, float sec = 1.5f)
        {
            if (m_FadingTasks.ContainsKey(graphic) == false)
            {
                m_FadingTasks.Add(graphic, new List<Action>());
            }
            if (graphic.color.a > 0)
            {
                graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, graphic.color.a - Time.deltaTime / sec);
                yield return null;
            }
            m_FadingTasks.Remove(graphic);
        }

        IEnumerator _FadeOut (Graphic graphic, Action callback, float sec = 1.5f)
        {
            if (m_FadingTasks.ContainsKey(graphic) == false)
            {
                m_FadingTasks.Add(graphic, new List<Action> { callback });
            }
            else
            {
                m_FadingTasks[graphic].Add(callback);
            }
            while (graphic.color.a > 0)
            {
                graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, graphic.color.a - Time.deltaTime / sec);
                yield return null;
            }
            foreach (var cb in m_FadingTasks[graphic])
            {
                cb();
            }
            m_FadingTasks.Remove(graphic);
        }

        IEnumerator _FadeOut(Graphic graphic, Color color, float sec = 1.5f)
        {
            if (m_FadingTasks.ContainsKey(graphic) == false)
            {
                m_FadingTasks.Add(graphic, new List<Action>());
            }
            graphic.color = new Color(color.r, color.g, color.b, graphic.color.a);
            while (graphic.color.a > 0)
            {
                graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, graphic.color.a - Time.deltaTime / sec);
                yield return null;
            }
            m_FadingTasks.Remove(graphic);
        }
        IEnumerator _FadeOut(Graphic graphic, Action callback, Color color, float sec = 1.5f)
        {
            if (m_FadingTasks.ContainsKey(graphic) == false)
            {
                m_FadingTasks.Add(graphic, new List<Action> { callback });
            }
            else
            {
                m_FadingTasks[graphic].Add(callback);
            }
            graphic.color = new Color(color.r, color.g, color.b, graphic.color.a);
            while (graphic.color.a > 0)
            {
                graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, graphic.color.a - Time.deltaTime / sec);
                yield return null;
            }
            foreach (var cb in m_FadingTasks[graphic])
            {
                cb();
            }
            m_FadingTasks.Remove(graphic);
        }
        #endregion

        #region FadeIn
        public static IEnumerator FadeIn(Graphic graphic, float sec = 1.5f)
        {
            if (Instance == null)
            {
                Debug.LogError("Instance not exist!!");
                yield break;
            }
            if (Instance.m_FadingTasks.ContainsKey(graphic) == false)
            {
                yield return Instance.StartCoroutine(Instance._FadeIn(graphic, sec));
            }
            else
            {
                Debug.LogWarning("重複呼叫Fade Out!! : " + graphic.name);
            }
        }

        public static IEnumerator FadeIn(Graphic graphic, Action callback, float sec = 1.5f)
        {
            if (Instance == null)
            {
                Debug.LogError("Instance not exist!!");
                yield break;
            }
            if (Instance.m_FadingTasks.ContainsKey(graphic) == false)
            {
                yield return Instance.StartCoroutine(Instance._FadeIn(graphic, callback, sec));
            }
            else
            {
                Instance.m_FadingTasks[graphic].Add(callback);
            }
        }

        public static IEnumerator FadeIn(Graphic graphic, Action callback, Color color, float sec = 1.5f)
        {
            if (Instance == null)
            {
                Debug.LogError("Instance not exist!!");
                yield break;
            }
            if (Instance.m_FadingTasks.ContainsKey(graphic) == false)
            {
                yield return Instance.StartCoroutine(Instance._FadeIn(graphic, callback, color, sec));
            }
            else
            {
                Instance.m_FadingTasks[graphic].Add(callback);
                graphic.color = new Color(color.r, color.g, color.b, graphic.color.a);
            }
        }

        IEnumerator _FadeIn(Graphic graphic, float sec = 1.5f)
        {
            if (m_FadingTasks.ContainsKey(graphic) == false)
            {
                m_FadingTasks.Add(graphic, new List<Action>());
            }
            if (graphic.color.a < 1)
            {
                graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, graphic.color.a + Time.deltaTime / sec);
                yield return null;
            }
            m_FadingTasks.Remove(graphic);
        }

        IEnumerator _FadeIn(Graphic graphic, Action callback, float sec = 1.5f)
        {
            if (m_FadingTasks.ContainsKey(graphic) == false)
            {
                m_FadingTasks.Add(graphic, new List<Action> { callback });
            }
            else
            {
                m_FadingTasks[graphic].Add(callback);
            }
            while (graphic.color.a < 1)
            {
                graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, graphic.color.a + Time.deltaTime / sec);
                yield return null;
            }
            foreach (var cb in m_FadingTasks[graphic])
            {
                cb();
            }
            m_FadingTasks.Remove(graphic);
        }

        IEnumerator _FadeIn(Graphic graphic, Color color, float sec = 1.5f)
        {
            if (m_FadingTasks.ContainsKey(graphic) == false)
            {
                m_FadingTasks.Add(graphic, new List<Action>());
            }
            graphic.color = new Color(color.r, color.g, color.b, graphic.color.a);
            while (graphic.color.a < 1)
            {
                graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, graphic.color.a + Time.deltaTime / sec);
                yield return null;
            }
            m_FadingTasks.Remove(graphic);
        }
        IEnumerator _FadeIn(Graphic graphic, Action callback, Color color, float sec = 1.5f)
        {
            if (m_FadingTasks.ContainsKey(graphic) == false)
            {
                m_FadingTasks.Add(graphic, new List<Action> { callback });
            }
            else
            {
                m_FadingTasks[graphic].Add(callback);
            }
            graphic.color = new Color(color.r, color.g, color.b, graphic.color.a);
            while (graphic.color.a < 1)
            {
                graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, graphic.color.a + Time.deltaTime / sec);
                yield return null;
            }
            foreach (var cb in m_FadingTasks[graphic])
            {
                cb();
            }
            m_FadingTasks.Remove(graphic);
        }
        #endregion
    }
}


