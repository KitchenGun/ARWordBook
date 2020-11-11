using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordSample : MonoBehaviour
{
    private Button button;


    // Start is called before the first frame update
    void Start()
    {
        button=this.gameObject.GetComponent<Button>();
        button.onClick.AddListener(Find);
    }

    public void Find()
    {
        //string eword =this.gameObject.transform.GetChild(0).GetComponent<Text>().text;
        WordScrollView wsv = GameObject.Find("Scroll View").GetComponent<WordScrollView>();
        wsv.WordRemoveButtonClick();
    }

}
