using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Audio_Manager : MonoBehaviour
{
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

    AudioSource _audioSource;
    [SerializeField] private AudioClip[] _audioClips;

    //BUILT-IN FUNCTIONS
    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0.139f;
    }

    void Update()
    {
        
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

}
