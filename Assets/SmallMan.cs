using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMan : MonoBehaviour {

    public float speedMultiplier;
    public float spinnyMultiplier;

    public Object particles;
    public Object levF;

    float r;
    float m;

    private float percentRotatePerSec; // 10% chance of man spawning every second
    private float percentMovePerSec; // 10% chance of man spawning every second


    // Use this for initialization
    void Start () {
        speedMultiplier = 1.0F;
        spinnyMultiplier = 1.0F;
        r = 1;
        m = 1;
        percentRotatePerSec = 0.6F;
        percentMovePerSec = 0.35F;
    }

    public void instM(float ran)
    {
        bigMove(ran);
    }

    public void instR(float ran) {
        startRotate(ran);
    }
	
	// Update is called once per frame
	void Update () {
            r = Random.Range(0F, 1F);
            if (r < (percentRotatePerSec * Time.deltaTime))
                rotate();
 
            m = Random.Range(0F, 1F);
            if (m < (percentMovePerSec * Time.deltaTime))
                move();        
    }

    private void OnDestroy()
    {
        Camera.main.GetComponent<FollowLigand>().light.GetComponent<Light>().range -= 60;
        if (Camera.main.GetComponent<FollowLigand>().light.GetComponent<Light>().range <= 200 + Camera.main.GetComponent<FollowLigand>().yInt) {
            GameObject levG = (GameObject)Instantiate(levF);
            levG.transform.SetParent(GameObject.FindGameObjectWithTag("Panel").transform, false);
            Destroy(Camera.main.GetComponent<FollowLigand>().ligand);
        }
        ((GameObject)Instantiate(particles)).transform.position = transform.position;
    }   

    private void move() {
       GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(speedMultiplier * 500 * Random.Range(0.5F, 1F), 0));
    }

    public void bigMove(float ran) {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(speedMultiplier * 500 * ran, Random.Range(0.4F, 0.9F) * 500 * speedMultiplier));
    }

    public void startRotate(float ran) {
        GetComponent<Rigidbody2D>().MoveRotation(ran);
    }

    private void rotate() {
        GetComponent<Rigidbody2D>().AddTorque(Random.Range(-1F, 1F) * spinnyMultiplier * 100);
    }
}
