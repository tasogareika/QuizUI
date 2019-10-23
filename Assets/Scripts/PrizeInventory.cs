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
        Debug.Log(words);
        return words;
    }

    private void formatPrizeList() //put each indv prize onto a list cell
    {
        string[] prizes = lastData.Split('/');
        foreach (string s in prizes)
        {
            string[] eachPrize = s.Split('-');
            if (eachPrize.Length > 1)
            {
                prizeList.Add(eachPrize[1]);
            }
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

    public void updateInventory(int prizeClaimed)
    {
        int prizeIndex = prizeClaimed - 1;
        int prizesLeft = int.Parse(prizeList[prizeIndex]);
        prizesLeft--;
        prizeList[prizeIndex] = prizesLeft.ToString();

        //delete previous file for rewriting
        File.Delete(path);
        StreamWriter writer = new StreamWriter(path, true);

        for (int i = 0; i < prizeList.Count; i++)
        {
            int n = i + 1;
            writer.Write("\nPrize " + n + "-" + prizeList[i] + "/");
        }

        writer.Close();
        prizeList.Clear();

        //reimport for update
        lastData = readInventory();
        formatPrizeList();
    }
}
