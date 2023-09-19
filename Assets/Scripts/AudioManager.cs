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
    [SerializeField]
    private AudioSource footstepsSource;
    [SerializeField]
    private AudioSource shootSound;
    [SerializeField]
    private AudioSource meleeSource;
    [SerializeField]
    private AudioSource goblinSource;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        footstepsSource.enabled = false;
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

    public void FootstepsOn()
    {
        footstepsSource.enabled = true;
    }

    public void FootstepsOff()
    {
        footstepsSource.enabled = false;
    }

    public void ShootingSFX(int index)
    {
        shootSound.clip = SFXclip[index];
        shootSound.Play();
    }

    public void MeleeSFX(int index)
    {
        meleeSource.clip = SFXclip[index];
        meleeSource.Play();
    }

    public void GoblinSFX(int index)
    {
        goblinSource.clip = SFXclip[index];
        goblinSource.Play();
    }

}
