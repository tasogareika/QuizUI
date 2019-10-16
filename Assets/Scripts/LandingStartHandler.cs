using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LandingStartHandler : MonoBehaviour
{
    public static LandingStartHandler singleton;
    [SerializeField] private GameObject landingPage, logo, startText, tutorialBG, countdown;
    private int secondCount;

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        secondCount = 3;
        logo.GetComponent<ObjectFloat>().enabled = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (logo.GetComponent<ObjectFloat>().enabled)
            {
                showStart();
            }
        }
    }

    public void backToLanding()
    {
        landingPage.GetComponent<Animator>().Play("ReturnToStart");
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
        secondCount = 3;
        countdown.GetComponent<TextMeshProUGUI>().text = secondCount.ToString();
        landingPage.GetComponent<Animator>().Play("MoveToTut");
        StartCoroutine(showTutorial(1.5f));
    }

    public void StartQuiz()
    {
        tutorialBG.SetActive(false);
        countdown.GetComponent<Animator>().Play("Countdown");
        StartCoroutine(countLoop(1.6f));
    }

    public void toggleCount()
    {
        if (secondCount > 1)
        {
            secondCount--;
            countdown.GetComponent<TextMeshProUGUI>().text = secondCount.ToString();
            countdown.GetComponent<Animator>().Play("Countdown");
            StartCoroutine(countLoop(1.6f));
        }
        else
        {
            countdown.GetComponent<Animator>().Play("New State");
            QuizHandler.singleton.beginQuiz();
        }
    }

    private IEnumerator showTutorial (float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        tutorialBG.SetActive(true);
        tutorialBG.GetComponent<Animator>().Play("TutorialAppear");
    }

    private IEnumerator returnToFloat (float waitTime)
    {
        yield return new WaitForSeconds (waitTime);
        landingPage.GetComponent<Animator>().Play("New State");
        logo.GetComponent<ObjectFloat>().enabled = true;
        startText.GetComponent<Image>().color = Color.clear;
    }

    private IEnumerator countLoop (float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        toggleCount();
    }
}
