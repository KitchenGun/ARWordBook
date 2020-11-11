using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollView : MonoBehaviour
{

    private List<Word> wordList;
    [SerializeField]
    private GameObject content;
    [SerializeField]
    private GameObject Toggle;
    private List<Toggle> contentList;
    private List<Word> rememberWordList;
    private GameObject Word;
    // Start is called before the first frame update
    void Start()
    {
        //db값 가져오기
         wordList = DB.Singleton.DataBaseRead("Select * from Word");
        ScrollViewInit();
    }
    #region  ScrollView 초기화
    private void ScrollViewInit()
    {
        foreach (Word curWord in wordList)
        {
            Word = Instantiate(Toggle, content.transform.position, Quaternion.identity) as GameObject;
            Word.name = curWord.Id.ToString();
            Word.GetComponentInChildren<Text>().text = curWord.eWord;
            Word.transform.parent = content.transform;
            //contentList.Add(Word.GetComponent<Toggle>());
          }
    }
    #endregion

    #region ScrollView 체크  및 단어스크립트로 보내는 용도
    public List<Word> ScrollCheck()
    {
        string quary;
        foreach (Toggle curToggle in contentList)
        {
            if(curToggle.isOn)
            {
                int id = int.Parse(curToggle.gameObject.name);
                quary = string.Format("Select *from Word where no=" + id);
                rememberWordList.Add(DB.Singleton.DataBaseRead(quary)[0]);
            }
        }
        return rememberWordList;
    }

    #endregion
}
