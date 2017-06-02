using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class gecis : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void startClk() 
    {
        SceneManager.LoadScene(1);
    }

    public void exitClk()
    {
        Application.Quit();
    }
}
