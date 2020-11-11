using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PapagoLanguage : MonoBehaviour
{
    private Manager manger;
    [SerializeField]
    private GameObject dropdown;
    // Start is called before the first frame update
    void Start()
    {
       manger = GameObject.Find("Manager").GetComponent<Manager>();

        foreach (string comparstring in manger.OcrList)
        {
            string url = "https://openapi.naver.com/v1/papago/detectLangs";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-Naver-Client-Id", "O5DRNru7NDVFATTOVipo");
            request.Headers.Add("X-Naver-Client-Secret", "AFkEKp5qpD");
            request.Method = "POST";

            byte[] byteDataParams = Encoding.UTF8.GetBytes("query=" + comparstring);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteDataParams.Length;
            Stream st = request.GetRequestStream();
            st.Write(byteDataParams, 0, byteDataParams.Length);
            st.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string text = reader.ReadToEnd();
            stream.Close();
            response.Close();
            reader.Close();

            JObject jobj = JObject.Parse(text);
            string translanguage = jobj["langCode"].ToString();

            if (translanguage != "en")
                manger.OcrList.Remove(comparstring);
        }
        dropdown.GetComponent<WordDropdown>().AddDropdown();
    }
    
}
