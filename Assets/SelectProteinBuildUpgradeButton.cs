using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SelectProteinBuildUpgradeButton : MonoBehaviour
{
   
    private GameObject proteinBuild;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    GameObject getProteinBuild()
    {
        return GameObject.FindGameObjectWithTag("ProteinBuild");
    }

    public void selectBuild()
    {
        GameObject.FindGameObjectWithTag("SelectController").GetComponent<SelectUpgradeButtonController>().selectBuild(getProteinBuild());
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
}
