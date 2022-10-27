using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineManager : MonoBehaviour
{
    private PlayableDirector director;
    private static TimelineManager instance;

    public bool dontmove = false;

    [Header("Reference")]
    [SerializeField] public TextMeshProUGUI overlayText;
    [SerializeField] public TextMeshProUGUI dialogText;
    [SerializeField] public TextMeshProUGUI monologText;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();

        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "TitleScreen")
        {
            Destroy(gameObject);
        }
    }

    public static TimelineManager GetInstance()
    {
        return instance;
    }

    public void playTimeline(PlayableAsset playableAsset)
    {
        director.Play(playableAsset);
    }

    public PlayState getPlayState()
    {
        return director.state;
    }
}
