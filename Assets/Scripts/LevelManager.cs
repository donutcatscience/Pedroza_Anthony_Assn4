using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    //load game scene
    public void LoadLevel (string name)
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(scene);
        SceneManager.LoadScene(name);
    }

    //handle quitting game
    public void QuitRequest()
    {
        Application.Quit();
    }
}
