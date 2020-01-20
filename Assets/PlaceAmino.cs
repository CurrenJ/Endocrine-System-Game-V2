using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceAmino : MonoBehaviour {

    public PID pid;
    public GameObject destination;
    public double acceptableRange;
    private Object aminoParticlesPrefab;
    public GameObject buildButton;


    private bool arrived;

	// Use this for initialization
	void Start () {
        buildButton = GameObject.FindGameObjectWithTag("BuildButtonTag");
        arrived = false;
	}

    public void setParticleColor() {
        aminoParticlesPrefab = GameObject.FindGameObjectWithTag("Panel").GetComponent<AAColors>().AminoParticlesBlue;

        Sprite aaSprite = GetComponent<SpriteRenderer>().sprite;
        AAColors hud = GameObject.FindGameObjectWithTag("Panel").GetComponent<AAColors>();
        if (aaSprite == hud.YellowAminoAcid)
            aminoParticlesPrefab = GameObject.FindGameObjectWithTag("Panel").GetComponent<AAColors>().AminoParticlesYellow;
        else if (aaSprite == hud.BlueAminoAcid)
            aminoParticlesPrefab = GameObject.FindGameObjectWithTag("Panel").GetComponent<AAColors>().AminoParticlesBlue;
        else if (aaSprite == hud.PinkAminoAcid)
            aminoParticlesPrefab = GameObject.FindGameObjectWithTag("Panel").GetComponent<AAColors>().AminoParticlesPink;
    }

    // Update is called once per frame
    void Update () {
        transform.Translate(pid.Update(destination.transform.position.x, transform.position.x, Time.deltaTime), pid.Update(destination.transform.position.y, transform.position.y, Time.deltaTime), 0);
        if (!arrived && Mathf.Abs(destination.transform.position.x - transform.position.x) < acceptableRange && Mathf.Abs(destination.transform.position.y - transform.position.y) < acceptableRange) {
            buildButton.GetComponent<StartProteinBuildButton>().arrived();
            Destroy(gameObject);
            arrived = true;
        }
    }

    void OnDestroy()
    {
        var obj = (GameObject) Instantiate(aminoParticlesPrefab);
        obj.transform.position = transform.position;
    }
}
