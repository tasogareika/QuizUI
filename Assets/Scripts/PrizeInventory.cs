using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeInventory : MonoBehaviour
{
    public static PrizeInventory singleton;
    public TextAsset prizesTxt;
    public int prizeNo;
    private string path, lastData;
    private List<string> prizeList;

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        path = Application.dataPath + "/" + "prizeInventory/prizeInventory.txt";
        prizeList = new List<string>();
        lastData = readInventory();
        formatPrizeList();
    }

    public string readInventory()
    {
        StreamReader reader = new StreamReader(path);
        string words = reader.ReadToEnd();
        reader.Close();
        return words;
    }

    private void formatPrizeList() //put each indv prize onto a list cell
    {
        string[] prizes = lastData.Split('\n');
        foreach (string s in prizes)
        {
            string[] eachPrize = s.Split('/');
            prizeList.Add(eachPrize[1]);
        }
    }

    public void getPrize(int score)
    {
        if (score == 20)
        {
            prizeNo = 1;
        }
        else if (score <= 19 && score >= 15)
        {
            prizeNo = 2;
        }
        else if (score <= 14 && score >= 10)
        {
            prizeNo = 3;
        }
        else if (score <= 9 && score >= 5)
        {
            prizeNo = 4;
        }
        else if (score <= 4)
        {
            prizeNo = 5;
        }
    }

    public void updateInventory()
    {

    }
}
