using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChageWord : MonoBehaviour
{
    public Text word;               
    public Text wordes;
    public float time;
    public Text count;
    public Text roop;
    public Button startbutton;
    public Button stopbutton;
    public ScrollView WordScroll;
    public Button Cam;
    
    private List<Word> rememberWordList;
    private int ran;
    private bool isbutton;       //버튼이 눌렸는지 확인

    private void Start()
    {
        WordEnabled(false);
    }

    #region Enable
    private void WordEnabled(bool enabled)
    {
        word.enabled = wordes.enabled = isbutton = roop.enabled = enabled;        
    }

    private void ButtonPosition()
    {
        var temp = startbutton.transform.position;
        startbutton.transform.position = stopbutton.transform.position;
        stopbutton.transform.position = temp;
    }    
    
    private void CamAndScrollEnable(bool click)
    {
        if (click)
        {
            Cam.transform.position += new Vector3(1000, 0, 0);
            WordScroll.transform.position += new Vector3(1000, 0, 0);
        }
        else
        {
            Cam.transform.position -= new Vector3(1000, 0, 0);
            WordScroll.transform.position -= new Vector3(1000, 0, 0);
        }
    }
    #endregion

    #region start버튼
    public void StartButton()
    {
        if (isbutton == false)
        {
            WordEnabled(true);
            CamAndScrollEnable(true);
            ButtonPosition();
            CreateList();
            ran = Random.Range(0, rememberWordList.Count);
            StartCoroutine("Chage");
        }
    }

    private IEnumerator Chage()
    {
        int roopcount = int.Parse(count.text);

        for (int i = 0; i < roopcount; i++)
        {
            roop.text = (roopcount - i).ToString();
            if (isbutton == true)
            {
                word.text = rememberWordList[ran].eWord;
                wordes.text = rememberWordList[ran].kWord;

                int temp = ran;
                do
                    ran = Random.Range(0, rememberWordList.Count);
                while (temp == ran);   
                
                yield return new WaitForSeconds(time);
            }
            else
                break;            
        }

        WordEnabled(false);
        CamAndScrollEnable(false);
        ButtonPosition();
    }
    #endregion

    #region stop버튼
    public void StopButton()
    {
        WordEnabled(false);
    }
    #endregion

    #region up & down count
    public void UpCount()
    {
        count.text = (int.Parse(count.text) + 1).ToString();
    }

    public void DownCount()
    {
        if (count.text != "1")
            count.text = (int.Parse(count.text) - 1).ToString();
    }
    #endregion

    private void CreateList()
    {
        if (rememberWordList != null)
            rememberWordList.Clear();
        Debug.Log(GameObject.Find("Scroll View").name);
        //단어추가
        rememberWordList = GameObject.Find("Scroll View").GetComponent<ScrollView>().ScrollCheck();
        
        //Word word = new Word("aaa", "AAA");
        //Word word2 = new Word("bbb", "BBB");
        //rememberWordList.Add(word);
        //rememberWordList.Add(word2);
    }
}
