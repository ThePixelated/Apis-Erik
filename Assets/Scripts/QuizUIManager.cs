using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;

public class QuizUIManager : MonoBehaviour
{
    [Header("Video")]
    public VideoPlayer videoPlayer;

    [Header("Question UI")]
    public Button[] answerButtons;
    public TMP_Text[] answerTexts;

    [Header("Progress")]
    public Slider progressBar;
    public TMP_Text soalCounterText;

    [Header("Question Data")]
    public QuestionData[] questions;
}