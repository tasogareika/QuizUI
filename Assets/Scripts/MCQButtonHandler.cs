using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MCQButtonHandler : MonoBehaviour
{
    public bool correct;
    public TextMeshProUGUI buttonText;
    public Sprite normalBtn, correctImg, wrongImg;

    public void checkAnswer()
    {
        QuizHandler.singleton.timerRun = false;
        if (correct)
        {
            GetComponent<Image>().sprite = correctImg;
            BackendHandler.singleton.playCorrectAns();
            StartCoroutine(nextQuestion(0.5f));
        } else
        {
            GetComponent<Image>().sprite = wrongImg;
            QuizHandler.singleton.showCorrectAnswer();
            BackendHandler.singleton.playWrongAns();
            StartCoroutine(nextQuestion(2f));
        }
        QuizHandler.singleton.closeButtons();
    }

    public void resetImage()
    {
        StartCoroutine(imageBack(2f));
    }

    private IEnumerator imageBack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GetComponent<Image>().sprite = normalBtn;
    }

    private IEnumerator nextQuestion (float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GetComponent<Image>().sprite = normalBtn;
        QuizHandler.singleton.answerToggle(correct);
    }
}
