using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    /* Make it accessible from other scripts */
    public static SoundManager Instance;

    public AudioClip shootClip;
    public AudioClip sheepHitClip;
    public AudioClip sheepDroppedClip;

    private Vector3 cameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        cameraPosition = Camera.main.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlaySound(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, cameraPosition);
    }


    public void PlayShootClip()
    {
        PlaySound(shootClip);
    }

    public void PlaySheepHitClip()
    {
        PlaySound(sheepHitClip);
    }

    public void PlaySheepDroppedClip()
    {
        PlaySound(sheepDroppedClip);
    }
}
