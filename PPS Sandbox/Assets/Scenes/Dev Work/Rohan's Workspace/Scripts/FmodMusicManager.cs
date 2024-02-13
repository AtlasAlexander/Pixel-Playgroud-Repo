using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmodMusicManager : MonoBehaviour
{
    public EventReference[] tracks;
    EventInstance songPlaying;

    public int songIndex;
    public bool paused;

    private void Awake()
    {
        songIndex = 0;
        paused = true;
    }

    private void Update()
    {
        FMOD.Studio.PLAYBACK_STATE playbackState;        //Finds if a song is currently playing
        songPlaying.getPlaybackState(out playbackState);

        if(Input.GetKeyDown(KeyCode.I))
        {
            togglePause();                 //Temporary method of playing/pausing until we find another way
        }

        if (playbackState.ToString() == "STOPPED" && paused == false)   //Plays the next track when a song ends
        {
            songPlaying.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            songPlaying.clearHandle();
            songPlaying.release();
            songIndex = songIndex+ 1;
            if (songIndex > tracks.Length - 1)
            { songIndex = 0; }
            songPlaying = FMODUnity.RuntimeManager.CreateInstance(tracks[songIndex]);
            songPlaying.start();
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(songPlaying, GetComponent<Transform>(), GetComponent<Rigidbody>());
        }
    }

    public void togglePause()
    {
        if (paused)  //Unpausing the game
        {
            songPlaying = FMODUnity.RuntimeManager.CreateInstance(tracks[songIndex]);
            songPlaying.start();
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(songPlaying, GetComponent<Transform>(), GetComponent<Rigidbody>());
            paused = false;
        }
        else         //Pausing the game
        {
            songPlaying.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            songIndex = songIndex + 1;
            if (songIndex > tracks.Length - 1)
            {songIndex = 0;}
            paused = true;
        }
    }

}