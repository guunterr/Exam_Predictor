using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;

public class Trainer{

    private TestNetwork testNetwork;
    //Constructor
    public Trainer(TestNetwork _testNetwork)
    {
        testNetwork = _testNetwork;
    }

    public void TrainNetwork(int numIterations)
    {
        //Debug.Log("Starting test data cost");
        //Debug.Log(testNetwork.CostFunction(testNetwork.testinputnorm, testNetwork.testoutput));
        //Calculates the cost at the beginning of training - for debugging purposes
        Debug.Log("Starting cost");
        Debug.Log(testNetwork.CostFunction(testNetwork.xnorm, testNetwork.y));
        //Adjusts the weights in the direction of the derivative using the UpdateWeights function found in the TestNetwork class.
        for (int i = 0; i < numIterations; i++)
        {
            //Debug.Log("Cost at attempt # " + i.ToString());
            //Debug.Log(testNetwork.CostFunction(testNetwork.x, testNetwork.y));
            testNetwork.UpdateWeights(testNetwork.CostFunctionPrime(testNetwork.xnorm, testNetwork.y));
        }
        //Returns the final cost of the model on the training inputs, used for debugging.
        Debug.Log("Final cost");
        Debug.Log(testNetwork.CostFunction(testNetwork.xnorm, testNetwork.y));
        //Debug.Log("Final test data cost");
        //Debug.Log(testNetwork.CostFunction(testNetwork.testinputnorm, testNetwork.testoutput));
    }

    public void TrainNetworkNumerically(int numIterations)
    {
        //Numerical testing function, used to check derivatives and train if the derivative function broke
        //(I ended up fixing the derivative function) - 01/11/2018
        //For testing purposes: model cost at start
        Debug.Log("Starting cost");
        Debug.Log(testNetwork.CostFunction(testNetwork.xnorm, testNetwork.y));

        for (int i = 0; i < numIterations; i++)
        {
            //Debug.Log("Cost at attempt # " + i.ToString());
            //Debug.Log(testNetwork.CostFunction(testNetwork.x, testNetwork.y));
            //Update weights in direction of negative gradients.
            testNetwork.UpdateWeights(testNetwork.NumericalGradients(testNetwork.xnorm, testNetwork.y));
        }

        //For testing purposes, final model cost
        Debug.Log("Final cost");
        Debug.Log(testNetwork.CostFunction(testNetwork.xnorm, testNetwork.y));
    }
}
