using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioClip shootClip;
    public AudioClip sheepHitClip;
    public AudioClip sheepDroppedClip;

    private Vector3 cameraPosition;

    // Start is called before the first frame update

    //We use awake because it runs before start. If we wanted to reference the singleton in start
    //we would have troubles otherwise.
    void Awake()
    {
        Instance = this;
        cameraPosition = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayShootClip(){
        PlaySound(shootClip);
    }

    public void PlaySheepHitClip(){
        PlaySound(sheepHitClip);
    }

    public void PlaySheepDroppedClip(){
        PlaySound(sheepDroppedClip);
    }

    private void PlaySound(AudioClip clip){
        AudioSource.PlayClipAtPoint(clip, cameraPosition);
    }
}
