using UnityEngine;
using UnityEngine.EventSystems;

public class KalimatIntiManager : MonoBehaviour
{
    [SerializeField] private DB_KalimatInti m_DBK;

    public void SetTargetKata(string keyDB)
    {
        m_DBK.SetTargetKata(keyDB, EventSystem.current.currentSelectedGameObject);
    }
}

