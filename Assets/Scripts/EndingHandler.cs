using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingHandler : MonoBehaviour
{
    public static EndingHandler singleton;
    public List<Sprite> scoreImgs;
    public Sprite wellDone, timesUp;
    [SerializeField] private GameObject EndingPage, scoreDisplay, header;
    private float returnTimer, maxTimer;
    private bool countdown;

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        EndingPage.SetActive(false);
        maxTimer = 90f;
        countdown = false;
    }

    private void Update()
    {
        if (countdown)
        {
            returnTimer -= Time.deltaTime;
            if (returnTimer <= 0)
            {
                countdown = false;
                EndingPage.SetActive(false);
                LandingStartHandler.singleton.backToLanding();
            }
        }
    }

    public void showEnd(bool complete)
    {
        EndingPage.SetActive(true);
        returnTimer = maxTimer;
        countdown = true;
        
        if (complete)
        {
            //if user finishes quiz
            BackendHandler.singleton.playQuizCompleteSound();
            header.GetComponent<Image>().sprite = wellDone;
        } else
        {
            //if user runs out of time
            BackendHandler.singleton.playTimeUpSound();
            header.GetComponent<Image>().sprite = timesUp;
        }

        //display score
        scoreDisplay.GetComponent<Image>().sprite = scoreImgs[QuizHandler.score];
        EndingPage.GetComponent<Animator>().Play("EndingRollDown");
        AnimationHandler.singleton.topBGRollDown();
        AnimationHandler.singleton.logoReturn();
        AnimationHandler.singleton.quizEndReturn(EndingPage.GetComponent<Animator>());
    }

    public void GoToRegister()
    {
        countdown = false;
        BackendHandler.singleton.playMainButtonClick();
        BackendHandler.singleton.playPageMove();
        EndingPage.GetComponent<Animator>().Play("MoveToReg");
        RegisterHandler.singleton.showRegister();
        AnimationHandler.singleton.switchRegister(EndingPage.GetComponent<Animator>());
    }
}
