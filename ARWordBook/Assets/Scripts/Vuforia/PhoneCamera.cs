using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using System.Text;

public class PhoneCamera : MonoBehaviour
{
    private bool camAvailable;
    private WebCamTexture webCam;
    private Texture texture; // in case camera does not open

    public RawImage rawImage;
    public AspectRatioFitter fitter;
    public Text textOCR;

    string[] files = null;

    public void btnClick()
    {
        byte[] jpg = getBytesFromImage();

        string encode = Convert.ToBase64String(jpg);

        StartCoroutine(requestVisionAPI(encode));
    }

    private byte[] getBytesFromImage()
    {
        files = Directory.GetFiles(Application.persistentDataPath + "/", "*.png");
        string pathToFile = files[0];
       
        Texture2D snap = GetScreenshotImage(pathToFile);
        snap.Apply();

        camAvailable = false;

        byte[] bytes = snap.EncodeToPNG();
        return bytes;
    }

   
    Texture2D GetScreenshotImage(string filePath)
    {
        Texture2D texture = null;
        byte[] fileBytes;
        if (File.Exists(filePath))
        {
            fileBytes = File.ReadAllBytes(filePath);
            texture = new Texture2D(2, 2, TextureFormat.RGB24, false);
            texture.LoadImage(fileBytes);
        }
        return texture;
    }

    private IEnumerator requestVisionAPI(string base64Image)
    {
        string apiKey = "AIzaSyAhEhu481O3F080Q6eLV2mPpKVhYB9gf_o";
        string url = "https://vision.googleapis.com/v1/images:annotate?key=" + apiKey;

        // requestBody
        var requests = new requestBody();
        requests.requests = new List<AnnotateImageRequest>();

        var request = new AnnotateImageRequest();
        request.image = new Image();
        request.image.content = base64Image;

        request.features = new List<Feature>();
        var feature = new Feature();
        feature.type = FeatureType.TEXT_DETECTION.ToString();
        feature.maxResults = 10;
        request.features.Add(feature);

        requests.requests.Add(request);

        // JSON
        string jsonRequestBody = JsonUtility.ToJson(requests);

        // "application/json"
        var webRequest = new UnityWebRequest(url, "POST");
        byte[] postData = Encoding.UTF8.GetBytes(jsonRequestBody);
        webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(postData);
        webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();

        // when google cloud vision responds
        if (webRequest.isNetworkError)
        {
            Debug.Log("Error: " + webRequest.error);
        }
        else
        {
            var responses = JsonUtility.FromJson<responseOCR>(webRequest.downloadHandler.text);

            string data = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);

            Debug.Log("Response: " + data);

            String textInImage = string.Empty;
            if (responses.responses.Count() == 1)
            {
                if (responses.responses.First().fullTextAnnotation.text != null)
                {
                    textInImage = responses.responses.First().fullTextAnnotation.text;

                }
            }
            Manager.instance.SplitManager(textInImage);
            textOCR.text = textInImage;
        }
    }

    [Serializable]
    public class requestBody
    {
        public List<AnnotateImageRequest> requests;
    }

    [Serializable]
    public class AnnotateImageRequest
    {
        public Image image;
        public List<Feature> features;
        //public string imageContext;
    }

    [Serializable]
    public class Image
    {
        public string content;
        //public ImageSource source;
    }

    [Serializable]
    public class Feature
    {
        public string type;
        public int maxResults;
    }

    public enum FeatureType
    {
        TYPE_UNSPECIFIED,
        FACE_DETECTION,
        LANDMARK_DETECTION,
        LOGO_DETECTION,
        LABEL_DETECTION,
        TEXT_DETECTION,
        SAFE_SEARCH_DETECTION,
        IMAGE_PROPERTIES
    }

    [Serializable]
    public class ImageContext
    {
        public LatLongRect latLongRect;
        public string languageHints;
    }

    [Serializable]
    public class LatLongRect
    {
        public LatLng minLatLng;
        public LatLng maxLatLng;
    }

    [Serializable]
    public class LatLng
    {
        public float latitude;
        public float longitude;
    }

    public class responseOCR
    {
        public List<AnnotateImageOCR_Response> responses;
    }

    [Serializable]
    public class AnnotateImageOCR_Response
    {
        public List<EntityTextAnnotation> textAnnotations;
        public EntityTextAnnotation fullTextAnnotation;
    }

    [Serializable]
    public class EntityTextAnnotation
    {
        public string mid;
        public string locale; // used in OCR
        public string description; // used in OCR
        public float score;
        public float confidence;
        public float topicality;
        public BoundingPoly boundingPoly; // used in OCR
        public List<LocationInfo> locations;
        public List<Property> properties;
        public string text; // used in OCR
    }

    [Serializable]
    public class BoundingPoly
    {
        public List<Vertex> vertices;
    }

    [Serializable]
    public class Vertex
    {
        public float x;
        public float y;
    }

    [Serializable]
    public class LocationInfo
    {
        LatLng latLng;
    }

    [Serializable]
    public class Property
    {
        string name;
        string value;
    }
}

