using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Ligand : MonoBehaviour
{
    public float rotationSpeed = 1F;
    public float movementSpeed = 1F;
    public char ligandType;
    public int ligIndex;
    public bool advancedMovement = true;
    public bool advancedRotation = true;

    private void Start()
    {
        //Debug.Log(ligIndex);
    }

    public void applyEffects() {
        Debug.Log(Camera.main.GetComponent<FollowLigand>().speedMultiplierUpgrade);
        rotationSpeed *= Camera.main.GetComponent<FollowLigand>().rotationMultiplierUpgrade;
        rotationSpeed += Camera.main.GetComponent<FollowLigand>().rotationAdditionUpgrade;

        movementSpeed *= Camera.main.GetComponent<FollowLigand>().speedMultiplierUpgrade;
        movementSpeed += Camera.main.GetComponent<FollowLigand>().speedAdditionUpgrade;

        Debug.Log(rotationSpeed + " | " + movementSpeed);
    }

    void Update()
    {
            //create rotation
            //Quaternion wantedRotation = Quaternion.LookRotation(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0) - player.position);
            //Quaternion wantedRotation = Quaternion.LookRotation(Input.GetTouch(0).position - new Vector2(player.position.x, player.position.y));
            //Debug.Log(SceneManager.GetActiveScene().name);
            if (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary))
            {
                Vector3 mouse_pos;
                Vector3 object_pos;

                // Debug.Log("OG Pos: " + transform.position);
                // Transform newOrigin = transform;
                //newOrigin.position += new Vector3(GetComponent<Renderer>().bounds.size.x / 2, GetComponent<Renderer>().bounds.size.y / 2, GetComponent<Renderer>().bounds.size.z);
                //newOrigin.loca
                //Debug.Log("New Pos: " + newOrigin.position);
                //Debug.Log("OG Pos After: " + transform.position);

                //GameObject newO = new GameObject();
                //newO.transform.localPosition = transform.localPosition;
                // newO.transform.localRotation = transform.localRotation;
                // newO.transform.localScale = transform.localScale;
                //newO.transform.position = transform.position;
                //newO.transform.rotation = transform.rotation;
                //newO.transform.localScale = transform.localScale;

                //newO.transform.Translate(new Vector3(GetComponent<Renderer>().bounds.size.x / 2, GetComponent<Renderer>().bounds.size.y / 2, 0));
                //Debug.Log("Position" + transform.position);

                mouse_pos = Input.GetTouch(0).position;
                mouse_pos.z = 1; //The distance between the camera and object
                object_pos = Camera.main.WorldToScreenPoint(transform.position);
                mouse_pos.x = mouse_pos.x - object_pos.x;
                mouse_pos.y = mouse_pos.y - object_pos.y;
                float degree = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
                Quaternion angle = Quaternion.Euler(0, 0, degree - 90);

            // Debug.Log("NE" + (Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg));

            //transform.RotateAround(newO.position, transform.forward, (Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg * Time.deltaTime - 90));
            //Debug.Log("BOI" + newO.transform.position);

            if (!advancedRotation)
                transform.rotation = Quaternion.Lerp(transform.rotation, angle, ((rotationSpeed - 1) / 2 + 1));


            //then rotate
            if (advancedMovement || advancedRotation)
            {
                if (degree < 0)
                {
                    degree = 360 - degree * -1;
                }
                degree -= 90;
                if (degree < 0)
                    degree = 360 - degree * -1;
            }

            if (advancedRotation)
            {
                float eul = transform.eulerAngles.z;
                if (eul < degree)
                    eul += 360;

                float reld = eul - degree;
                float dif = transform.eulerAngles.z - degree;
                if (reld < 180)
                    GetComponent<Rigidbody2D>().AddTorque(((2 * Mathf.Pow((reld / 180), 2)) + 0.02F) * -5000 * ((rotationSpeed-1) / 4 + 1) * Time.deltaTime * 100);
                else GetComponent<Rigidbody2D>().AddTorque(((2 * Mathf.Pow(Mathf.Abs(180 - (reld - 180)) / 180, 2)) + 0.02F) * 5000 * ((rotationSpeed - 1) / 2 + 1) * Time.deltaTime * 100);
            }



            //Debug.Log("DEG " + (degree) + " EUL " + eul + " RELD " + reld);

            //Vector3 oldRot = transform.rotation.eulerAngles; transform.rotation = Quaternion.Euler(0, 0, oldRot.z);
            if (advancedMovement)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos((degree + 90) * Mathf.Deg2Rad), Mathf.Sin((degree + 90) * Mathf.Deg2Rad)) * ((movementSpeed-1) / 4 + 1) * 100 * Time.deltaTime * 100 * 0.8F);
            }
            else transform.position += transform.up * Time.deltaTime * movementSpeed * 100 * 0.8F;

        }
            else if (Input.GetMouseButton(0))
            {
                Vector3 mouse_pos;
                Vector3 object_pos;

                // Debug.Log("OG Pos: " + transform.position);
                // Transform newOrigin = transform;
                //newOrigin.position += new Vector3(GetComponent<Renderer>().bounds.size.x / 2, GetComponent<Renderer>().bounds.size.y / 2, GetComponent<Renderer>().bounds.size.z);
                //newOrigin.loca
                //Debug.Log("New Pos: " + newOrigin.position);
                //Debug.Log("OG Pos After: " + transform.position);

                //GameObject newO = new GameObject();
                //newO.transform.localPosition = transform.localPosition;
                // newO.transform.localRotation = transform.localRotation;
                // newO.transform.localScale = transform.localScale;
                //newO.transform.position = transform.position;
                //newO.transform.rotation = transform.rotation;
                //newO.transform.localScale = transform.localScale;

                //newO.transform.Translate(new Vector3(GetComponent<Renderer>().bounds.size.x / 2, GetComponent<Renderer>().bounds.size.y / 2, 0));
                //Debug.Log("Position" + transform.position);

                mouse_pos = Input.mousePosition;
                mouse_pos.z = 1; //The distance between the camera and object
                object_pos = Camera.main.WorldToScreenPoint(transform.position);
                mouse_pos.x = mouse_pos.x - object_pos.x;
                mouse_pos.y = mouse_pos.y - object_pos.y;
                float degree = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
                Quaternion angle = Quaternion.Euler(0, 0, degree - 90);

            // Debug.Log("NE" + (Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg));

            //transform.RotateAround(newO.position, transform.forward, (Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg * Time.deltaTime - 90));
            //Debug.Log("BOI" + newO.transform.position);

            if(!advancedRotation)
                transform.rotation = Quaternion.Lerp(transform.rotation, angle, ((rotationSpeed - 1) / 2 + 1));


            //then rotate
            if (advancedMovement || advancedRotation)
            {
                if (degree < 0)
                {
                    degree = 360 - degree * -1;
                }
                degree -= 90;
                if (degree < 0)
                    degree = 360 - degree * -1;
            }

            if (advancedRotation)
            {
                float eul = transform.eulerAngles.z;
                if (eul < degree)
                    eul += 360;

                float reld = eul - degree;
                float dif = transform.eulerAngles.z - degree;
                if (reld < 180)
                    GetComponent<Rigidbody2D>().AddTorque(((2 * Mathf.Pow((reld / 180), 2)) + 0.02F) * -5000 * ((rotationSpeed - 1) / 2 + 1) * Time.deltaTime * 100);
                else GetComponent<Rigidbody2D>().AddTorque(((2 * Mathf.Pow(Mathf.Abs(180 - (reld - 180)) / 180, 2)) + 0.02F) * 5000 * ((rotationSpeed - 1) / 2 + 1) * Time.deltaTime * 100);
            }



            //Debug.Log("DEG " + (degree) + " EUL " + eul + " RELD " + reld);

            //Vector3 oldRot = transform.rotation.eulerAngles; transform.rotation = Quaternion.Euler(0, 0, oldRot.z);
            if (advancedMovement)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos((degree + 90) * Mathf.Deg2Rad), Mathf.Sin((degree + 90) * Mathf.Deg2Rad)) * ((movementSpeed - 1) / 4 + 1) * 100 * Time.deltaTime * 100 * 0.8F);
            }
            else transform.position += transform.up * Time.deltaTime * movementSpeed * 100 * 0.8F;

        }
    }

    void bindToReceptor() {
        GameObject gO = new GameObject();
        //gO.transform.SetParent(Camera.main.GetComponent<FollowLigand>().light.transform.parent.parent);
        //gO.transform.position = Camera.main.GetComponent<FollowLigand>().light.transform.position;
        //Component c = Camera.main.GetComponent<FollowLigand>().light.GetComponent<Light>();
        //gO.AddComponent<Light>();
        //gO.GetComponent<Light>().range = Camera.main.GetComponent<FollowLigand>().light.GetComponent<Light>().range;
        //gO.GetComponent<Light>().intensity = Camera.main.GetComponent<FollowLigand>().light.GetComponent<Light>().intensity;
        //gO.GetComponent<Light>().color = Camera.main.GetComponent<FollowLigand>().light.GetComponent<Light>().color;
        //gO.GetComponent<Light>().lightmappingMode = Camera.main.GetComponent<FollowLigand>().light.GetComponent<Light>().lightmappingMode;

        Destroy(gameObject);
    }
}
