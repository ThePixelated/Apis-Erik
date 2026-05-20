
using AnotherFileBrowser.Windows;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FileBrowserUpdate : MonoBehaviour
{
    public static FileBrowserUpdate Instance;

    [SerializeField] private CreateEditData m_createEditData;
    [SerializeField] private RawImage rawImage;
    [SerializeField] private Color defaultColor;
    [SerializeField] private GameObject guide;

    private string tempPath;
    public string TempPath { get { return tempPath; } }

    private void Awake()
    {
        Instance = this;
    }

    public void OpenFileBrowser(GameObject targetRawImage)
    {
        rawImage = targetRawImage.GetComponent<RawImage>();

        var bp = new BrowserProperties();
        bp.filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
        bp.filterIndex = 0;

        new FileBrowser().OpenFileBrowser(bp, path =>
        {
            tempPath = path;
            //Debug.Log($"PATH FOLDER: {path}");

            //m_createEditData.tempSelectedPath = path;
            //Debug.Log($"tempSelectedPath: {m_createEditData.tempSelectedPath}");
            StartCoroutine(LoadImage(path));
        });
    }

    IEnumerator LoadImage(string path)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(path))
        {
            yield return uwr.SendWebRequest();

            bool connectionError = uwr.result == UnityWebRequest.Result.ConnectionError;
            bool HttpError = uwr.result == UnityWebRequest.Result.ProtocolError;

            if (connectionError || HttpError)
            {
                Debug.LogWarning(uwr.error);
            }
            else
            {
                var uwrTexture = DownloadHandlerTexture.GetContent(uwr);
                rawImage.texture = uwrTexture;
                rawImage.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, 1);
                
            }
        }
    }
}

/// Referensi
/// https://www.youtube.com/watch?v=Z1qT65GL-6Q