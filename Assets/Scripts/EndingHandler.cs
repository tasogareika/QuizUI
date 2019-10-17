using System;
using System.IO;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingHandler : MonoBehaviour
{
    public static EndingHandler singleton;
    [SerializeField] private GameObject EndingPage;
    [SerializeField] private TextMeshProUGUI headerText, finalScore;
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
            headerText.text = "Quiz Complete!";
        } else
        {
            //if user runs out of time
            headerText.text = "Time's Up!";
        }

        finalScore.text = QuizHandler.score.ToString();
    }

    public void GoToRegister()
    {
        countdown = false;
        EndingPage.SetActive(false);
        RegisterHandler.singleton.showRegister();
    }
}
