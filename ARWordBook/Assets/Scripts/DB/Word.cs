using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Word : MonoBehaviour
{
    public int Id { get; set; }
    public string eWord { get; set; }
    public string kWord { get; set; }

    public Word(string eword, string kword)
    {
        eWord = eword;
        kWord = kword;
    }
    public Word(int id, string eword, string kword)
    {
        Id = id;
        eWord = eword;
        kWord = kword;
    }
}
