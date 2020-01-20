using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aaNodeLine : MonoBehaviour {

    public GameObject connectedNode;
   
    private Sprite mine;
    private Sprite his;

    public Object LineMaterialInit;
    public Object LineMaterialComp;


    // Use this for initialization
    void Start () {
        mine = transform.GetComponent<aaNode>().spriteArrived;
        his = connectedNode.GetComponent<aaNode>().spriteArrived;
        GetComponent<LineRenderer>().material = (Material)LineMaterialInit;

    }

    // Update is called once per frame
    void Update () {
        if (GetComponent<SpriteRenderer>().sprite == mine && connectedNode.GetComponent<SpriteRenderer>().sprite == his) {
            GetComponent<LineRenderer>().material = (Material)LineMaterialComp;

        }
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, connectedNode.transform.position);
    }
}
