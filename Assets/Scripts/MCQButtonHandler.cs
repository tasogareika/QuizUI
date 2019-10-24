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
        } else
        {
            GetComponent<Image>().sprite = wrongImg;
            QuizHandler.singleton.showCorrectAnswer();
            BackendHandler.singleton.playWrongAns();
        }
        QuizHandler.singleton.closeButtons();
        StartCoroutine(nextQuestion(0.5f));
    }

    public void resetImage()
    {
        StartCoroutine(imageBack(0.5f));
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
