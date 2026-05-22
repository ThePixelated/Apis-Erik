using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class QuizManager : MonoBehaviour
{
    [Header("DATABASE SOAL")]
    public SO_QuestionData dbsoalkuis;

    [Header("UI SOAL")]
    public TMP_Text questionText;

    public Button[] answerButtons;

    public TMP_Text[] answerTexts;

    public VideoPlayer videoPlayer;

    [Header("PANEL")]
    public GameObject correctPanel;

    public GameObject wrongPanel;

    public GameObject finishPanel;

    [Header("WARNING PANEL")]
    public GameObject warningPilihNamaPanel;

    [Header("SCENE LOADER")]
    public SceneLoader sceneLoader;

    [Header("RESULT UI")]
    public TMP_Text scoreText;

    public TMP_Text correctText;

    public TMP_Text wrongText;

    public TMP_Text studentNameText;

    // =====================================

    private List<QuestionData> questions =
        new List<QuestionData>();

    private int currentQuestionIndex = 0;

    private int score = 0;

    private int correctAnswerCount = 0;

    private int wrongAnswerCount = 0;

    [HideInInspector]
    public bool isQuizStarted = false;

    // =====================================

    void Start()
    {
        // PANEL
        if (warningPilihNamaPanel != null)
        {
            warningPilihNamaPanel.SetActive(false);
        }

        if (correctPanel != null)
        {
            correctPanel.SetActive(false);
        }

        if (wrongPanel != null)
        {
            wrongPanel.SetActive(false);
        }

        if (finishPanel != null)
        {
            finishPanel.SetActive(false);
        }

        // LOAD DATABASE
        if (dbsoalkuis != null)
        {
            questions =
                new List<QuestionData>(
                    dbsoalkuis.questions
                );
        }
    }

    // =====================================

    public void StartQuiz()
    {
        // VALIDASI NAMA
        if (
            StudentManager.currentStudent == null ||
            string.IsNullOrEmpty(
                StudentManager
                .currentStudent
                .studentName
            )
        )
        {
            Debug.Log("BELUM PILIH NAMA");

            if (warningPilihNamaPanel != null)
            {
                warningPilihNamaPanel.SetActive(true);
            }

            return;
        }

        // TUTUP WARNING
        if (warningPilihNamaPanel != null)
        {
            warningPilihNamaPanel.SetActive(false);
        }

        // STATUS QUIZ
        isQuizStarted = true;

        // RESET QUIZ
        currentQuestionIndex = 0;

        score = 0;

        correctAnswerCount = 0;

        wrongAnswerCount = 0;

        // SHUFFLE SOAL
        ShuffleQuestions();

        // PINDAH PANEL
        if (sceneLoader != null)
        {
            sceneLoader.SwitchPanel();
        }

        // TAMPILKAN SOAL
        ShowQuestion();
    }

    // =====================================

    void ShuffleQuestions()
    {
        for (int i = 0; i < questions.Count; i++)
        {
            QuestionData temp =
                questions[i];

            int randomIndex =
                Random.Range(i, questions.Count);

            questions[i] =
                questions[randomIndex];

            questions[randomIndex] =
                temp;
        }
    }

    // =====================================

    void ShowQuestion()
    {
        if (questions.Count <= 0)
            return;

        QuestionData currentQuestion =
            questions[currentQuestionIndex];

        // =====================================
        // VIDEO
        // =====================================

        if (
            videoPlayer != null &&
            currentQuestion.questionVideo != null
        )
        {
            // AKTIFKAN OBJECT VIDEO
            if (
                !videoPlayer.gameObject
                .activeSelf
            )
            {
                videoPlayer.gameObject
                    .SetActive(true);
            }

            // AKTIFKAN COMPONENT VIDEO PLAYER
            videoPlayer.enabled = true;

            // SET VIDEO
            videoPlayer.clip =
                currentQuestion.questionVideo;

            // PLAY VIDEO
            videoPlayer.Play();
        }
        else
        {
            if (videoPlayer != null)
            {
                videoPlayer.Stop();
            }
        }

        // =====================================
        // TEXT SOAL
        // =====================================

        if (questionText != null)
        {
            questionText.text =
                currentQuestion.questionName;
        }

        // =====================================
        // JAWABAN
        // =====================================

        for (
            int i = 0;
            i < answerTexts.Length;
            i++
        )
        {
            answerTexts[i].text =
                currentQuestion.options[i];

            int index = i;

            answerButtons[i]
                .onClick
                .RemoveAllListeners();

            answerButtons[i]
                .onClick
                .AddListener(() =>
                {
                    CheckAnswer(
                        currentQuestion
                        .options[index]
                    );
                });
        }
    }

    // =====================================

    void CheckAnswer(
        string selectedAnswer
    )
    {
        QuestionData currentQuestion =
            questions[currentQuestionIndex];

        // BENAR
        if (
            selectedAnswer ==
            currentQuestion.correctAnswer
        )
        {
            score += 10;

            correctAnswerCount++;

            if (correctPanel != null)
            {
                correctPanel.SetActive(true);
            }
        }
        // SALAH
        else
        {
            wrongAnswerCount++;

            if (wrongPanel != null)
            {
                wrongPanel.SetActive(true);
            }
        }
    }

    // =====================================

    public void ContinueQuiz()
    {
        if (correctPanel != null)
        {
            correctPanel.SetActive(false);
        }

        if (wrongPanel != null)
        {
            wrongPanel.SetActive(false);
        }

        currentQuestionIndex++;

        if (
            currentQuestionIndex <
            questions.Count
        )
        {
            ShowQuestion();
        }
        else
        {
            FinishQuiz();
        }
    }

    // =====================================

    void FinishQuiz()
    {
        isQuizStarted = false;

        if (finishPanel != null)
        {
            finishPanel.SetActive(true);
        }

        // SCORE
        if (scoreText != null)
        {
            scoreText.text =
                score.ToString();
        }

        // BENAR
        if (correctText != null)
        {
            correctText.text =
                correctAnswerCount
                .ToString();
        }

        // SALAH
        if (wrongText != null)
        {
            wrongText.text =
                wrongAnswerCount
                .ToString();
        }

        // NAMA SISWA
        if (
            studentNameText != null &&
            StudentManager.currentStudent != null
        )
        {
            studentNameText.text =
                StudentManager
                .currentStudent
                .studentName;
        }

        // SAVE SCORE
        SaveStudentScore();
    }

    // =====================================

    void SaveStudentScore()
    {
        if (
            StudentManager.currentStudent
            == null
        )
        {
            Debug.LogError(
                "CURRENT STUDENT NULL"
            );

            return;
        }

        StudentManager
            .currentStudent
            .scores
            .Add(score);

        StudentManager manager =
            FindFirstObjectByType<StudentManager>();

        if (manager != null)
        {
            manager.SaveStudentDatabase();
        }
    }

    // =====================================

    public void CloseWarningPanel()
    {
        if (warningPilihNamaPanel != null)
        {
            warningPilihNamaPanel.SetActive(false);
        }
    }
}