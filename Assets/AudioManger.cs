using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManger : MonoBehaviour
{
    [Header("-------------Audio Source-----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

[Header("-------------Audio Clip-----------")]
   public AudioClip background;
   public AudioClip jump;
   public AudioClip doubleJump;
   public AudioClip coin;
   public AudioClip Hit;
   public AudioClip powerUP;

   private void Start()
   {
    musicSource.clip = background;
    musicSource.Play();
   }
}
