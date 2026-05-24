using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardInfo : MonoBehaviour
{
    public CardManager m_cardManager;

    //[SerializeField] private int dataIndex;
    [SerializeField] private int primaryKey;
    [SerializeField] private string judulCard;
    [TextArea(2, 5)]
    [SerializeField] private string deskripsiCard;
    [SerializeField] private TextMeshProUGUI judulCardTxt;
    [SerializeField] private TextMeshProUGUI deskripsiCardTxt;
    [SerializeField] private Button hapusCardBtn;
    [SerializeField] private Button editCardBtn;

    // ini buat apa dah
    //public string JudulObjekKalimat_CI { get { return judulCard; } set { judulCard = value; } }
    public int PrimaryKey { get { return primaryKey; } private set { primaryKey = value; } }
    //public int DataIndex { get { return dataIndex; } set { dataIndex = value; } }
    public DB_Kalimat DataKalimat { get; private set; } 

    public void OnDestroy()
    {
        // m_cardManager. ... kasih notice ke CardManager
        // apus dari list

        Debug.Log("Card : " + PrimaryKey.ToString("X") + " destroyed");
    }

    public void InnitButtons()
    {
        
        // masih double juga deletnya
        //editCardBtn.onClick.AddListener(m_cardManager.);
        Debug.Log("Innit butons succes..");
    }

    public void InnitData(DB_Kalimat targetDB)
    {
        DataKalimat = targetDB;

        PrimaryKey = DataKalimat.PrimaryKey;

        judulCard = DataKalimat.JudulObjek;
        deskripsiCard = DataKalimat.DeskripsiObjek;

        judulCardTxt.text = judulCard;
        deskripsiCardTxt.text = deskripsiCard;
    }

    public void CardBtnPressed()
    {
        m_cardManager.CardPressed(primaryKey);
        m_cardManager.SetUIPage("objek page", true);
        Debug.Log(gameObject.name);
    }

    public void DeleteCardBtn()
    {
        m_cardManager.ShowDeletePopup(this);

    }

    public void EditCardBtn()
    {
        //m_cardManager.DeleteProtocol();
        Debug.Log("Edit card pressed");
        m_cardManager.EditPressed(PrimaryKey);
        m_cardManager.EditProtocol();
    }
}
