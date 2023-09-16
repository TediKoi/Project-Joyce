using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Properties")]
    [SerializeField] private List<AudioClip> BGMclip;
    [SerializeField] private List<AudioClip> SFXclip;

    private static AudioManager instance;

    [SerializeField]
    private AudioSource BGMSource;
    [SerializeField]
    private AudioSource SFXSource;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        
        BGMSource.clip = BGMclip[0];
        BGMSource.Play();
    }

    public static AudioManager GetInstance()
    {
        return instance;
    }

    public void PlaySFX(int index)
    {
        SFXSource.clip = SFXclip[index];
        SFXSource.Play();
    }


}
