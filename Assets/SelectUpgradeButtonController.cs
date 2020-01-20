using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SelectUpgradeButtonController : MonoBehaviour
{

    public GameObject button0;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public GameObject button6;
    public GameObject button7;
    public GameObject button8;
    public GameObject button9;

    public Color redTextColor;

    private GameObject[] buttons;


    // Use this for initialization
    void Start()
    {
        buttons = new GameObject[10];
        buttons[0] = button0;
        buttons[1] = button1;
        buttons[2] = button2;
        buttons[3] = button3;
        buttons[4] = button4;
        buttons[5] = button5;
        buttons[6] = button6;
        buttons[7] = button7;
        buttons[8] = button8;
        buttons[9] = button9;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void toggleButton(int buttonIndex)
    {
            saveState();
            GameObject.FindGameObjectWithTag("ProteinBuildArea").GetComponent<ProteinBuildSpawn>().spawnProteinBuild(buttons[buttonIndex].GetComponent<SelectUpgradeButton>().proteinBuild);
        
    }

    public void selectBuild(GameObject build) {
        foreach (GameObject button in buttons)
        {
            try
            {
                if (button.GetComponent<SelectUpgradeButton>().proteinBuild.name + "(Clone)" != build.name)
                    button.GetComponent<SelectUpgradeButton>().deselect();
                else button.GetComponent<SelectUpgradeButton>().select();
            }
            catch (Exception e) { };
        }
    }

    public void saveState()
    {
        GameObject build = GameObject.FindGameObjectWithTag("ProteinBuild");
        if (build != null)
        {
            if (Application.platform == RuntimePlatform.Android)
            {

                String andLevelPath = Application.persistentDataPath + "/Protein Builds/";
                String andPath = andLevelPath + build.name + ".bytes";

                if (!Directory.Exists(andLevelPath))
                {
                    Directory.CreateDirectory(andLevelPath);
                }

                bool enabled = build.GetComponentInChildren<aaNode>().gameObject.GetComponent<SpriteRenderer>().sprite == build.GetComponentInChildren<aaNode>().spriteArrived;

                bool[] a = new bool[] { enabled };
                byte[] b = (from x in a select x ? (byte)0x1 : (byte)0x0).ToArray();

                System.IO.File.WriteAllBytes(andPath, b);

            }
            else
            {
                String levelPath = Application.streamingAssetsPath + "/Protein Builds/";
                String path = levelPath + build.name + ".bytes";

                if (!Directory.Exists(levelPath))
                {
                    Directory.CreateDirectory(levelPath);
                }

                bool enabled = build.GetComponentInChildren<aaNode>().gameObject.GetComponent<SpriteRenderer>().sprite == build.GetComponentInChildren<aaNode>().spriteArrived;

                Debug.Log("Saving state: " + enabled + " on " + build.name);

                bool[] a = new bool[] { enabled };
                byte[] b = (from x in a select x ? (byte)0x1 : (byte)0x0).ToArray();

                System.IO.File.WriteAllBytes(path, b);
            }
        }
    }

    public bool loadState(GameObject build)
    {
        try
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                String realPath = Application.persistentDataPath + "/Protein Builds/" + build.name + ".bytes";

                return Convert.ToBoolean(System.IO.File.ReadAllBytes(realPath)[0]);
            }
            else
            {
                String path = Application.streamingAssetsPath + "/Protein Builds/" + build.name + ".bytes";
                return Convert.ToBoolean(System.IO.File.ReadAllBytes(path)[0]);
            }
        }
        catch (Exception e)
        {
            return false;
        }
    }


}
