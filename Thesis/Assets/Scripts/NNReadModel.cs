/*
 * Made with <3
 * - Kei
 * 
 * Game Neural Network Model (Load and Test)
 * 1.   Provide the directory of the model file (format: TXT)
 * 2.   Wait for the program to load the model
 * 3.   Provide input and the model will output its prediction
 */

using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class NNReadModel : MonoBehaviour
{
    public static int tempo;
    public void Init()
    {
        String input = "", path = "";


        // Request for neural network model file directory
        path = "Model_4-32-32-3.txt";

        // Load neural network model
        int[] modelStructure = GetStructure(path);
        GameNeuralNetwork model = new(modelStructure);
        model.LoadModel(path);
        model.NetworkDetails();

        {
            List<double> inputFeatures = new();

            int time = PassIt.finalTime;
            int score = PassIt.finalCoins;
            int death = PassIt.finalDeaths;
            int level = PassIt.level;
            input = time + " " + score + " " + (death + 1) + " " + level;

            print("Inputs: " + input);
            if (input.Split(' ').Length == 4)
            {
                int predTempo = 0;
                double featureMax = model.GetValue("MAX");
                double featureMin = model.GetValue("MIN");

                List<List<double>> tempPredict = new();
                List<List<double>> prediction = new();
                List<String> tempInput = input.Split(' ').ToList();

                // Check if input is numeric
                inputFeatures = tempInput.Select(x => double.Parse(x)).ToList();

                // Normalize input values
                for (int i = 0; i < inputFeatures.Count; i++)
                {
                    inputFeatures[i] = (inputFeatures[i] - featureMin) / (featureMax - featureMin);
                }

                // Add bias value
                inputFeatures.Add(1.0);

                tempPredict.Add(inputFeatures);
                prediction = model.Predict(tempPredict);
                predTempo = ArgMax(prediction[0]);

                Debug.Log("Outputs: " + string.Join(",", prediction[0]));

                // Change prediction to equivalent tempo
                if (predTempo == 0)
                {
                    tempo = 0;
                }
                else if (predTempo == 1)
                {
                    tempo = 1;
                }
                else if (predTempo == 2)
                {
                    tempo = 2;
                }
            }
            else
            {
                Debug.Log("\n[ERROR] Unknown input!\n");
            }
        }
    }
    public static void Main()
    {

    }
    private static int[] GetStructure(String input)
    {
        // Make sure file name follows proper format
        String path = input;

        // Remove file directory and 'Model_' from file name
        string[] filter1 = path.Split('_');
        path = filter1[filter1.Length - 1];

        // Remove .txt from file name
        string[] filter2 = path.Split('.');
        path = filter2[0];

        // Numbers separated by '-' shows number of nodes per layer
        string[] nodes = path.Split('-');
        int[] neuralNodes = new int[nodes.Length];

        for (int i = 0; i < nodes.Length; i++)
        {
            int.TryParse(nodes[i], out neuralNodes[i]);
        }

        Debug.Log("[OUTPUT] Model Structure: " + string.Join(", ", neuralNodes) + "\n");

        return neuralNodes;
    }
    private static int ArgMax(List<double> input)
    {
        int index = 0;
        double prediction = double.MinValue;
        List<double> predictions = new();

        foreach (var i in input)
        {
            predictions.Add((double)i);
        }

        // Return index of highest value in the list
        for (int i = 0; i < predictions.Count; i++)
        {
            if (predictions[i] > prediction)
            {
                index = i;
                prediction = predictions[i];
            }
        }

        return index;
    }
}
class GameNeuralNetwork
{
    double featureMin, featureMax;
    int[] layerArray;
    List<List<double>>[] weights;

    public GameNeuralNetwork(int[] layers)
    {
        layerArray = layers;
        weights = new List<List<double>>[layerArray.Length - 1];

        Random rand = new(0);

        for (int i = 0; i < layerArray.Length - 1; i++)
        {
            List<List<double>> tempWW = new();
            if (i == (weights.Length - 1))
            {
                for (int a = 0; a < layerArray[i] + 1; a++)
                {
                    List<double> tempW = new();
                    for (int b = 0; b < layerArray[i + 1]; b++)
                    {
                        double w = (rand.NextDouble()) * (rand.Next(0, 2) * 2 - 1);
                        tempW.Add(w);
                    }
                    tempWW.Add(tempW);
                }
            }
            else
            {
                for (int x = 0; x < layerArray[i] + 1; x++)
                {
                    List<double> tempW = new();
                    for (int y = 0; y < layerArray[i + 1] + 1; y++)
                    {
                        double w = (rand.NextDouble()) * (rand.Next(0, 2) * 2 - 1);
                        tempW.Add(w);
                    }
                    tempWW.Add(tempW);
                }
            }
            weights[i] = tempWW;
        }
    }
    public double GetValue(String m)
    {
        if (m.Equals("MAX"))
        {
            return featureMax;
        }
        else
        {
            return featureMin;
        }
    }
    public void NetworkDetails()
    {
        String networkWeights = "";

        foreach (var weight in weights)
        {
            string networkWeight = "";
            foreach (var w in weight)
            {
                String tempWeight = "";
                foreach (var x in w)
                {
                    double d = Math.Round(x, 3);
                    tempWeight += d + "\t";
                }
                networkWeight += "[\t" + tempWeight + "\t],\n";
            }
            networkWeights += "\nInput Weights for Neural Layer: [\n" + networkWeight + "] ";
        }

        //Debug.Log("Model Weights: " + networkWeights);

    }
    private List<List<double>> Sigmoid(List<List<double>> input)
    {
        List<List<double>> output = new();

        for (int i = 0; i < input.Count; i++)
        {
            List<double> tempO = new();
            for (int j = 0; j < input[0].Count; j++)
            {
                tempO.Add(0.0);
            }
            output.Add(tempO);
        }

        for (int i = 0; i < input.Count; i++)
        {
            for (int j = 0; j < input[0].Count; j++)
            {
                output[i][j] = 1.0 / (1 + Math.Exp(-input[i][j]));
            }
        }

        return output;
    }
    public List<List<double>> Predict(List<List<double>> featureSet)
    {
        List<List<double>> predictions = featureSet;
        List<List<double>> dotProduct = new();

        for (int i = 0; i < weights.Length; i++)
        {
            dotProduct = DotProduct(predictions, weights[i]);
            predictions = Sigmoid(dotProduct);
        }

        return predictions;
    }
    private List<List<double>> DotProduct(List<List<double>> A, List<List<double>> B)
    {
        int rA = A.Count;
        int cA = A[0].Count;
        int rB = B.Count;
        int cB = B[0].Count;
        List<List<double>> P = new();

        if (cA != rB)
        {
            Debug.Log("Matrixes can't be multiplied!!");
            Debug.Log("A row: " + rA + " A column: " + cA);
            Debug.Log("B row: " + rB + " B column: " + cB);
        }
        else
        {
            for (int i = 0; i < rA; i++)
            {
                List<double> tempP = new();
                for (int j = 0; j < cB; j++)
                {
                    tempP.Add(0.0);
                }
                P.Add(tempP);
            }

            for (int i = 0; i < rA; i++)
            {
                for (int j = 0; j < cB; j++)
                {
                    double temp = 0;
                    for (int k = 0; k < cA; k++)
                    {
                        temp += A[i][k] * B[k][j];
                    }
                    P[i][j] = temp;
                }
            }
        }

        return P;
    }
    public void LoadModel(String path)
    {
        try
        {

            Debug.Log("TextFile location: " + Application.dataPath + "/Scripts/Model_4-32-32-3.txt");
            using StreamReader sr = File.OpenText(Application.dataPath + "/Scripts/" + path);

            string s = String.Empty;
            int i = 0, j = 0;

            while ((s = sr.ReadLine()) != null)
            {
                if (s.Split(' ')[0].Equals("layer"))
                {
                    int.TryParse(s.Split(' ')[1], out i);
                    j = 0;
                }
                else if (s.Split(' ')[0].Equals("max"))
                {
                    double.TryParse(s.Split(' ')[1], out featureMax);
                }
                else if (s.Split(' ')[0].Equals("min"))
                {
                    double.TryParse(s.Split(' ')[1], out featureMin);
                }
                else
                {
                    List<String> temp = s.Split(',').ToList();
                    weights[i][j] = temp.Select(x => double.Parse(x)).ToList();
                    j++;
                }
            }
        }
        catch (Exception)
        {
            Debug.Log("\n[ERROR] TXT File does not follow required content format!\n");
        }
    }
}