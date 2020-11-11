using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WordScrollView : MonoBehaviour
{

    private List<Word> wordList;
    [SerializeField]
    private GameObject content;//위치값 받는 용도
    [SerializeField]
    private GameObject canvas;//위치값 받는 용도
    [SerializeField]
    private GameObject Button;
    [SerializeField]
    private GameObject DeletePanel;
    private List<GameObject> ButtonList;
    public Text text;
    private GameObject prbutton;
    private GameObject panel;
    private string name;//삭제할 아이디 저장용

    #region  ScrollView 초기화
    public void ScrollViewInit()
    {
        //wordList.Clear();
         //db값 가져오기
        wordList = DB.Singleton.DataBaseRead("Select * from Word");
        text.text = wordList.Count.ToString();

        //foreach(GameObject gamobj in ButtonList)
        //{
        //    Destroy(gamobj);
        //}
        //ButtonList.Clear();

        foreach (Word curWord in wordList)
        {//버튼을 추가
            GameObject Word = Instantiate(Button, content.transform.position, Quaternion.identity) as GameObject;
            Word.name = curWord.Id.ToString();
            Word.GetComponentInChildren<Text>().text = curWord.eWord;
            Word.transform.parent = content.transform;
            //추가 버튼 리스트 추가
            //ButtonList.Add(Word);
        }
        wordList.Clear();
    }
    #endregion

    #region 버튼 클릭시 버튼의 단어 삭제 
   public void WordRemoveButtonClick()
    {
        prbutton = EventSystem.current.currentSelectedGameObject;
        name = (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text);
        panel = Instantiate(DeletePanel, canvas.transform.position, Quaternion.identity) as GameObject;
        panel.transform.parent = canvas.transform;
    }
    public void Remove()
    {
        Destroy(prbutton);
        Destroy(panel);
        DB.Singleton.DataBaseDelete("Delete from Word where word = '" + name+"'");
       
    }
    public void RemoveCancel()
    {
        Destroy(panel);
    }
    #endregion
}
