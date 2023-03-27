using UnityEngine;
using System.Collections;
using System.IO;

public class HighScores : MonoBehaviour
{

    public int[] scores = new int[10];

    string currentDirectory;

    public string scoreFileName = "highscores.txt";

    // Start is called before the first frame update
    void Start()
    {
        currentDirectory = Application.dataPath;
        Debug.Log("Our current directory is: " + currentDirectory);

        LoadScoresFromFile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScoresFromFile()
    {
        bool fileExists = File.Exists(currentDirectory + "\\" + scoreFileName);
        if(fileExists == true)
        {
            Debug.Log("Found high score file " + scoreFileName);
        }
        else
        {
            Debug.Log("The file " + scoreFileName + " does not exist. No scores will be loaded.", this);
            return;
        }

        scores = new int[scores.Length];

        StreamReader fileReader = new StreamReader(currentDirectory + "\\" + scoreFileName);

        int scoreCount = 0;

        while(fileReader.Peek() != 0 && scoreCount < scores.Length)
        {
            string fileLine = fileReader.ReadLine();

            int readScore = -1;

            bool didParse = int.TryParse(fileLine, out readScore);
            if (didParse)
            {
                scores[scoreCount] = readScore;
            }
            else
            {
                Debug.Log("Invalid line in scores file at " + scoreCount + ", using default value.", this);
                scores[scoreCount] = 0;
            }
            scoreCount++;
        }

        fileReader.Close();
        Debug.Log("High scores read from " + scoreFileName);
    }

    public void SaveScoresToFile()
    {
        // Create a StreamWriter for our file path. 
        StreamWriter fileWriter = new StreamWriter(currentDirectory + "\\" + scoreFileName);

        // Write the lines to the file 
        for (int i = 0; i < scores.Length; i++)
        {
            fileWriter.WriteLine(scores[i]);
        }

        // Close the stream 
        fileWriter.Close();

        // Write a log message. 
        Debug.Log("High scores written to " + scoreFileName);
    }

    public void AddScore(int newScore)
    {
         int desiredIndex = -1; 
        for (int i = 0; i < scores.Length; i++) 
        { 
            // Instead of checking the value of desiredIndex  
            // we could also use 'break' to stop the loop. 
            if (scores[i] > newScore || scores[i] == 0) 
            { 
                desiredIndex = i; 
                break; 
            } 
        } 
 
        // If no desired index was found then the score  
        // isn't high enough to get on the table, so we just  
        // abort. 
        if (desiredIndex < 0) 
        { 
            Debug.Log("Score of " + newScore +  " not high enough for high scores list.", this); 
            return; 
        } 
 
        // Then we move all of the scores after that index  
        // back by one position. We'll do this by looping from  
        // the back of the array to our desired index. 
        for (int i = scores.Length - 1; i > desiredIndex; i--) 
        { 
            scores[i] = scores[i - 1]; 
        } 
 
        // Insert our new score in its place 
        scores[desiredIndex] = newScore; 
        Debug.Log("Score of " + newScore +  " entered into high scores at position " + desiredIndex, this); 
    }
}
