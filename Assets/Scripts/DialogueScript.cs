using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueScript : MonoBehaviour
{
    public AudioClip[] dialogueClips;
    private AudioSource source;
    private static DialogueScript _instance;

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
    public static void PlayClip(int index)
    {
        _instance.source.clip = _instance.dialogueClips[index];
        _instance.source.Play();
    }
}
