using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public sceneSebelumnya prevScene;

    [Header("PANEL")]
    public GameObject currentPanel;

    public GameObject targetPanel;

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
        if(prevScene.prevScene == "")
            SceneManager.LoadScene("menu");
        else
            SceneManager.LoadScene(prevScene.prevScene);
    }

    // SWITCH PANEL
    public void SwitchPanel()
    {
        currentPanel.SetActive(false);

        targetPanel.SetActive(true);
    }
}