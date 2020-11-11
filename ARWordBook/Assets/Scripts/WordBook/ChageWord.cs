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
    
    private List<string> PrintList = new List<string>();
    private List<string> PrintListDes = new List<string>();
    private int ran;
    private bool isbutton = false;       //버튼이 눌렸는지 확인


    #region start버튼
    public void StartButton()
    {
        if (isbutton == false)
        {
            CreateList();
            ran = Random.Range(0, PrintList.Count);
            isbutton = true;
            StartCoroutine("Chage");
        }
    }

    private IEnumerator Chage()
    {
        int roopcount = int.Parse(count.text) * PrintList.Count;
        for (int i = 0; i < roopcount; i++)
        {
            if (isbutton == true)
            {
                word.text = PrintList[ran];
                wordes.text = PrintListDes[ran];

                int temp = ran;
                do
                    ran = Random.Range(0, PrintList.Count);
                while (temp == ran);   
                
                yield return new WaitForSeconds(time);
            }
            else
                break;            
        }
        isbutton = false;
    }
    #endregion

    #region stop버튼
    public void StopButton()
    {
        isbutton = false;
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
        PrintList.Clear();
        PrintListDes.Clear();
        

        //단어추가
        PrintList.Add("aaa");
        PrintList.Add("bbb");
        PrintList.Add("ccc");
        PrintListDes.Add("AAA");
        PrintListDes.Add("BBB");
        PrintListDes.Add("CCC");
    }
}
