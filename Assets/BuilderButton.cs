using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuilderButton : MonoBehaviour {

    public GameObject ButtonController;
    public UnityEngine.Object proteinBuild;
    public int buttonIndex = 0;

    public string[] reqs;

    public Sprite darkTile;
    public Sprite lightTile;

    [HideInInspector]
    public bool reqsMet;


    // Use this for initialization
    void Start () {
        reqsMet = true;
        foreach (String req in reqs)
        {
            if (!loadState(req))
                reqsMet = false;
        }
        if (!reqsMet) {
            GetComponent<Image>().sprite = darkTile;
            GetComponentInChildren<BuilderButtonLock>().lockButton();
        }
    }

    public void updateLock() {
        reqsMet = true;
        foreach (String req in reqs)
        {
            if (!loadState(req))
                reqsMet = false;
        }
        if (!reqsMet)
        {
            GetComponent<Image>().sprite = darkTile;
            GetComponentInChildren<BuilderButtonLock>().lockButton();
        }
        else {
            GetComponent<Image>().sprite = lightTile;
            GetComponentInChildren<BuilderButtonLock>().unlockButton();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void toggleButton() {
        ButtonController.GetComponent<BuilderButtonController>().toggleButton(buttonIndex);
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
}
