using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditKalimat : CreateEditData
{
    [SerializeField] private CardManager m_cardManager;
    //[SerializeField] private RawImage gambar;

    private DB_Kalimat m_dataKalimat;
    public DB_Kalimat DataKalimat { get { return m_dataKalimat; } private set { m_dataKalimat = value; } }

    private void Start()
    {
        m_cardManager.onEditPressed += InnitPage;
        gameObject.SetActive(false);
    }

    private void InnitPage(int primaryKey)
    {
        BackBtnConfig(Gambar);

        Debug.LogWarning("IP - Primarykey : " + primaryKey.ToString("X"));

        m_dataKalimat = m_cardManager.FetchData(primaryKey);

        //Gambar = m_dataKalimat.Image;
        //m_cardManager.LoadImageFromLocal(m_dataKalimat.ImagePath, Gambar);
        ObjekKalimat_InputField.text = m_dataKalimat.JudulObjek;
        ObjekKalimat_InputField.textComponent.text = m_dataKalimat.JudulObjek;

        //Debug.LogWarning(m_dataKalimat.ContohKalimat.Count);
        for (int i = 0; i < m_dataKalimat.ContohKalimat.Count; i++)
        {
            //Debug.Log(m_dataKalimat.ContohKalimat[i]);
            ContohKalimat_InputField[i].text = m_dataKalimat.ContohKalimat[i];
            ContohKalimat_InputField[i].textComponent.text = m_dataKalimat.ContohKalimat[i];
            //Debug.LogWarning(contohKalimat[i].gameObject.name);
            //contohKalimat[i].gameObject.SetActive(true);
        }

        gameObject.SetActive(true);

        ButtonSubmit.interactable = true;

        StartCoroutine(
        m_cardManager.LoadImageFromLocal(m_dataKalimat.ImagePath, Gambar)
);
    }

    public void SubmitBtn()
    {
        TambahDataKeDatabase("Update", DataKalimat);
    }
}
