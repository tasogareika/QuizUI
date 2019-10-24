using TMPro;
using System.Collections;
using UnityEngine;

public class LastPageHandler : MonoBehaviour
{
    public static LastPageHandler singleton;
    [SerializeField] private GameObject lastPage, prizeDisplay;
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
        BackendHandler.singleton.playPageMove();
        lastPage.GetComponent<Animator>().Play("ShowLast");
        prizeDisplay.GetComponent<TextMeshProUGUI>().text = PrizeInventory.singleton.prizeNo.ToString();
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
                BackendHandler.singleton.playPageMove();
                PrizeInventory.singleton.updateInventory(PrizeInventory.singleton.prizeNo);
                lastPage.GetComponent<Animator>().Play("MoveToStart");
                AnimationHandler.singleton.topBGVanish();
                StartCoroutine(timeOut(1f));
            }
        }
    }

    public void returnToStart()
    {
        BackendHandler.singleton.playMainButtonClick();
        currTimer = 0.1f;
    }

    IEnumerator timeOut(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        LandingStartHandler.singleton.backToLanding();
    }
}
