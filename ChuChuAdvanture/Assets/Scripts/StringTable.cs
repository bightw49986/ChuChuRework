using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Single class for storing every string references.
/// </summary>
public static class StringTable
{
    public struct SceneSetting
    {
        public static readonly string Default = "ScriptableObjects/SceneSetting/_Default";

        public static readonly string Title = "ScriptableObjects/SceneSetting/Title";
        public static readonly string Loading = "ScriptableObjects/SceneSetting/Loading";
        public static readonly string Game = "ScriptableObjects/SceneSetting/Game"; 
    }

    public struct UI
    {
        public static readonly string RootCanvasPath = "Prefabs/UI/Root Canvas";
    }
}
