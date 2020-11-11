using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    public List<string> OcrList { get; set; }
    private int SceneNum=0;
    // Start is called before the first frame update

    void Awake()
    {
        if(instance == null)
        {
            instance=this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        SceneNum =SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(DB.Singleton.DBCreate());
    }

    public void LateUpdate()
    {
        #region 뒤로가기 키 입력
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                switch (SceneNum)
                {
                    case 0:
                        Application.Quit();
                        break;
                    case 1:
                        Application.Quit();
                        break;
                    case 2:
                        SceneManager.LoadScene(1);
                        break;
                }
            }
        }
        #endregion
    }

    #region 현재씬 확인
    void OnEnable()
    {
        // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Load");
        SceneNum = SceneManager.GetActiveScene().buildIndex;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    #endregion

    #region 카메라 씬으로 이동
    public void OnCamScene()
    {
        SceneManager.LoadScene(2);
    }
    #endregion

    #region ocr결과를 배열에 저장

    public void SplitManager(string str)
    {
        OcrList = str.Split('\n').ToList();
    }

    #endregion

    #region 단어 선택 씬으로 이동
    public void CallSelectWordScene()
    {
        SceneManager.LoadScene(3);
    }
    #endregion
}
