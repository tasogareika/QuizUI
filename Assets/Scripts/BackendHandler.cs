using UnityEngine;
using UnityEngine.SceneManagement;

public class BackendHandler : MonoBehaviour
{
    public static BackendHandler singleton;
    private AudioSource audioPlayer;
    public AudioSource BGplayer, xtraPlayer;
    public AudioClip mainBGM, quizBGM, mainButtonClick, countdownBeep, countdownLast, correctSound, wrongSound, last5secs, timeUpSound, quizCompleteSFX, audienceCheerSFX, pageshiftSFX, errorSFX;
    private int clickNo;
    private float clickTime, clickDelay;

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        //set portrait res
        #if UNITY_STANDALONE
        //Screen.SetResolution(720, 1280, true);
        Screen.SetResolution(1080, 1920, true);
        #endif

        clickNo = 0;
        clickDelay = 0.5f;
        audioPlayer = GetComponent<AudioSource>();
        playMainBGM();
    }

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

        if (Input.GetKeyDown(KeyCode.F9))
        {
            if (QuizHandler.singleton.timerRun)
            {
                QuizHandler.singleton.skipTime();
            }
        }
    }

    //ref: https://forum.unity.com/threads/detect-double-click-on-something-what-is-the-best-way.476759/
    public void doubleClick()
    {
        clickNo++;
        if (clickNo == 1)
        {
            clickTime = Time.time;
        }

        if (clickNo > 1 && Time.time - clickTime < clickDelay)
        {
            clickNo = 0;
            clickTime = 0;
            SceneManager.LoadScene(0);
        }
        else if (clickNo > 2 || Time.time - clickTime > 1)
        {
            clickNo = 0;
        }
    }

    public void playMainBGM ()
    {
        BGplayer.Stop();
        BGplayer.clip = mainBGM;
        BGplayer.Play();
    }

    public void playQuizBGM()
    {
        BGplayer.Stop();
        BGplayer.clip = quizBGM;
        BGplayer.Play();
    }

    public void stopBGM()
    {
        BGplayer.Stop();
    }

    public void playLast5Secs()
    {
        xtraPlayer.clip = last5secs;
        xtraPlayer.Play();
    }

    public void stopXtraPlayer()
    {
        xtraPlayer.Stop();
    }

    public void playTimeUpSound()
    {
        audioPlayer.clip = timeUpSound;
        audioPlayer.Play();
    }

    public void playQuizCompleteSound()
    {
        audioPlayer.clip = quizCompleteSFX;
        audioPlayer.Play();
    }

    public void playAudienceCheer()
    {
        xtraPlayer.clip = audienceCheerSFX;
        xtraPlayer.Play();
    }

    public void playMainButtonClick()
    {
        audioPlayer.clip = mainButtonClick;
        audioPlayer.Play();
    }

    public void playCountdownBeep()
    {
        audioPlayer.clip = countdownBeep;
        audioPlayer.Play();
    }

    public void playCountdownLast()
    {
        audioPlayer.clip = countdownLast;
        audioPlayer.Play();
    }

    public void playCorrectAns ()
    {
        audioPlayer.clip = correctSound;
        audioPlayer.Play();
    }

    public void playWrongAns()
    {
        audioPlayer.clip = wrongSound;
        audioPlayer.Play();
    }

    public void playPageMove()
    {
        xtraPlayer.clip = pageshiftSFX;
        xtraPlayer.Play();
    }

    public void playError()
    {
        audioPlayer.clip = errorSFX;
        audioPlayer.Play();
    }
}
