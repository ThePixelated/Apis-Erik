using TMPro;
using UnityEngine;

public class StudentManager : MonoBehaviour
{
    [Header("DATABASE")]
    public SOStudentDatabase database;

    [Header("UI")]
    public TMP_InputField addNameInput;

    public Transform contentParent;

    public GameObject cardPrefab;

    public TMP_Text selectedNameText;

    [Header("PANEL")]
    public GameObject dropdownPanel;

    [Header("DELETE PANEL")]
    public GameObject deletePanel;

    // SISWA YANG DIPILIH
    public static StudentData currentStudent;

    // SISWA YANG MAU DIHAPUS
    private StudentData selectedDeleteData;

    void Start()
    {
        LoadStudentDatabase();

        RefreshStudentList();

        // tutup dropdown
        if (dropdownPanel != null)
        {
            dropdownPanel.SetActive(false);
        }

        // tutup panel delete
        if (deletePanel != null)
        {
            deletePanel.SetActive(false);
        }
    }

    // =========================
    // OPEN DROPDOWN
    // =========================
    public void OpenDropdown()
    {
        dropdownPanel.SetActive(true);

        RefreshStudentList();
    }

    // =========================
    // CLOSE DROPDOWN
    // =========================
    public void CloseDropdown()
    {
        dropdownPanel.SetActive(false);
    }

    // =========================
    // ADD STUDENT
    // =========================
    public void AddStudent()
    {
        string studentName =
            addNameInput.text;

        // cek kosong
        if (string.IsNullOrEmpty(studentName))
            return;

        // buat data baru
        StudentData newStudent =
            new StudentData();

        newStudent.studentName =
            studentName;

        // tambah database
        database.students.Add(newStudent);

        // save
        SaveStudentDatabase();

        // reset input
        addNameInput.text = "";

        // refresh
        RefreshStudentList();
    }

    // =========================
    // REFRESH LIST
    // =========================
    public void RefreshStudentList()
    {
        // hapus card lama
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // generate card baru
        foreach (StudentData data
            in database.students)
        {
            GameObject newCard =
                Instantiate(
                    cardPrefab,
                    contentParent
                );

            StudentCardUI cardUI =
                newCard.GetComponent<StudentCardUI>();

            cardUI.Setup(data, this);
        }
    }

    // =========================
    // SELECT STUDENT
    // =========================
    public void SelectStudent(
    StudentData data
)
{
    currentStudent = data;

    Debug.Log(
        "Selected Student: " +
        data.studentName
    );

    selectedNameText.text =
        data.studentName;

    // TUTUP DROPDOWN
    if (dropdownPanel != null)
    {
        dropdownPanel.SetActive(false);
    }
}

    // =========================
    // OPEN DELETE PANEL
    // =========================
    public void OpenDeletePanel(
        StudentData data
    )
    {
        selectedDeleteData = data;

        if (deletePanel != null)
        {
            deletePanel.SetActive(true);
        }
    }

    // =========================
    // CONFIRM DELETE
    // =========================
    public void ConfirmDelete()
    {
        if (selectedDeleteData != null)
        {
            database.students.Remove(
                selectedDeleteData
            );

            SaveStudentDatabase();

            RefreshStudentList();

            Debug.Log(
                "DATA DIHAPUS: " +
                selectedDeleteData.studentName
            );

            selectedDeleteData = null;
        }

        // tutup panel
        if (deletePanel != null)
        {
            deletePanel.SetActive(false);
        }
    }

    // =========================
    // CANCEL DELETE
    // =========================
    public void CancelDelete()
    {
        selectedDeleteData = null;

        if (deletePanel != null)
        {
            deletePanel.SetActive(false);
        }
    }

    // =========================
    // SAVE DATABASE
    // =========================
    public void SaveStudentDatabase()
    {
        StudentDatabase db =
            new StudentDatabase();

        db.students =
            database.students;

        string json =
            JsonUtility.ToJson(db);

        PlayerPrefs.SetString(
            "StudentDB",
            json
        );

        PlayerPrefs.Save();
    }

    // =========================
    // LOAD DATABASE
    // =========================
    void LoadStudentDatabase()
    {
        if (PlayerPrefs.HasKey("StudentDB"))
        {
            string json =
                PlayerPrefs.GetString("StudentDB");

            StudentDatabase db =
                JsonUtility.FromJson<StudentDatabase>(
                    json
                );

            database.students =
                db.students;
        }
    }
}