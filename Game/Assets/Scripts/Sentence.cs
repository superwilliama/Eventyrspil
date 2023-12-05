using UnityEngine;

[System.Serializable]
public struct Sentence
{
    public string text;
    [Range(0.01f, 0.2f)] public float secondsPerWord;
}
