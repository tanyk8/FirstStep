using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Credit : MonoBehaviour
{
    public string creditprogress = "progress0";

    [SerializeField] PlayableDirector creditdirector;
    

    // Start is called before the first frame update
    void Start()
    {

        PlayableAsset cutscene = Resources.Load<PlayableAsset>("Timeline/Credit");
        creditdirector.Play(cutscene);
        creditprogress = "progress1";
        SoundManager.GetInstance().playMusic(Resources.Load<AudioClip>("Sound/Music/bgm_credit"));

        //play timeline
    }

    // Update is called once per frame
    void Update()
    {

        if (SoundManager.GetInstance().musicSource.clip.name != "bgm_credit")
        {

            SoundManager.GetInstance().playMusic(Resources.Load<AudioClip>("Sound/Music/bgm_credit"));
        }

        if (creditprogress == "progress1"&&creditdirector.state != PlayState.Playing)
        {
            SceneManager.LoadScene("TitleScreen");
        }
    }
}
