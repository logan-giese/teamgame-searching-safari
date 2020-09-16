using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectScript : MonoBehaviour
{
    public AudioClip[] effectClips;
    private AudioSource source;
    private static SoundEffectScript _instance;

    // Start is called before the first frame update
    void Start()
    {
        // Maintain a static instance to allow static calling of PlayClip()
        _instance = this;

        source = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Play the specified dialogue clip
    public static void PlayEffect(int index)
    {
        _instance.source.clip = _instance.effectClips[index];
        _instance.source.Play();
    }
}
