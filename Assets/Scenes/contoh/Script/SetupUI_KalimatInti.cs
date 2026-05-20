using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SetupUI_KalimatInti : MonoBehaviour
{
    [Header("DATABASE")]
    [SerializeField] private DB_KalimatInti m_DB_KalimatInti;

    [Header("UI")]
    [SerializeField] private Image imageCerita;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private TextMeshProUGUI kataSambung;

    [Header("PANELS")]
    [SerializeField] private GameObject panelCerita;
    [SerializeField] private GameObject panelSIBI;
    [SerializeField] private GameObject panelText;

    private DataKataStatis currentData;

    // STEP SYSTEM
    private int currentStep = 0;

    /*
        STEP 0 = Cerita
        STEP 1 = Cerita + SIBI
        STEP 2 = Cerita + SIBI + Text
    */

    private void Start()
    {
        LoadData();
        UpdateStepUI();
    }

    void LoadData()
    {
        currentData = m_DB_KalimatInti.currentData;

        // GAMBAR CERITA
        if (currentData.GambarSibi != null)
        {
            imageCerita.sprite = currentData.GambarSibi;
        }

        // VIDEO SIBI
        if (currentData.VideoClip != null)
        {
            videoPlayer.clip = currentData.VideoClip;
        }

        // TEXT
        kataSambung.text = m_DB_KalimatInti.currentData.KalimatSibi;
        // RESET VIDEO
        if (videoPlayer.targetTexture != null)
        {
            videoPlayer.targetTexture.Release();
        }

        videoPlayer.Stop();
    }

    void UpdateStepUI()
    {
        // STEP 0
        if (currentStep == 0)
        {
            panelCerita.SetActive(true);
            panelSIBI.SetActive(false);
            panelText.SetActive(false);

            videoPlayer.Stop();
        }

        // STEP 1
        else if (currentStep == 1)
        {
            panelCerita.SetActive(true);
            panelSIBI.SetActive(true);
            panelText.SetActive(false);

            videoPlayer.Play();
        }

        // STEP 2
        else if (currentStep == 2)
        {
            panelCerita.SetActive(true);
            panelSIBI.SetActive(true);
            panelText.SetActive(true);

            if (!videoPlayer.isPlaying)
            {
                videoPlayer.Play();
            }
        }
    }

    public void NextStep()
    {
        currentStep++;

        if (currentStep > 2)
        {
            currentStep = 2;
        }

        UpdateStepUI();
    }

    public void PreviousStep()
    {
        currentStep--;

        if (currentStep < 0)
        {
            currentStep = 0;
        }

        UpdateStepUI();
    }
}