using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public sceneSebelumnya prevScene;

    [Header("PANEL")]
    public GameObject currentPanel;

    public GameObject targetPanel;

    [Header("QUIZ PANEL")]
    public GameObject quizPanel;

    public GameObject inputNamaPanel;

    [Header("STUDENT")]
    public StudentManager studentManager;

    // LOAD SCENE
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // SIMPAN SCENE
    public void SetScene(string nameScene)
    {
        prevScene.prevScene = nameScene;
    }

    // BACK SCENE
    public void GoToTargetScene()
    {
        if (prevScene.prevScene == "")
            SceneManager.LoadScene("menu");
        else
            SceneManager.LoadScene(prevScene.prevScene);
    }

    // SWITCH PANEL BIASA
    public void SwitchPanel()
    {
        currentPanel.SetActive(false);

        targetPanel.SetActive(true);
    }

    // BACK KE INPUT NAMA
public void BackToInputNama()
{
    // reset siswa
    StudentManager.currentStudent = null;

    // reset quiz state
    QuizManager quiz =
        FindFirstObjectByType<QuizManager>();

    if (quiz != null)
    {
        quiz.isQuizStarted = false;
    }

    // reset nama tampilan
    StudentManager manager =
        FindFirstObjectByType<StudentManager>();

    if (manager != null)
    {
        manager.selectedNameText.text = "NAMA";
    }

    // pindah panel
    currentPanel.SetActive(false);

    targetPanel.SetActive(true);
}
}