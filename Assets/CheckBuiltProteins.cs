using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBuiltProteins : MonoBehaviour {

    public String[] reqs;

    private bool reqsMet;

    // Use this for initialization
    void Start() {
        reqsMet = true;
        foreach (String req in reqs) {
            if (!loadState(req))
                reqsMet = false;
        }

        GameObject child = GetComponentInChildren<LockedUnlocked>().gameObject;
                if (reqsMet)
                    child.GetComponent<LockedUnlocked>().unlockButton();
                else child.GetComponent<LockedUnlocked>().lockButton();

        

    }

    // Update is called once per frame
    void Update () {
	
	}

    public void clickButton() {
        if(reqsMet)
            GetComponent<LoadScene>().loadScene();
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
            return false;
        }
    }
}
