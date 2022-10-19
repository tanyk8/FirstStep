using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

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

    }

    // Update is called once per frame
    void Update()
    {
        if (ProgressManager.GetInstance().loading)
        {
            return;
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
                callonce = true;
                //after battle dialogue
                //set unactive enemy
            }

            if (ProgressManager.GetInstance().gameProgress == "progress10" && DialogueVariableObserver.variables["mainquest_progress"].ToString() == "6"&& TimelineManager.GetInstance().getPlayState()!=PlayState.Playing)
            {
                callonce = false;
                ProgressManager.GetInstance().gameProgress = "progress11";
                TextAsset textAsset = Resources.Load<TextAsset>("Story/MainStoryPart2");
                DialogueManager.GetInstance().notInteractDialogue = true;
                DialogueManager.GetInstance().EnterDialogueMode(textAsset);
                callonce = true;
            }
        }

        
    }
}
