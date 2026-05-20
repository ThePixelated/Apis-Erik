using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SO_CreateKalimat", menuName = "Scriptable Objects/SO_CreateKalimat")]
public class SO_CreateKalimat : ScriptableObject
{
    [SerializeField] private int currentPrimaryKeyVal = 0;
    [SerializeField] private List<DB_Kalimat> databaseKalimat = new List<DB_Kalimat>();

    public int CurrentPrimaryKeyVal
    { 
        get { return currentPrimaryKeyVal; } 
        set { currentPrimaryKeyVal = value; } 
    }

    public List<DB_Kalimat> DatabaseKalimat
    {
        get { return databaseKalimat; }
        set { databaseKalimat = value; }
    }

    public void IncrementPrimaryKey() => currentPrimaryKeyVal++;

    public void DeleteData(int index)
    {
        databaseKalimat.RemoveAt(index);
    }

    public void DeleteData(int firstIndex, int targetIndex)
    {
        //..
    }

    public void CreateData(string imagePath, TMP_InputField judulObjek, List<TMP_InputField> contohKalimat)
    {
        DatabaseKalimat.Add(new DB_Kalimat());
        
        DB_Kalimat placeholderDB = databaseKalimat[DatabaseKalimat.Count - 1];
        placeholderDB.PrimaryKey = CurrentPrimaryKeyVal;
        if (!string.IsNullOrEmpty(imagePath)) { placeholderDB.ImagePath = imagePath; }
        placeholderDB.JudulObjek = judulObjek.textComponent.text;
        placeholderDB.ContohKalimat = new List<string>();

        for (int i = 0; i < contohKalimat.Count; i++)
        {
            placeholderDB.ContohKalimat.Add(null);
            placeholderDB.ContohKalimat[i] = contohKalimat[i].textComponent.text;
        }

        IncrementPrimaryKey();
    }

    public void UpdateData(string imagePath, TMP_InputField newJudulObjek, List<TMP_InputField> newContohKalimat, DB_Kalimat targetDatabase)
    {
        //DatabaseKalimat.Add(new DB_Kalimat());
        Debug.Log(imagePath == null);
        Debug.Log(newJudulObjek == null);
        Debug.Log(newContohKalimat == null);
        Debug.Log(targetDatabase == null);
        Debug.LogWarning("IDK MAN YALLAH MAU ISTIRAHAT");
        
        DB_Kalimat placeholderDB = targetDatabase; // !!! ada kemungkinan loss data !!!!
        //placeholderDB.PrimaryKey = CurrentPrimaryKeyVal;
        placeholderDB.ImagePath = imagePath;
        placeholderDB.JudulObjek = newJudulObjek.textComponent.text;
        placeholderDB.ContohKalimat = new List<string>();

        for (int i = 0; i < newContohKalimat.Count; i++)
        {
            placeholderDB.ContohKalimat.Add(null);
            placeholderDB.ContohKalimat[i] = newContohKalimat[i].textComponent.text;
        }
    }

    public void DestroyDatabase()
    {
        //..
    }
}

[System.Serializable]
public class DB_Kalimat
{
    [TextArea(3, 10)]
    public string JudulObjek;
    public int PrimaryKey;
    public string ImagePath;
    [TextArea(3, 10)]
    public string DeskripsiObjek;
    [TextArea(3, 10)]
    public List<string> ContohKalimat = new List<string>();
}

// buatin folder buat gambar
// buatin dictionary buat ngeretrive gambar
// RawImage DB_Kalimat ga bisa run time