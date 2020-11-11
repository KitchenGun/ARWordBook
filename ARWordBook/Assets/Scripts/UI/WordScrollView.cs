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
    private GameObject DeletePanel;
    private List<Button> ButtonList;
    public Text text;
    

    private int id;//삭제할 아이디 저장용

    // Start is called before the first frame update
    void Start()
    {
        //db값 가져오기
        wordList = DB.Singleton.DataBaseRead("Select * from Word");
    }
    #region  ScrollView 초기화
    public void ScrollViewInit()
    {
        text.text = wordList.Count.ToString();
        foreach (Word curWord in wordList)
        {//버튼을 추가
            GameObject Word = Instantiate(Button, content.transform.position, Quaternion.identity) as GameObject;
            Word.name = curWord.Id.ToString();
            Word.GetComponentInChildren<Text>().text = curWord.eWord;
            //추가 버튼 리스트 추가
            ButtonList.Add(Word.GetComponent<Button>());
        }
    }
    #endregion

    #region 버튼 클릭시 버튼의 단어 삭제 
   public void WordRemoveButtonClick()
    {
        int id = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        GameObject panel = Instantiate(DeletePanel, canvas.transform.position, Quaternion.identity) as GameObject;
    }
    public void Remove()
    {
        Destroy(DeletePanel);
        GameObject curWord=GameObject.Find(EventSystem.current.currentSelectedGameObject.name);
        Destroy(curWord);
        DB.Singleton.DataBaseDelete("Delete from Word where no = " + id);
    }
    public void RemoveCancel()
    {
        Destroy(DeletePanel);
    }
    #endregion
}
