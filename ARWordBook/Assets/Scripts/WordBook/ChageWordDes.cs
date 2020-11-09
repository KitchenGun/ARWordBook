
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class ChageWordDes : MonoBehaviour
{
    public Text wordes;
    private string[] List = { "AAA", "BBB", "CCC" };
    private int i = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        wordes.text = List[i];
        if (i == List.Length - 1)
            i = 0;
        else
            i++;

        Thread.Sleep(1000);
    }
}
