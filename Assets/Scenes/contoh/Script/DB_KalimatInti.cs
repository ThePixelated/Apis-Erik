using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "DB_KalimatInti", menuName = "Scriptable Objects/DB_KalimatInti")]
public class DB_KalimatInti : ScriptableObject
{
    [Header("CURRENT DATA")]
    public DataKataStatis currentData;

    [SerializeField] private string keyCurrentData;

    [Header("LIST KATA STATIS")]
    public List<DataKataStatis> m_DB_KataStatis;

    private Dictionary<string, DataKataStatis> lookUpDBKataStatis =
        new Dictionary<string, DataKataStatis>();

    // =========================
    // BUILD DATABASE
    // =========================
    private void OnEnable()
    {
        BuildDatabase();
    }

    private void OnValidate()
    {
        BuildDatabase();
    }

    private void BuildDatabase()
    {
        lookUpDBKataStatis.Clear();

        if (m_DB_KataStatis == null || m_DB_KataStatis.Count == 0)
            return;

        foreach (var data in m_DB_KataStatis)
        {
            if (string.IsNullOrEmpty(data.KalimatSibi))
            {
                Debug.LogWarning("Ada KalimatSibi kosong!");
                continue;
            }

            string key = data.KalimatSibi.ToLower();

            if (lookUpDBKataStatis.ContainsKey(key))
            {
                Debug.LogWarning($"Duplicate data: {data.KalimatSibi}");
                continue;
            }

            lookUpDBKataStatis.Add(key, data);
        }

        // LOAD LAST SELECTED DATA
        if (!string.IsNullOrEmpty(keyCurrentData) &&
            lookUpDBKataStatis.ContainsKey(keyCurrentData))
        {
            currentData = lookUpDBKataStatis[keyCurrentData];
        }
        else
        {
            currentData = lookUpDBKataStatis.Values.First();
            keyCurrentData = lookUpDBKataStatis.Keys.First();
        }
    }

    // =========================
    // SET TARGET KATA
    // =========================
    public void SetTargetKata(string keyDB, GameObject args = null)
    {
        string key = keyDB.ToLower();

        Debug.Log($"KeyDB: {key}");

        if (lookUpDBKataStatis.ContainsKey(key))
        {
            currentData = lookUpDBKataStatis[key];

            // SIMPAN KEY TERAKHIR
            keyCurrentData = key;

            Debug.Log($"Data aktif sekarang: {currentData.KalimatSibi}");
        }
        else
        {
            string buttonName =
                args != null ? args.name : "Unknown Button";

            Debug.LogWarning(
                $"Pada button {buttonName}; Key {keyDB} tidak ditemukan!");
        }
    }
}

[System.Serializable]
public class DataKataStatis
{
    public string KalimatSibi;
    public Sprite GambarSibi;
    public VideoClip VideoClip;
}