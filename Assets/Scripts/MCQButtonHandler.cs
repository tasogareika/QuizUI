using TMPro;
using UnityEngine;

public class MCQButtonHandler : MonoBehaviour
{
    public bool correct;
    public TextMeshProUGUI buttonText;

    public void checkAnswer()
    {
        QuizHandler.singleton.answerToggle(correct);
    }
}
