using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;

public enum Subject { English, Maths, Science, Latin, Spanish, Geography, History, Art, French, Mandarin, Computing }
public enum Parameter { Sleep, Study, Tuition, SleepRange }

public class AppManager : MonoBehaviour {



    const string TEST_FILE_LOCATION = "Assets/Resources/test.txt";
    const string FILE_NAME = "test";
    const string FILE_PATH = "Assets/Resources/";
    const string FILE_TYPE = ".txt";

    public static List<float> ParameterValues = new List<float> { 8, 3, 1, 2 };
    public static List<Test> ExistingTests = new List<Test>();

    public TextAsset testFile;

    private void OnEnable()
    {
        //TODO: Subscribe to appropriate events
        //For example AppEventManager.thisdelegate += function;
        TestNetwork Network = new TestNetwork();
        List<Matrix<double>> derivatives = Network.CostFunctionPrime(Network.x, Network.y);
        Debug.Log("Symbolic dJdW1");
        Debug.Log(derivatives[0].ToString());
        Debug.Log("Numerical dJdW1");
        Debug.Log(Network.TestGradient1(Network.x, Network.y));
        Debug.Log("Difference between symbolic and numeric dJdW1");
        Debug.Log((derivatives[0] - Network.TestGradient1(Network.x, Network.y)).ToString());
        Debug.Log("Symbolic dJdW2");
        Debug.Log(derivatives[1].ToString());
        Debug.Log("Numerical dJdW2");
        Debug.Log(Network.TestGradient2(Network.x, Network.y));
        Debug.Log("Cost with randomly initialised variables");
        Debug.Log(Network.CostFunction(Network.x, Network.y).ToString());

        Trainer trainer = new Trainer(Network);
        trainer.TrainNetwork(500);
    }

    private void OnDisable()
    {
        //TODO: Unsubscribe to appropriate events
        //For example AppEventManager.thisdelegate -= function;
    }

    private void Start()
    {
        LoadTests();
        SaveTests();

    }

    private void LoadTests()
    {
        /**StreamReader testreader;
        try
        {
            testreader = new StreamReader(TEST_FILE_LOCATION);
        }
        catch
        {
            //TODO: Create file if file does not exist.

            Debug.LogWarning("The reader has found no text file at: " + TEST_FILE_LOCATION);
            testreader = null;
        }

        //if (reader.ReadToEnd() == "")
        //{
        //    Debug.Log("Text file empty at " + TEST_FILE_LOCATION);
        //}
        //Read the text from directly from the test.txt file
        Debug.Log("Text file: " + testreader.ReadLine());
        testreader.Close();
        **/

        Debug.Log("File text: " + testFile.text);
    }


    private void SaveTests()
    {
        StreamWriter testWriter;

        testFile = Resources.Load<TextAsset>(FILE_NAME + FILE_TYPE) as TextAsset;

        try
        {
            testWriter = File.AppendText(FILE_PATH + FILE_NAME + FILE_TYPE);
        }
        catch
        {
            Debug.LogWarning("The writer has found no text file at: " + TEST_FILE_LOCATION);
            testWriter = null;
        }

        for (int i = 0; i < ExistingTests.Count; i++)
        {
            Debug.Log("Writing following line to test file " + ExistingTests[i].GetAttributes());
            testWriter.WriteLine(ExistingTests[i].GetAttributes());
        }
        testWriter.WriteLine("Lorem Ipsum is boring and awful");
        testWriter.Close();
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
