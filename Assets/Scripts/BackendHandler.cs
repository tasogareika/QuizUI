using UnityEngine;
using UnityEngine.SceneManagement;

public class BackendHandler : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            if (RegisterHandler.singleton.timerRun)
            {
                RegisterHandler.singleton.SkipRegister();
            }
        }
    }
}
