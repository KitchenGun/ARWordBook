using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public class NewBehaviourScript : MonoBehaviour
{
    List<Word_db> confirmed = new List<Word_db>();

    // Use this for initialization
    void Start()
    {
        LoadXml();
    }

    // Update is called once per frame
    void Update()
    {

    }
    #region XML 로드
    void LoadXml()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("Word");
        Debug.Log(textAsset);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNodeList nodes = xmlDoc.SelectNodes("Notebook/Item");

        foreach (XmlNode node in nodes)
        {
            Debug.Log("Word :: " + node.SelectSingleNode("Word").InnerText);
            Debug.Log("Mean :: " + node.SelectSingleNode("Mean").InnerText);
        }
    }
    #endregion
    #region XML 파싱
    void parseXML()
    {

        TextAsset textAsset = (TextAsset)Resources.Load("Word");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList xnList;

        //Confirmed
        xnList = xmlDoc.SelectNodes("/Item");
        foreach (XmlNode node in xnList)
        {

            Word_db cd = new Word_db();
            cd.Word = node["Word"].InnerText;
            Debug.Log(cd.Word);
            cd.Mean = node["Mean"].InnerText;
            confirmed.Add(cd);
        }
    }
    #endregion
    #region XML생성
    void CreateXml()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));
        XmlNode root = xmlDoc.CreateNode(XmlNodeType.Element, "Notebook", string.Empty);
        xmlDoc.AppendChild(root);
        XmlNode child = xmlDoc.CreateNode(XmlNodeType.Element, "Item", string.Empty);
        root.AppendChild(child);

        // 자식 노드에 들어갈 속성 생성
        XmlElement name = xmlDoc.CreateElement("Name");
        name.InnerText = "wergia";
        child.AppendChild(name);

        XmlElement lv = xmlDoc.CreateElement("Level");
        lv.InnerText = "1";
        child.AppendChild(lv);

        XmlElement exp = xmlDoc.CreateElement("Experience");
        exp.InnerText = "45";
        child.AppendChild(exp);

        xmlDoc.Save("./Assets/Resources/Word.xml");
    }
    #endregion
}

public class Word_db
{
    public string Word;
    public string Mean;
}