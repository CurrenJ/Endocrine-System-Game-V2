using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetNextLevel : MonoBehaviour {

    public int nextLevelIndex;

	// Use this for initialization
	void Start () {
        //nextLevelIndex = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setNextLevel(int index) {
        nextLevelIndex = index;
    }

    public void nextLevel() {
        Debug.Log("NLI " + nextLevelIndex);
        if (nextLevelIndex == 0) {
            loadLevelSelect();
        }
        else
        SceneManager.LoadScene(nextLevelIndex);
    }

    public void loadLevelSelect()
    {
        SceneManager.LoadScene(1);
    }

    public void restartCurrentLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
