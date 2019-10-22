using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardHandler : MonoBehaviour
{
    public static KeyboardHandler singleton;
    public List<GameObject> keyboardKeys;

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        KeyboardKey[] keys = transform.GetComponentsInChildren<KeyboardKey>();
        foreach (var k in keys)
        {
            keyboardKeys.Add(k.gameObject);
        }
    }

    public void shiftPressed(bool on)
    {
        foreach (var k in keyboardKeys)
        {
            KeyboardKey key = k.GetComponent<KeyboardKey>();
            if (!key.isBackSpace && !key.isShift && !key.isSpace)
            {
                if (on)
                {
                    key.textDisplay.text = key.altKeyData.ToString();
                }
                else
                {
                    key.textDisplay.text = key.keyData.ToString();
                }
            }
        }
    }
}
