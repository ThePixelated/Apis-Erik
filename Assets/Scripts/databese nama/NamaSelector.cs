using TMPro;
using UnityEngine;

public class NamaSelector : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI namaText;

    public static NamaSelector Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PilihNama(string nama)
    {
        // Update text button
        namaText.text = nama;

        // Simpan nama TERBARU
        PlayerPrefs.SetString("SelectedStudent", nama);

        // Save paksa
        PlayerPrefs.Save();

        Debug.Log("Selected Student: " + nama);
    }
}