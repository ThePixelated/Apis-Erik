using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private SO_CreateKalimat m_databaseKalimat;
    public SO_CreateKalimat m_DBKalimat { get { return m_databaseKalimat; } }

    private void Start()
    {
        LoadDataFromJson();
    }

    public void ExitApplication()
    {
        CreateEditData.Instance.Save();
        Application.Quit();
    }

    public void LoadDataFromJson()
    {
        string path = Path.Combine(Application.persistentDataPath, "database.json");

        if (File.Exists(path))
        {
            // 1. Baca string dari file
            string jsonString = File.ReadAllText(path);

            // 2. Deserialisasi menggunakan Wrapper
            DatabaseWrapper wrapper = JsonUtility.FromJson<DatabaseWrapper>(jsonString);

            // 3. Masukkan data dari wrapper ke ScriptableObject
            m_databaseKalimat.CurrentPrimaryKeyVal = wrapper.currentPrimaryKeyVal;
            m_databaseKalimat.DatabaseKalimat = wrapper.databaseKalimat;

            Debug.Log("Data Berhasil di-load ke ScriptableObject");
        }
        else
        {
            Debug.LogWarning("File JSON tidak ditemukan di " + path);
        }
    }
}

[System.Serializable]
public class DatabaseWrapper
{
    public int currentPrimaryKeyVal;
    public List<DB_Kalimat> databaseKalimat;
}