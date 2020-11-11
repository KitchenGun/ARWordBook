using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Permissions;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PapagoTrans : MonoBehaviour
{
    public Dropdown dropdown;
    string EnWord;

    public Text text;
    private Manager manger = GameObject.Find("Manager").GetComponent<Manager>();

    public void Click()
    {
        EnWord = dropdown.captionText.text;
        WordInsert(EnWord);
    }

    public void Start()
    {
        foreach(string eword in manger.OcrList)
        {
            text.text += eword;
        }
    }

    private void WordInsert(string language)
    {
        string jsonstr = TransLanguage(language);


        //방법 1
        JObject jobj = JObject.Parse(jsonstr);
        string translanguage = jobj["message"]["result"]["translatedText"].ToString();
        DB.Singleton.DataBaseInsert("insert into Word(word,mean) values(\"" + language + "\",\"" + translanguage + "\")");
        DB.Singleton.DataBaseRead("Select * From Word");
        Debug.Log(translanguage);
    }

    public static string TransLanguage(string msg)
    {
        //요청 기본 URL
        string url = "https://openapi.naver.com/v1/papago/n2mt";


        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Headers.Add("X-Naver-Client-Id", "O5DRNru7NDVFATTOVipo");
        request.Headers.Add("X-Naver-Client-Secret", "AFkEKp5qpD");
        request.Method = "POST";


        string query = msg;


        byte[] byteDataParams = Encoding.UTF8.GetBytes("source=en&target=ko&text=" + query);
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = byteDataParams.Length;
        Stream st = request.GetRequestStream();
        st.Write(byteDataParams, 0, byteDataParams.Length);
        st.Close();

        //응답(Response)
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream stream = response.GetResponseStream();
        StreamReader reader = new StreamReader(stream, Encoding.UTF8);
        string text = reader.ReadToEnd();   //응답에 대한 결과물
        stream.Close();
        response.Close();
        reader.Close();

        return text;
    }

    

}
