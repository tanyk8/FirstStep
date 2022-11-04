using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class LevelThreeShadow : MonoBehaviour
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
            player.transform.position = new Vector3(0.08f, -0.8f, 0);
        }

        if (ProgressManager.GetInstance().loaded)
        {
            ProgressManager.GetInstance().loaded = false;
            GameObject.Find("Player").transform.position = ProgressManager.GetInstance().loadedposition;
        }

        if (TimelineManager.GetInstance().getPlayState() != PlayState.Playing  && SoundManager.GetInstance().musicSource.clip.name != "bgm_shadow")
        {

            SoundManager.GetInstance().playMusic(Resources.Load<AudioClip>("Sound/Music/bgm_shadow"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ProgressManager.GetInstance().loading)
        {
            return;
        }

        if (TimelineManager.GetInstance().getPlayState() != PlayState.Playing && SoundManager.GetInstance().musicSource.clip.name != "bgm_shadow")
        {

            SoundManager.GetInstance().playMusic(Resources.Load<AudioClip>("Sound/Music/bgm_shadow"));
        }

        if (!DialogueManager.GetInstance().dialogueIsPlaying && callonce && TimelineManager.GetInstance().getPlayState() != PlayState.Playing)
        {
            //if (!shadowaura_after.activeInHierarchy && DialogueVariableObserver.variables["mainquest_progress"].ToString() == "10" && ProgressManager.GetInstance().gameProgress == "progress18")
            //{
            //    shadowaura_after.SetActive(true);
            //}
            //else if (shadowaura_after.activeInHierarchy && DialogueVariableObserver.variables["mainquest_progress"].ToString() != "10" && ProgressManager.GetInstance().gameProgress != "progress18")
            //{
            //    shadowaura_after.SetActive(false);
            //}

            if (ProgressManager.GetInstance().gameProgress == "progress23" && DialogueVariableObserver.variables["mainquest_progress"].ToString() == "14")
            {
                callonce = false;
                ProgressManager.GetInstance().gameProgress = "progress24";
                TextAsset textAsset = Resources.Load<TextAsset>("Story/MainStoryPart4");
                DialogueManager.GetInstance().notInteractDialogue = true;
                DialogueManager.GetInstance().EnterDialogueMode(textAsset);
                callonce = true;
            }

            if (ProgressManager.GetInstance().gameProgress == "progress25" && DialogueVariableObserver.variables["mainquest_progress"].ToString() == "15")
            {
                callonce = false;
                ProgressManager.GetInstance().gameProgress = "progress26";
                PlayableAsset tempPlayableasset = Resources.Load<PlayableAsset>("Timeline/Memory3");
                TimelineManager.GetInstance().playTimeline(tempPlayableasset);
                callonce = true;
            }

            

            if (ProgressManager.GetInstance().gameProgress == "progress26" && DialogueVariableObserver.variables["mainquest_progress"].ToString() == "15" && TimelineManager.GetInstance().getPlayState() != PlayState.Playing)
            {
                callonce = false;
                ProgressManager.GetInstance().gameProgress = "progress27";
                TextAsset textAsset = Resources.Load<TextAsset>("Story/MainStoryPart4");
                DialogueManager.GetInstance().notInteractDialogue = true;
                DialogueManager.GetInstance().EnterDialogueMode(textAsset);
                callonce = true;
            }

            if (ProgressManager.GetInstance().gameProgress == "progress28" && DialogueVariableObserver.variables["mainquest_progress"].ToString() == "16" && !DialogueManager.GetInstance().dialogueIsPlaying)
            {
                SceneManager.LoadScene("Ending");
            }

            //if (ProgressManager.GetInstance().gameProgress == "progress28" && DialogueVariableObserver.variables["mainquest_progress"].ToString() == "14")
            //{
            //    callonce = false;
            //    ProgressManager.GetInstance().gameProgress = "progress29";
            //    PlayableAsset tempPlayableasset = Resources.Load<PlayableAsset>("Timeline/Ending");
            //    TimelineManager.GetInstance().playTimeline(tempPlayableasset);
            //    callonce = true;
            //}
            //battle again
            //cut scene
            //ending
        }
    }
}
