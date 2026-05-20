using TMPro;
using UnityEngine;

public class ScoreItemUI : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text scoreText;

    public void Setup(string studentName, int score)
    {
        nameText.text = studentName;
        scoreText.text = score + "/10";
    }
}