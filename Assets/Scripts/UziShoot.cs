using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UziShoot : MonoBehaviour
{
    //UI Variables
    public Text enemyText;
    public Text timeText;
    public GameObject winExit;

    // Bullet Variables
    public GameObject Bullet_Emitter;
    public GameObject Bullet;
    public float Bullet_Forward_Force;

    // Casing Variables
    public GameObject Casing_Emitter;
    public GameObject Casing;
    public float Casing_Upward_Force;

    // Uzi Variables
    public float damage = 10f;
    public float range = 1000f;
    public int fireRate = 15;
    public AudioClip shootSound;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    //private variables
    private int frameCount = 0;
    private bool fire = false;
    private AudioSource source;
    private float volLowRange = .5f;
    private float volHightRange = 1.0f;
    private int aliveEnemies;
    private float gameTime;

    // Get Sound Source
    void Awake()
    {
        source = GetComponent<AudioSource>();

    }

    void Start()
    {
        aliveEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemyText.text = "Enemies Alive: " + aliveEnemies;
        gameTime++;
        timeText.text = "Timer: " + Mathf.Round((gameTime/60));
    }

    // Update is called once per frame
    void Update()
    {
        //control/display game time
        gameTime++;
        timeText.text = "Timer: " + Mathf.Round((gameTime/60));
        
        //control/display game time
        aliveEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemyText.text = "Enemies Alive: " + aliveEnemies;

        if (aliveEnemies <= 0 && (GameObject.FindGameObjectsWithTag("WinCube").Length == 0))
        {
            Vector3 playerPos = this.transform.position;
            Vector3 playerDirection = this.transform.right;
            Quaternion playerRotation = this.transform.rotation;
            float spawnDistance = 10;
            Vector3 spawnPos = playerPos + playerDirection * spawnDistance;

            Instantiate(winExit, spawnPos, playerRotation );
        }

        if (frameCount < fireRate)
        {
            ++frameCount;
        }

        if (Input.GetButton("Fire1") && frameCount >= fireRate)
        {
            fire = true;
        }

        if (fire == true)
        {
            muzzleFlash.GetComponent<ParticleSystem>().Emit(100);


            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);

                Enemy target = hit.transform.GetComponent<Enemy>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }

                GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGo, 0.25f);
            }

            //The Bullet instantiation happens here.
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;

            //Retrieve the Rigidbody component from the instantiated Bullet and control it.
            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

            //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force. 
            Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);

            //The Casing instantiation happens here.
            GameObject Temporary_Casing_Handler;
            Temporary_Casing_Handler = Instantiate(Casing, Casing_Emitter.transform.position, Casing_Emitter.transform.rotation) as GameObject;

            //Retrieve the Rigidbody component from the instantiated Casing and control it.
            Rigidbody Casing_RigidBody;
            Casing_RigidBody = Temporary_Casing_Handler.GetComponent<Rigidbody>();

            //Tell the Casing to be "pushed" upward by an amount set by Casing_Upward_Force. 
            Casing_RigidBody.AddForce(transform.right * Casing_Upward_Force);


            //Get volume and play sound
            float vol = Random.Range(volLowRange, volHightRange);
            source.PlayOneShot(shootSound, vol);

            //set the Bullets and Casings to self destruct after 3 seconds
            Destroy(Temporary_Bullet_Handler, 3.0f);
            Destroy(Temporary_Casing_Handler, 3.0f);

            // Bullet Fire Controls
            fire = false;
            frameCount = 0;
        }
    }
}