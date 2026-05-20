using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    [Header("UI Config")]
    [SerializeField] private GameObject objekPage;
    [SerializeField] private GameObject editPage;
    [SerializeField] private GameObject prefabCard;
    [SerializeField] private Transform parentCard;

    [Header("Data Config")]
    [SerializeField] private List<DB_Kalimat> m_database;
    [SerializeField] private List<GameObject> cards_gObj;

    private int _dbSize;

    public DB_Kalimat TargetData { get; set; }
    public int TargetPrimaryKey { get; private set; }

    public event Action<int> onCardPressed;
    public event Action<int> onEditPressed;
    public event Action<string> onDeletePressed;
    public event Action onEnterSearchPressed;

    public void CardPressed(int primaryKey)
    {
        if (onCardPressed != null)
        {
            onCardPressed(primaryKey);
        }
    }

    public void EditPressed(int primaryKey)
    {
        if (onEditPressed != null)
        {
            onEditPressed(primaryKey);
        }
    }

    public void DeletePressed(string judulObjek)
    {
        if (onDeletePressed != null)
        {
            onDeletePressed(judulObjek);
        }
    }

    public void EnterSearchBarPressed()
    {
        if (onEnterSearchPressed != null)
        {
            onEnterSearchPressed();
        }
    }

    private void Start()
    {
        m_database = GameManager.instance.m_DBKalimat.DatabaseKalimat;
        _dbSize = m_database.Count;

        RefreshRender();
    }

    private void Update()
    {
        if (m_database.Count != _dbSize)
        {
            Debug.Log("DB Berubah!");

            RefreshRender();

            _dbSize = m_database.Count;
        }
    }

    public void RenderCallAll()
    {
        for (int i = 0; i < m_database.Count; i++)
        {
            TargetData = m_database[i];

            GameObject tempCard = Instantiate(prefabCard, parentCard);

            tempCard.name =
                "Card : " + m_database[i].PrimaryKey.ToString("X");

            CardInfo cardInfo = tempCard.GetComponent<CardInfo>();

            cardInfo.m_cardManager = this;

            cardInfo.InnitData(TargetData);
            cardInfo.InnitButtons();

            cards_gObj.Add(tempCard);
        }

        EnterSearchBarPressed();
    }

    public void RenderCall(int index = -1, int cases = -1)
    {
        if (index == -1)
            index = m_database.Count - 1;

        if (cases == -1)
            cases = IntegrityCheck();

        switch (cases)
        {
            case 1:
                break;

            default:
                Debug.LogWarning(
                    "Semua data dah ke render, ga perlu render lagi!"
                );
                break;
        }
    }

    public void ClearAllRender()
    {
        foreach (var item in cards_gObj)
        {
            GameObject placeholder = item;

            Destroy(placeholder);
        }

        cards_gObj.RemoveAll(n => n is GameObject);

        Debug.Log("Clear all renders..");
    }

    public void RefreshRender()
    {
        ClearAllRender();

        RenderCallAll();
    }

    public int IntegrityCheck()
    {
        if (cards_gObj.Count < m_database.Count)
            return 1;

        return -1;
    }

    public DB_Kalimat FetchData(int primaryKey)
    {
        Debug.LogWarning("Data being fetched");

        foreach (var item in cards_gObj)
        {
            CardInfo placeholder =
                item.GetComponent<CardInfo>();

            if (placeholder.PrimaryKey == primaryKey)
            {
                TargetData = placeholder.DataKalimat;

                Debug.Log(
                    "CM - Primarykey : " +
                    primaryKey.ToString("X")
                );

                break;
            }
        }

        return TargetData;
    }

    public List<GameObject> FetchAllData()
    {
        return cards_gObj;
    }

    public void DeleteProtocol(int primaryKey)
    {
        int index = 0;

        foreach (var item in cards_gObj)
        {
            int placeholder =
                item.GetComponent<CardInfo>().PrimaryKey;

            if (placeholder == primaryKey)
            {
                GameManager.instance
                    .m_DBKalimat
                    .DeleteData(index);

                break;
            }

            index++;
        }
    }

    public void EditProtocol()
    {
        Debug.Log("Editing..");

        SetUIPage("edit page", true);
    }

    public void SetUIPage(string pageName, bool state)
    {
        if (pageName.ToLower() == "objek page")
        {
            objekPage.SetActive(state);
        }
        else if (pageName.ToLower() == "edit page")
        {
            editPage.SetActive(state);
        }
    }

    public IEnumerator LoadImageFromLocal(
        string fileName,
        RawImage targetUI
    )
    {
        string fullPath = Path.Combine(
            Application.persistentDataPath,
            "UserImages",
            fileName
        );

        Debug.Log($"Load Image Path: {fullPath}");

        if (File.Exists(fullPath))
        {
            using (
                UnityWebRequest uwr =
                UnityWebRequestTexture.GetTexture(
                    "file://" + fullPath
                )
            )
            {
                yield return uwr.SendWebRequest();

                if (
                    uwr.result ==
                    UnityWebRequest.Result.Success
                )
                {
                    Texture texture =
                        DownloadHandlerTexture.GetContent(uwr);

                    targetUI.texture = texture;

                    // tampilkan image
                    targetUI.gameObject.SetActive(true);

                    // cari placeholder
                    Transform placeholder =
                        targetUI.transform.parent.Find(
                            "Placeholder"
                        );

                    // matikan placeholder
                    if (placeholder != null)
                    {
                        placeholder.gameObject.SetActive(false);
                    }

                    Debug.Log("Previous image loaded!");
                }
                else
                {
                    Debug.LogError(
                        "Failed load image!"
                    );
                }
            }
        }
        else
        {
            Debug.LogWarning(
                "Image file not found!"
            );
        }
    }
}

