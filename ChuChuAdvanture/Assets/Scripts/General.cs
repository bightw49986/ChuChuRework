using System.Collections;
using UnityEngine;


/// <summary>
/// Suspend the coroutine execution for given amount of frames.
/// </summary>
public class WaitForFrames : IEnumerator
{
    private int m_iCount = -1;

    public WaitForFrames(int frame)
    {
        if (frame <= 0)
        {
            Debug.LogWarning("Assigned a number less than 0 to WaitForFrame, will yield return immediately");
            frame = 0;
        }
        m_iCount = frame;
    }

    public object Current { get { return m_iCount; } }

    public bool MoveNext()
    {
        if (m_iCount <= 1) return false;
        m_iCount -= 1;
        return true;
    }

    public void Reset()
    {
        m_iCount = -1;
    }
}

public enum RenderMode
{
    Opaque,
    Cutout,
    Fade,        // Old school alpha-blending mode, fresnel does not affect amount of transparency
    Transparent // Physically plausible transparency mode, implemented as alpha pre-multiply
}

public static class Extensions
{
    /// <summary>
    /// Try to find the child with specific name by using breadth-first-search.
    /// </summary>
    /// <returns>The child found. (Return null if failed.)</returns>
    /// <param name="useDFS">If set to <c>true</c> use depth-first-search instead.</param>
    public static Transform FindDeepChild(this Transform parent,string name, bool useDFS = false)
    {
        if(useDFS)
        {
            foreach (Transform child in parent)
            {
                if (child.name == name)
                    return child;
                var result = child.FindDeepChild(name);
                if (result != null)
                    return result;
            }
            return null;
        }
        else
        {
            var result = parent.Find(name);
            if (result != null)
                return result;
            foreach (Transform child in parent)
            {
                result = child.FindDeepChild(name);
                if (result != null)
                    return result;
            }
            return null;
        }
    }

    /// <summary>
    /// Change the rendering mode and set corresponding keywords.
    /// </summary>
    public static void SetRenderMode(this Material material, RenderMode mode)
    {
        switch (mode)
        {
            case RenderMode.Opaque:
                material.SetOverrideTag("RenderType", "");
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = -1;
                break;
            case RenderMode.Cutout:
                material.SetOverrideTag("RenderType", "TransparentCutout");
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.EnableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 2450;
                break;
            case RenderMode.Fade:
                material.SetOverrideTag("RenderType", "Transparent");
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;
            case RenderMode.Transparent:
                material.SetOverrideTag("RenderType", "Transparent");
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;
        }
    }
}
