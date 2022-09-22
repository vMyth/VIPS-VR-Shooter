using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadAudioThroughEvent : MonoBehaviour
{
    public AudioSource playerAudioSource;
    public AudioClip reloadAudio;

    void PlayAudioReload()
    {
        playerAudioSource.PlayOneShot(reloadAudio);
    }
}
