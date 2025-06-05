using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public string audioPrefix;
    public static SoundManager instance;
    public AudioSource audioS;
    public AudioSource musicS;
    public AudioMixer masterMixer;
    Dictionary<string,AudioClip> audioDictionary = new Dictionary<string,AudioClip>();
    void Awake()
    {
        if(instance == null){
            instance = this;
        }else if(instance != this){
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Play(string soundname, float volume, float pitch)
    {
        if (!audioDictionary.ContainsKey(soundname) || audioDictionary[soundname] == null)
        {
            AudioClip audio = Resources.Load<AudioClip>("Audio/" + audioPrefix + soundname);
            if (audio != null)
            {
                audioDictionary[soundname] = audio;
            }
            else
            {
                Debug.LogWarning($"SoundManager: Could not load or find sound '{soundname}' in Resources/Audio/");
                return;
            }
        }

        audioS.pitch = pitch;
        audioS.volume = volume;
        audioS.PlayOneShot(audioDictionary[soundname], volume);
    }

    public void Play(string soundname){
        Play(soundname,1f,1f);
    }

    public void Play(string soundname,float volume){
        Play(soundname,volume,1f);
    }

    public void PlayMusic(string soundname, float volume)
    {
        if (!audioDictionary.ContainsKey(soundname) || audioDictionary[soundname] == null)
        {
            AudioClip audio = Resources.Load<AudioClip>("Audio/" + audioPrefix + soundname);
            if (audio != null)
            {
                audioDictionary[soundname] = audio;
            }
            else
            {
                Debug.LogWarning($"SoundManager: Could not load or find music '{soundname}' in Resources/Audio/");
                return;
            }
        }

        musicS.Stop();
        musicS.loop = true;
        musicS.clip = audioDictionary[soundname];
        musicS.volume = volume;
        musicS.Play();
    }

    public void PlayMusic(string soundname){
        PlayMusic(soundname,1f);
    }

    public void PlayAudioClip(AudioClip audio,float pitch){
        audioS.clip = audio;
        audioS.pitch = pitch;
        audioS.volume = 1f;
        audioS.Play();
    }

    public void StopMusic(){
        musicS.Stop();
    }

    public void SetMixerVolume(float volume){

        masterMixer.SetFloat("masterVolume",volume);
    }
}
