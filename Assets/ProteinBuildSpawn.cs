using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProteinBuildSpawn : MonoBehaviour {

    public Color redTextColor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void spawnProteinBuild(UnityEngine.Object proteinBuild) {
        Destroy(GameObject.FindGameObjectWithTag("ProteinBuild"));
        GameObject pB = (GameObject)Instantiate(proteinBuild);
        pB.transform.SetParent(transform);
        pB.transform.localPosition = new Vector3(((GameObject)proteinBuild).transform.position.x, ((GameObject)proteinBuild).transform.position.y, 0);
        try
        {
            checkAminos(pB);
        }
        catch (Exception e) { };
        if (loadState(pB))
            completeBuild(pB);

        if (GameObject.FindGameObjectWithTag("UpgradeText") != null)
        {
            String upgText = "";
            if (pB.name == "Insulin" + "(Clone)")
            {
                upgText = "+0.1 Speed";
            }
            else if (pB.name == "Oxytocin" + "(Clone)")
            {
                upgText = "+0.1 Speed" + "\n" + "+0.1 Rotation";
            }
            else if (pB.name == "Relaxin" + "(Clone)")
            {
                upgText = "+0.2 Speed" + "\n" + "-0.1 Rotation";
            }
            else if (pB.name == "Leptin" + "(Clone)")
            {
                upgText = "x1.1 FOV";
            }
            else if (pB.name == "Orexin" + "(Clone)")
            {
                upgText = "+0.3 Speed";
            }
            GameObject.FindGameObjectWithTag("UpgradeText").GetComponent<Text>().text = upgText;
        }
    }



    void completeBuild(GameObject build)
    {
        Debug.Log("Completing Build");
        foreach (aaNode node in build.GetComponentsInChildren<aaNode>())
        {
            node.gameObject.GetComponent<SpriteRenderer>().sprite = node.spriteArrived;
            node.gameObject.GetComponent<LineRenderer>().material = (Material)node.gameObject.GetComponent<aaNodeLine>().LineMaterialComp;
        }

        try
        {
            GameObject.FindGameObjectWithTag("ReqsYellow").GetComponentInParent<Image>().enabled = false;
            GameObject.FindGameObjectWithTag("ReqsBlue").GetComponentInParent<Image>().enabled = false;
            GameObject.FindGameObjectWithTag("ReqsPink").GetComponentInParent<Image>().enabled = false;
            GameObject.FindGameObjectWithTag("ReqsGreen").GetComponentInParent<Image>().enabled = false;
            GameObject.FindGameObjectWithTag("ReqsRed").GetComponentInParent<Image>().enabled = false;

            GameObject.FindGameObjectWithTag("ReqsYellow").GetComponent<Text>().enabled = false;
            GameObject.FindGameObjectWithTag("ReqsBlue").GetComponent<Text>().enabled = false;
            GameObject.FindGameObjectWithTag("ReqsPink").GetComponent<Text>().enabled = false;
            GameObject.FindGameObjectWithTag("ReqsGreen").GetComponent<Text>().enabled = false;
            GameObject.FindGameObjectWithTag("ReqsRed").GetComponent<Text>().enabled = false;
        }
        catch (Exception e) { }
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

    public bool checkAminos(GameObject build)
    {
        int yellowNeeded = 0;
        int blueNeeded = 0;
        int pinkNeeded = 0;
        int greenNeeded = 0;
        int redNeeded = 0;

        GameObject.FindGameObjectWithTag("ReqsYellow").GetComponentInParent<Image>().enabled = true;
        GameObject.FindGameObjectWithTag("ReqsBlue").GetComponentInParent<Image>().enabled = true;
        GameObject.FindGameObjectWithTag("ReqsPink").GetComponentInParent<Image>().enabled = true;
        GameObject.FindGameObjectWithTag("ReqsGreen").GetComponentInParent<Image>().enabled = true;
        GameObject.FindGameObjectWithTag("ReqsRed").GetComponentInParent<Image>().enabled = true;

        GameObject.FindGameObjectWithTag("ReqsYellow").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("ReqsBlue").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("ReqsPink").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("ReqsGreen").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("ReqsRed").GetComponent<Text>().enabled = true;

        AAColors hud = GameObject.FindGameObjectWithTag("Panel").GetComponent<AAColors>();
        foreach (Transform node in build.GetComponentsInChildren<Transform>())
        {
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


        bool reqsMet = true;

        GameObject.FindGameObjectWithTag("ReqsYellow").GetComponent<Text>().text = yellowNeeded + "";
        if (Camera.main.GetComponent<LoadAminoAcidTotals>().aminoYellow < yellowNeeded)
        {
            reqsMet = false;
            GameObject.FindGameObjectWithTag("ReqsYellow").GetComponent<Text>().color = redTextColor;
        }
        else GameObject.FindGameObjectWithTag("ReqsYellow").GetComponent<Text>().color = Color.white;

        GameObject.FindGameObjectWithTag("ReqsBlue").GetComponent<Text>().text = blueNeeded + "";
        if (Camera.main.GetComponent<LoadAminoAcidTotals>().aminoBlue < blueNeeded)
        {
            reqsMet = false;
            GameObject.FindGameObjectWithTag("ReqsBlue").GetComponent<Text>().color = redTextColor;
        }
        else GameObject.FindGameObjectWithTag("ReqsBlue").GetComponent<Text>().color = Color.white;

        GameObject.FindGameObjectWithTag("ReqsPink").GetComponent<Text>().text = pinkNeeded + "";
        if (Camera.main.GetComponent<LoadAminoAcidTotals>().aminoPink < pinkNeeded)
        {
            reqsMet = false;
            GameObject.FindGameObjectWithTag("ReqsPink").GetComponent<Text>().color = redTextColor;
        }
        else GameObject.FindGameObjectWithTag("ReqsPink").GetComponent<Text>().color = Color.white;

        GameObject.FindGameObjectWithTag("ReqsGreen").GetComponent<Text>().text = greenNeeded + "";
        if (Camera.main.GetComponent<LoadAminoAcidTotals>().aminoGreen < greenNeeded)
        {
            reqsMet = false;
            GameObject.FindGameObjectWithTag("ReqsGreen").GetComponent<Text>().color = redTextColor;
        }
        else GameObject.FindGameObjectWithTag("ReqsGreen").GetComponent<Text>().color = Color.white;

        GameObject.FindGameObjectWithTag("ReqsRed").GetComponent<Text>().text = redNeeded + "";
        if (Camera.main.GetComponent<LoadAminoAcidTotals>().aminoRed < redNeeded)
        {
            reqsMet = false;
            GameObject.FindGameObjectWithTag("ReqsRed").GetComponent<Text>().color = redTextColor;
        }
        else GameObject.FindGameObjectWithTag("ReqsRed").GetComponent<Text>().color = Color.white;

        return reqsMet;
    }
}
