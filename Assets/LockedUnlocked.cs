using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockedUnlocked : MonoBehaviour {

    public Sprite locked;
    public Sprite unlocked;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void unlockButton(){
        GetComponent<Image>().overrideSprite = unlocked;
    }

    public void lockButton() {
        GetComponent<Image>().overrideSprite = locked;
    }
}
