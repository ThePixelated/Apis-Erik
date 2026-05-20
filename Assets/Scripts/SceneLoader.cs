using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public sceneSebelumnya prevScene;
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void SetScene(string nameScene)
{
     prevScene.prevScene = nameScene;
}

public void GoToTargetScene()
{
    if(prevScene.prevScene == "")
        SceneManager.LoadScene("menu");
    else 
        SceneManager.LoadScene(prevScene.prevScene);
}
}