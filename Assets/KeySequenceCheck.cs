using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeySequenceCheck : MonoBehaviour {

    private KeyCode[] sequence = new KeyCode[]{
    KeyCode.R,
    KeyCode.E,
    KeyCode.S,
    KeyCode.E,
    KeyCode.T};
    private int sequenceIndex;

    private KeyCode[] sequence2 = new KeyCode[]{
    KeyCode.A,
    KeyCode.M,
    KeyCode.I,
    KeyCode.N,
    KeyCode.O};
    private int sequenceIndex2;

    void Start() { }

    private void Update()
    {
        if (Input.GetKeyDown(sequence[sequenceIndex]))
        {
            if (++sequenceIndex == sequence.Length)
            {
                sequenceIndex = 0;
                doStuff();
            }
        }
        else if (Input.anyKeyDown) sequenceIndex = 0;

        if (Input.GetKeyDown(sequence2[sequenceIndex2]))
        {
            if (++sequenceIndex2 == sequence2.Length)
            {
                sequenceIndex2 = 0;
                doStuff2();
            }
        }
        else if (Input.anyKeyDown) sequenceIndex2 = 0;


    }

    void doStuff() {
        if (SceneManager.GetActiveScene().name == "Enzyme Builder Menu")
        {
            Camera.main.GetComponent<LoadAminoAcidTotals>().aminoYellow = 0;
            Camera.main.GetComponent<LoadAminoAcidTotals>().aminoBlue = 0;
            Camera.main.GetComponent<LoadAminoAcidTotals>().aminoPink = 0;
            Camera.main.GetComponent<LoadAminoAcidTotals>().aminoRed = 0;
            Camera.main.GetComponent<LoadAminoAcidTotals>().aminoGreen = 0;
        }
        else
        {
            foreach (AminoAcid aa in GameObject.FindGameObjectWithTag("LevelAminoAcids").GetComponentsInChildren<AminoAcid>())
            {
                Debug.Log("HEYYYYYY");
                aa.enable();
            }
        }
        
    }

    void doStuff2()
    {
        if (SceneManager.GetActiveScene().name == "Enzyme Builder Menu")
        {
            Camera.main.GetComponent<LoadAminoAcidTotals>().aminoYellow = 25;
            Camera.main.GetComponent<LoadAminoAcidTotals>().aminoBlue = 25;
            Camera.main.GetComponent<LoadAminoAcidTotals>().aminoPink = 25;
            Camera.main.GetComponent<LoadAminoAcidTotals>().aminoRed = 25;
            Camera.main.GetComponent<LoadAminoAcidTotals>().aminoGreen = 25;
        }

    }
}
