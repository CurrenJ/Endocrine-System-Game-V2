using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleMenuElement : MonoBehaviour {

    private float baseWidth = 1440.0F;
    private float baseHeight = 2560.0F;

	// Use this for initialization
	void Start () {
        float scale = Screen.width / baseWidth;
        //Debug.Log(Screen.width + " / " + baseWidth + " = " + scale);
        transform.localScale = new Vector3(transform.localScale.x * scale, transform.localScale.y * scale, transform.localScale.z);
	}
	
	// Update is called once per frame
	void Update () {
       
    }
}
