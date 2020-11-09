using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChageWord : MonoBehaviour
{
    public Text word;               
    public Text wordes;
    private string[] List = { "aaa", "bbb", "ccc" };
    private string[] ListDes = { "AAA", "BBB", "CCC" };
    private int i = 0;
    private bool isbutton = false;       //버튼이 눌렸는지 확인
    public float time;

    public void StartButton()
    {
        if (isbutton == false)
        {
            isbutton = true;
            StartCoroutine("Chage");
        }
    }

    private IEnumerator Chage()
    {
        while (true)
        {
            if (isbutton == true)
            {
                word.text = List[i];
                wordes.text = ListDes[i];
                if (i == List.Length - 1)
                    i = 0;
                else
                    i++;
                yield return new WaitForSeconds(time);
            }
            else
                break;
        }
    }

    public void StopButton()
    {
        isbutton = false;
    }
}
