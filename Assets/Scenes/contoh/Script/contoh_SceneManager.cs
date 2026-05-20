using UnityEngine;
using UnityEngine.SceneManagement;

public class contoh_SceneManager : MonoBehaviour
{
    public void LoadScene(string targetScene)
    {
        SceneManager.LoadScene(targetScene);
    }
}
