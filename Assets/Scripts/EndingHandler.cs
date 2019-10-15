using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingHandler : MonoBehaviour
{
    public static EndingHandler singleton;
    [SerializeField] private GameObject EndingPage;
    [SerializeField] private TextMeshProUGUI headerText, finalScore;

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        EndingPage.SetActive(false);
    }

    public void showEnd(bool complete)
    {
        EndingPage.SetActive(true);
        
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
}
