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
        maxTimer = 10;
        currTimer = maxTimer;
        lastPage.SetActive(true);
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
                lastPage.SetActive(false);
                StartCoroutine(timeOut(1f));
            }

            if (Input.GetMouseButtonDown(0))
            {
                currTimer = 0.1f;
            }
        }
    }

    IEnumerator timeOut(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        LandingStartHandler.singleton.backToLanding();
    }
}
