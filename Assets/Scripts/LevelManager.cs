using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    //load game scene
    public void LoadLevel (string name)
    {
        Debug.Log("Level load requested for " +name);
        Application.LoadLevel(name);
    }

    //handle quitting game
    public void QuitRequest()
    {
        Debug.Log("I want to quit!");
        Application.Quit();
    }
}
