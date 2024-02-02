using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] GameObject PauseOptionMenu;

    Slider MusicSlider;
    Slider SFXSlider;
    Slider XControllersensSlider;
    Slider YControllersensSlider;
    Slider XMousesensSlider;
    Slider YMousesensSlider;
    Toggle MusicMuteToggle;
    Toggle SFXMuteToggle;
    Toggle InvertLookToggle;


    bool invertLook;
    bool SFXMute;

    // Start is called before the first frame update
    void Start()
    {
        MusicSlider = PauseOptionMenu.transform.Find("Music VolumeSlider").GetComponent<Slider>();
        SFXSlider = PauseOptionMenu.transform.Find("SFX VolumeSlider").GetComponent<Slider>();
        XControllersensSlider = PauseOptionMenu.transform.Find("XSens Slider - Controller").GetComponent<Slider>();
        YControllersensSlider = PauseOptionMenu.transform.Find("YSens Slider - Controller").GetComponent<Slider>();
        XMousesensSlider = PauseOptionMenu.transform.Find("XSens Slider").GetComponent<Slider>();
        YMousesensSlider = PauseOptionMenu.transform.Find("YSens Slider - Controller").GetComponent<Slider>();
        MusicMuteToggle = PauseOptionMenu.transform.Find("MusicToggle").GetComponent<Toggle>();
        SFXMuteToggle = PauseOptionMenu.transform.Find("SFX Toggle").GetComponent<Toggle>();
        InvertLookToggle = PauseOptionMenu.transform.Find("InvertToggle").GetComponent<Toggle>();

        if (PlayerPrefs.HasKey("MusicVolume"))
            MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        if (PlayerPrefs.HasKey("SFXVolume"))
            SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        if (PlayerPrefs.HasKey("XMouseSensitivity"))
            XMousesensSlider.value = PlayerPrefs.GetFloat("XMouseSensitivity");
        if (PlayerPrefs.HasKey("YMouseSensitivity"))
            YMousesensSlider.value = PlayerPrefs.GetFloat("YMouseSensitivity");
        if (PlayerPrefs.HasKey("XControllerSensitivity"))
            XControllersensSlider.value = PlayerPrefs.GetFloat("XControllerSensitivity");
        if (PlayerPrefs.HasKey("YControllerSensitivity"))
            YControllersensSlider.value = PlayerPrefs.GetFloat("YControllerSensitivity");

        if (PlayerPrefs.HasKey("InvertLook"))
        {
            if (PlayerPrefs.GetString("InvertLook") == "true")
                invertLook = true;
            else
                invertLook = false;
            InvertLookToggle.isOn = invertLook;
        }

        if (PlayerPrefs.HasKey("MusicMute"))
        {
            if (PlayerPrefs.GetString("MusicMute") == "true")
                MusicMuteToggle.isOn = true;
            else
                MusicMuteToggle.isOn = false;
        }
        if (PlayerPrefs.HasKey("SFXMute"))
        {
            if (PlayerPrefs.GetString("SFXMute") == "true")
                SFXMute = true;
            else
                SFXMute = false;
            SFXMuteToggle.isOn = SFXMute;
        }

    }

    public void SaveMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void SaveSFXVolume(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    public void SaveMusicMute(bool value)
    {
        PlayerPrefs.SetString("MusicMute", value.ToString());
    }

    public void SaveSFXMute(bool value)
    {
        PlayerPrefs.SetString("SFXMute", value.ToString());
    }

    public void SaveXMouseSens(float value)
    {
        PlayerPrefs.SetFloat("XMouseSensitivity", value);
    }
    public void SaveYMouseSens(float value)
    {
        PlayerPrefs.SetFloat("YMouseSensitivity", value);
    }
    public void SaveXControllerSens(float value)
    {
        PlayerPrefs.SetFloat("XControllerSensitivity", value);
    }
    public void SaveYControllerSens(float value)
    {
        PlayerPrefs.SetFloat("YControllerSensitivity", value);
    }

    public void SaveInvertLook(bool value)
    {
        PlayerPrefs.SetString("InvertLook", value.ToString());
    }
}
