using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class ChageWord : MonoBehaviour
{
    public Text word;
    private string[] List = { "aaa", "bbb", "ccc" };
    private int i = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        word.text = List[i];
        if (i == List.Length - 1)
            i = 0;
        else
            i++;

        Thread.Sleep(1000);
    }
}
