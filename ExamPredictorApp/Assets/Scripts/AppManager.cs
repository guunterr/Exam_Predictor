using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public enum Subject { English, Maths, Science, Latin, Spanish, Geography, History, Art, French, Mandarin, Computing }
public enum Parameter { Sleep, Study, Tuition, SleepRange }

public class AppManager : MonoBehaviour {

    const string TEST_FILE_LOCATION = "Assets/Resources/test.txt";

    public static List<float> ParameterValues = new List<float> { 8, 3, 1, 2 };
    public static List<Test> ExistingTests = new List<Test>();

    private void Start()
    {
        LoadTests();
    }

    private void LoadTests()
    {
        StreamReader reader;
        try
        {
            reader = new StreamReader(TEST_FILE_LOCATION);
        }
        catch
        {
            //TODO: Create file if file does not exist.

            Debug.LogWarning("There is no text file at: " + TEST_FILE_LOCATION);
            reader = null;
        }

        //Read the text from directly from the test.txt file
        //StreamReader reader = new StreamReader(TEST_FILE_LOCATION);
        //Debug.Log("Text file: " + reader.ReadToEnd());
        //reader.Close();
    }


    private void SaveTests()
    {

    }

    public void AddTest(Test test)
    {
        ExistingTests.Add(test);
    }

    public void RemoveTest(Test test)
    {
        ExistingTests.Remove(test);
    }

}
