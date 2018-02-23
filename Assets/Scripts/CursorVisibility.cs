using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorVisibility : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>() == null)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
