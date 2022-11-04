using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LevelTwoShadow : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject shadowaura_after;

    public bool continuestory = false;
    public bool callonce = true;

    // Start is called before the first frame update
    void Start()
    {
        if (GameStateManager.GetInstance().battleRun)
        {
            GameStateManager.GetInstance().battleRun = false;
            player.transform.position = GameStateManager.GetInstance().position;
            Debug.Log(GameStateManager.GetInstance().position);
        }
        else
        {
            player.transform.position = new Vector3(0.875f, -0.95f, 0);
        }

        if (ProgressManager.GetInstance().loaded)
        {
            ProgressManager.GetInstance().loaded = false;
            GameObject.Find("Player").transform.position = ProgressManager.GetInstance().loadedposition;
        }

        if (TimelineManager.GetInstance().getPlayState() != PlayState.Playing && SoundManager.GetInstance().musicSource.clip.name != "bgm_stage2")
        {

            SoundManager.GetInstance().playMusic(Resources.Load<AudioClip>("Sound/Music/bgm_stage2"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ProgressManager.GetInstance().loading)
        {
            return;
        }

        if (TimelineManager.GetInstance().getPlayState() != PlayState.Playing && !SoundManager.GetInstance().musicSource.isPlaying && SoundManager.GetInstance().musicSource.clip.name != "bgm_stage2")
        {

            SoundManager.GetInstance().playMusic(Resources.Load<AudioClip>("Sound/Music/bgm_stage2"));
        }

        if (!DialogueManager.GetInstance().dialogueIsPlaying && callonce)
        {
            if (!shadowaura_after.activeInHierarchy && DialogueVariableObserver.variables["mainquest_progress"].ToString() == "10"&& ProgressManager.GetInstance().gameProgress == "progress18")
            {
                shadowaura_after.SetActive(true);
            }
            else if (shadowaura_after.activeInHierarchy && DialogueVariableObserver.variables["mainquest_progress"].ToString() != "10" && ProgressManager.GetInstance().gameProgress != "progress18")
            {
                shadowaura_after.SetActive(false);
            }

            if (ProgressManager.GetInstance().gameProgress == "progress19" && DialogueVariableObserver.variables["mainquest_progress"].ToString() == "10")
            {
                callonce = false;
                ProgressManager.GetInstance().gameProgress = "progress20";
                PlayableAsset tempPlayableasset = Resources.Load<PlayableAsset>("Timeline/Memory2");
                TimelineManager.GetInstance().playTimeline(tempPlayableasset);
                SoundManager.GetInstance().playMusic(Resources.Load<AudioClip>("Sound/Music/bgm_memories"));
                callonce = true;
            }

            if (ProgressManager.GetInstance().gameProgress == "progress20" && DialogueVariableObserver.variables["mainquest_progress"].ToString() == "10" && TimelineManager.GetInstance().getPlayState() != PlayState.Playing)
            {
                callonce = false;
                ProgressManager.GetInstance().gameProgress = "progress21";
                SoundManager.GetInstance().playMusic(Resources.Load<AudioClip>("Sound/Music/bgm_stage2"));
                TextAsset textAsset = Resources.Load<TextAsset>("Story/MainStoryPart3");
                DialogueManager.GetInstance().notInteractDialogue = true;
                DialogueManager.GetInstance().EnterDialogueMode(textAsset);
                callonce = true;
            }
        }
    }
}
