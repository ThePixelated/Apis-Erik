using UnityEngine;

public class TambahKalimat : CreateEditData
{
    [SerializeField] private CardManager m_cardManager;

    //private DB_Kalimat m_dataKalimat;
    //public DB_Kalimat DataKalimat { get { return m_dataKalimat; } private set { m_dataKalimat = value; } }

    private void Start()
    {
    //    m_cardManager.onEditPressed += InnitPage;
       Debug.Log("testing");
    //    gameObject.SetActive(false);
    }

    //private void InnitPage(int primaryKey)
    //{
    //    Debug.LogWarning("IP - Primarykey : " + primaryKey.ToString("X"));

    //    m_dataKalimat = m_cardManager.FetchData(primaryKey);

    //    //gambar.sprite = m_dataKalimat.Image;
    //    ObjekKalimat_InputField.text = m_dataKalimat.JudulObjek;
    //    ObjekKalimat_InputField.textComponent.text = m_dataKalimat.JudulObjek;

    //    //Debug.LogWarning(m_dataKalimat.ContohKalimat.Count);
    //    for (int i = 0; i < m_dataKalimat.ContohKalimat.Count; i++)
    //    {
    //        //Debug.Log(m_dataKalimat.ContohKalimat[i]);
    //        ContohKalimat_InputField[i].text = m_dataKalimat.ContohKalimat[i];
    //        ContohKalimat_InputField[i].textComponent.text = m_dataKalimat.ContohKalimat[i];
    //        //Debug.LogWarning(contohKalimat[i].gameObject.name);
    //        //contohKalimat[i].gameObject.SetActive(true);
    //    }

    //    gameObject.SetActive(true);
    //}

    public void SubmitBtn()
    {
        TambahDataKeDatabase("Create");
    }
}
