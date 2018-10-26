using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Linq;

public enum Subject { English, Maths, Science, Latin, Spanish, Geography, History, Art, French, Mandarin, Computing }
public enum Parameter { Sleep, Study, Tuition, SleepRange }
public enum Grade {A, B, C, D, E, F, G}

public class AppManager : MonoBehaviour {



    const string TEST_FILE_LOCATION = "Assets/Resources/test.txt";
    const string FILE_NAME = "test";
    const string FILE_PATH = "Assets/Resources/";
    const string FILE_TYPE = ".txt";

    public static List<float> ParameterValues = new List<float> { 8, 3, 1, 2 };
    public List<Test> tests = new List<Test>(); 
    public TextAsset testFile;

    private void OnEnable()
    {
        //TODO: Subscribe to appropriate events
        //For example AppEventManager.thisdelegate += function;
        TestNetwork Network = new TestNetwork();
        List<Matrix<double>> derivatives = Network.CostFunctionPrime(Network.x, Network.y);
        Debug.Log("Symbolic dJdW1");
        Debug.Log(derivatives[0].ToString());
        Debug.Log("Symbolic dJdW2");
        Debug.Log(derivatives[1].ToString());
        Debug.Log("Cost with randomly initialised variables");
        Debug.Log(Network.CostFunction(Network.x, Network.y).ToString());

        Trainer trainer = new Trainer(Network);
        trainer.TrainNetwork(100);

        Debug.Log("Expected input behaviours to get an A");
        Debug.Log(Network.ReversePropagation(Network.reverseInput));

        Debug.Log("Symbolic dJdW1");
        Debug.Log(derivatives[0].ToString());
        Debug.Log("Symbolic dJdW2");
        Debug.Log(derivatives[1].ToString());
    }

    private void OnDisable()
    {
        //TODO: Unsubscribe to appropriate events
        //For example AppEventManager.thisdelegate -= function;
    }

    private void Start()
    {
        //tests.Add(new Test( ((DateTime.UtcNow.Ticks/TimeSpan.TicksPerSecond).ToString() + ",3,3,5,4,6,2").Split(new[] { "," }, StringSplitOptions.None)));
        //ReadTests();
        //WriteTests();
    }


    public void ReadTests()
    {
        string filetext = testFile.text;
        string[] lines = filetext.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        if (lines.Length == 0)
        {
            return;
        }
        for (int i = 0; i < lines.Length - 1; i++)
        {
            Debug.Log(lines[i]);
            string[] parameters = lines[i].Split(new[] { "," }, StringSplitOptions.None);
            if (parameters.Length == 7)
            {
                Debug.Log(parameters[0]);
                tests.Add(new Test(parameters));
            }
            else
            {
                Debug.Log("Reading unsuitable text from file");

            }
        }

        tests = tests.Distinct().ToList();
    }

    public void WriteTests()
    {
        tests = tests.Distinct().ToList();

        using (StreamWriter testWriter = new StreamWriter(TEST_FILE_LOCATION, true))
        {
            for (int i = 0; i < tests.Count; i++)
            {
                testWriter.WriteLine(tests[i].GetParams());
            }
        }

    }
}
