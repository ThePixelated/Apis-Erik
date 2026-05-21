using UnityEngine;

public class ScoreUIManager : MonoBehaviour
{
    public StudentManager studentManager;

    public Transform contentParent;

    public GameObject scoreItemPrefab;

    void OnEnable()
    {
        LoadScores();
    }

 void LoadScores()
{
    Debug.Log("LOAD SCORE JALAN");

    // hapus item lama
    foreach (Transform child in contentParent)
    {
        Destroy(child.gameObject);
    }

    // cek jumlah siswa
    Debug.Log(
        "JUMLAH SISWA: " +
        studentManager.database.students.Count
    );

    // loop semua siswa
    foreach (StudentData data
        in studentManager.database.students)
    {
        Debug.Log(
            "SISWA: " +
            data.studentName
        );

        Debug.Log(
            "JUMLAH SCORE: " +
            data.scores.Count
        );

        // kalau belum pernah main
        if (data.scores.Count <= 0)
            continue;

        // ambil nilai terakhir
        int latestScore =
            data.scores[data.scores.Count - 1];

        Debug.Log(
            "SPAWN SCORE: " +
            latestScore
        );

        GameObject item =
            Instantiate(
                scoreItemPrefab,
                contentParent
            );

        ScoreItemUI ui =
            item.GetComponent<ScoreItemUI>();

        ui.Setup(
            data.studentName,
            latestScore
        );
    }
}
}