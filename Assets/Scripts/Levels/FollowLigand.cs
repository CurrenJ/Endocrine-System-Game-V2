using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;

public class FollowLigand : MonoBehaviour
{
    public Vector3 offset = new Vector3(0.0F, 0.0F, -10.0F);
    public GameObject ligand;

    public float doubleClickTime = 0.5F;
    private float ButtonCooler; // Half a second before reset 
    private int ButtonCount = 0;
    public Camera pov;
    public GameObject light;

    public int aminoYellow;
    public int aminoBlue;
    public int aminoPink;
    public int aminoGreen;
    public int aminoRed;

    public GameObject aminoYellowText;
    public GameObject aminoBlueText;
    public GameObject aminoPinkText;
    public GameObject aminoGreenText;
    public GameObject aminoRedText;

    public int aminoYellowBonus;
    public int aminoBlueBonus;
    public int aminoPinkBonus;
    public int aminoGreenBonus;
    public int aminoRedBonus;

    public int goalLiLevel = 500;
    public int yInt = 250;

    public bool levelCompletedBefore;

    public float speedAdditionUpgrade;
    public float rotationAdditionUpgrade;
    public float speedMultiplierUpgrade;
    public float rotationMultiplierUpgrade;

    public float defaultSize = 227.5F;
    public float fovMultiplierUpgrade;

    public void Start() {
        ButtonCooler = doubleClickTime;
        pov.enabled = true;
        loadState();
        levelCompletedBefore = loadLevelState();

        speedAdditionUpgrade = 0F;
        rotationAdditionUpgrade = 0F;
        speedMultiplierUpgrade = 1F;
        rotationMultiplierUpgrade = 1F;
        fovMultiplierUpgrade = 1;
        upgradeEffect();
        ligand.GetComponent<Ligand>().applyEffects();

        GetComponent<Camera>().orthographicSize *= fovMultiplierUpgrade;
        light.GetComponent<Light>().range = 1000 + yInt;
    }

    public void upgradeEffect() {
        if (loadSelectedUpgradeState("Insulin")) {
            speedAdditionUpgrade = 0.1F;
        }
        else if (loadSelectedUpgradeState("Oxytocin")) {
            speedAdditionUpgrade = 0.1F;
            rotationAdditionUpgrade = 0.1F;
        }
        else if (loadSelectedUpgradeState("Relaxin"))
        {
            speedAdditionUpgrade = 0.2F;
            rotationAdditionUpgrade = -0.1F;
        }
        else if (loadSelectedUpgradeState("Leptin"))
        {
            fovMultiplierUpgrade = 1.1F;
        }
        else if (loadSelectedUpgradeState("Orexin"))
        {
            speedAdditionUpgrade = 0.3F;
        }
    }

    public UnityEngine.Object getParticles(Sprite aaSprite) {
        AAColors hud = GameObject.FindGameObjectWithTag("Panel").GetComponent<AAColors>();
        if (aaSprite == hud.YellowAminoAcid)
            return GameObject.FindGameObjectWithTag("Panel").GetComponent<AAColors>().AminoParticlesYellow;
        else if (aaSprite == hud.BlueAminoAcid)
            return GameObject.FindGameObjectWithTag("Panel").GetComponent<AAColors>().AminoParticlesBlue;
        else if (aaSprite == hud.PinkAminoAcid)
            return GameObject.FindGameObjectWithTag("Panel").GetComponent<AAColors>().AminoParticlesPink;
        else if (aaSprite == hud.GreenAminoAcid)
            return GameObject.FindGameObjectWithTag("Panel").GetComponent<AAColors>().AminoParticlesGreen;
        else if (aaSprite == hud.RedAminoAcid)
            return GameObject.FindGameObjectWithTag("Panel").GetComponent<AAColors>().AminoParticlesRed;
        else return null;
    }

    public void OnDestroy()
    {
        saveState();
        saveLevelState();
    }

    public void Update()
    {
        aminoYellowText.GetComponent<Text>().text = "" + aminoYellow;
        aminoBlueText.GetComponent<Text>().text = "" + aminoBlue;
        aminoPinkText.GetComponent<Text>().text = "" + aminoPink;
        aminoGreenText.GetComponent<Text>().text = "" + aminoGreen;
        aminoRedText.GetComponent<Text>().text = "" + aminoRed;


        transform.position = new Vector3(ligand.transform.position.x, ligand.transform.position.y, -10);
        light.transform.position = new Vector3(ligand.transform.position.x, ligand.transform.position.y, light.transform.position.z);
        if (light.GetComponent<Light>().range > goalLiLevel + yInt) {
            light.GetComponent<Light>().range -= ((light.GetComponent<Light>().range - goalLiLevel * (9/10) + yInt) / (1000 - goalLiLevel + yInt) * 10) * Time.deltaTime;
            //Debug.Log(light.GetComponent<Light>().range);
        }

        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            if (ButtonCooler > 0 && ButtonCount == 1/*Number of Taps you want Minus One*/)
            {
       //Doube tap stuff here 
                ButtonCount = 0;
            }
            else
            {
                ButtonCooler = doubleClickTime;
                ButtonCount += 1;
            }
        }

        //Debug.Log(ButtonCooler);
        if (ButtonCooler > 0)
        {

            ButtonCooler -= 1 * Time.deltaTime;

        }
        else
        {
            ButtonCount = 0;
        }
       // Debug.Log(ButtonCooler);
    }



    void setFollow(GameObject lig)
    {
        ligand = lig;
        ligand.GetComponent<Ligand>().applyEffects();
        light.GetComponent<Light>().range = 1000 + yInt;
    }

    public bool loadSelectedUpgradeState(String proteinName)
    {
        try
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                String realPath = Application.persistentDataPath + "/Selected Upgrades/" + proteinName + ".bytes";

                return Convert.ToBoolean(System.IO.File.ReadAllBytes(realPath)[0]);
            }
            else
            {
                String path = Application.streamingAssetsPath + "/Selected Upgrades/" + proteinName + ".bytes";
                return Convert.ToBoolean(System.IO.File.ReadAllBytes(path)[0]);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return false;
        }
    }

    public void saveLevelState()
    {
        try
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                String andLevelPath = Application.persistentDataPath + "/Level Completion/" + SceneManager.GetActiveScene().name + "/";
                String andPath = andLevelPath + "Level Completed" + ".bytes";

                if (!Directory.Exists(andLevelPath))
                {
                    Directory.CreateDirectory(andLevelPath);
                }



                bool[] a = new bool[] { levelCompletedBefore };
                byte[] b = (from x in a select x ? (byte)0x1 : (byte)0x0).ToArray();

                System.IO.File.WriteAllBytes(andPath, b);

            }
            else
            {
                String levelPath = Application.streamingAssetsPath + "/Level Completion/" + SceneManager.GetActiveScene().name + "/";
                String path = levelPath + "Level Completed" + ".bytes";

                if (!Directory.Exists(levelPath))
                {
                    Directory.CreateDirectory(levelPath);
                }

                bool[] a = new bool[] { levelCompletedBefore };
                byte[] b = (from x in a select x ? (byte)0x1 : (byte)0x0).ToArray();

                System.IO.File.WriteAllBytes(path, b);
            }
        }
        catch (Exception e) {
            Debug.Log(e.Message);
        }
    }

    public bool loadLevelState()
    {
        try
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                String realPath = Application.persistentDataPath + "/Level Completion/" + SceneManager.GetActiveScene().name + "/" + "Level Completed" + ".bytes";

                return Convert.ToBoolean(System.IO.File.ReadAllBytes(realPath)[0]);
            }
            else
            {
                String path = Application.streamingAssetsPath + "/Level Completion/" + SceneManager.GetActiveScene().name + "/" + "Level Completed" + ".bytes";
                return Convert.ToBoolean(System.IO.File.ReadAllBytes(path)[0]);
            }
        }
        catch (Exception e) {
            Debug.Log(e.Message);
            return false;
        }
    }

    public void saveState()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            String andTotalPath = Application.persistentDataPath + "/Amino Acids/Totals/";
            String andPathYellow = andTotalPath + "yellow.bytes";
            String andPathBlue = andTotalPath + "blue.bytes";
            String andPathPink = andTotalPath + "pink.bytes";
            String andPathGreen = andTotalPath + "green.bytes";
            String andPathRed = andTotalPath + "red.bytes";

            if (!Directory.Exists(andTotalPath))
            {
                Directory.CreateDirectory(andTotalPath);
            }   

            System.IO.File.WriteAllBytes(andPathYellow, BitConverter.GetBytes(aminoYellow));
            System.IO.File.WriteAllBytes(andPathBlue, BitConverter.GetBytes(aminoBlue));
            System.IO.File.WriteAllBytes(andPathPink, BitConverter.GetBytes(aminoPink));
            System.IO.File.WriteAllBytes(andPathGreen, BitConverter.GetBytes(aminoGreen));
            System.IO.File.WriteAllBytes(andPathRed, BitConverter.GetBytes(aminoRed));

        }
        else
        {
            String totalPath = Application.streamingAssetsPath + "/Amino Acids/Totals/";
            String pathYellow = totalPath + "yellow.bytes";
            String pathBlue = totalPath + "blue.bytes";
            String pathPink = totalPath + "pink.bytes";
            String pathGreen = totalPath + "green.bytes";
            String pathRed = totalPath + "red.bytes";

            if (!Directory.Exists(totalPath))
            {
                Directory.CreateDirectory(totalPath);
            }

            System.IO.File.WriteAllBytes(pathYellow, BitConverter.GetBytes(aminoYellow));
            System.IO.File.WriteAllBytes(pathBlue, BitConverter.GetBytes(aminoBlue));
            System.IO.File.WriteAllBytes(pathPink, BitConverter.GetBytes(aminoPink));
            System.IO.File.WriteAllBytes(pathGreen, BitConverter.GetBytes(aminoGreen));
            System.IO.File.WriteAllBytes(pathRed, BitConverter.GetBytes(aminoRed));
        }
    }

    public void loadState()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            String totalPath = Application.persistentDataPath + "/Amino Acids/Totals/";
            String pathYellow = totalPath + "yellow.bytes";
            String pathBlue = totalPath + "blue.bytes";
            String pathPink = totalPath + "pink.bytes";
            String pathGreen = totalPath + "green.bytes";
            String pathRed = totalPath + "red.bytes";

            try
            {
                aminoYellow = BitConverter.ToInt32(System.IO.File.ReadAllBytes(pathYellow), 0);
                aminoBlue = BitConverter.ToInt32(System.IO.File.ReadAllBytes(pathBlue), 0);
                aminoPink = BitConverter.ToInt32(System.IO.File.ReadAllBytes(pathPink), 0);
                aminoGreen = BitConverter.ToInt32(System.IO.File.ReadAllBytes(pathGreen), 0);
                aminoRed = BitConverter.ToInt32(System.IO.File.ReadAllBytes(pathRed), 0);
            }
            catch (Exception e) {
                
            }
        }
        else
        {
            String totalPath = Application.streamingAssetsPath + "/Amino Acids/Totals/";
            String pathYellow = totalPath + "yellow.bytes";
            String pathBlue = totalPath + "blue.bytes";
            String pathPink = totalPath + "pink.bytes";
            String pathGreen = totalPath + "green.bytes";
            String pathRed = totalPath + "red.bytes";

            try
            {
                aminoYellow = BitConverter.ToInt32(System.IO.File.ReadAllBytes(pathYellow), 0);
                aminoBlue = BitConverter.ToInt32(System.IO.File.ReadAllBytes(pathBlue), 0);
                aminoPink = BitConverter.ToInt32(System.IO.File.ReadAllBytes(pathPink), 0);
                aminoGreen = BitConverter.ToInt32(System.IO.File.ReadAllBytes(pathGreen), 0);
                aminoRed = BitConverter.ToInt32(System.IO.File.ReadAllBytes(pathRed), 0);
            }
            catch (Exception e) {
                Debug.Log(e.Message);
            }
        }
    }
}