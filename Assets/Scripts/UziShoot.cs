using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UziShoot : MonoBehaviour
{
    // Bullet Variables
    public GameObject Bullet_Emitter;
    public GameObject Bullet;
    public float Bullet_Forward_Force;

    // Casing Variables
    public GameObject Casing_Emitter;
    public GameObject Casing;
    public float Bullet_Upward_Force;

    public int fireRate = 15;
    public AudioClip shootSound;

    private int frameCount = 0;
    private bool fire = false;
    private AudioSource source;
    private float volLowRange = .5f;
    private float volHightRange = 1.0f;

    // Get Sound Source
    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (frameCount < fireRate)
        {
            ++frameCount;
        }

        if (Input.GetMouseButton(0) && frameCount >= fireRate)
        {
            fire = true;
        }

        if (fire == true)
        {
            //The Bullet instantiation happens here.
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;

            //Retrieve the Rigidbody component from the instantiated Bullet and control it.
            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

            //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force. 
            Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);

            //Get volume and play sound
            float vol = Random.Range(volLowRange, volHightRange);
            source.PlayOneShot(shootSound, vol);

            //set the Bullets to self destruct after 3
            Destroy(Temporary_Bullet_Handler, 3.0f);



            // Bullet Fire Controls
            fire = false;
            frameCount = 0;
        }
    }
}