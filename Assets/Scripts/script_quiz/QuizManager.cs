using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class QuizManager : MonoBehaviour
{
    [Header("DATABASE")]
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

    [Header("RESULT UI")]
    public TMP_Text scoreText;

    public TMP_Text correctText;

    public TMP_Text wrongText;

    public TMP_Text studentNameText;

    private List<QuestionData> questions =
        new List<QuestionData>();

    private int currentQuestionIndex = 0;

    private int score = 0;

    private int correctAnswerCount = 0;

    private int wrongAnswerCount = 0;

    // =========================
    // START
    // =========================
    void Start()
    {
        if (dbsoalkuis != null)
        {
            questions =
                dbsoalkuis.questions;
        }

        if (correctPanel != null)
            correctPanel.SetActive(false);

        if (wrongPanel != null)
            wrongPanel.SetActive(false);

        if (finishPanel != null)
            finishPanel.SetActive(false);

        if (questions.Count <= 0)
        {
            Debug.LogError(
                "DATABASE SOAL KOSONG"
            );

            return;
        }

        ShuffleQuestions();
    }

    // =========================
    // START QUIZ
    // =========================
    public void StartQuiz()
    {
        // VALIDASI NAMA
        if (StudentManager.currentStudent == null)
        {
            Debug.LogError(
                "NAMA SISWA BELUM DIPILIH"
            );

            return;
        }

        Debug.Log(
            "START QUIZ: " +
            StudentManager
            .currentStudent
            .studentName
        );

        currentQuestionIndex = 0;

        score = 0;

        correctAnswerCount = 0;

        wrongAnswerCount = 0;

        ShowQuestion();
    }

    // =========================
    // SHUFFLE SOAL
    // =========================
    void ShuffleQuestions()
    {
        for (
            int i = 0;
            i < questions.Count;
            i++
        )
        {
            QuestionData temp =
                questions[i];

            int randomIndex =
                Random.Range(
                    i,
                    questions.Count
                );

            questions[i] =
                questions[randomIndex];

            questions[randomIndex] =
                temp;
        }
    }

    // =========================
    // TAMPILKAN SOAL
    // =========================
    void ShowQuestion()
    {
        QuestionData currentQuestion =
            questions[currentQuestionIndex];

        // SOAL
        questionText.text =
            currentQuestion.questionName;

        // VIDEO
        if (
            videoPlayer != null &&
            currentQuestion.questionVideo != null
        )
        {
            videoPlayer.gameObject
                .SetActive(true);

            videoPlayer.clip =
                currentQuestion.questionVideo;

            videoPlayer.Play();
        }
        else
        {
            if (videoPlayer != null)
            {
                videoPlayer.Stop();

                videoPlayer.gameObject
                    .SetActive(false);
            }
        }

        // RANDOM JAWABAN
        List<string> shuffledAnswers =
            new List<string>();

        shuffledAnswers.Add(
            currentQuestion.correctAnswer
        );

        foreach (
            string answer
            in currentQuestion.options
        )
        {
            shuffledAnswers.Add(answer);
        }

        // SHUFFLE
        for (
            int i = 0;
            i < shuffledAnswers.Count;
            i++
        )
        {
            string temp =
                shuffledAnswers[i];

            int randomIndex =
                Random.Range(
                    i,
                    shuffledAnswers.Count
                );

            shuffledAnswers[i] =
                shuffledAnswers[randomIndex];

            shuffledAnswers[randomIndex] =
                temp;
        }

        // SET BUTTON
        for (
            int i = 0;
            i < answerButtons.Length;
            i++
        )
        {
            string answer =
                shuffledAnswers[i];

            answerTexts[i].text =
                answer;

            answerButtons[i]
                .onClick
                .RemoveAllListeners();

            answerButtons[i]
                .onClick
                .AddListener(() =>
                {
                    CheckAnswer(answer);
                });

            answerButtons[i]
                .interactable = true;
        }
    }

    // =========================
    // CHECK JAWABAN
    // =========================
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

        // DISABLE BUTTON
        foreach (
            Button btn
            in answerButtons
        )
        {
            btn.interactable = false;
        }
    }

    // =========================
    // LANJUT
    // =========================
    public void ContinueQuiz()
    {
        if (correctPanel != null)
            correctPanel.SetActive(false);

        if (wrongPanel != null)
            wrongPanel.SetActive(false);

        NextQuestion();
    }

    // =========================
    // NEXT QUESTION
    // =========================
    void NextQuestion()
    {
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

    // =========================
    // FINISH QUIZ
    // =========================
    void FinishQuiz()
    {
        Debug.Log("QUIZ SELESAI");

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
            StudentManager.currentStudent
            != null
        )
        {
            studentNameText.text =
                StudentManager
                .currentStudent
                .studentName;
        }

        SaveStudentScore();
    }

    // =========================
    // SAVE SCORE
    // =========================
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

        Debug.Log(
            "SAVE SCORE: " +
            StudentManager
            .currentStudent
            .studentName
        );

        StudentManager
            .currentStudent
            .scores
            .Add(score);

        StudentManager manager =
            FindFirstObjectByType
            <StudentManager>();

        if (manager != null)
        {
            manager.SaveStudentDatabase();
        }
    }
}