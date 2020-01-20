using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class LoadLigandSequence : MonoBehaviour {

    public char[] ligOrder;
    public int finalLigIndex;
    public UnityEngine.Object LevelDone;

    // Use this for initialization
    void Start () {
        int ind = 0;
        ligOrder = new char[100];
        // Handle any problems that might arise when reading the text
        try
        {
            // Create a new StreamReader, tell it which file to read and what encoding the file
            // was saved as
            Debug.Log(Application.streamingAssetsPath);


            if (Application.platform == RuntimePlatform.Android)
            {
                String path = Application.streamingAssetsPath + "/Ligand Sequences/" + SceneManager.GetActiveScene().name + ".txt";
                WWW www = new WWW(path);
                while (!www.isDone) { }
                String contents = www.text;
                string[] lines = contents.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                //string bigL = "";
                for (int i = 0; i < lines.Length; i++)
                {
                    //bigL += lines[i] + " ";
                    ligOrder[i] = lines[i].ToCharArray()[0];
                }
                finalLigIndex = lines.Length;
               // GameObject.FindGameObjectWithTag("instrucText").GetComponent<TextMesh>().text = bigL;
            }
            else
            {
                String path = Application.streamingAssetsPath + "/Ligand Sequences/" + SceneManager.GetActiveScene().name + ".txt";
                string[] lines = File.ReadAllLines(path);

                //string bigL = "";
                for (int i = 0; i < lines.Length; i++)
                {
                    //bigL += lines[i] + " ";
                    ligOrder[i] = char.Parse(lines[i]);
                }
                finalLigIndex = lines.Length;
                // GameObject.FindGameObjectWithTag("instrucText").GetComponent<TextMesh>().text = bigL;

            }

            //GameObject.FindGameObjectWithTag("instrucText").GetComponent<TextMesh>().text = "";

            //Debug.Log(line);
            ind++;
           
        }
        // If anything broke in the try block, we throw an exception with information
        // on what didn't work
        catch (Exception e)
        {
           GameObject.FindGameObjectWithTag("instrucText").GetComponent<TextMesh>().text = e.Message;
            Console.WriteLine("{0}\n", e.Message);
            //return false;
        }

    }


    // Update is called once per frame
    void Update () {
		
	}
}
