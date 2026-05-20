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

        RefreshStudentList();
    }
}