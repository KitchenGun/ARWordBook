using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using UnityEditor;
using UnityEngine;

public class PapagoLanguage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string url = "https://openapi.naver.com/v1/papago/detectLangs";
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Headers.Add("X-Naver-Client-Id", "O5DRNru7NDVFATTOVipo");
        request.Headers.Add("X-Naver-Client-Secret", "AFkEKp5qpD");
        request.Method = "POST";

        foreach (string comparstring in Manager.instance.OcrList)
        { 

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
                Manager.instance.OcrList.Remove(comparstring);
        }
    }
    
}
