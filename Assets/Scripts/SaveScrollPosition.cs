using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SaveScrollPosition : MonoBehaviour
{
    public ScrollRect scrollRect;

    private const string SCROLL_KEY = "KosakataScrollPos";

    IEnumerator Start()
    {
        yield return null;

        if (PlayerPrefs.HasKey(SCROLL_KEY))
        {
            float pos = PlayerPrefs.GetFloat(SCROLL_KEY);
            scrollRect.verticalNormalizedPosition = pos;
        }
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(SCROLL_KEY, scrollRect.verticalNormalizedPosition);
        PlayerPrefs.Save();
    }
}