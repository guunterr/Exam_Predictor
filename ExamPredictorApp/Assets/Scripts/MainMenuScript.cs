using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class MainMenuScript : MonoBehaviour {

    public GameObject listItemPrefab;
    public Button myTestsButton;
    public Button getPredictionButton;
    public TMP_Dropdown subjectDropdown;
    public GameObject listItemPanel;
    private List<GameObject> itemPanelObjects = new List<GameObject>();

    private void PopulateDropdownMenu()
    {
        string[] subjectList = Enum.GetNames(typeof(Subject));
        if (subjectList == null)
        {
            Debug.LogWarning("In AppManager, Subject List is uninitialised (AppManager.subjectList)");
            subjectDropdown.AddOptions(new List<string> { "No Subjects" });
            return;
        }

        if (subjectList.Length == 0)
        {
            Debug.Log("Subject List in Appmanager is empty (AppManager.subjectList)");
            subjectDropdown.AddOptions(new List<string> {"No Subjects"});
            return;
        }
        subjectDropdown.AddOptions(subjectList.ToList<string>());
    }

    private void ClearDropdownMenu()
    {
        subjectDropdown.ClearOptions();
    }
    
    private void PopulateParameterList()
    {

        string[] parameterList = Enum.GetNames(typeof(Parameter));
        if (parameterList.Length == 0)
        {
            Debug.LogWarning("In AppManager, parameterList list variable is not initialised");
            return;
        }

        if (AppManager.ParameterValues == null)
        {
            Debug.LogWarning("In AppManager, parameterValues list variable is not initialised");
            return;
        }

        if (parameterList.Length != AppManager.ParameterValues.Count)
        {
            Debug.LogWarning("In Appmanager, the amount of parameters is not the same as the amount of values (parameterList != parameterValues).");
            return;
        }

        for (int i = 0; i < parameterList.Length; i++)
        {
            GameObject newListItem = Instantiate(listItemPrefab, listItemPanel.transform);
            newListItem.GetComponentInChildren<TMP_Text>().SetText(parameterList[i] + " " + AppManager.ParameterValues[i].ToString());
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
    }
}
