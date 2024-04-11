using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Código implementado por:
//EDUARDO GALLARDO Y ENRIQUE JUAN GAMBOA

public class AudioManager : MonoBehaviour
{
    

    public AudioClip[] audioClips;
    static AudioSource audio;
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    public void PlaySound(int n)
    {
        audio.PlayOneShot(audioClips[n]);
    }   
}
