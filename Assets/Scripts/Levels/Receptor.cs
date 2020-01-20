using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receptor : MonoBehaviour {

    public SpriteRenderer spriteRenderer;
    public Sprite unbound;
    public Sprite bound;



    public char receptorType;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = unbound;
    }
	
	// Update is called once per frame
	void Update () {
        if (Camera.main.GetComponent<FollowLigand>().ligand.GetComponent<Ligand>().ligandType == receptorType)
        {
            GetComponentInChildren<Light>().enabled = true;
            if(GetComponentInChildren<ParticleSystem>().isStopped)
                GetComponentInChildren<ParticleSystem>().Play();
        }
        else {
            GetComponentInChildren<Light>().enabled = false;
            GetComponentInChildren<ParticleSystem>().Stop();
        } 

    }

    void bindToLigand() {
        spriteRenderer.sprite = bound;
    }
}
