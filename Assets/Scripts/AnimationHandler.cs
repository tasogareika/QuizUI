using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimationHandler : MonoBehaviour
{
    public static AnimationHandler singleton;
    private float time;
    [SerializeField] private GameObject mainButton, buttonText, topBG, logo;
    public List<Sprite> buttonLines;
    private Animator thisAnim;

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        thisAnim = mainButton.GetComponent<Animator>();
    }

    public float getAnimTime(Animator anim)
    {
        float returnTime;
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        for (var i = 0; i < clips.Length; i++)
        {
            switch (clips[i].name)
            {
                case "EndingRollDown":
                    time = clips[i].length;
                    break;

                case "ButtonAfterQuiz":
                    time = clips[i].length;
                    break;

                case "MoveToReg":
                    time = clips[i].length;
                    break;

                case "ShowLast":
                    time = clips[i].length;
                    break;

                case "ReturnToStart":
                    time = clips[i].length;
                    break;

                case "KeyboardHide":
                    time = clips[i].length;
                    break;

                case "KeyboardAppear":
                    time = clips[i].length;
                    break;
            }
        }
        returnTime = time;
        return returnTime;
    }

    public void attachFunction(string pageType)
    {
        //add new listener
        var btn = mainButton.GetComponent<Button>();

        switch (pageType) {
            case "LandingStartHandler":
                changeText("Tap here to start");
                btn.onClick.AddListener(delegate { LandingStartHandler.singleton.StartGame(); });
                break;

            case "EndingHandler":
                btn.onClick.AddListener(delegate { EndingHandler.singleton.GoToRegister(); });
                break;

            case "RegisterHandler":
                btn.onClick.AddListener(delegate { RegisterHandler.singleton.clickRegister(); });
                break;

            case "LastPageHandler":
                btn.onClick.AddListener(delegate { LastPageHandler.singleton.returnToStart(); });
                break;
        }

        mainButton.GetComponent<Button>().interactable = true;
    }

    public void changeText (string text) //change text of button
    {
        //change button text image according to what is passed
        switch (text)
        {
            case "Tap to continue":
                buttonText.GetComponent<Image>().sprite = buttonLines[1];
                break;

            case "Get Reward":
                buttonText.GetComponent<Image>().sprite = buttonLines[2];
                break;

            case "Register":
                buttonText.GetComponent<Image>().sprite = buttonLines[3];
                break;

            case "Tap here to start":
                buttonText.GetComponent<Image>().sprite = buttonLines[0];
                break;

            default:
                buttonText.GetComponent<Image>().color = Color.clear;
                break;
        }
        buttonText.GetComponent<Image>().color = Color.white;
        buttonText.GetComponent<Image>().SetNativeSize();
    }

    public void topBGRollDown()
    {
        topBG.GetComponent<Animator>().Play("BGRollDown");
    }

    public void topBGVanish()
    {
        topBG.GetComponent<Animator>().Play("BGAway");
    }

    public void logoVanish()
    {
        logo.GetComponent<Animator>().Play("LogoVanish");
    }

    public void logoReturn()
    {
        logo.GetComponent<Animator>().Play("LogoReturn");
    }

    public void logoShiftUp()
    {
        logo.GetComponent<Animator>().Play("LogoShiftUp");
    }

    public void logoShiftDown()
    {
        logo.GetComponent<Animator>().Play("LogoShiftDown");
    }

    public void returnToStart(Animator anim)
    {
        disableButton();
        StartCoroutine(enableButton(getAnimTime(anim) + 0.5f, "LandingStartHandler"));
    }

    public void leaveScreen()
    {
        disableButton();
        thisAnim.Play("LeaveScreen");
    }

    public void quizEndReturn(Animator anim)
    {
        changeText("Tap to continue");
        thisAnim.Play("ButtonAfterQuiz");
        StartCoroutine(enableButton(getAnimTime(anim) + 2f, "EndingHandler"));
    }

    public void switchRegister(Animator anim)
    {
        disableButton();
        StartCoroutine(enableButton(getAnimTime(anim) + 0.2f, "RegisterHandler"));
    }

    public void lastPage(Animator anim)
    {
        disableButton();
        StartCoroutine(enableButton(getAnimTime(anim) + 0.2f, "LastPageHandler"));
    }

    public void disableButton()
    {
        mainButton.GetComponent<Button>().onClick.RemoveAllListeners();
        mainButton.GetComponent<Button>().interactable = false;
    }

    public void toggleButton()
    {
        if (mainButton.activeInHierarchy)
        {
            mainButton.SetActive(false);
        } else
        {
            mainButton.SetActive(true);
        }
    }

    IEnumerator enableButton (float waitTime, string pageType)
    {
        yield return new WaitForSeconds (waitTime);
        attachFunction(pageType);
    }
}
