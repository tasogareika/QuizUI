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

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        registerPage.SetActive(false);
    }

    public void showRegister()
    {
        registerPage.SetActive(true);
    }

    public void openTerms()
    {
        termsPage.SetActive(true);
    }

    public void closeTerms()
    {
        termsPage.SetActive(false);
    }

    public void clickRegister()
    {
        if (registerCheck())
        {
            saveInformation();
            registerPage.SetActive(false);
            LastPageHandler.singleton.showLast();
        } 
    }

    public bool registerCheck()
    {
        for (int i = 0; i < entryFields.Count; i++)
        {
            bool empty = string.IsNullOrEmpty(entryFields[i].text);
            if (empty)
            {
                entryFields[i].GetComponent<Image>().color = Color.red;
                return false;
            }
        }

        if (!checkTnC.isOn)
        {
            checkTnC.transform.GetChild(0).GetComponent<Image>().color = Color.red;
            return false;
        }

        return true;
    }

    public void selectInput()
    {
        GameObject obj = EventSystem.current.currentSelectedGameObject;
        obj.GetComponent<Image>().color = Color.white;
    }

    public void selectToggle()
    {
        checkTnC.transform.GetChild(0).GetComponent<Image>().color = Color.white;
    }

    private void saveInformation()
    {
        DateTime currDT = DateTime.Now;
        string path = "Assets/Resources/" + entryFields[0].text + entryFields[1].text + "_" + currDT.Year + currDT.Month + currDT.Day + "_" + currDT.Hour + currDT.Minute + ".txt";
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

    public void SkipRegister()
    {
        DateTime currDT = DateTime.Now;
        string path = "Assets/Resources/UnRegEntry_" + currDT.Year + currDT.Month + currDT.Day + "_" + currDT.Hour + currDT.Minute + ".txt";
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine("Date: " + currDT.ToShortDateString() +
            "\nTime: " + currDT.ToShortTimeString() +
            "\nScore: " + QuizHandler.score);
        writer.Close();
        registerPage.SetActive(false);
        LastPageHandler.singleton.showLast();
    }
}
