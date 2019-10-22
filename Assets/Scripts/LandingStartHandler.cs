using TMPro;
using System.Collections;
using UnityEngine;

public class LandingStartHandler : MonoBehaviour
{
    public static LandingStartHandler singleton;
    [SerializeField] private GameObject landingPage, logo, tutorialBG, countdown;
    private int secondCount;

    private void Awake()
    {
        singleton = this;
        //set portrait res
        #if UNITY_STANDALONE
        Screen.SetResolution(720, 1280, false);
        //Screen.SetResolution(1080, 1920, false);
        #endif
    }

    private void Start()
    {
        secondCount = 3;
        AnimationHandler.singleton.attachFunction("LandingStartHandler");
        logo.GetComponent<ObjectFloat>().enabled = true;
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
        landingPage.GetComponent<Animator>().Play("ReturnToStart");
        AnimationHandler.singleton.returnToStart(landingPage.GetComponent<Animator>());
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
        logo.GetComponent<ObjectFloat>().enabled = false;
        secondCount = 3;
        countdown.GetComponent<TextMeshProUGUI>().text = secondCount.ToString();
        landingPage.GetComponent<Animator>().Play("MoveToTut");
        AnimationHandler.singleton.leaveScreen();
        StartCoroutine(showTutorial(1.5f));
    }

    public void StartQuiz()
    {
        StopAllCoroutines();
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

    private IEnumerator countLoop (float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        toggleCount();
    }
}
