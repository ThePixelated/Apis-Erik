using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StudentCardUI : MonoBehaviour
{
    [Header("UI")]
    public TMP_InputField nameInputField;

    public Button editButton;
    public Button deleteButton;
    public Button selectButton;

    private StudentData currentData;

    private StudentManager manager;

    // Setup data card
    public void Setup(
        StudentData data,
        StudentManager studentManager
    )
    {
        currentData = data;

        manager = studentManager;

        nameInputField.text =
            currentData.studentName;

        nameInputField.interactable = false;

        // tombol pilih siswa
        selectButton.onClick.RemoveAllListeners();

        selectButton.onClick.AddListener(() =>
        {
            manager.SelectStudent(currentData);
        });

        // tombol edit
        editButton.onClick.RemoveAllListeners();

        editButton.onClick.AddListener(() =>
        {
            StartEdit();
        });

        // tombol delete
        deleteButton.onClick.RemoveAllListeners();

        deleteButton.onClick.AddListener(() =>
        {
            manager.DeleteStudent(currentData);
        });

        // submit enter
        nameInputField.onSubmit.AddListener(delegate
        {
            FinishEdit();
        });

        // klik luar input
        nameInputField.onDeselect.AddListener(delegate
        {
            FinishEdit();
        });
    }

    void StartEdit()
    {
        nameInputField.interactable = true;

        nameInputField.ActivateInputField();

        nameInputField.Select();
    }

    void FinishEdit()
    {
        if (!nameInputField.interactable)
            return;

        currentData.studentName =
            nameInputField.text;

        nameInputField.interactable = false;

        manager.RefreshStudentList();
    }
}