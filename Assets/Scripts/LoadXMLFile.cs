using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class LoadXMLFile : MonoBehaviour {

    public TextAsset questionsXML;
    private TextAsset xmlRawFile;
    public static LoadXMLFile singleton;
    [HideInInspector] public string data, label, question, choices;
    [HideInInspector] public int answer;

    void Awake()
    {
        singleton = this;
    }

    void Start()
    {
        xmlRawFile = questionsXML;
        data = xmlRawFile.text;
    }

    public void updateQuestion()
    {
        parseXMLFile(data);
    }

    void parseXMLFile (string xmlData)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlData));

        string xmlPathPattern = "//document/" + label;
        XmlNodeList myNodeList = xmlDoc.SelectNodes(xmlPathPattern);
        XmlNode firstNode = myNodeList.Item(0);
        XmlNodeList theseNodes = firstNode.ChildNodes;

        for (int i = 0; i < theseNodes.Count; i++)
        {
            //refactored node organization to relevant lists
            XmlNode finalNode = theseNodes[i];
            switch (finalNode.Name)
            {
                case "qns":
                    question = finalNode.InnerXml;
                    break;

                case "choices":
                    choices = finalNode.InnerXml;
                    break;

                case "answer":
                    answer = int.Parse(finalNode.InnerXml);
                    break;
            }
        }
    }
}
