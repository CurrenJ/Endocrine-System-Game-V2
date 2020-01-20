using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadAminoAcidTotals : MonoBehaviour {

    public int aminoYellow = 0;
    public int aminoBlue = 0;
    public int aminoPink = 0;
    public int aminoGreen = 0;
    public int aminoRed = 0;

    public GameObject aminoYellowText;
    public GameObject aminoBlueText;
    public GameObject aminoPinkText;
    public GameObject aminoGreenText;
    public GameObject aminoRedText;

    // Use this for initialization
    void Start () {
        loadState();
	}
	
	// Update is called once per frame
	void Update () {
        aminoYellowText.GetComponent<Text>().text = aminoYellow + "";
        aminoBlueText.GetComponent<Text>().text = aminoBlue + "";
        aminoPinkText.GetComponent<Text>().text = aminoPink + "";
        aminoGreenText.GetComponent<Text>().text = aminoGreen + "";
        aminoRedText.GetComponent<Text>().text = aminoRed + "";
    }

    public void OnDestroy()
    {
        saveState();
    }

    public void saveState()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            String andTotalPath = Application.persistentDataPath + "/Amino Acids/Totals/";
            String andPathYellow = andTotalPath + "yellow.bytes";
            String andPathBlue = andTotalPath + "blue.bytes";
            String andPathPink = andTotalPath + "pink.bytes";
            String andPathGreen = andTotalPath + "green.bytes";
            String andPathRed = andTotalPath + "red.bytes";


            if (!Directory.Exists(andTotalPath))
            {
                Directory.CreateDirectory(andTotalPath);
            }

            System.IO.File.WriteAllBytes(andPathYellow, BitConverter.GetBytes(aminoYellow));
            System.IO.File.WriteAllBytes(andPathBlue, BitConverter.GetBytes(aminoBlue));
            System.IO.File.WriteAllBytes(andPathPink, BitConverter.GetBytes(aminoPink));
            System.IO.File.WriteAllBytes(andPathGreen, BitConverter.GetBytes(aminoGreen));
            System.IO.File.WriteAllBytes(andPathRed, BitConverter.GetBytes(aminoRed));

        }
        else
        {
            String totalPath = Application.streamingAssetsPath + "/Amino Acids/Totals/";
            String pathYellow = totalPath + "yellow.bytes";
            String pathBlue = totalPath + "blue.bytes";
            String pathPink = totalPath + "pink.bytes";
            String pathGreen = totalPath + "green.bytes";
            String pathRed = totalPath + "red.bytes";

            if (!Directory.Exists(totalPath))
            {
                Directory.CreateDirectory(totalPath);
            }

            System.IO.File.WriteAllBytes(pathYellow, BitConverter.GetBytes(aminoYellow));
            System.IO.File.WriteAllBytes(pathBlue, BitConverter.GetBytes(aminoBlue));
            System.IO.File.WriteAllBytes(pathPink, BitConverter.GetBytes(aminoPink));
            System.IO.File.WriteAllBytes(pathGreen, BitConverter.GetBytes(aminoGreen));
            System.IO.File.WriteAllBytes(pathRed, BitConverter.GetBytes(aminoRed));
        }
    }

    public void loadState()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            String totalPath = Application.persistentDataPath + "/Amino Acids/Totals/";
            String pathYellow = totalPath + "yellow.bytes";
            String pathBlue = totalPath + "blue.bytes";
            String pathPink = totalPath + "pink.bytes";
            String pathGreen = totalPath + "green.bytes";
            String pathRed = totalPath + "red.bytes";

            try
            {
                aminoYellow = BitConverter.ToInt32(System.IO.File.ReadAllBytes(pathYellow), 0);
                aminoBlue = BitConverter.ToInt32(System.IO.File.ReadAllBytes(pathBlue), 0);
                aminoPink = BitConverter.ToInt32(System.IO.File.ReadAllBytes(pathPink), 0);
                aminoGreen = BitConverter.ToInt32(System.IO.File.ReadAllBytes(pathGreen), 0);
                aminoRed = BitConverter.ToInt32(System.IO.File.ReadAllBytes(pathRed), 0);
            }
            catch (Exception e)
            {
            }
        }
        else
        {
            String totalPath = Application.streamingAssetsPath + "/Amino Acids/Totals/";
            String pathYellow = totalPath + "yellow.bytes";
            String pathBlue = totalPath + "blue.bytes";
            String pathPink = totalPath + "pink.bytes";
            String pathGreen = totalPath + "green.bytes";
            String pathRed = totalPath + "red.bytes";
            try
            {
                aminoYellow = BitConverter.ToInt32(System.IO.File.ReadAllBytes(pathYellow), 0);
                aminoBlue = BitConverter.ToInt32(System.IO.File.ReadAllBytes(pathBlue), 0);
                aminoPink = BitConverter.ToInt32(System.IO.File.ReadAllBytes(pathPink), 0);
                aminoGreen = BitConverter.ToInt32(System.IO.File.ReadAllBytes(pathGreen), 0);
                aminoRed = BitConverter.ToInt32(System.IO.File.ReadAllBytes(pathRed), 0);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
    }
    }
