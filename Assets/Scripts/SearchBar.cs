using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class SearchBar : MonoBehaviour
{
    [SerializeField] private CardManager m_cardManager;
    [SerializeField] private TMP_InputField searchBar;

    [SerializeField] private List<GameObject> m_container;

    private void Start()
    {
        m_cardManager.onDeletePressed += CleanSearchbar;
        m_cardManager.onEnterSearchPressed += CleanSearchbar;
    }

    public void CleanSearchbar(string judulObjek)
    {
        searchBar.text = "";
        searchBar.textComponent.text = "";
    }

    public void CleanSearchbar()
    {
        searchBar.text = "";
        searchBar.textComponent.text = "";
    }

    public void Search()
    {
        //Debug.LogWarning("Serach Start");
        if (m_container == null || m_container != m_cardManager.FetchAllData())
        {
            m_container = m_cardManager.FetchAllData();
        }

        string SearchText = searchBar.text;
        int searchTxtLenght = SearchText.Length;

        int searchedElements = 0;

        foreach (GameObject element in m_container)
        {
            searchedElements++;

            Transform childPlhd = element.transform.GetChild(1);
            Transform targetChild = childPlhd.transform.GetChild(0);

            TextMeshProUGUI eleTxt = null;
            if (targetChild.GetComponent<TextMeshProUGUI>() != null)
            {
                eleTxt = targetChild.GetComponent<TextMeshProUGUI>();
            }

            if (eleTxt.text.Length >= searchTxtLenght)
            {
                if (SearchText.ToLower() == eleTxt.text.Substring(0, searchTxtLenght).ToLower())
                {
                    element.SetActive(true);
                }
                else
                {
                    element.SetActive(false);
                }
            }
        }
    }
}

/// Referensi:
/// https://www.youtube.com/watch?v=Pu5gehrPy90