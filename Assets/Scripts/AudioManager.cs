using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Properties")]
    [SerializeField] private List<AudioClip> BGMsources;
    [SerializeField] private List<AudioClip> SFXsources;
    

    private AudioSource audioSource;


    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = BGMsources[0];
        audioSource.Play();
    }

    
}
