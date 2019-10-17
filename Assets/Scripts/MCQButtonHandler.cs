using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MCQButtonHandler : MonoBehaviour
{
    public bool correct;
    public TextMeshProUGUI buttonText;

    public void checkAnswer()
    {
        QuizHandler.singleton.timerRun = false;
        if (correct)
        {
            GetComponent<Image>().color = Color.green;
        } else
        {
            GetComponent<Image>().color = Color.red;
        }
        QuizHandler.singleton.closeButtons();
        StartCoroutine(nextQuestion(0.5f));
    }

    private IEnumerator nextQuestion (float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        QuizHandler.singleton.answerToggle(correct);
    }
}
