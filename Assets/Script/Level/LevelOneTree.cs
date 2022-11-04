using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class LevelOneTree : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject shadowaura_after;

    //[SerializeField] GameObject director;

    public bool continuestory = false;
    public bool callonce = true;

    // Start is called before the first frame update
    void Start()
    {
        //var timelineAsset = TimelineManager.GetInstance().GetComponent<PlayableDirector>().playableAsset as TimelineAsset;
        //var trackList = timelineAsset.GetOutputTracks();
        //foreach (var track in trackList)
        //{
        //    // check to see if this is the one you are looking for (by name, index etc)
        //    if (track.name == "SQ1_setrack")
        //    {
        //        // bind the track to our new actor instance
        //        TimelineManager.GetInstance().GetComponent<PlayableDirector>().SetGenericBinding(track, SoundManager.GetInstance().effectSource);
        //    }
        //    else if (track.name == "SQ1_musictrack")
        //    {
        //        TimelineManager.GetInstance().GetComponent<PlayableDirector>().SetGenericBinding(track, SoundManager.GetInstance().musicSource);
        //    }

        //}


        if (GameStateManager.GetInstance().battleRun)
        {
            GameStateManager.GetInstance().battleRun = false;
            player.transform.position = GameStateManager.GetInstance().position;
            Debug.Log(GameStateManager.GetInstance().position);
        }
        else
        {
            player.transform.position = new Vector3(0, 0.2f, 0);
        }
        if (ProgressManager.GetInstance().loaded)
        {
            ProgressManager.GetInstance().loaded = false;
            GameObject.Find("Player").transform.position = ProgressManager.GetInstance().loadedposition;
        }
        if (SoundManager.GetInstance().musicSource.clip.name != "bgm_stage1")
        {

            SoundManager.GetInstance().playMusic(Resources.Load<AudioClip>("Sound/Music/bgm_stage1"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ProgressManager.GetInstance().loading)
        {
            return;
        }

        if (TimelineManager.GetInstance().getPlayState() != PlayState.Playing && !SoundManager.GetInstance().musicSource.isPlaying && SoundManager.GetInstance().musicSource.clip.name != "bgm_stage1")
        {

            SoundManager.GetInstance().playMusic(Resources.Load<AudioClip>("Sound/Music/bgm_stage1"));
        }

        if (!DialogueManager.GetInstance().dialogueIsPlaying && callonce)
        {

            if (!shadowaura_after.activeInHierarchy && DialogueVariableObserver.variables["mainquest_progress"].ToString() == "6")
            {
                shadowaura_after.SetActive(true);
            }
            else if (shadowaura_after.activeInHierarchy && DialogueVariableObserver.variables["mainquest_progress"].ToString() != "6")
            {
                shadowaura_after.SetActive(false);
            }

            if (ProgressManager.GetInstance().gameProgress == "progress9" && DialogueVariableObserver.variables["mainquest_progress"].ToString() == "6")
            {
                callonce = false;
                ProgressManager.GetInstance().gameProgress = "progress10";
                PlayableAsset tempPlayableasset = Resources.Load<PlayableAsset>("Timeline/Memory1");
                TimelineManager.GetInstance().playTimeline(tempPlayableasset);
                SoundManager.GetInstance().playMusic(Resources.Load<AudioClip>("Sound/Music/bgm_memories"));
                callonce = true;
                //after battle dialogue
                //set unactive enemy
            }

            if (ProgressManager.GetInstance().gameProgress == "progress10" && DialogueVariableObserver.variables["mainquest_progress"].ToString() == "6"&& TimelineManager.GetInstance().getPlayState()!=PlayState.Playing)
            {
                callonce = false;
                ProgressManager.GetInstance().gameProgress = "progress11";
                SoundManager.GetInstance().playMusic(Resources.Load<AudioClip>("Sound/Music/bgm_stage1"));
                TextAsset textAsset = Resources.Load<TextAsset>("Story/MainStoryPart2");
                DialogueManager.GetInstance().notInteractDialogue = true;
                DialogueManager.GetInstance().EnterDialogueMode(textAsset);
                callonce = true;
            }
        }

        
    }
}
