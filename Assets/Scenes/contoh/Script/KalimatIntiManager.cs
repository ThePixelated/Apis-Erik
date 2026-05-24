using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class KalimatIntiManager : MonoBehaviour
{
    [SerializeField] private DB_KalimatInti m_DBK;

    public void SetTargetKata(string keyDB)
    {
        m_DBK.SetTargetKata(keyDB, EventSystem.current.currentSelectedGameObject);
    }
    public int index;
public List<DataKataStatis> m_DB_KataStatis;

private void Start()
{
    innit ();

}
private void innit()
{
    m_DB_KataStatis = m_DBK.m_DB_KataStatis;
    index =  m_DB_KataStatis.IndexOf(m_DBK.m_DB_KataStatis.Find(item => item.KalimatSibi == m_DBK.currentData.KalimatSibi));
}

public void NextPage()
{
    index++;
    var targetIndex = index % m_DB_KataStatis.Count;
    m_DBK.currentData = m_DB_KataStatis[targetIndex];
    SceneManager.LoadScene("ShowcaseSIBI");
}

public void BeforePage()
{
    index--;
    var targetIndex = index;
    if (index <= -1)
        targetIndex = m_DB_KataStatis.Count-1;
    m_DBK.currentData = m_DB_KataStatis[targetIndex];
    SceneManager.LoadScene("ShowcaseSIBI");
}
}


