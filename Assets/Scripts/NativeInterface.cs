using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class NativeInterface : MonoBehaviour
{
    public static event System.Action<string> OnNativeCall;

#if !UNITY_EDITOR && UNITY_IOS
    [DllImport("__Internal")]
    private static extern string CallPluginIOS();
#endif

    public static string CallPlugin()
    {
#if UNITY_EDITOR
        return "";
#elif UNITY_IOS
        return CallPluginIOS();
#else
        return "";
#endif
    }

    public void CallUnity(string message)
    {
        OnNativeCall?.Invoke(message);
    }
}
