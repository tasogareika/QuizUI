using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeInventory : MonoBehaviour
{
    public static PrizeInventory singleton;
    public int prizeNo;
    private string path, lastData;
    public List<Sprite> prizeImgs;
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
        string[] prizes = lastData.Split('\n');
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
        prizeNo = 0;
        if (score == 20)
        {
            checkStock(1);
        }
        else if (score <= 19 && score >= 15)
        {
            checkStock(2);
        }
        else if (score <= 14 && score >= 10)
        {
            checkStock(3);
        }
        else if (score <= 9 && score >= 5)
        {
            checkStock(4);
        }
        else if (score <= 4)
        {
            checkStock(5);
        }
    }

    private void checkStock(int stockNo)
    {
        int prizeIndex = stockNo - 1;
        int prizeLeft = int.Parse(prizeList[prizeIndex]);
        if (prizeLeft >= 1)
        {
            prizeNo = stockNo;
        } else
        {
            for (int i = prizeIndex; i < prizeList.Count; i++) //check for lower tier prizes first
            {
                int p = int.Parse(prizeList[i]);
                if (p >= 1)
                {
                    prizeNo = i + 1;
                    break;
                }
            }

            if (prizeNo == 0)
            {
                for (int j = prizeIndex; j > -1; j--) //check higher tier prizes only if all lower ones are out of stock
                {
                    Debug.Log(j);
                    int p = int.Parse(prizeList[j]);
                    if (p >= 1)
                    {
                        prizeNo = j + 1;
                        break;
                    }
                }
            }
        }

        if (prizeNo == 0)
        {
            Debug.Log("no prizes left");
        }
    }

    public void updateInventory(int prizeClaimed)
    {
        if (prizeClaimed != 0)
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
                writer.Write("\nPrize " + n + "-" + prizeList[i]);
            }

            writer.Close();
            prizeList.Clear();

            //reimport for update
            lastData = readInventory();
            formatPrizeList();
        }
    }
}
