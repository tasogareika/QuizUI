using TMPro;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RegisterHandler : MonoBehaviour
{
    public static RegisterHandler singleton;
    [SerializeField] private GameObject registerPage, termsPage;
    public List<TMP_InputField> entryFields;
    public Toggle checkTnC;
    public Color inputSelectColor;
    private string pathFile;
    private float maxTimer, currTimer;
    public bool timerRun, mobileShift;
    private Vector2 lastPos;

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        maxTimer = 92f;
        timerRun = false;

        pathFile = Application.dataPath + "/" + "regEntries"; //directory creation for data entries
        if (!Directory.Exists(pathFile))
        {
            Directory.CreateDirectory(pathFile);
        }

        registerPage.SetActive(false);
    }

    private void Update()
    {
        //ability to tab to next input when entering info 
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GameObject obj = EventSystem.current.currentSelectedGameObject;
            TMP_InputField input = obj.GetComponent<TMP_InputField>();
            if (input != null)
            {
                for (int i = 0; i < entryFields.Count; i++)
                {
                    if (obj.name == entryFields[i].name)
                    {
                        int b = i + 1;
                        if (b >= entryFields.Count)
                        {
                            b = 0;
                        }
                        EventSystem.current.SetSelectedGameObject(entryFields[b].gameObject);
                    }
                }
            }
        }

        //reset timer when there is activity
        if (timerRun)
        {
            Vector2 currPos = Input.mousePosition;
            currTimer -= Time.deltaTime;

            if (currTimer <= 0)
            {
                timerRun = false;
                SkipRegister(); //timeout
            }

            if (Input.anyKeyDown || currPos != lastPos)
            {
                lastPos = currPos;
                currTimer = maxTimer;
            }

            //hide keyboard when no input is clicked
            if (Input.GetMouseButtonDown(0))
            {
                GameObject obj = EventSystem.current.currentSelectedGameObject;
                if (obj == null && !KeyboardHandler.singleton.touchingKeys && KeyboardHandler.singleton.isOnScreen)
                {
                    foreach (var entry in entryFields)
                    {
                        entry.GetComponent<Image>().color = Color.white;
                    }
                    KeyboardHandler.singleton.hideKeyboard();

                    if (mobileShift)
                    {
                        numberToggle(mobileShift);
                    }
                }

                if (obj != null)
                {
                    var input = obj.GetComponent<TMP_InputField>();
                    if (input != null && input.text.Length != 0)
                    {
                        KeyboardHandler.singleton.middleCaret = true;
                        KeyboardHandler.singleton.cursorPos = input.caretPosition;
                    } else if (input != null && input.text.Length < 1)
                    {
                        KeyboardHandler.singleton.middleCaret = false;
                        KeyboardHandler.singleton.cursorPos = 0;
                    }
                }
            }
        }
    }

    public void showRegister()
    {
        registerPage.SetActive(true);
        //clear forms
        foreach (var t in entryFields)
        {
            t.text = null;
        }
        checkTnC.isOn = false;
        currTimer = maxTimer;
        timerRun = true;
        mobileShift = false;
        lastPos = Input.mousePosition;
        registerPage.GetComponent<Animator>().Play("ShowReg");
    }

    public void openTerms()
    {
        termsPage.SetActive(true);
        if (KeyboardHandler.singleton.isOnScreen)
        {
           if (mobileShift)
            {
                numberToggle(mobileShift);
            }
            KeyboardHandler.singleton.hideKeyboard();
        }
        AnimationHandler.singleton.toggleButton();
    }

    public void closeTerms()
    {
        termsPage.SetActive(false);
        AnimationHandler.singleton.toggleButton();
    }

    public void clickRegister()
    {
        if (registerCheck())
        {
            saveInformation();
            BackendHandler.singleton.playMainButtonClick();
            registerPage.GetComponent<Animator>().Play("MoveToEnd");
            LastPageHandler.singleton.showLast();
        } else
        {
            BackendHandler.singleton.playError();
        }
    }

    public bool registerCheck() //checking to see if all entries are filled and the check for terms and conditions is toggled on
    {
        for (int i = 0; i < entryFields.Count; i++)
        {
            bool empty = string.IsNullOrEmpty(entryFields[i].text);
            if (empty)
            {
                entryFields[i].GetComponent<Image>().color = Color.red;
                foreach (var entry in entryFields)
                {
                    if (string.IsNullOrEmpty(entry.text))
                    {
                        entry.GetComponent<Image>().color = Color.red;
                    }
                }

                if (!checkTnC.isOn)
                {
                    checkTnC.transform.GetChild(0).GetComponent<Image>().color = Color.red;;
                }

                return false;
            }
        }

        //check email validity
        foreach (var entry in entryFields)
        {
            if (entry.name == "EmailInput")
            {
                string emailAdd = entry.text.ToString();
                if (emailAdd.Length < 5 || !emailAdd.Contains("@") || !emailAdd.Contains("."))
                {
                    entry.GetComponent<Image>().color = Color.red;

                    if (!checkTnC.isOn)
                    {
                        checkTnC.transform.GetChild(0).GetComponent<Image>().color = Color.red;
                    }

                    return false;
                }
            }
        }

        if (!checkTnC.isOn)
        {
            checkTnC.transform.GetChild(0).GetComponent<Image>().color = Color.red;
            return false;
        }

        return true;
    }

    public void formClicked() //previously only had the first empty item to give feedback, changed to all
    {
        selectToggle();
        selectInput();
    }

    public void selectInput()
    {
        GameObject obj = EventSystem.current.currentSelectedGameObject;
        
        //obj.GetComponent<Image>().color = Color.white;
        foreach (var entry in entryFields)
        {
            entry.GetComponent<Image>().color = Color.white;
            if (entry.gameObject == obj)
            {
                if (entry.name != "MobileInput" && mobileShift == true)
                {
                    numberToggle(mobileShift);
                }

                entry.GetComponent<Image>().color = inputSelectColor;
                if (!KeyboardHandler.singleton.isOnScreen)
                {
                    KeyboardHandler.singleton.showKeyboard();
                }
                KeyboardHandler.singleton.currInput = obj.GetComponent<TMP_InputField>();

                if (entry.name == "MobileInput" && mobileShift == false)
                {
                    numberToggle(mobileShift);
                }
            }
        }
    }

    public void selectToggle()
    {
        KeyboardHandler.singleton.currInput = null;
        checkTnC.transform.GetChild(0).GetComponent<Image>().color = Color.white;
    }

    private void numberToggle(bool on)
    {
        if (!on)
        {
            mobileShift = true;
            registerPage.GetComponent<Animator>().Play("RegShiftUp");
        } else
        {
            mobileShift = false;
            registerPage.GetComponent<Animator>().Play("RegShiftDown");
        }
        KeyboardHandler.singleton.numberToggle(mobileShift);
    }

    private void saveInformation() //save information from register page
    {
        DateTime currDT = DateTime.Now;
        string path = pathFile + "/" + entryFields[0].text + entryFields[1].text + "_" + currDT.Year + currDT.Month + currDT.Day + "_" + currDT.Hour + currDT.Minute + ".txt";
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine("Date: " + currDT.ToShortDateString() +
            "\nTime: " + currDT.ToShortTimeString() +
            "\nName: " + entryFields[0].text + " " + entryFields[1].text + 
            "\nDesgination: " + entryFields[2].text + 
            "\nCompany: " + entryFields[3].text + 
            "\nEmail: " + entryFields[4].text + 
            "\nMobile: " + entryFields[5].text +
            "\nScore: " + QuizHandler.score);
        writer.Close();
    }

    public void SkipRegister() //triggers upon time out on info page; save info just in case
    {
        DateTime currDT = DateTime.Now;
        string path = pathFile + "/UnRegEntry_" + currDT.Year + currDT.Month + currDT.Day + "_" + currDT.Hour + currDT.Minute + ".txt";
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine("Date: " + currDT.ToShortDateString() +
            "\nTime: " + currDT.ToShortTimeString() +
            "\nScore: " + QuizHandler.score);
        writer.Close();
        registerPage.GetComponent<Animator>().Play("MoveToEnd");
        LastPageHandler.singleton.showLast();
    }
}