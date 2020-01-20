using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReceptorTrigger : MonoBehaviour {

    public GameObject receptor;
    public Object ligA;
    public Object ligB;
    public Object ligC;
    public Object ligD;
    public Object ligE;

    public void Start()
    {
    }

    public Object getNextLig(int index)
    {
        Debug.Log("Spawning Ligand " + Camera.main.GetComponent<LoadLigandSequence>().ligOrder[index]);
        char lig = Camera.main.GetComponent<LoadLigandSequence>().ligOrder[index];
        if (lig == 'A')
        {
            return ligA;
        }
       else if (lig == 'B')
        {
            return ligB;
        }
        else if (lig == 'C')
        {
            return ligC;
        }
        else if (lig == 'D')
        {
            return ligD;
        }
        else if (lig == 'E')
        {
            return ligE;
        }
        else return ligA;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D ligand)
    {
        Debug.Log("Ligand type '" + ligand.GetComponent<Ligand>().ligandType + "' has collided with receptor type '" + receptor.GetComponent<Receptor>().receptorType + "'.");
        if (ligand.GetComponent<Ligand>().ligandType == receptor.GetComponent<Receptor>().receptorType && receptor.GetComponent<Receptor>().spriteRenderer.sprite != receptor.GetComponent<Receptor>().bound)
        {
            StartCoroutine(LigandTransition (ligand));
        }
    }

    IEnumerator LigandTransition(Collider2D ligand) {
        int nextIndex = ligand.GetComponent<Ligand>().ligIndex + 1;
      
            Object nextLig = getNextLig(nextIndex);
            receptor.SendMessage("bindToLigand");
            ligand.SendMessage("bindToReceptor");
            yield return new WaitForSeconds(1.0f);

        if (nextIndex == Camera.main.GetComponent<LoadLigandSequence>().finalLigIndex)
        {
            Debug.Log("Level Complete");
            Object levD = Camera.main.GetComponent<LoadLigandSequence>().LevelDone;
            GameObject levG = (GameObject)Instantiate(levD);

            //GameObject.FindGameObjectWithTag("NextLevBut").SendMessage("setNextLevel", SceneManager.GetActiveScene().buildIndex + 1);
            levG.transform.SetParent(GameObject.FindGameObjectWithTag("Panel").transform, false);

            if (!Camera.main.GetComponent<FollowLigand>().levelCompletedBefore)
            {
                Camera.main.GetComponent<FollowLigand>().aminoYellow += Camera.main.GetComponent<FollowLigand>().aminoYellowBonus;
                Camera.main.GetComponent<FollowLigand>().aminoBlue += Camera.main.GetComponent<FollowLigand>().aminoBlueBonus;
                Camera.main.GetComponent<FollowLigand>().aminoPink += Camera.main.GetComponent<FollowLigand>().aminoPinkBonus;
                Camera.main.GetComponent<FollowLigand>().aminoGreen += Camera.main.GetComponent<FollowLigand>().aminoGreenBonus;
                Camera.main.GetComponent<FollowLigand>().aminoRed += Camera.main.GetComponent<FollowLigand>().aminoRedBonus;

                GameObject.FindWithTag("yellow").GetComponent<Text>().text = Camera.main.GetComponent<FollowLigand>().aminoYellowBonus + "";
                GameObject.FindWithTag("blue").GetComponent<Text>().text = Camera.main.GetComponent<FollowLigand>().aminoBlueBonus + "";
                GameObject.FindWithTag("pink").GetComponent<Text>().text = Camera.main.GetComponent<FollowLigand>().aminoPinkBonus + "";
                GameObject.FindWithTag("green").GetComponent<Text>().text = Camera.main.GetComponent<FollowLigand>().aminoGreenBonus + "";
                GameObject.FindWithTag("red").GetComponent<Text>().text = Camera.main.GetComponent<FollowLigand>().aminoRedBonus + "";
            }
            Camera.main.GetComponent<FollowLigand>().levelCompletedBefore = true;
            Camera.main.GetComponent<FollowLigand>().saveLevelState();
        }
        else
        {
            GameObject lig = (GameObject)Instantiate(nextLig);
            lig.GetComponent<Ligand>().ligIndex = nextIndex;
            Camera.main.GetComponent<FollowLigand>().SendMessage("setFollow", lig);
        }
    }

}
