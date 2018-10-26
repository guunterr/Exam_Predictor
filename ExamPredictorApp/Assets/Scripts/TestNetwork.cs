using MathNet.Numerics;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class TestNetwork {

    //Network Parameters
    //Amount of different training examples - number of tests in main program
    public int numExamples;
    //Amount of testing data examples - used to calculate overfitting
    public int numTestingData;
    //Amount of parameters for each input
    public int inputLayerSize;
    //Size of the hidden layer
    public int hiddenLayerSize;
    //Size of the output layer. Will likely be 8 for A* - G Grades
    public int outputLayerSize;
    //Hidden layer 1 matrix, part of neural network black box calculator
    public Matrix<double> hiddenLayer;

    public Matrix<double> hiddenLayerActivation;
    //Output matrix of A* - G grades.
    public Matrix<double> outputLayer;

    public Matrix<double> outputLayerActivation;
    //Weights connecting input layer and hidden layer
    public Matrix<double> weight1;
    //Weights connecting hidden layer and output layer
    public Matrix<double> weight2;
    //Inputs to be trained on - matrix
    public Matrix<double> x;
    //Outputs to be trained on - matrix
    public Matrix<double> y;
    //Training data to measure overfitting
    public Matrix<double> testinput;
    public Matrix<double> testoutput;

    public Matrix<double> reverseInput;
    //Rate at which corrections are made to the network - this needs to be tuned by trial and error without Quasi-Newton Algorithms
    public double learningRate;

    public TestNetwork()
    {

        var V = Vector<double>.Build;
        //Network parameters - can be fiddled with at any time
        numExamples = 6;
        inputLayerSize = 4;
        hiddenLayerSize = 12;
        outputLayerSize = 1;
        learningRate = 0.05;
        reverseInput = Matrix<double>.Build.Dense(1, outputLayerSize);
        reverseInput[0, 0] = 1;

        //Initialising random weights and training data for testing
        weight1 = Matrix<double>.Build.Random(inputLayerSize, hiddenLayerSize);
        weight2 = Matrix<double>.Build.Random(hiddenLayerSize, outputLayerSize);
        //Distribute Sleep, Study time etc as they are expected to be distributed (normally with different means and standard deviations
        //Based on consultation with focus group.
        //Normalised variables (X-mu)/sigma
        x = Matrix<double>.Build.DenseOfColumnVectors((V.Random(6, Normal.WithMeanStdDev(8, 1)) - 6)/1,
            (V.Random(6, Normal.WithMeanStdDev(3, 0.6))-3)/0.6,
            (V.Random(6, Normal.WithMeanStdDev(1, 0.3))-1)/0.3,
            (V.Random(6, Normal.WithMeanStdDev(6, 1))-6)/1);
        //x = x / x.Enumerate().Maximum();
        y = Matrix<double>.Build.Dense(numExamples, outputLayerSize);
        System.Random rnd = new System.Random();
        for (int i = 0; i < numExamples; i++)
        {
            y[i, rnd.Next(1, 6)] = 1;
        }

        //Prints matrices to the log in order to test them
        Debug.Log("Weight 1");
        Debug.Log(weight1.ToString());
        Debug.Log("Weight 2");
        Debug.Log(weight2.ToString());
        Debug.Log("X");
        Debug.Log(x.ToString());
        Debug.Log("Y");
        Debug.Log(y.ToString());
    }

    public Matrix<double> Forward(Matrix<double> input)
    {
        //Testing: assigning random inputs just to test forward propagation function:
        //inputLayer = Matrix<double>.Build.Random(numExamples, inputLayerSize);
        //Debug.Log("Input Matrix:");
        //Debug.Log(inputLayer.ToString());
        //Debug.Log("Hidden Layer before and after activation function respectively:");
        hiddenLayer = input.Multiply(weight1);
        //Debug.Log(hiddenLayer.ToString());
        hiddenLayerActivation = hiddenLayer.Map(Activation);
        //Debug.Log(hiddenLayerActivation.ToString());
        //Debug.Log("Output layer before activation function:");
        outputLayer = hiddenLayerActivation.Multiply(weight2);
        //Debug.Log(outputLayer.ToString());
        outputLayerActivation = outputLayer.Map(Activation);
        //Debug.Log("Output of forward propagation function:");
        //Debug.Log(outputLayerActivation.ToString());

        return outputLayerActivation;
    }

    public Matrix<double> Forward(Matrix<double> input, Matrix<double> weightmatrix1, Matrix<double> weightmatrix2)
    {
        //Testing: assigning random inputs just to test forward propagation function:
        //inputLayer = Matrix<double>.Build.Random(numExamples, inputLayerSize);
        //Debug.Log("Input Matrix:");
        //Debug.Log(inputLayer.ToString());
        //Debug.Log("Hidden Layer before and after activation function respectively:");
        hiddenLayer = input.Multiply(weightmatrix1);
        //Debug.Log(hiddenLayer.ToString());
        hiddenLayerActivation = hiddenLayer.Map(Activation);
        //Debug.Log(hiddenLayerActivation.ToString());
        //Debug.Log("Output layer before activation function:");
        outputLayer = hiddenLayerActivation.Multiply(weightmatrix2);
        //Debug.Log(outputLayer.ToString());
        outputLayerActivation = outputLayer.Map(Activation);
        //Debug.Log("Output of forward propagation function:");
        //Debug.Log(outputLayerActivation.ToString());

        return outputLayerActivation;
    }

    public double Activation(double z)
    {
        //Sigmoid logstic function. To be applied pointwise to matrix
        return 1 / (1 + Math.Exp(-z));
    }

    public double Square(double z)
    {
        //Square function, to be applied pointwise to cost function
        return Math.Pow(z, 2);
    }

    public double ActivationPrime(double z)
    {
        //Sigmoid derivative - e^-x/(1+e^-x)^2
        //Used for calculating partial derivative of costs with respect to weights.
        return Math.Exp(-z)/Square(1d+Math.Exp(-z));
    }

    public double Mod(double z)
    {
        return Math.Abs(z);
    }

    public double CostFunction(Matrix<double> input, Matrix<double> expectedOutput)
    {
        //implementation of J = 1/2(y-yHat)^2
        Matrix<double> yHat = Forward(input);
        Matrix<double> costmatrix = expectedOutput - yHat;
        costmatrix.MapInplace(Square);
        costmatrix = costmatrix * 0.5d;
        //Turns enumerable to array because I don't know how to work with enumerables
        double[] costlist = costmatrix.Enumerate().ToArray();

        double cost = 0;
        //sums every difference in expected output into one large double
        for (int i = 0; i < costlist.Length; i++)
        {
            cost = cost + costlist[i];
        }

        //Debug.Log("Cost = ");
        //Debug.Log(cost.ToString());

        return cost;
    }

    public double CostFunction(Matrix<double> input, Matrix<double> expectedOutput, Matrix<double> weightmatrix1, Matrix<double> weightmatrix2)
    {
        //implementation of J = 1/2(y-yHat)^2
        Matrix<double> yHat = Forward(input, weightmatrix1, weightmatrix2);
        Matrix<double> costmatrix = expectedOutput - yHat;
        costmatrix.MapInplace(Square);
        costmatrix = costmatrix * 0.5d;
        //Turns enumerable to array because I don't know how to work with enumerables
        double[] costlist = costmatrix.Enumerate().ToArray();

        double cost = 0;
        //sums every difference in expected output into one large double
        for (int i = 0; i < costlist.Length; i++)
        {
            cost = cost + costlist[i];
        }

        //Debug.Log("Cost = ");
        //Debug.Log(cost.ToString());

        return cost;
    }

    public List<Matrix<double>> CostFunctionPrime(Matrix<double> input, Matrix<double> expectedOutput)
    {

        //FINISHED! NOW I MUST COMMENT!
        //Calculates output from the network
        Matrix<double> yHat = Forward(input);
        //Backpropagation of the error. First apply power rule to cost function,
        //Then multiply by the derivative of the sigmoid function to find the derivative of y hat with respect to the output layer activation
        Matrix<double> delta3 = -(expectedOutput - yHat).PointwiseMultiply(outputLayer.Map(ActivationPrime));
        //Finally multiply by the activation of the hidden layer in order to 'assign blame' for the error.
        Matrix<double> dJdW2 = hiddenLayerActivation.Transpose().Multiply(delta3);
        //Debug.Log("dJ/dW2");
        //Debug.Log(dJdW2.ToString());
        //Continue the chain rule. This time backpropagate the weight errors to the hidden layer, and then multiply by the input matrix to 'assign blame'
        Matrix<double> delta2 = delta3.Multiply(weight2.Transpose()).PointwiseMultiply(hiddenLayer.Map(ActivationPrime));
        Matrix<double> dJdW1 = input.Transpose().Multiply(delta2);
        //Debug.Log("dJ/dW1");
        //Debug.Log(dJdW1.ToString());

        List<Matrix<double>> outputList = new List<Matrix<double>>
        {
            dJdW1,
            dJdW2
        };

        return outputList;
    }

    public Matrix<double> TestGradient1(Matrix<double> input, Matrix<double> expectedOutput)
    {
        //Uses the fundamental definition of the derivative - small change in x over small change in output
        //Since this a multivariable function we change each individual entry in the weight matrix to find an approximation of the error.
        //Though this will give us a good answer, it is not very practical to apply to the final problem, especially as the dimensionality grows
        //Thus this function is going to be used as a test for our gradients.
        double epsilon = 1e-3;
        //Matrix which will edit the individual elements in the weight matrix which will be fed into the
        Matrix<double> perturb = Matrix<double>.Build.Dense(inputLayerSize, hiddenLayerSize);
        //Matrix into which the numerical gradient will be input. Same dimensions as the weight matrix.
        Matrix<double> numgrad = Matrix<double>.Build.Dense(inputLayerSize, hiddenLayerSize);
        //Because c# is a hard language, if i just say weightmatrix = weight1, I will not actually create a new variable weightmatrix
        //But I will instead place a pointer where weightmatrix is pointing to weight1, which would ordinarily save memory.
        //However this means that any mistake or runtime crash here would cause my weight to change, completely undoing all the training
        //Therefore for the purposes of this function I decided to convert weight1 into an enumerable, which would be stored in a new namespace
        //And then reroll this enumerator into a matrix with the same dimensions as the original.
        IEnumerable<Tuple<int,int,double>> weightenum = weight1.EnumerateIndexed();
        Matrix<double> weightmatrix = Matrix<double>.Build.DenseOfIndexed(inputLayerSize, hiddenLayerSize, weightenum);
        // Test to see whether those two are the same, because if they aren't then it's a big problem

        //Debug.Log("weightmatrix - weight1, all good if a bunch of zeros else we are in trouble");
        //Debug.Log((weightmatrix - weight1).ToString());

        double cost1, cost2;

        for (int i = 0; i < inputLayerSize; i++)
        {
            for (int j = 0; j < hiddenLayerSize; j++)
            {

                perturb[i, j] = epsilon;
                weightmatrix = weightmatrix - perturb;
                cost1 = CostFunction(input, expectedOutput, weightmatrix, weight2);
                weightmatrix = weightmatrix + 2d * perturb;
                cost2 = CostFunction(input, expectedOutput, weightmatrix, weight2);
                weightmatrix = weightmatrix - perturb;
                perturb[i, j] = 0d;
                numgrad[i, j] = (cost2 - cost1) /(2*epsilon);
            }
        }

        return numgrad;
    }

    public Matrix<double> TestGradient2(Matrix<double> input, Matrix<double> expectedOutput)
    {
        double epsilon = 1e-3;
        Matrix<double> perturb = Matrix<double>.Build.Dense(hiddenLayerSize, outputLayerSize);
        Matrix<double> numgrad = Matrix<double>.Build.Dense(hiddenLayerSize, outputLayerSize);
        IEnumerable<Tuple<int, int, double>> weightenum = weight2.EnumerateIndexed();
        Matrix<double> weightmatrix = Matrix<double>.Build.DenseOfIndexed(hiddenLayerSize, outputLayerSize, weightenum);

        double cost1, cost2;

        for (int i = 0; i < hiddenLayerSize; i++)
        {
            for (int j = 0; j < outputLayerSize; j++)
            {

                perturb[i, j] = epsilon;
                weightmatrix = weightmatrix - perturb;
                cost1 = CostFunction(input, expectedOutput, weight1, weightmatrix);
                weightmatrix = weightmatrix + 2d * perturb;
                cost2 = CostFunction(input, expectedOutput, weight1, weightmatrix);
                weightmatrix = weightmatrix - perturb;
                perturb[i, j] = 0d;
                numgrad[i, j] = (cost2 - cost1) / (2 * epsilon);
            }
        }

        return numgrad;
    }

    public List<Matrix<double>> NumericalGradients(Matrix<double> input, Matrix<double> expectedOutput)
    {
        Matrix<double> dJdW1 = TestGradient1(input, expectedOutput);
        Matrix<double> dJdW2 = TestGradient2(input, expectedOutput);

        List<Matrix<double>> outputList = new List<Matrix<double>>
        {
            dJdW1,
            dJdW2
        };

        return outputList;
    }

    public void UpdateWeights(List<Matrix<double>> derivatives)
    {
        weight1 = weight1 - learningRate * derivatives[0];
        weight2 = weight2 - learningRate * derivatives[1];
    }

    public Matrix<double> ReversePropagation(Matrix<double> result)
    {
        Matrix<double> middleLayer = result.Multiply(weight2.Transpose());
        Matrix<double> projectedInput = middleLayer.Multiply(weight1.Transpose());

        return projectedInput;

    }
}
