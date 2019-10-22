using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyboardHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator thisAnim;
    public static KeyboardHandler singleton;
    public bool isOnScreen, touchingKeys;
    public List<GameObject> keyboardKeys;
    public TMP_InputField currInput;

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
        thisAnim = GetComponent<Animator>();
        isOnScreen = false;
        touchingKeys = false;
        gameObject.SetActive(false);
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

    public void OnPointerEnter (PointerEventData pointer)
    {
        touchingKeys = true;
    }

    public void OnPointerExit (PointerEventData pointer)
    {
        touchingKeys = false;
    }

    public void hideKeyboard()
    {
        isOnScreen = false;
        StopCoroutine("toggleButton");
        thisAnim.Play("KeyboardHide");
        StartCoroutine(toggleButton(AnimationHandler.singleton.getAnimTime(thisAnim) + 0.2f));
    }

    public void showKeyboard()
    {
        isOnScreen = true;
        StopCoroutine("toggleButton");
        gameObject.SetActive(true);
        thisAnim.Play("KeyboardAppear");
        StartCoroutine(toggleButton(AnimationHandler.singleton.getAnimTime(thisAnim) + 0.2f));
    }

    public void textEntry(string s)
    {
        if (currInput != null)
        {
            if (currInput.name != "MobileInput")
            {
                currInput.text += s;
            } else
            {
                int i;
                if (int.TryParse(s, out i))
                {
                    currInput.text += s;
                }
            }
        }
    }

    public void backSpace()
    {
        if (currInput != null)
        {
            string currText = currInput.text.ToString();
            if (currText.Length > 1)
            {
                currText = currText.Remove(currText.Length - 1);
            } else
            {
                currText = string.Empty;
            }
            currInput.text = currText;
        }
    }

    public void letterSpace()
    {
        if (currInput != null)
        {
            currInput.text += " ";
        }
    }

    IEnumerator toggleButton (float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        AnimationHandler.singleton.toggleButton();
    }
}
