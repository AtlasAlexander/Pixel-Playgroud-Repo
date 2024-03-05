using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using FMOD.Studio;

using System;
using System.Data.SqlTypes;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class FmodAudioManager : MonoBehaviour
{
    [Header("Volume")]
    [Range(0, 1)]
    public float masterVolume = 1;

    [Range(0, 1)]
    public float soundEffectsVolume = 1;

    [Range(0, 1)]
    public float musicVolume = 1;

    private string[] UnityBuildWorkAround =
    {
        "footsteps","shootShrinkRay","objectShrink","objectGrow",
        "buttonClick","closeDoor","openDoor","gatePassthrough",
        "roomAmbience","playerGrow","playerShrink","laserOn",
        "menuSelection","pause","gameTheme-StuckInTheWormHole",
        "shootGrowthRay","static","laserConstant","navigateMenu",
        "Cutscene1"

    };

    private Bus masterBus;
    private Bus sfxBus;
    private Bus musicBus;

    public EventReference[] gameplaySounds;     //Creates the array of sounds so that sounds can be easily added from the inspector


    FMOD.Studio.EventDescription eventDescription;


    [SerializeField] float footstepsRate;       //changes the speed of footsteps
    [SerializeField] GameObject player;         
    [SerializeField] FirstPersonController controller;

    float time;

    public Slider musicSlider;
    public Slider sfxSlider;

    private float navigationTimer;

    EventInstance menuMusic;

    private void Awake()
    {
        menuMusic = FMODUnity.RuntimeManager.CreateInstance(gameplaySounds[FindEventReferenceByName("gameTheme-StuckInTheWormHole")]);
        navigationTimer = 0.0f;

        masterBus = RuntimeManager.GetBus("bus:/");
        sfxBus = RuntimeManager.GetBus("bus:/SoundEffects");
        musicBus = RuntimeManager.GetBus("bus:/Music");

        
    }

    private void Start()
    {
        
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        
        if (sceneName == "Main Scene 1")
        {
            FMOD.Studio.PLAYBACK_STATE playbackState; 
            
            menuMusic.getPlaybackState(out playbackState);

        
            if (!playbackState.ToString().Contains("PLAYING")){
                
                menuMusic.start();
                FMODUnity.RuntimeManager.AttachInstanceToGameObject(menuMusic, GameObject.FindWithTag("MainCamera").transform);
            }
           
            


        }
        else
        {
            QuickPlaySound("roomAmbience", player);
        }
    }

    public void QuickPlaySound(string soundName, GameObject soundSource) 
    {
        //Use this function form any script to play a sound
        //Use the name of the sound in Assets/Sounds for soundName
        //Pass in the object you want the sound to play from into soundSource

        RuntimeManager.PlayOneShotAttached(gameplaySounds[FindEventReferenceByName(soundName)], soundSource);     
    }

    public int FindEventReferenceByName(string eventName)  //Finds the position of a sound name in the gameplaySounds Array
    {
        int soundIndex = 0;
        //foreach (EventReference eventRef in gameplaySounds)
        foreach (string eventRef in UnityBuildWorkAround)
        {

            
            //string soundName = eventRef.Path.Replace("event:/GameSoundEffects/", "");
            //if (soundName == eventName)
            // {
            if (eventRef.Contains(eventName))
            {
                
                return soundIndex;
            }
            soundIndex++;
        }
        Debug.Log("Sound: " + eventName + " not found in the gameplaySounds array.");
        return -1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            killMusic();                 //Temporary method of playing/pausing until we find another way
        }
        navigationTimer += Time.deltaTime;

        masterBus.setVolume(masterVolume);
        sfxBus.setVolume(soundEffectsVolume);
        musicBus.setVolume(musicVolume);

        time += Time.deltaTime;
        if (controller.isWalking)    //controls player footsteps
        {
            if (time >= footstepsRate)
            {
                QuickPlaySound("footsteps", player);
                time = 0;
            }
        }
    }


    public void SetFootstepsRate(float rate)
    {

        footstepsRate = rate;
    }

    public void MusicSliderChanged(float volume)
    {
        if (navigationTimer > 0.15f)
        {
            QuickPlaySound("navigateMenu", player);
            musicVolume = volume;
            navigationTimer = 0;
        }
        
    }

    public void SFXSliderChanged(float volume)
    {
        if (navigationTimer > 0.15f)
        {
            QuickPlaySound("navigateMenu", player);
            soundEffectsVolume = volume;
            navigationTimer = 0;
        }
    }

    public void MasterSliderChanged(float volume)
    {
        if (navigationTimer > 0.15f)
        {
            QuickPlaySound("navigateMenu", player);
            masterVolume = volume;
            navigationTimer = 0;
        }
    }

    public void killMusic()
    {
        menuMusic.setVolume(0);
    }

}
