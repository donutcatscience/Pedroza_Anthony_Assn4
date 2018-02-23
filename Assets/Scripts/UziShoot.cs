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
    public float Casing_Upward_Force;

    // Uzi Variables
    public float damage = 10f;
    public float range = 1000f;
    public int fireRate = 15;
    public AudioClip shootSound;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

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

        if (Input.GetButton("Fire1") && frameCount >= fireRate)
        {
            fire = true;
        }

        if (fire == true)
        {
            muzzleFlash.GetComponent<ParticleSystem>().Emit(1);


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