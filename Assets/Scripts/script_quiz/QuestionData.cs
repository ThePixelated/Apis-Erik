using UnityEngine;
using UnityEngine.Video;

[System.Serializable]
public class QuestionData
{
    [Header("SOAL")]
    public string questionName;

    public VideoClip questionVideo;

    [Header("JAWABAN")]
    public string correctAnswer;

    public string[] options;

    [Header("VISUAL FEEDBACK")]
    public Sprite correctImage;
}