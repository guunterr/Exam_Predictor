using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuScript : MonoBehaviour {

    public GameObject listItemPrefab;
    public Button myTestsButton;
    public Button getPredictionButton;
    public TMP_Dropdown subjectDropdown;
    public GameObject listItemPanel;
    private List<GameObject> itemPanelObjects = new List<GameObject>();

    private void PopulateDropdownMenu()
    {
        if (AppManager.subjectList == null)
        {
            Debug.LogWarning("In AppManager, Subject List is uninitialised (AppManager.subjectList)");
            subjectDropdown.AddOptions(new List<string> { "No Subjects" });
            return;
        }

        if (AppManager.subjectList.Count == 0)
        {
            Debug.Log("Subject List in Appmanager is empty (AppManager.subjectList)");
            subjectDropdown.AddOptions(new List<string> {"No Subjects"});
            return;
        }
        subjectDropdown.AddOptions(AppManager.subjectList);
    }

    private void ClearDropdownMenu()
    {
        subjectDropdown.ClearOptions();
    }
    
    private void PopulateParameterList()
    {
        if (AppManager.parameterList == null)
        {
            Debug.LogWarning("In AppManager, parameterList list variable is not initialised");
            return;
        }

        if (AppManager.parameterValues == null)
        {
            Debug.LogWarning("In AppManager, parameterValues list variable is not initialised");
            return;
        }

        if (AppManager.parameterList.Count != AppManager.parameterValues.Count)
        {
            Debug.LogWarning("In Appmanager, the amount of parameters is not the same as the amount of values (parameterList != parameterValues).");
            return;
        }

        for (int i = 0; i < AppManager.parameterList.Count; i++)
        {
            GameObject newListItem = Instantiate(listItemPrefab, listItemPanel.transform);
            newListItem.GetComponentInChildren<TMP_Text>().SetText(AppManager.parameterList[i] + " " + AppManager.parameterValues[i].ToString());
            itemPanelObjects.Add(newListItem);
        }

    }

    private void ClearParameterList()
    {
        for (int i = 0; i < itemPanelObjects.Count; i++)
        {
            Destroy(itemPanelObjects[i]);
        }
        itemPanelObjects.Clear();
    }

    public void OnEnable()
    {
        ClearDropdownMenu();
        PopulateDropdownMenu();
        ClearParameterList();
        PopulateParameterList();
        ClearParameterList();
    }
}
