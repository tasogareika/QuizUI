using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingHandler : MonoBehaviour
{
    public static EndingHandler singleton;
    public List<Sprite> scoreImgs;
    [SerializeField] private GameObject EndingPage, scoreDisplay;
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
        } else
        {
            //if user runs out of time
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
        EndingPage.GetComponent<Animator>().Play("MoveToReg");
        RegisterHandler.singleton.showRegister();
        AnimationHandler.singleton.switchRegister(EndingPage.GetComponent<Animator>());
    }
}
