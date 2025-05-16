using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public string audioPrefix;
    public static SoundManager instance;
    public string[] audioList = {"pop"};
    public AudioSource audioS;
    public AudioSource musicS;
    public AudioMixer masterMixer;
    Dictionary<string,AudioClip> audioDictionary;
    void Awake()
    {
        if(instance == null){
            instance = this;
        }else if(instance != this){
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        audioS = GetComponent<AudioSource>();
        audioDictionary = new Dictionary<string,AudioClip>();
        foreach(string key in audioList){
            AudioClip audio = Resources.Load<AudioClip>("Audio/" + audioPrefix + key); 
            audioDictionary.Add(key,audio);
        }
    }

    public void Play(string soundname,float volume, float pitch) {
        
        if (!audioDictionary.ContainsKey(soundname)) {
            Debug.LogWarning("SoundManager: Tried to play undefined sound: " + soundname);
        }else{
            audioS.pitch = pitch;
            audioS.volume = volume;
            audioS.PlayOneShot(audioDictionary[soundname],volume);
        }
    
    }

    public void Play(string soundname){
        Play(soundname,1f,1f);
    }

    public void Play(string soundname,float volume){
        Play(soundname,volume,1f);
    }

    public void PlayMusic(string soundname,float volume){
        if (!audioDictionary.ContainsKey(soundname)) {
            Debug.LogWarning("SoundManager: Tried to play undefined sound: " + soundname);
        }else{
            musicS.Stop();
            musicS.loop = true;
            musicS.clip = audioDictionary[soundname];
            musicS.volume = volume;
            musicS.Play();
        }
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
