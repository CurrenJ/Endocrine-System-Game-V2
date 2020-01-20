using UnityEngine;

public class aaNode : MonoBehaviour {


    public int freeMovementRadius;

    public float speedMultiplier;
    public int repetitionMax;
    public int repetitionMin;

    private Vector2 velocity;
    private int repetitions;

    private double startPosX;
    private double startPosY;
    private double limitX;
    private double negLimitX;
    private double limitY;
    private double negLimitY;

    public Sprite spriteArrived;

    // Use this for initialization
    void Start () {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        limitX = startPosX + freeMovementRadius;
        negLimitX = startPosX - freeMovementRadius;
        limitY = startPosY + freeMovementRadius;
        negLimitY = startPosY - freeMovementRadius;

        //Debug.Log(limitX + " : " + limitY);
       // Debug.Log(negLimitX + " ; " + negLimitY);

        repetitions = 0;
    }

    void moveABit()
    {
        //Debug.Log("Movin a bit");

        float s = Random.Range(0.0F, 1.0F);
        float r = Random.Range(0.0F, 2.0F * Mathf.PI);
        Vector2 v =  new Vector2(Mathf.Cos(r) * s * speedMultiplier, Mathf.Sin(r) * s * speedMultiplier);

        int reps = Random.Range(repetitionMin, repetitionMax);

        //Debug.Log(v.x * reps);

        if (transform.position.x + v.x * reps < limitX && transform.position.y + v.y * reps < limitY && transform.position.x + v.x * reps > negLimitX && transform.position.y + v.y * reps > negLimitY)
        {
            repetitions = reps;
            velocity = v;
        }
        else {
            //Debug.Log("FUCK");
        }

    }

    // Update is called once per frame
    void Update () {

        if (repetitions <= 0)
        {
            moveABit();
        }
        else {
            //Debug.Log("IM HERE " + repetitions + " : " + transform.position.x + velocity.x);

            transform.position = new Vector3(transform.position.x + velocity.x, transform.position.y + velocity.y, transform.position.z);
            repetitions--;
        }
	}
}
