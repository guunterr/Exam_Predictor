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

    public static List<double> ParameterValues = new List<double> { 0, 0, 0, 0 };
    public List<Test> tests = new List<Test>(); 
    public TextAsset testFile;

    private void OnEnable()
    {
        //TODO: Subscribe to appropriate events
        //For example AppEventManager.thisdelegate += function;
        //Initiialises the network and trainer
        TestNetwork Network = new TestNetwork();
        //Trains the network
        Trainer trainer = new Trainer(Network);
        trainer.TrainNetwork(500);
        //Recieves expected input behaviours.
        //Network.reverseInput is the vector that corresponds to an A or A* (The highest grade)
        Debug.Log("Expected input behaviours to get an A");
        Matrix<double> recommendations = Network.ReturnRecommendations(Network.reverseInput);
        Debug.Log(recommendations);
        //Sets parameter value list to recommendations, then update will be called in the main menu script
        for (int i = 0; i < recommendations.ColumnCount; i++)
        {
            ParameterValues[i] = Math.Round(recommendations[0, i], 1);
        }
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
