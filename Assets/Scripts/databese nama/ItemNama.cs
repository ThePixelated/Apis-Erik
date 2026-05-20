using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemNama : MonoBehaviour, IPointerClickHandler
{
    private TMP_InputField inputField;

    private void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        NamaSelector.Instance.PilihNama(inputField.text);
    }
}