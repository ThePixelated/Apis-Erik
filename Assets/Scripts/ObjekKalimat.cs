using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjekKalimat : MonoBehaviour
{
    [SerializeField] private RectTransform content;
    [SerializeField] private CardManager m_cardManager;
    [SerializeField] private RawImage gambar;
    [SerializeField] private TextMeshProUGUI objekKalimat;
    [SerializeField] private TextMeshProUGUI[] contohKalimat;

    private DB_Kalimat m_dataKalimat;

    private const string defaultObjekText = "Objek Kalimat#";
    private const string defaultContohText = "Lorem Ipsum dolor sit amet.";

    private void Start()
    {
        m_cardManager.onCardPressed += InnitPage;
        gameObject.SetActive(false);
        Debug.Log(content.position);
    }

    private void InnitPage(int primaryKey)
    {
        if (CreateEditData.Instance != null)
        {
            CreateEditData.Instance.BackBtnConfig(gambar);
        }
        Debug.Log("IP - Primarykey : " + primaryKey.ToString("X"));

        m_dataKalimat = m_cardManager.FetchData(primaryKey);
        


        // retrive data dari folder berdasarkan imagepath

        //gambar = m_dataKalimat.Image;
        if (m_dataKalimat == null)
        {
            Debug.LogError("Data kalimat NULL!");
            return;
        }
        objekKalimat.text = m_dataKalimat.JudulObjek;
        Debug.LogWarning(m_dataKalimat.ContohKalimat.Count);
        
        for (int i = 0; i < Mathf.Min(m_dataKalimat.ContohKalimat.Count, contohKalimat.Length); i++)
        {
            Debug.Log(m_dataKalimat.ContohKalimat[i]);

            contohKalimat[i].text = m_dataKalimat.ContohKalimat[i];

            Debug.LogWarning(contohKalimat[i].gameObject.name);

            contohKalimat[i].gameObject.SetActive(true);
        }
        gameObject.SetActive(true);

        StartCoroutine(m_cardManager.LoadImageFromLocal(m_dataKalimat.ImagePath, gambar));
    }

    public void BackBtn()
    {
        gambar.texture = null;
        objekKalimat.text = defaultObjekText;
        foreach (var item in contohKalimat)
        {
            item.text = defaultContohText;
            item.gameObject.SetActive(false);
        }

        content.position = new Vector3(0,0, 0);
    }

    
}
