using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallManTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void OnTriggerEnter2D(Collider2D ligand)
    {
        if (ligand.GetComponents<Ligand>().Length > 0)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
