using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateEditData : MonoBehaviour
{
    public static CreateEditData Instance;

    [SerializeField] private Color defaultColor;
    [SerializeField] private GameObject guide;
    [SerializeField] private RectTransform content;
    [SerializeField] private Button submitBtn;
    [SerializeField] private RawImage gambar;

    [SerializeField] private TMP_InputField objekKalimat_InputField;

    [SerializeField]
    private List<TMP_InputField> contohKalimat_InputField =
        new List<TMP_InputField>();

    public RectTransform Content
    {
        get { return content; }
        set { content = value; }
    }

    public RawImage Gambar
    {
        get { return gambar; }
    }

    public Button ButtonSubmit
    {
        get { return submitBtn; }
    }

    public TMP_InputField ObjekKalimat_InputField
    {
        get { return objekKalimat_InputField; }
        set { objekKalimat_InputField = value; }
    }

    public List<TMP_InputField> ContohKalimat_InputField
    {
        get { return contohKalimat_InputField; }
        set { contohKalimat_InputField = value; }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        RawImageReset(Gambar);

        ResetRectTransPosition();

        submitBtn.interactable = false;
    }

    public void TambahDataKeDatabase(
        string dbState,
        DB_Kalimat targetDB = null
    )
    {
        string folderPath = Path.Combine(
            Application.persistentDataPath,
            "UserImages"
        );

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        List<TMP_InputField> clean_contohKalimat_InputField =
            new List<TMP_InputField>();

        foreach (var item in contohKalimat_InputField)
        {
            string temp = item.textComponent.text;

            if (!(temp.Length <= 1))
            {
                clean_contohKalimat_InputField.Add(item);
            }
        }

        switch (dbState)
        {
            case "Create":

                GameManager.instance.m_DBKalimat.CreateData(
                    "",
                    objekKalimat_InputField,
                    clean_contohKalimat_InputField
                );

                break;

            case "Update":

                GameManager.instance.m_DBKalimat.UpdateData(
                    "",
                    objekKalimat_InputField,
                    clean_contohKalimat_InputField,
                    targetDB
                );

                break;
        }

        Save(targetDB);

        // reset ui
        CleanUpInputField();

        ResetRectTransPosition();

        submitBtn.interactable = false;

        RawImageReset(Gambar);
    }

    public void InputFieldChecker()
    {
        Debug.Log(
            objekKalimat_InputField.textComponent.text.Length +
            ", " +
            objekKalimat_InputField.textComponent.text
        );

        if (
            objekKalimat_InputField.textComponent.text.Length <= 1 ||
            objekKalimat_InputField.textComponent.text == ""
        )
        {
            submitBtn.interactable = false;

            Debug.Log("Kosong");
        }
        else
        {
            submitBtn.interactable = true;

            Debug.Log("Berisi");
        }
    }

    public void CleanUpInputField()
    {
        Debug.Log("Bersih bersih");

        objekKalimat_InputField.textComponent.text = "";
        objekKalimat_InputField.text = "";

        for (int i = 0; i < contohKalimat_InputField.Count; i++)
        {
            contohKalimat_InputField[i].textComponent.text = "";

            contohKalimat_InputField[i].text = "";
        }
    }

    public void TambahkanBtn()
    {
        RawImageReset(Gambar);

        CleanUpInputField();

        ResetRectTransPosition();

        submitBtn.interactable = false;
    }

    public void BackBtnConfig(RawImage gambars = null)
    {
        if (gambars != null);
        RawImageReset(gambars);

        CleanUpInputField();

        ResetRectTransPosition();

        submitBtn.interactable = false;
    }

    public void ResetRectTransPosition()
    {
        Vector3 returnPos = new Vector3(300, 0, 0);

        content.position = returnPos;
    }

    public void RawImageReset(RawImage gambars)
    {
        if (gambars != null)
        {
            gambars.texture = null;

            gambars.color = new Color(
                defaultColor.r,
                defaultColor.g,
                defaultColor.b,
                1
            );
        }
    }

    public void Save(DB_Kalimat targetDB = null)
    {
        string tempSelectedPath =
            FileBrowserUpdate.Instance.TempPath;

        // kalau user tidak pilih gambar baru
        if (string.IsNullOrEmpty(tempSelectedPath))
        {
            Debug.Log("No new image selected");

            // IMPORTANT:
            // kalau update dan ga pilih gambar baru,
            // pertahankan gambar lama
            if (targetDB != null)
            {
                Debug.Log("Keeping old image path: " + targetDB.ImagePath);
            }

            string savePath = Path.Combine(
                Application.persistentDataPath,
                "database.json"
            );

            string saveJson =
                JsonUtility.ToJson(
                    GameManager.instance.m_DBKalimat
                );

            File.WriteAllText(savePath, saveJson);

            return;
        }

        string folderPath = Path.Combine(
            Application.persistentDataPath,
            "UserImages"
        );

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string extension =
            Path.GetExtension(tempSelectedPath).Trim();

        string newFileName =
            "IMG_" +
            System.DateTime.Now.ToString(
                "yyyyMMdd_HHmmss"
            ) +
            extension;

        string finalPath =
            Path.Combine(folderPath, newFileName);

        File.Copy(tempSelectedPath, finalPath, true);

        // CREATE
        if (targetDB == null)
        {
            var tempDB =
                GameManager.instance
                .m_DBKalimat
                .DatabaseKalimat;

            tempDB[tempDB.Count - 1].ImagePath =
                newFileName;
        }
        // UPDATE
        else
        {
            targetDB.ImagePath = newFileName;
        }

        string path = Path.Combine(
            Application.persistentDataPath,
            "database.json"
        );

        string json =
            JsonUtility.ToJson(
                GameManager.instance.m_DBKalimat
            );

        File.WriteAllText(path, json);

        Debug.Log("Data saved at: " + path);
    }
}