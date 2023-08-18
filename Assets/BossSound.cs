using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSound : MonoBehaviour
{
    private static BossSound instance;
    private AudioSource source;
    public AudioClip[] clips;
    // Start is called before the first frame update

    public float timeBetweenEffects;
    private float nextSoundEffect;
    void Start()
    {
        if(instance== null)
        {
            instance = this;
        }
        
        source= GetComponent<AudioSource>();

        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextSoundEffect)
        {
            int randomNumber = Random.Range(0, clips.Length);
            source.clip= clips[randomNumber];
            source.Play();
            nextSoundEffect = Time.time + timeBetweenEffects;
        }
    }
}
