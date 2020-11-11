using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class WordDropdown : MonoBehaviour
{
    [SerializeField]
    private Dropdown OcrTransListDropdown;
    public Text text;
    private Manager manager;

    private List<string> words;

    private void Start()
    {//정의
        OcrTransListDropdown = GameObject.Find("OcrTransListDropdown").GetComponent<Dropdown>();
        manager = GameObject.Find("Manager").GetComponent<Manager>();
        words = manager.OcrList;
        text.text = words[0];
    }
    #region 드롭다운에 추가
    public void AddDropdown()
    {
        foreach (string word in words)
        {
            //OcrTransListDropdown.options.Add()
        } 
    }
    #endregion
    #region 드롭다운 초기화
    private void ClearDropdown()
    {
        OcrTransListDropdown.ClearOptions();
    }
    #endregion
}
