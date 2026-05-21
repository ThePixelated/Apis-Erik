using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StudentCardUI : MonoBehaviour
{
    [Header("UI")]
    public TMP_InputField nameInputField;

    public Button editButton;
    public Button deleteButton;
    public Button selectButton;

    private StudentData currentData;

    private StudentManager manager;

    // =========================
    // SETUP CARD
    // =========================
    public void Setup(
        StudentData data,
        StudentManager studentManager
    )
    {
        currentData = data;

        manager = studentManager;

        // tampilkan nama
        nameInputField.text =
            currentData.studentName;

        // default gabisa edit
        nameInputField.interactable = false;

        // =========================
        // SELECT SISWA
        // =========================
        selectButton.onClick.RemoveAllListeners();

        StudentData selectedData = currentData;

        selectButton.onClick.AddListener(() =>
        {
            Debug.Log(
                "BUTTON CLICK: " +
                selectedData.studentName
            );

            manager.SelectStudent(selectedData);
        });

        // =========================
        // EDIT NAMA
        // =========================
        editButton.onClick.RemoveAllListeners();

        editButton.onClick.AddListener(() =>
        {
            StartEdit();
        });

        // =========================
        // DELETE SISWA
        // =========================
        deleteButton.onClick.RemoveAllListeners();

        deleteButton.onClick.AddListener(() =>
        {
            Debug.Log(
                "OPEN DELETE PANEL: " +
                currentData.studentName
            );

            manager.OpenDeletePanel(currentData);
        });

        // =========================
        // ENTER SUBMIT
        // =========================
        nameInputField.onSubmit.RemoveAllListeners();

        nameInputField.onSubmit.AddListener(delegate
        {
            FinishEdit();
        });

        // =========================
        // KLIK LUAR INPUT
        // =========================
        nameInputField.onDeselect.RemoveAllListeners();

        nameInputField.onDeselect.AddListener(delegate
        {
            FinishEdit();
        });
    }

    // =========================
    // START EDIT
    // =========================
    void StartEdit()
    {
        nameInputField.interactable = true;

        nameInputField.ActivateInputField();

        nameInputField.Select();
    }

    // =========================
    // FINISH EDIT
    // =========================
    void FinishEdit()
    {
        if (!nameInputField.interactable)
            return;

        currentData.studentName =
            nameInputField.text;

        nameInputField.interactable = false;

        manager.SaveStudentDatabase();

        manager.RefreshStudentList();
    }
}