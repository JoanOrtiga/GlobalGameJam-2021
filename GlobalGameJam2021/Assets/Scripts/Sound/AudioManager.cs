using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour
{
    private AudioSource music;
    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private AudioClip[] attackClips;
    private int currentIndex = 0;
    private static float musicVolume = 1;
    private static float sfxVolume = 1;

    private Slider musicSlider;
    private Slider sfxSlider;

    //singleton
    public static AudioManager audioInstance;

    void Awake()
    {
        GetComponent<AudioLowPassFilter>().enabled = false;
        music = GetComponent<AudioSource>();
        
        if (audioInstance == null)
        {

            audioInstance = this;
            DontDestroyOnLoad(audioInstance);
            music.loop = true;
            SetMusicClip(musicClips[currentIndex]);
            music.Play();
            musicSlider = GameObject.FindGameObjectWithTag("MusicSlider").GetComponent<Slider>();
            
            musicSlider.value = musicVolume;
            sfxSlider = GameObject.FindGameObjectWithTag("SFXSlider").GetComponent<Slider>();
            sfxSlider.value = sfxVolume;
        }
        else
        {
            Destroy(gameObject);
        }

       
    }

    private void Start()
    {
        this.enabled = true;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += LevelLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LevelLoaded;
    }

    private void Update()
    {
        if (musicSlider.value != musicVolume)
        {
            musicVolume = musicSlider.value;
        }
        if (sfxSlider.value != sfxVolume)
        {
            sfxVolume = sfxSlider.value;
        }

        if (SceneManager.GetActiveScene().buildIndex == 0)
            currentIndex = 0;
    }
    private void SetMusicClip(AudioClip clip)
    {
        music.clip = clip;
        music.Play();
    }
    public void IsBeingAttacked()
    {
        print(currentIndex);
        SetMusicClip(attackClips[currentIndex % attackClips.Length]);
    }
    public void StopAttacking()
    {
        SetMusicClip(musicClips[currentIndex]);
    }
    public void IsHide()
    {
        GetComponent<AudioLowPassFilter>().enabled = true;
    }
    public void StopHiding()
    {
        GetComponent<AudioLowPassFilter>().enabled = false;
    }

    private void LevelLoaded(Scene scene, LoadSceneMode mode)
    {
        print("Xd");

        currentIndex++;
        SetMusicClip(musicClips[currentIndex]);
    }
}
