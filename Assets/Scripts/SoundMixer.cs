using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMixer : MonoBehaviour
{

    public static AudioSource soundeffect;

    
    public static readonly Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>();
    private static string prefix = "Audio/";
    private static string[] sfxStrings = { "Explosion", "Gunshot", "Gunshot2", "Heal", "Laser", "MissileLaunch1", "MissileLaunch2", 
        "MissileLaunch3", "MissileLaunch4", "Shield"};

    // Start is called before the first frame update
    void Start()
    {
        soundeffect = this.gameObject.AddComponent(typeof(AudioSource)) as AudioSource;

        foreach (string str in sfxStrings)
        {
            AudioClip clip = Resources.Load(prefix + str) as AudioClip;
            sounds.Add(str, clip);
        }
    }

    public static void PlaySound(string sound, float volume = 0.5f)
    {
        soundeffect.PlayOneShot(sounds[sound], volume);
    }

}

