using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LandingStartHandler : MonoBehaviour
{
    public static LandingStartHandler singleton;
    [SerializeField] private GameObject landingPage, logo, tutorialBG, countdown, countdownImg;
    public List<Sprite> countDownNos;
    private int secondCount;

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        secondCount = 3;
        AnimationHandler.singleton.attachFunction("LandingStartHandler");
        landingPage.GetComponent<Animator>().Play("TitlePulse");
        countdown.GetComponent<Animator>().Play("CountStart");
        logo.GetComponent<ObjectFloat>().enabled = true;
        logo.GetComponent<Animator>().enabled = false;
    }

    //orginally had interaction of clicking/tapping screen to show start button, but decperated as it was unneeded
    /*private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (logo.GetComponent<ObjectFloat>().enabled)
            {
                showStart();
            }
        }
    }*/

    public void backToLanding()
    {
        BackendHandler.singleton.playMainBGM();
        countdown.GetComponent<Animator>().Play("CountStart");
        landingPage.GetComponent<Animator>().Play("ReturnToStart");
        AnimationHandler.singleton.returnToStart(landingPage.GetComponent<Animator>());
        StartCoroutine(startPulse(AnimationHandler.singleton.getAnimTime(landingPage.GetComponent<Animator>())));
        logo.GetComponent<ObjectFloat>().enabled = true;
    }

    private void showStart()
    {
        logo.GetComponent<ObjectFloat>().enabled = false;
        landingPage.GetComponent<Animator>().Play("StartTextShow");
        StartCoroutine(returnToFloat(5f));
    }

    public void StartGame()
    {
        StopAllCoroutines();
        BackendHandler.singleton.playMainButtonClick();
        logo.GetComponent<ObjectFloat>().enabled = false;
        logo.GetComponent<Animator>().enabled = true;
        secondCount = 3;
        landingPage.GetComponent<Animator>().Play("MoveToTut");
        countdownImg.GetComponent<Image>().sprite = countDownNos[secondCount];
        countdown.GetComponent<Animator>().Play("MoveToCount");
        AnimationHandler.singleton.leaveScreen();
        StartCoroutine(showTutorial(1.5f));
    }

    public void StartQuiz()
    {
        StopAllCoroutines();
        tutorialBG.SetActive(false);
        BackendHandler.singleton.playCountdownBeep();
        countdown.GetComponent<Animator>().Play("Countdown");
        StartCoroutine(countLoop(1.6f));
    }

    public void toggleCount()
    {
        if (secondCount > 1)
        {
            secondCount--;
            countdownImg.GetComponent<Image>().sprite = countDownNos[secondCount];
            countdown.GetComponent<Animator>().Play("Countdown");
            StartCoroutine(countLoop(1.6f));
        } 
        else
        {
            countdown.GetComponent<Animator>().Play("CountStart");
            QuizHandler.singleton.beginQuiz();
        }

        if (secondCount == 1)
        {
            AnimationHandler.singleton.logoVanish();
        }
    }

    private IEnumerator showTutorial (float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //removed tutorial segment as it is combined with landing page; directly start the game
        StartQuiz();
        /*tutorialBG.SetActive(true);
        tutorialBG.GetComponent<Animator>().Play("TutorialAppear");
        StartCoroutine(closeTutorial(90f));*/
    }

    private IEnumerator returnToFloat (float waitTime)
    {
        yield return new WaitForSeconds (waitTime);
        landingPage.GetComponent<Animator>().Play("New State");
        logo.GetComponent<ObjectFloat>().enabled = true;
    }

    private IEnumerator closeTutorial (float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        tutorialBG.GetComponent<Animator>().Play("New State");
        tutorialBG.SetActive(false);
        backToLanding();
    }

    private IEnumerator startPulse (float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        landingPage.GetComponent<Animator>().Play("TitlePulse");
    }

    private IEnumerator countLoop (float waitTime)
    {
        BackendHandler.singleton.playCountdownBeep();
        yield return new WaitForSeconds(waitTime);
        toggleCount();
    }
}
