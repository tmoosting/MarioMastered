using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControllerScript : MonoBehaviour
{
    public static AudioClip jumpSound, woohooSound, backgroundSound, bumpSound, coinSound, textSound, clotheschangeSound, endingSound;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        jumpSound = Resources.Load<AudioClip>("jump");
        woohooSound = Resources.Load<AudioClip>("mario-woohoo");
        backgroundSound = Resources.Load<AudioClip>("background sound");
        bumpSound = Resources.Load<AudioClip>("bump sound (fail)");
        coinSound = Resources.Load<AudioClip>("coin");
        textSound = Resources.Load<AudioClip>("text");
        clotheschangeSound = Resources.Load<AudioClip>("changing his clothes");
        endingSound = Resources.Load<AudioClip>("FINAL sound - end scene beach");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void StopSound()
    {
        audioSrc.Stop();
    }
    public static void PlaySound (string clip)
    { switch (clip)
        {
            case "jump":
                audioSrc.PlayOneShot(jumpSound);
                break;
            case "mario-woohoo":
                audioSrc.PlayOneShot(woohooSound);
                break;
            case "background sound":
                audioSrc.PlayOneShot(backgroundSound);
                break;
            case "bump sound (fail)":
                audioSrc.PlayOneShot(bumpSound);
                break;
            case "coin":
                audioSrc.PlayOneShot(coinSound);
                break;
            case "text":
                audioSrc.PlayOneShot(textSound);
                break;
            case "changing his clothes":
                audioSrc.PlayOneShot(clotheschangeSound);
                break;
            case "FINAL sound - end scene beach":
                audioSrc.PlayOneShot(endingSound);
                break;
        }
            }
}
