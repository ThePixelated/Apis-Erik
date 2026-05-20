using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;

public class QuizManager : MonoBehaviour
{
    [Header("VIDEO")]
    public VideoPlayer questionVideoPlayer;

    [Header("QUESTION UI")]
    public TMP_Text questionText;

    [Header("ANSWER BUTTONS")]
    public Button[] answerButtons;
    public TMP_Text[] answerTexts;

    [Header("PROGRESS UI")]
    public Slider progressBar;
    public TMP_Text soalCounterText;

    [Header("RESULT PANEL")]
    public GameObject correctPanel;
    public GameObject wrongPanel;

    [Header("CORRECT PANEL UI")]
    public TMP_Text correctAnswerText;
    public UnityEngine.UI.Image correctVisualImage;

    [Header("FINISH PANEL")]
    public GameObject finishPanel;

    [Header("FINISH RESULT UI")]
    public TMP_Text finalScoreText;
    public TMP_Text benarText;
    public TMP_Text salahText;
    public TMP_Text studentNameText;

    [Header("QUESTION DATABASE")]
    public SO_QuestionData dbsoalkuis;
    private List<QuestionData> questions;

    private int currentQuestionIndex = 0;

    // SCORE SYSTEM
    private int score = 0;
    private int correctCount = 0;
    private int wrongCount = 0;

    void Start()
    {
        questions = dbsoalkuis.questions;
        // Sembunyikan panel
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

        // Cek soal
        if (questions.Count == 0)
        {
            Debug.LogError("Questions masih kosong!");
            return;
        }

        // Acak soal
        ShuffleQuestions();

        // Tampilkan soal pertama
        ShowQuestion();
    }

    void ShowQuestion()
    {
        // Aktifkan kembali button
        foreach (Button btn in answerButtons)
        {
            btn.interactable = true;
        }

        // Ambil soal sekarang
        QuestionData q = questions[currentQuestionIndex];

        // Acak jawaban
        ShuffleOptions(q.options);

        // ===== VIDEO =====
        if (questionVideoPlayer != null)
        {
            questionVideoPlayer.clip = q.questionVideo;
            questionVideoPlayer.isLooping = true;
            questionVideoPlayer.Play();
        }

        // ===== QUESTION TEXT =====
        if (questionText != null)
        {
            questionText.text = "Apakah ini?";
        }

        // ===== ANSWER OPTIONS =====
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < q.options.Length)
            {
                answerButtons[i].gameObject.SetActive(true);

                // Set text jawaban
                if (answerTexts[i] != null)
                {
                    answerTexts[i].text = q.options[i];
                }

                // Hapus listener lama
                answerButtons[i].onClick.RemoveAllListeners();

                int index = i;

                // Tambah listener baru
                answerButtons[i].onClick.AddListener(() =>
                {
                    CheckAnswer(q.options[index]);
                });
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false);
            }
        }

        // ===== PROGRESS BAR =====
        if (progressBar != null)
        {
            progressBar.value =
                (float)(currentQuestionIndex + 1) / questions.Count;
        }

        // ===== COUNTER TEXT =====
        if (soalCounterText != null)
        {
            soalCounterText.text =
                "Soal " + (currentQuestionIndex + 1) +
                " dari " + questions.Count;
        }
    }

    void CheckAnswer(string selectedAnswer)
    {
        string correctAnswer =
            questions[currentQuestionIndex].correctAnswer;

        // Disable semua button
        foreach (Button btn in answerButtons)
        {
            btn.interactable = false;
        }

        // ===== BENAR =====
        if (selectedAnswer == correctAnswer)
        {
            Debug.Log("BENAR");

            // Tambah score
            score += 10;

            // Tambah jumlah benar
            correctCount++;

            // Tampilkan panel benar
            if (correctPanel != null)
            {
                correctPanel.SetActive(true);
            }

            // Set text jawaban
            if (correctAnswerText != null)
            {
                correctAnswerText.text =
                    "Bagus! Ini adalah \"" +
                    correctAnswer + "\"";
            }

            // Set gambar
            if (correctVisualImage != null)
            {
                correctVisualImage.sprite =
                    questions[currentQuestionIndex].correctImage;
            }
        }

        // ===== SALAH =====
        else
        {
            Debug.Log("SALAH");

            // Tambah jumlah salah
            wrongCount++;

            // Tampilkan panel salah
            if (wrongPanel != null)
            {
                wrongPanel.SetActive(true);
            }
        }
    }

    // BUTTON LANJUT
    public void ContinueQuiz()
    {
        // Tutup panel
        if (correctPanel != null)
        {
            correctPanel.SetActive(false);
        }

        if (wrongPanel != null)
        {
            wrongPanel.SetActive(false);
        }

        // Next soal
        NextQuestion();
    }

    void NextQuestion()
    {
        currentQuestionIndex++;

        // Kalau masih ada soal
        if (currentQuestionIndex < questions.Count)
        {
            ShowQuestion();
        }
        else
        {
            FinishQuiz();
        }
    }

    void FinishQuiz()
    {
        Debug.Log("QUIZ SELESAI");

        Debug.Log("Score: " + score);

        // Stop video
        if (questionVideoPlayer != null)
        {
            questionVideoPlayer.Stop();
        }

        // Tampilkan finish panel
        if (finishPanel != null)
        {
            finishPanel.SetActive(true);
        }

        // Set nilai akhir
        if (finalScoreText != null)
        {
            finalScoreText.text = score.ToString();
        }

        // Set jumlah benar
        if (benarText != null)
        {
            benarText.text = correctCount.ToString();
        }

        // Set jumlah salah
        if (salahText != null)
        {
            salahText.text = wrongCount.ToString();
        }

        // Tampilkan nama siswa
        // if (studentNameText != null)
        // {
        //     studentNameText.text =
        //         StudentManager.currentStudentName;
        // }
    }

    // Acak urutan soal
    void ShuffleQuestions()
    {
        for (int i = 0; i < questions.Count; i++)
        {
            QuestionData temp = questions[i];

            int randomIndex =
                Random.Range(i, questions.Count);

            questions[i] = questions[randomIndex];
            questions[randomIndex] = temp;
        }
    }

    // Acak posisi jawaban
    void ShuffleOptions(string[] options)
    {
        for (int i = 0; i < options.Length; i++)
        {
            string temp = options[i];

            int randomIndex =
                Random.Range(i, options.Length);

            options[i] = options[randomIndex];
            options[randomIndex] = temp;
        }
    }
}