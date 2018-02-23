using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour {

    public GameObject firewordsEffect;
    public AudioClip winSound;

    private AudioSource source;

    // Use this for initialization
    void Awake () {
        Instantiate(firewordsEffect, this.transform.position, this.transform.rotation);

        source = GetComponent<AudioSource>();
        source.PlayOneShot(winSound, 1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(scene);
        SceneManager.LoadScene("Win");
    }
}
