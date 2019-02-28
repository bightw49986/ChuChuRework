using ChuDebug;
using UnityEngine;
[Debug(EDebugType.Other)]
public class DebugExample : MonoBehaviour
{
    private void Start()
    {
#if !UNITY_EDITOR //Remember to implement a way to call the "DebugCenter.DestoryDebugCenter()" function to stop logging in your release-build for better performance.
        DebugCenter.DestoryDebugCenter();
#endif
    }

    void Update()
    {
        LetsPrintSomething(1, 1f);
    }

    void LetsPrintSomething(int a,params float[] b)
    {
        Debug.Log("yaya");
        CDebug.Log("123");
    }
}
