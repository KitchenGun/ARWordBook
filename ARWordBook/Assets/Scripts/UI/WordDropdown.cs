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
    private Manager manager = GameObject.Find("Manager").GetComponent<Manager>();

    private void Start()
    {//정의
        OcrTransListDropdown = GameObject.Find("OcrTransListDropdown").GetComponent<Dropdown>();
    }
    #region 드롭다운에 추가
    public void AddDropdown()
    {
        //드롭다운 초기화
        ClearDropdown();
        //정의
        Dropdown.OptionData data = new Dropdown.OptionData();
        //데이터 삽입
        foreach (string ocrTransWord in manager.OcrList)
        {
            data.text = ocrTransWord;
            if (data.text == null)
            {
                text.text = "값이안들어옴";
            }
            text.text = data.text;
            OcrTransListDropdown.options.Add(data);
        }

        manager.OcrList.Clear();
    }
    #endregion
    #region 드롭다운 초기화
    private void ClearDropdown()
    {
        OcrTransListDropdown.ClearOptions();
    }
    #endregion
}
