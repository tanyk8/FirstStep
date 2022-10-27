using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    [SerializeField] TextMeshProUGUI musicVolValue;
    [SerializeField] TextMeshProUGUI effectVolValue;

    [SerializeField] TextMeshProUGUI muteMusicText;
    [SerializeField] TextMeshProUGUI muteEffectText;

    [SerializeField] private AudioSource musicSource, effectSource;

    [SerializeField] public Button backsettingbtn;
    [SerializeField] public GameObject muteMusicObj;

    
    public float tempMusicVolume = 0f;
    public float tempEffectVolume = 0f;
    public bool tempMuteMusic = true;
    public bool tempMuteEffect= true;

    SettingsData currentSettings;

    [SerializeField] public GameObject settingsobj;
    [SerializeField] public GameObject settings;

    public bool loadfromtitle = false;
    public int loadfromtitleindex = 0;
    

    private void Awake()
    {
        if (!File.Exists(Application.dataPath + Path.AltDirectorySeparatorChar + "save" + Path.AltDirectorySeparatorChar + "settings.json"))
        {
            createDefault();
        }
        else
        {
            currentSettings= new SettingsData();
            currentSettings.loadSetting();
            updateSettings(currentSettings);
            setTempValue(currentSettings);
        }

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(settingsobj);
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    public static SoundManager GetInstance()
    {
        return instance;
    }

    public void playSE(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }

    public void playMusic(AudioClip clip)
    {
        musicSource.clip = clip;
    }

    public void playmuzic()
    {
        musicSource.Play();
    }

    public void setTempValue(SettingsData tempdata)
    {
        tempMusicVolume=tempdata.currentMusicVolume;
        tempEffectVolume = tempdata.currentEffectVolume;
        tempMuteMusic = tempdata.muteMusic;
        tempMuteEffect = tempdata.muteEffect;
    }

    public void updateUI()
    {
        musicVolValue.text = (tempMusicVolume * 100) + "%";
        effectVolValue.text = (tempEffectVolume * 100) + "%";

        if (tempMuteMusic)
        {
            muteMusicText.text = "Music OFF";
        }
        else
        {
            muteMusicText.text = "Music ON";
        }

        if (tempMuteEffect)
        {
            muteEffectText.text = "Effect OFF";
        }
        else
        {
            muteEffectText.text = "Effect ON";
        }
    }

    public void changeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void toggleMusic()
    {
        musicSource.mute = !musicSource.mute;
        if (musicSource.mute)
        {
            muteMusicText.text = "Music OFF";
            tempMuteMusic = true;
        }
        else
        {
            muteMusicText.text = "Music ON";
            tempMuteMusic = false;
        }
    }

    public void toggleEffect()
    {
        effectSource.mute = !effectSource.mute;
        if (effectSource.mute)
        {
            muteEffectText.text = "Effect OFF";
            tempMuteEffect = true;
        }
        else
        {
            muteEffectText.text = "Effect ON";
            tempMuteEffect = false;
        }
    }

    public void changeMusicVolume()
    {
        

        switch (tempMusicVolume)
        {
            case 0f:
                tempMusicVolume = 0.2f;
                break;
            case 0.2f:
                tempMusicVolume = 0.4f;
                break;
            case 0.4f:
                tempMusicVolume = 0.6f;
                break;
            case 0.6f:
                tempMusicVolume = 0.8f;
                break;
            case 0.8f:
                tempMusicVolume = 1f;
                break;
            case 1f:
                tempMusicVolume = 0f;
                break;
        }
        musicVolValue.text = tempMusicVolume * 100 + "%";
        musicSource.volume = tempMusicVolume;
    }

    public void changeEffectVolume()
    {
        switch (tempEffectVolume)
        {
            case 0f:
                tempEffectVolume = 0.2f;
                break;
            case 0.2f:
                tempEffectVolume = 0.4f;
                break;
            case 0.4f:
                tempEffectVolume = 0.6f;
                break;
            case 0.6f:
                tempEffectVolume = 0.8f;
                break;
            case 0.8f:
                tempEffectVolume = 1f;
                break;
            case 1f:
                tempEffectVolume = 0f;
                break;
        }
        effectVolValue.text = tempEffectVolume * 100 + "%";
        effectSource.volume = tempEffectVolume;
    }

    public void updateSettings(SettingsData data)
    {
        Debug.Log(data.currentMusicVolume+"|"+ data.currentEffectVolume+"|"+ data.muteMusic+"|"+ data.muteEffect); 

        musicSource.volume = data.currentMusicVolume;
        effectSource.volume = data.currentEffectVolume;
        musicSource.mute = data.muteMusic;
        effectSource.mute = data.muteEffect;
    }

    public void revertValue()
    {
        musicSource.volume =currentSettings.currentMusicVolume;
        effectSource.volume = currentSettings.currentEffectVolume;
        musicSource.mute = currentSettings.muteMusic;
        effectSource.mute = currentSettings.muteEffect;

        setTempValue(currentSettings);
        updateUI();
        
    }

    public void btnApply()
    {
        

        musicSource.volume = tempMusicVolume;
        effectSource.volume = tempEffectVolume;
        musicSource.mute = tempMuteMusic;
        effectSource.mute = tempMuteEffect;

        currentSettings.currentMusicVolume = tempMusicVolume;
        currentSettings.currentEffectVolume = tempEffectVolume;
        currentSettings.muteMusic = tempMuteMusic;
        currentSettings.muteEffect = tempMuteEffect;

        updateUI();

        SavetoSFile();

    }

    public void btnDefault()
    {
        tempMusicVolume = 1f;
        tempEffectVolume = 1f;
        tempMuteMusic = false;
        tempMuteEffect = false;

        btnApply();
    }

    public void SavetoSFile()
    {
        string savePath = Application.dataPath + Path.AltDirectorySeparatorChar + "save" + Path.AltDirectorySeparatorChar + "settings.json";

        SettingsData settingdata = new SettingsData();
        settingdata.currentMusicVolume = currentSettings.currentMusicVolume;
        settingdata.currentEffectVolume = currentSettings.currentEffectVolume;
        settingdata.muteMusic = currentSettings.muteMusic;
        settingdata.muteEffect = currentSettings.muteEffect;

        Debug.Log("Data saved at " + savePath);
        string json = JsonUtility.ToJson(settingdata);
        Debug.Log(json);

        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
    }

    public void createDefault()
    {
        string savePath = Application.dataPath + Path.AltDirectorySeparatorChar + "save" + Path.AltDirectorySeparatorChar + "settings.json";

        SettingsData settingdata = new SettingsData();

        Debug.Log("Data saved at " + savePath);
        string json = JsonUtility.ToJson(settingdata);
        Debug.Log(json);

        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
    }
}

[System.Serializable]
public class SettingsData
{
    public float currentMusicVolume;
    public float currentEffectVolume;

    public bool muteMusic;
    public bool muteEffect;

    public SettingsData()
    {
        currentMusicVolume = 1f;
        currentEffectVolume = 1f;
        muteMusic = false;
        muteEffect = false;

    }

    public void updateSetting(SettingsData data)
    {
        currentMusicVolume = data.currentMusicVolume;
        currentEffectVolume = data.currentEffectVolume;

        muteMusic = data.muteMusic;
        muteEffect = data.muteEffect;

    }

    public void loadSetting()
    {
        string path = Application.dataPath + Path.AltDirectorySeparatorChar + "save" + Path.AltDirectorySeparatorChar + "settings.json";

        using StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();

        SettingsData data = JsonUtility.FromJson<SettingsData>(json);

        updateSetting(data);

        Debug.Log(json);
    }

}
