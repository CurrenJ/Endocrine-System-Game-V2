using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter : MonoBehaviour {

    private GameObject li;
    float r;
    public Object smallManPrefab;

    private float percentSpawnPerSec = 0.15F; // 2.5% chance of man spawning every second

    // Use this for initialization
    void Start () {
        r = 1;
        li = GameObject.FindGameObjectWithTag("PlayerLight");
    }

    // Update is called once per frame
    void Update()
    {
        int liBase = Camera.main.GetComponent<FollowLigand>().goalLiLevel + Camera.main.GetComponent<FollowLigand>().yInt;

        if (li.GetComponent<Light>().range < liBase)
        {
                r = Random.Range(0F, 1F);
                if (r < (percentSpawnPerSec * Time.deltaTime))
                    spawn();
            //Debug.Log(r);
        }
        
    }

    private void spawn() {
        Debug.Log("spawn");
        int num = Random.Range(1, 3);

        GameObject gam;
        for (int i = 0; i < num; i++) {
            gam = (GameObject)Instantiate(smallManPrefab);
            gam.GetComponent<SmallMan>().instR(Random.Range(0, 360));
            gam.GetComponent<SmallMan>().instM(Random.Range(0.4F, 0.9F));
            gam.transform.position = gameObject.GetComponentsInChildren<Transform>()[1].transform.position;

        }

    }
}
