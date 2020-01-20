using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    public int sceneIndex;

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void loadScene() {
        Debug.Log("Loading Scene...");
        if (sceneIndex != 0)
            SceneManager.LoadScene(sceneIndex);
        else {
            if (GameObject.FindGameObjectsWithTag("BuildButtonTag").Length > 0)
            {
                if (!GameObject.FindGameObjectWithTag("BuildButtonTag").GetComponent<StartProteinBuildButton>().inProgress)
                    SceneManager.LoadScene(sceneIndex);
            }
            else SceneManager.LoadScene(sceneIndex);
        }
    }
}
