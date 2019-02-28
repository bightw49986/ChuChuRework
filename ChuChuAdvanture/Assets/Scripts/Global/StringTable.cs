using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Single class for storing every string references.
/// </summary>
public static class StringTable
{
    public struct SceneConfig
    {
        public static readonly string DefaultConfig = "ScriptableObjects/Scene/Config/_Default";
    }

    public struct UI
    {
        public static readonly string UIManager = "Prefabs/UI/UIManager";
        public static readonly string FadingImage = "FadingImage";
    }
}
