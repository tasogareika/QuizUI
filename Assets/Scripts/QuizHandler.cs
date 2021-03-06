﻿using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizHandler : MonoBehaviour
{
    public static QuizHandler singleton;
    public static int score, questionCount;
    private IDictionary<int, string> choicesRef;
    private IDictionary<int, bool> answerRef;
    [SerializeField] private GameObject quizPage;
    [SerializeField] private Slider numberSlider;
    [SerializeField] private TextMeshProUGUI currQuestionNo, timerDisplay, questionDisplay;
    public List<int> questionPool;
    [HideInInspector] public List<String> MCQChoices;
    public List<GameObject> MCQButtons;
    [HideInInspector] public List<GameObject> tempButtonList;
    private int questionProg, questionNo, totalQns, realAns;
    private float timerMax, currTimer;
    public bool timerRun;

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        questionCount = 20;
        numberSlider.maxValue = questionCount;
        questionPool = new List<int>();
        MCQChoices = new List<string>();
        tempButtonList = new List<GameObject>();
        choicesRef = new Dictionary<int, string>();
        answerRef = new Dictionary<int, bool>();
        quizPage.SetActive(false);
    }

    public void beginQuiz() //start of quiz setup
    {
        score = 0;
        questionProg = 0;
        questionPool.Clear();
        totalQns = LoadXMLFile.singleton.getNodeNumber() + 1; //get all questions from XML file
        for (int i = 1; i < totalQns; i++)
        {
            questionPool.Add(i);
        }
        timerMax = 60;
        timerRun = false;
        timerDisplay.text = timerMax.ToString();
        numberSlider.value = questionProg;
        currQuestionNo.text = questionProg.ToString();
        quizPage.SetActive(true);
        quizPage.GetComponent<Animator>().Play("ShowQuiz");
        BackendHandler.singleton.playQuizBGM();
        StartCoroutine(startTimer(1.1f));
    }

    private void nextQuestion()
    {
        //clear temp list for shuffle later + Dicts
        tempButtonList.Clear();
        answerRef.Clear();
        choicesRef.Clear();

        //pause timer
        timerRun = false;

        //update progress
        questionProg++;
        numberSlider.value = questionProg;
        currQuestionNo.text = questionProg.ToString();

        //select new question from pool and update
        if (questionPool.Count > 1)
        {
            System.Random ran = new System.Random();
            int index = ran.Next(0, questionPool.Count);
            questionNo = questionPool[index];
            questionPool.RemoveAt(index);
        }
        else if (questionPool.Count == 1)
        {
            questionNo = questionPool[0];
            questionPool.Clear();
        }

        LoadXMLFile.singleton.label = "question" + questionNo.ToString();
        LoadXMLFile.singleton.updateQuestion();
        questionDisplay.text = LoadXMLFile.singleton.question;
        realAns = LoadXMLFile.singleton.answer;

        //update answers for new question
        MCQChoices.Clear();
        string[] choices = LoadXMLFile.singleton.choices.Split('|');
        foreach (string s in choices)
        {
            MCQChoices.Add(s);
        }

        //update buttons with answers, toggle on correct bool for right answer
        for (int i = 0; i < MCQButtons.Count; i++)
        {
            var buttonHandler = MCQButtons[i].GetComponent<MCQButtonHandler>();
            buttonHandler.buttonText.text = MCQChoices[i];
            buttonHandler.correct = false;
            tempButtonList.Add(MCQButtons[i]);
        }
        MCQButtons[realAns-1].GetComponent<MCQButtonHandler>().correct = true;

        //shuffle list, put ref to Dict 
        for (int n = 0; n < MCQButtons.Count; n++)
        {
            System.Random ran2 = new System.Random();
            int index = ran2.Next(0, tempButtonList.Count);
            choicesRef.Add(n, tempButtonList[index].GetComponent<MCQButtonHandler>().buttonText.text);
            answerRef.Add(n, tempButtonList[index].GetComponent<MCQButtonHandler>().correct);
            tempButtonList.RemoveAt(index);
        }

        //transfer ref from Dict onto button choices and enable buttons again
        for (int b = 0; b < MCQButtons.Count; b++)
        {
            var buttonHandler = MCQButtons[b].GetComponent<MCQButtonHandler>();
            buttonHandler.buttonText.text = choicesRef[b];
            buttonHandler.correct = answerRef[b];
            MCQButtons[b].GetComponent<Image>().color = Color.white;
            MCQButtons[b].GetComponent<Button>().interactable = true;
            if (buttonHandler.correct)
            {
                Debug.Log("answer is " + buttonHandler.buttonText.text + ": " + MCQButtons[b].name);
            } 
        }

        //continue timer
        timerRun = true;
    }

    public void closeButtons() //disable buttons to prevent double responses to a question
    {
        foreach (var b in MCQButtons)
        {
           b.GetComponent<Button>().interactable = false;
        }
    }

    public void answerToggle (bool correct) //trigged when user presses a button to answer
    {
        if (correct)
        {
            Debug.Log("correct answer");
            score++;
        }
        
        if (numberSlider.value != numberSlider.maxValue)
        {
            nextQuestion();
        } else
        {
            endQuiz(true);
        }
    }

    public void showCorrectAnswer () //shows correct ans when user picks the wrong one
    {
        foreach (var b in MCQButtons)
        {
            if (b.GetComponent<MCQButtonHandler>().correct)
            {
                b.GetComponent<Image>().sprite = b.GetComponent<MCQButtonHandler>().correctImg;
                b.GetComponent<MCQButtonHandler>().resetImage();
            }
        }
    }

    public void skipTime() //for testing purposes
    {
        currTimer = 3f;
    }

    private void Update()
    {
        if (timerRun)
        {
            currTimer -= Time.deltaTime;
            timerDisplay.text = currTimer.ToString("F0");

            if (currTimer <= 5)
            {
                if (!BackendHandler.singleton.xtraPlayer.isPlaying)
                {
                    BackendHandler.singleton.playLast5Secs();
                }
            }

            if (currTimer <= 0)
            {
                endQuiz(false);
            }
        }
    }

    private void endQuiz(bool complete) //end of quiz
    {
        BackendHandler.singleton.stopBGM();
        BackendHandler.singleton.stopXtraPlayer();
        timerRun = false;
        quizPage.SetActive(false);
        EndingHandler.singleton.showEnd(complete);
    }

    IEnumerator startTimer (float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        currTimer = timerMax;
        nextQuestion();
    }
}
