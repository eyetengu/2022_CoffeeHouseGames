using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Audio_Manager : MonoBehaviour
{
    [Header("AUDIO MANAGEMENT")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioClip[] _musicClips;

    [SerializeField] private AudioClip[] _audioClips;
    AudioSource _audioSource;

    int _musicID;


    //SINGLETON
    private static Audio_Manager _instance;
    public static Audio_Manager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("The Audio_Manager Is Null");

            return _instance;
        }
    }

    //BUILT-IN FUNCTIONS
    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0.139f;
        _musicSource.volume = 0.05f;
    }

    private void Update()
    {
        if (_musicSource.isPlaying == false)
            ChooseNextMusicTrack();
    }

    //CORE FUNCTIONS
    public void PlayGreatClip()
    {
        _audioSource.PlayOneShot(_audioClips[0]);
    }

    public void PlayGoodClip()
    {
        _audioSource.PlayOneShot(_audioClips[1]);
    }

    public void PlayBadClip()
    {
        _audioSource.PlayOneShot(_audioClips[2]);
    }


    public void PlayMusicTrack()
    {
        _musicSource.PlayOneShot(_musicClips[_musicID]);
    }
    void ChooseNextMusicTrack()
    {
        _musicID++;
        if (_musicID > _musicClips.Length - 1)
            _musicID = 0;
        PlayMusicTrack();
    }
}
