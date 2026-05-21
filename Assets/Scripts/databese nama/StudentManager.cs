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

    public static StudentData currentStudent;

    void Start()
{
    LoadStudentDatabase();

    RefreshStudentList();

    dropdownPanel.SetActive(false);
}

    // OPEN DROPDOWN
    public void OpenDropdown()
    {
        dropdownPanel.SetActive(true);

        RefreshStudentList();
    }

    // CLOSE DROPDOWN
    public void CloseDropdown()
    {
        dropdownPanel.SetActive(false);
    }

    // CREATE DATA
    public void AddStudent()
    {
        string studentName =
            addNameInput.text;

        if (studentName == "")
            return;

        StudentData newStudent =
            new StudentData();

        newStudent.studentName =
            studentName;

        database.students.Add(newStudent);
        SaveStudentDatabase();

        addNameInput.text = "";

        RefreshStudentList();
    }

    // REFRESH UI
    public void RefreshStudentList()
    {
        // clear old card
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // generate new card
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

    // SELECT
 public void SelectStudent(
    StudentData data
)
{
    currentStudent = data;

    Debug.Log(
        "SELECT SISWA: " +
        data.studentName
    );

    selectedNameText.text =
        data.studentName;

    dropdownPanel.SetActive(false);
}

    // DELETE
    public void DeleteStudent(
        StudentData data
    )
    {
        database.students.Remove(data);

        SaveStudentDatabase();

        RefreshStudentList();
    }

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