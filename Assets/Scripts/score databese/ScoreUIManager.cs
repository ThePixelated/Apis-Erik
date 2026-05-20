using UnityEngine;

public class ScoreUIManager : MonoBehaviour
{
    public Transform contentParent;

    public GameObject scoreItemPrefab;

    void Start()
    {
        LoadScores();
    }

    void LoadScores()
    {
        if (!PlayerPrefs.HasKey("ScoreDB"))
            return;

        string json =
            PlayerPrefs.GetString("ScoreDB");

        ScoreDatabase db =
            JsonUtility.FromJson<ScoreDatabase>(json);

        foreach (StudentScoreData data in db.scores)
        {
            GameObject item =
                Instantiate(scoreItemPrefab, contentParent);

            ScoreItemUI ui =
                item.GetComponent<ScoreItemUI>();

            ui.Setup(data.studentName, data.score);
        }
    }
}