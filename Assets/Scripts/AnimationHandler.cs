using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimationHandler : MonoBehaviour
{
    public static AnimationHandler singleton;
    private float time;
    [SerializeField] private GameObject mainButton, buttonText;
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
                changeText("Register");
                btn.onClick.AddListener(delegate { RegisterHandler.singleton.clickRegister(); });
                break;

            case "LastPageHandler":
                changeText("Get Reward");
                btn.onClick.AddListener(delegate { LastPageHandler.singleton.returnToStart(); });
                break;
        }

        mainButton.GetComponent<Button>().interactable = true;
    }

    public void changeText (string text) //change text of button
    {
        buttonText.GetComponent<TextMeshProUGUI>().text = text;
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
