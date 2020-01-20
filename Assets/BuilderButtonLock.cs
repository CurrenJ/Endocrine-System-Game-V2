using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuilderButtonLock : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void lockButton() {
        GetComponent<Image>().enabled = true;
    }

    public void unlockButton() {
        GetComponent<Image>().enabled = false;
    }

}
