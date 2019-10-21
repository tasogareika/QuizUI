using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPageHandler : MonoBehaviour
{
    public static LastPageHandler singleton;
    [SerializeField] private GameObject lastPage;
    private float maxTimer, currTimer;
    private bool runTimer;

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        runTimer = false;
        lastPage.SetActive(false);
    }

    public void showLast()
    {
        maxTimer = 12;
        currTimer = maxTimer;
        lastPage.SetActive(true);
        lastPage.GetComponent<Animator>().Play("ShowLast");
        AnimationHandler.singleton.lastPage(lastPage.GetComponent<Animator>());
        runTimer = true;
    }

    private void Update()
    {
        if (runTimer)
        {
            currTimer -= Time.deltaTime;
            if (currTimer <= 0)
            {
                runTimer = false;
                lastPage.GetComponent<Animator>().Play("MoveToStart");
                StartCoroutine(timeOut(1f));
            }
        }
    }

    public void returnToStart()
    {
        currTimer = 0.1f;
    }

    IEnumerator timeOut(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        LandingStartHandler.singleton.backToLanding();
    }
}
