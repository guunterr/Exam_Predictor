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

    private void PopulateDropdownMenu()
    {
        subjectDropdown.AddOptions(AppManager.subjectList);
    }

    private void ClearDropdownMenu()
    {
        subjectDropdown.ClearOptions();
    }
    
    private void PopulateParameterList()
    {

    }

    public void OnEnable()
    {
        ClearDropdownMenu();
        PopulateDropdownMenu();
    }
}
