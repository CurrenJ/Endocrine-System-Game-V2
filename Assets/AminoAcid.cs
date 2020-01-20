using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AminoAcid : MonoBehaviour {

    bool aaEnabled;
    float id;

    // Use this for initialization
    void Start() {
        id = transform.position.sqrMagnitude;

        try
        {
            aaEnabled = loadState();
        }
        catch (Exception e) {
            //Debug.Log(e.Message);
            GameObject.FindGameObjectWithTag("instrucText").GetComponent<TextMesh>().text = e.Message;
            aaEnabled = true;
        }

        if (aaEnabled)
            enable();
        else disable();

    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter2D(Collider2D ligand)
    {
        //Debug.Log("BLOOP");

        collect();
    }

    public void saveState() {
        if (Application.platform == RuntimePlatform.Android)
        {
            String andLevelPath = Application.persistentDataPath + "/Amino Acids/" + SceneManager.GetActiveScene().name + "/";
            String andPath = andLevelPath + id + ".bytes";

            if (!Directory.Exists(andLevelPath)) {
                Directory.CreateDirectory(andLevelPath);
            }

            

            bool[] a = new bool[] { aaEnabled };
            byte[] b = (from x in a select x ? (byte)0x1 : (byte)0x0).ToArray();

            System.IO.File.WriteAllBytes(andPath, b);

        }
        else
        {
            String levelPath = Application.streamingAssetsPath + "/Amino Acids/" + SceneManager.GetActiveScene().name + "/";
            String path = levelPath + id + ".bytes";

            if (!Directory.Exists(levelPath))
            {
                Directory.CreateDirectory(levelPath);
            }

            bool[] a = new bool[] { aaEnabled };
            byte[] b = (from x in a select x ? (byte)0x1 : (byte)0x0).ToArray();

            System.IO.File.WriteAllBytes(path, b);
        }
    }

    public bool loadState() {
        if (Application.platform == RuntimePlatform.Android)
        {
            String realPath = Application.persistentDataPath + "/Amino Acids/" + SceneManager.GetActiveScene().name + "/" + id + ".bytes";

            return Convert.ToBoolean(System.IO.File.ReadAllBytes(realPath)[0]);
        }
        else
        {
            String path = Application.streamingAssetsPath + "/Amino Acids/" + SceneManager.GetActiveScene().name + "/" + id + ".bytes";
            return Convert.ToBoolean(System.IO.File.ReadAllBytes(path)[0]);
        }
    }

    public void disable() {
        aaEnabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<AminoAcid>().enabled = false;
        GetComponentInChildren<ParticleSystem>().Stop();   
    }

    public void collect() {
        disable();

        Sprite aaSprite = GetComponent<SpriteRenderer>().sprite;
        AAColors hud = GameObject.FindGameObjectWithTag("Panel").GetComponent<AAColors>();
        if (aaSprite == hud.YellowAminoAcid)
            Camera.main.GetComponent<FollowLigand>().aminoYellow++;
        else if (aaSprite == hud.BlueAminoAcid)
            Camera.main.GetComponent<FollowLigand>().aminoBlue++;
        else if (aaSprite == hud.PinkAminoAcid)
            Camera.main.GetComponent<FollowLigand>().aminoPink++;
        else if (aaSprite == hud.GreenAminoAcid)
            Camera.main.GetComponent<FollowLigand>().aminoGreen++;
        else if (aaSprite == hud.RedAminoAcid)
            Camera.main.GetComponent<FollowLigand>().aminoRed++;

        var obj = (GameObject) Instantiate(Camera.main.GetComponent<FollowLigand>().getParticles(GetComponent<SpriteRenderer>().sprite));
        obj.transform.position = transform.position;
        Debug.Log("BLOOP2");
    }

    public void enable() {
        aaEnabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
        GetComponent<AminoAcid>().enabled = true;
        GetComponentInChildren<ParticleSystem>().Play();
    }

    void OnDestroy()
    {
        saveState();
    }
}
