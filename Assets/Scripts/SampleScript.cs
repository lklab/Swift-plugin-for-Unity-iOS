using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SampleScript : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Text _test;

    private void Awake()
    {
        _button.onClick.AddListener(delegate
        {
            string ret = NativeInterface.CallPlugin();
            Debug.Log("Call plugin returns: " + ret);
        });

        NativeInterface.OnNativeCall += message =>
        {
            _test.text = message;
        };
    }
}
