using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;

public class Trainer{

    private TestNetwork testNetwork;

    public Trainer(TestNetwork _testNetwork)
    {
        testNetwork = _testNetwork;
    }

    public void TrainNetwork(int numIterations)
    {
        Debug.Log("Starting cost");
        Debug.Log(testNetwork.CostFunction(testNetwork.x, testNetwork.y));

        for (int i = 0; i < numIterations; i++)
        {
            //Debug.Log("Cost at attempt # " + i.ToString());
            //Debug.Log(testNetwork.CostFunction(testNetwork.x, testNetwork.y));
            testNetwork.UpdateWeights(testNetwork.CostFunctionPrime(testNetwork.x, testNetwork.y));
        }

        Debug.Log("Final cost");
        Debug.Log(testNetwork.CostFunction(testNetwork.x, testNetwork.y));
    }

    public void TrainNetworkNumerically(int numIterations)
    {

        //For testing purposes: model cost at start
        Debug.Log("Starting cost");
        Debug.Log(testNetwork.CostFunction(testNetwork.x, testNetwork.y));

        for (int i = 0; i < numIterations; i++)
        {
            //Debug.Log("Cost at attempt # " + i.ToString());
            //Debug.Log(testNetwork.CostFunction(testNetwork.x, testNetwork.y));
            //Update weights in direction of negative gradients.
            testNetwork.UpdateWeights(testNetwork.NumericalGradients(testNetwork.x, testNetwork.y));
        }

        //For testing purposes, final model cost
        Debug.Log("Final cost");
        Debug.Log(testNetwork.CostFunction(testNetwork.x, testNetwork.y));
    }
}
