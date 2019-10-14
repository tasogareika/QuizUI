using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizHandler : MonoBehaviour
{
    public static QuizHandler singleton;
    [SerializeField] private GameObject quizPage;
    [SerializeField] private Slider numberSlider;
    [SerializeField] private TextMeshProUGUI currQuestionNo, timerDisplay;
    [HideInInspector] public int[] questionPool;
    private int questionProg, questionNo;
    private float timerMax, currTimer;
    private bool timerRun;

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        questionPool = new int[20];
        for (var i = 0; i < questionPool.Length; i++)
        {
            questionPool[i] = i;
        }
        quizPage.SetActive(false);
    }

    public void beginQuiz()
    {
        questionProg = 1;
        System.Random ran = new System.Random();
        questionNo = questionPool[ran.Next(0, questionPool.Length)];
        LoadXMLFile.singleton.label = "question" + questionNo.ToString();
        //TO DO: reduce array so no repeats
        LoadXMLFile.singleton.updateQuestion();
        timerMax = 60;
        timerRun = false;
        numberSlider.value = questionProg;
        currQuestionNo.text = questionProg.ToString();
        quizPage.SetActive(true);
        quizPage.GetComponent<Animator>().Play("ShowQuiz");
        StartCoroutine(startTimer(1.1f));
    }

    private void Update()
    {
        if (timerRun)
        {
            currTimer -= Time.deltaTime;
            timerDisplay.text = currTimer.ToString("F0");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            System.Random ran = new System.Random();
            questionNo = questionPool[ran.Next(0, questionPool.Length)];
            LoadXMLFile.singleton.label = "question" + questionNo.ToString();
            LoadXMLFile.singleton.updateQuestion();
        }
    }

    IEnumerator startTimer (float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        currTimer = timerMax;
        timerRun = true;
    }
}
