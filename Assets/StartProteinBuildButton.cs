using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class StartProteinBuildButton : MonoBehaviour {
    
    public GameObject AminoAcidBuilderPrefab;
    public bool inProgress;
    private int curIndex;
    private GameObject proteinBuild;

	// Use this for initialization
	void Start () {
        inProgress = false;
        curIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    GameObject getProteinBuild() {
        return GameObject.FindGameObjectWithTag("ProteinBuild");
    }

    public bool checkAminos() {
        int yellowNeeded = 0;
        int blueNeeded = 0;
        int pinkNeeded = 0;
        int greenNeeded = 0;
        int redNeeded = 0;

        GameObject build = getProteinBuild();
        AAColors hud = GameObject.FindGameObjectWithTag("Panel").GetComponent<AAColors>();
        foreach (Transform node in build.GetComponentsInChildren<Transform>()) {
            try
            {
                if (node.GetComponent<aaNode>().spriteArrived == hud.YellowAminoAcid)
                    yellowNeeded++;
                else if (node.GetComponent<aaNode>().spriteArrived == hud.BlueAminoAcid)
                    blueNeeded++;
                else if (node.GetComponent<aaNode>().spriteArrived == hud.PinkAminoAcid)
                    pinkNeeded++;
                else if (node.GetComponent<aaNode>().spriteArrived == hud.GreenAminoAcid)
                    greenNeeded++;
                else if (node.GetComponent<aaNode>().spriteArrived == hud.RedAminoAcid)
                    redNeeded++;
            }
            catch (Exception e) { }
        }
        if (Camera.main.GetComponent<LoadAminoAcidTotals>().aminoYellow < yellowNeeded || Camera.main.GetComponent<LoadAminoAcidTotals>().aminoBlue < blueNeeded || Camera.main.GetComponent<LoadAminoAcidTotals>().aminoPink < pinkNeeded || Camera.main.GetComponent<LoadAminoAcidTotals>().aminoGreen < greenNeeded || Camera.main.GetComponent<LoadAminoAcidTotals>().aminoRed < redNeeded)
        {
            return false;
        }
        else return true;
    }

    public void startBuild()
    {
        Debug.Log("NOTICE ME");
        Debug.Log("REQS MET: " + updateLocks().reqsMet);
        if (!inProgress && checkAminos() && !loadState(getProteinBuild()) && updateLocks().reqsMet)
        {
            proteinBuild = getProteinBuild();
            inProgress = true;
            curIndex = 0;
            progressBuild();
        }
    }

    public BuilderButton updateLocks() {
        //returns builder button used for current template
        BuilderButton buttonForThisBuild = null;
        Debug.Log(GameObject.FindGameObjectsWithTag("BuildTemplateButton").Length);
        foreach (BuilderButton button in GameObject.FindGameObjectWithTag("Canvas").GetComponentsInChildren<BuilderButton>())
        {
            Debug.Log("Button's Build Name: " + button.proteinBuild.name);
            Debug.Log("Build Instance's Name: " + getProteinBuild().name);
            button.updateLock();
            if (button.proteinBuild.name + "(Clone)" == getProteinBuild().name)
                buttonForThisBuild = button;
        }
        return buttonForThisBuild;
    }

    public void arrived() {
        proteinBuild.GetComponentsInChildren<Transform>()[curIndex].GetComponent<SpriteRenderer>().sprite = proteinBuild.GetComponentsInChildren<Transform>()[curIndex].GetComponent<aaNode>().spriteArrived;
        progressBuild();
    }

    public void shiftStartingPoint(GameObject aa) {
        Sprite aaSprite = aa.GetComponent<SpriteRenderer>().sprite;
        AAColors hud = GameObject.FindGameObjectWithTag("Panel").GetComponent<AAColors>();
        if (aaSprite == hud.YellowAminoAcid)
        {
            Camera.main.GetComponent<LoadAminoAcidTotals>().aminoYellow--;
            return;
        }
        else if (aaSprite == hud.BlueAminoAcid)
        {
            Camera.main.GetComponent<LoadAminoAcidTotals>().aminoBlue--;
            aa.transform.position = new Vector3(aa.transform.position.x, aa.transform.position.y - 15, aa.transform.position.z);
        }
        else if (aaSprite == hud.PinkAminoAcid)
        {
            Camera.main.GetComponent<LoadAminoAcidTotals>().aminoPink--;
            aa.transform.position = new Vector3(aa.transform.position.x, aa.transform.position.y - 15 * 2, aa.transform.position.z);
        }
        else if (aaSprite == hud.GreenAminoAcid)
        {
            Camera.main.GetComponent<LoadAminoAcidTotals>().aminoGreen--;
            aa.transform.position = new Vector3(aa.transform.position.x, aa.transform.position.y - 15 * 3, aa.transform.position.z);
        }
        else if (aaSprite == hud.RedAminoAcid)
        {
            Camera.main.GetComponent<LoadAminoAcidTotals>().aminoRed--;
            aa.transform.position = new Vector3(aa.transform.position.x, aa.transform.position.y - 15 * 4, aa.transform.position.z);
        }
    }

    public void progressBuild() {
        if (inProgress)
        {
            //i is 1 because the getComponenet includes the parent
            for (int i = 1; i < proteinBuild.GetComponentsInChildren<Transform>().Length; i++)
            {
                Debug.Log("Made it to point 1");
                if (i == curIndex+1)
                {
                    Debug.Log("Made it to point 2");

                    Transform node = proteinBuild.GetComponentsInChildren<Transform>()[i];
                    if (node.gameObject.GetInstanceID() != proteinBuild.gameObject.GetInstanceID())
                    {
                        Debug.Log("boop");
                        GameObject aa = Instantiate(AminoAcidBuilderPrefab);
                        aa.transform.SetParent(GameObject.FindGameObjectWithTag("Panel").transform);
                        aa.transform.localPosition = aa.transform.position;

                        aa.GetComponent<PlaceAmino>().destination = node.gameObject;
                        aa.GetComponent<SpriteRenderer>().sprite = aa.GetComponent<PlaceAmino>().destination.GetComponent<aaNode>().spriteArrived;
                        aa.GetComponent<PlaceAmino>().setParticleColor();
                        shiftStartingPoint(aa);
                    }
                }

            }
        }
        curIndex++;
        if (curIndex > proteinBuild.GetComponentsInChildren<Transform>().Length - 1) {
            inProgress = false;
            curIndex = 0;
            saveState();
            updateLocks();
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
