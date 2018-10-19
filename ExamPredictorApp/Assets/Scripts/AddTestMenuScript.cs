using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddTestMenuScript : MonoBehaviour {
    public Button homeButton;
    public TMP_Dropdown subjectDropdown;
    public TMP_InputField sleepTimeField;
    public TMP_InputField studyTimeField;
    public TMP_InputField sleepRangeField;
    public TMP_InputField tuitionTimeField;
    public TMP_InputField testScore;
    public Button finishButton;

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
            subjectDropdown.AddOptions(new List<string> { "No Subjects" });
            return;
        }
        subjectDropdown.AddOptions(subjectList.ToList<string>());
    }

    private void ClearDropdownMenu()
    {
        subjectDropdown.ClearOptions();
    }

    private void CollectFormData()
    {

        //Test test = new Test();
    }

    public void OnEnable()
    {
        ClearDropdownMenu();
        PopulateDropdownMenu();
    }
}
