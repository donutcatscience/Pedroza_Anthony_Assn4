using UnityEngine;

public class BulletHit : MonoBehaviour
{

    public AudioClip hitSolid;


    private AudioSource source;
    private float lowPitchRange = .75F;
    private float highPitchRange = 1.5F;
    private float velToVol = .2F;
    private float velocityClipSplit = 10F;


    void Awake()
    {
        source = GetComponent<AudioSource>();
    }


    void OnCollisionEnter(Collision coll)
    {
        source.pitch = Random.Range(lowPitchRange, highPitchRange);
        float hitVol = coll.relativeVelocity.magnitude * velToVol;
        source.PlayOneShot(hitSolid, hitVol);
        Destroy(gameObject, 0.5f);
    }

}
