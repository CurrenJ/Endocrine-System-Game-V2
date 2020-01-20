using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SelectUpgradeButton : MonoBehaviour
{

    public GameObject ButtonController;
    public UnityEngine.Object proteinBuild;
    public int buttonIndex = 0;
    public bool selected;

    // Use this for initialization
    void Start()
    {
        if (!loadState(proteinBuild.name)) {
            Destroy(gameObject);
        }
        if (loadSelectedState(proteinBuild.name))
        {
            select();
            ButtonController.GetComponent<SelectUpgradeButtonController>().toggleButton(buttonIndex);
        }
        else deselect();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void toggleButton()
    {
        ButtonController.GetComponent<SelectUpgradeButtonController>().toggleButton(buttonIndex);
    }

    public void select() {
        selected = true;
        saveSelectedState();
        GetComponentInChildren<SelectUpgradeDot>().select();
    }

    public void deselect() {
        selected = false;
        saveSelectedState();
        GetComponentInChildren<SelectUpgradeDot>().deselect();
    }

    public bool loadState(String proteinName)
    {
        try
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                String realPath = Application.persistentDataPath + "/Protein Builds/" + proteinName + "(Clone)" + ".bytes";

                return Convert.ToBoolean(System.IO.File.ReadAllBytes(realPath)[0]);
            }
            else
            {
                String path = Application.streamingAssetsPath + "/Protein Builds/" + proteinName + "(Clone)" + ".bytes";
                return Convert.ToBoolean(System.IO.File.ReadAllBytes(path)[0]);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return false;
        }
    }

    public bool loadSelectedState(String proteinName)
    {
        try
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                String realPath = Application.persistentDataPath + "/Selected Upgrades/" + proteinName + ".bytes";

                return Convert.ToBoolean(System.IO.File.ReadAllBytes(realPath)[0]);
            }
            else
            {
                String path = Application.streamingAssetsPath + "/Selected Upgrades/" + proteinName + ".bytes";
                return Convert.ToBoolean(System.IO.File.ReadAllBytes(path)[0]);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return false;
        }
    }

    public void saveSelectedState()
    {
            if (Application.platform == RuntimePlatform.Android)
            {

                String andLevelPath = Application.persistentDataPath + "/Selected Upgrades/";
                String andPath = andLevelPath + proteinBuild.name + ".bytes";

                if (!Directory.Exists(andLevelPath))
                {
                    Directory.CreateDirectory(andLevelPath);
                }

                bool[] a = new bool[] { selected };
                byte[] b = (from x in a select x ? (byte)0x1 : (byte)0x0).ToArray();

                System.IO.File.WriteAllBytes(andPath, b);

            }
            else
            {
                String levelPath = Application.streamingAssetsPath + "/Selected Upgrades/";
                String path = levelPath + proteinBuild.name + ".bytes";

                if (!Directory.Exists(levelPath))
                {
                    Directory.CreateDirectory(levelPath);
                }


                bool[] a = new bool[] { selected };
                byte[] b = (from x in a select x ? (byte)0x1 : (byte)0x0).ToArray();

                System.IO.File.WriteAllBytes(path, b);
            }
    }
}
