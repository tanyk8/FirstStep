using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Ink.Runtime;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;

public class LevelZero : MonoBehaviour
{
    [SerializeField] GameObject controlsPanel;
    [SerializeField] GameObject closeControlsBtn;

    [SerializeField] GameObject obj_void1;
    [SerializeField] GameObject obj_void2;
    [SerializeField] GameObject obj_void3;

    [SerializeField] GameObject obj_gate1;
    [SerializeField] GameObject obj_gate2;
    [SerializeField] GameObject obj_gate3;

    [SerializeField] GameObject obj_openedgate1;
    [SerializeField] GameObject obj_openedgate2;
    [SerializeField] GameObject obj_openedgate3;
    [SerializeField] GameObject obj_openedgate31;

    [SerializeField] GameObject obj_crystal;

    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject Toverlay;
    [SerializeField] Animator Toverlayanimator;

    [SerializeField] GameObject mainenemyObj;
    [SerializeField] Animator mainenemyAnimator;
    [SerializeField] Animator darkauraAnimator;

    [SerializeField] GameObject magicObj;
    [SerializeField] Animator magicAnimator;

    public bool continuestory = false;
    public bool callonce = true;

    private void Start()
    {
        PlayableAsset magicatt = Resources.Load<PlayableAsset>("Timeline/MagicAttack");
        TimelineManager.GetInstance().GetComponent<PlayableDirector>().playableAsset = magicatt;

        var timelineAsset=TimelineManager.GetInstance().GetComponent<PlayableDirector>().playableAsset as TimelineAsset;
        var trackList = timelineAsset.GetOutputTracks();

        foreach (var track in trackList)
        {
            // check to see if this is the one you are looking for (by name, index etc)
            if (track.name=="MagicActivate")
            {
                // bind the track to our new actor instance
                TimelineManager.GetInstance().GetComponent<PlayableDirector>().SetGenericBinding(track, magicObj);
            }
            else if(track.name == "MagicAnimation")
            {
                TimelineManager.GetInstance().GetComponent<PlayableDirector>().SetGenericBinding(track, magicAnimator);
            }
            else if (track.name == "MagicAudio")
            {
                TimelineManager.GetInstance().GetComponent<PlayableDirector>().SetGenericBinding(track, SoundManager.GetInstance().effectSource);
            }

        }

        PlayableAsset openingcutscene = Resources.Load<PlayableAsset>("Timeline/Opening");
        TimelineManager.GetInstance().GetComponent<PlayableDirector>().playableAsset = openingcutscene;

        timelineAsset = TimelineManager.GetInstance().GetComponent<PlayableDirector>().playableAsset as TimelineAsset;
        trackList = timelineAsset.GetOutputTracks();

        foreach (var track in trackList)
        {
            // check to see if this is the one you are looking for (by name, index etc)
            if (track.name == "Player")
            {
                TimelineManager.GetInstance().GetComponent<PlayableDirector>().SetGenericBinding(track, player);
            }

        }

        Toverlay =GameObject.Find("TransitionOverlay").gameObject;
        Toverlayanimator = GameObject.Find("TransitionOverlay").GetComponent<Animator>();

        if (ProgressManager.GetInstance().gameProgress == "opening")
        {
            PlayableAsset opCutscene = Resources.Load<PlayableAsset>("Timeline/Opening");
            TimelineManager.GetInstance().playTimeline(opCutscene);
            ProgressManager.GetInstance().gameProgress = "progress1";
        }

        if (ProgressManager.GetInstance().loaded)
        {
            ProgressManager.GetInstance().loaded = false;
            GameObject.Find("Player").transform.position = ProgressManager.GetInstance().loadedposition;
        }

        //Based on game state manager 
        //if last scene name is park then set position at gate 1
        else if(GameStateManager.GetInstance().updateOnce == true)
        {
            GameStateManager.GetInstance().updateOnce = false;

            if (GameStateManager.GetInstance().lastscene == "FirstStage_Park")
            {
                player.transform.position = new Vector3(-0.885f, 0.65f, 0);
            }
            else if (GameStateManager.GetInstance().lastscene == "SecondStage_Hallway")
            {
                player.transform.position = new Vector3(0.885f, 0.65f, 0);
            }
            else if (GameStateManager.GetInstance().lastscene == "ThirdStage_hospitalhall")
            {
                player.transform.position = new Vector3(0f, 1.9f, 0);
            }

        }

        if (TimelineManager.GetInstance().getPlayState() != PlayState.Playing && SoundManager.GetInstance().musicSource.clip.name != "bgm_lobby")
        {

            SoundManager.GetInstance().playMusic(Resources.Load<AudioClip>("Sound/Music/bgm_lobby"));
        }


    }
    private void LateUpdate()
    {
        if (ProgressManager.GetInstance().loading)
        {
            return;
        }

        if(TimelineManager.GetInstance().getPlayState() != PlayState.Playing && !SoundManager.GetInstance().musicSource.isPlaying&&SoundManager.GetInstance().musicSource.clip.name!="bgm_lobby")
        {

            SoundManager.GetInstance().playMusic(Resources.Load<AudioClip>("Sound/Music/bgm_lobby"));
        }

        if (TimelineManager.GetInstance().getPlayState() != PlayState.Playing && ProgressManager.GetInstance().gameProgress == "progress1")
        {
            ProgressManager.GetInstance().gameProgress = "progress2p";
            TextAsset textAsset = Resources.Load<TextAsset>("Story/MainStoryPart1");
            DialogueManager.GetInstance().notInteractDialogue = true;
            DialogueManager.GetInstance().EnterDialogueMode(textAsset);
            ProgressManager.GetInstance().gameProgress = "progress2p2";
        }

        if (!DialogueManager.GetInstance().dialogueIsPlaying&& TimelineManager.GetInstance().getPlayState() != PlayState.Playing&& ProgressManager.GetInstance().gameProgress == "progress2p2")
        {
            ProgressManager.GetInstance().gameProgress = "progress2";
            controlsPanel.SetActive(true);
            StartCoroutine(ListLayout.selectOption(closeControlsBtn));
        }

        if (!DialogueManager.GetInstance().dialogueIsPlaying&&callonce)
        {
            if(obj_void1.activeInHierarchy&&QuestManager.GetInstance().questlist.Count != 0 && QuestManager.GetInstance().questlist.ElementAt(0).questState != QuestState.INPROGRESS)
            {
                obj_void1.SetActive(false);
                obj_void2.SetActive(false);
                obj_void3.SetActive(false);

                obj_gate1.SetActive(false);
                obj_gate2.SetActive(false);
                obj_gate3.SetActive(false);

                //obj_crystal.transform.GetChild(0).gameObject.SetActive(false);
                //obj_crystal.transform.GetChild(1).gameObject.SetActive(false);
            }
            else if (QuestManager.GetInstance().questlist.Count != 0 && QuestManager.GetInstance().questlist.ElementAt(0).questState == QuestState.INPROGRESS && QuestManager.GetInstance().questlist.ElementAt(0).quest_progress == 1)
            {
                if (!obj_void1.activeInHierarchy && DialogueVariableObserver.variables["void_checked"].ToString() == "false")
                {

                    obj_void1.SetActive(true);
                    obj_void2.SetActive(true);
                    obj_void3.SetActive(true);
                }
                else if (obj_void1.activeInHierarchy && DialogueVariableObserver.variables["void_checked"].ToString() == "true")
                {
                    obj_void1.SetActive(false);
                    obj_void2.SetActive(false);
                    obj_void3.SetActive(false);
                }
                if (!obj_gate1.activeInHierarchy && DialogueVariableObserver.variables["gate_checked"].ToString() == "false")
                {
                    obj_gate1.SetActive(true);
                    obj_gate2.SetActive(true);
                    obj_gate3.SetActive(true);
                }
                else if (obj_gate1.activeInHierarchy && DialogueVariableObserver.variables["gate_checked"].ToString() == "true")
                {
                    obj_gate1.SetActive(false);
                    obj_gate2.SetActive(false);
                    obj_gate3.SetActive(false);
                }
                //if (!obj_crystal.transform.GetChild(0).gameObject.activeInHierarchy && DialogueVariableObserver.variables["crystal_checked"].ToString() == "false")
                //{
                //    obj_crystal.transform.GetChild(0).gameObject.SetActive(true);
                //    obj_crystal.transform.GetChild(1).gameObject.SetActive(true);
                //}
                //else if (obj_crystal.transform.GetChild(0).gameObject.activeInHierarchy && DialogueVariableObserver.variables["crystal_checked"].ToString() == "true")
                //{
                //    obj_crystal.transform.GetChild(0).gameObject.SetActive(false);
                //    obj_crystal.transform.GetChild(1).gameObject.SetActive(false);
                //}

            }

            if (!obj_openedgate1.activeInHierarchy&&DialogueVariableObserver.variables["gate_portal1"].ToString() == "true")
            {
                obj_openedgate1.SetActive(true);
            }
            else if (obj_openedgate1.activeInHierarchy && DialogueVariableObserver.variables["gate_portal1"].ToString() == "false")
            {
                obj_openedgate1.SetActive(false);
            }

            if(!obj_openedgate2.activeInHierarchy&& DialogueVariableObserver.variables["gate_portal2"].ToString() == "true")
            {
                obj_openedgate2.SetActive(true);
            }
            else if(obj_openedgate2.activeInHierarchy && DialogueVariableObserver.variables["gate_portal2"].ToString() == "false")
            {
                obj_openedgate2.SetActive(false);
            }

            if (!obj_openedgate3.activeInHierarchy && DialogueVariableObserver.variables["gate_portal3"].ToString() == "true")
            {
                obj_openedgate3.SetActive(true);
                obj_openedgate31.SetActive(true);
            }
            else if (obj_openedgate3.activeInHierarchy && DialogueVariableObserver.variables["gate_portal3"].ToString() == "false")
            {
                obj_openedgate3.SetActive(false);
                obj_openedgate31.SetActive(false);
            }

            if (ProgressManager.GetInstance().gameProgress == "progress2" && DialogueVariableObserver.variables["mainquest_progress"].ToString() == "1")
            {
                if (QuestManager.GetInstance().questlist.Count != 0 && QuestManager.GetInstance().questlist.ElementAt(0).quest_progress == 1)
                {
                    if (QuestManager.GetInstance().questlist.ElementAt(0).quest_progressvalue == QuestManager.GetInstance().questlist.ElementAt(0).questData.quest_progress[0].requirement)
                    {
                        callonce = false;
                        QuestManager.GetInstance().questlist.ElementAt(0).quest_progress = 2;
                        continuestory = false;

                        QuestManager.GetInstance().updateQuestProgress(QuestManager.GetInstance().questlist.ElementAt(0).questData, "force");
                        StartCoroutine(fadeTransition("moveplayer"));
                    }
                }

                else if (continuestory && QuestManager.GetInstance().questlist.ElementAt(0).quest_progress==2)
                {
                    callonce = false;
                    continuestory = false;
                    TextAsset textAsset = Resources.Load<TextAsset>("Story/MainStoryPart1");
                    DialogueManager.GetInstance().notInteractDialogue = true;
                    DialogueManager.GetInstance().EnterDialogueMode(textAsset);
                    TimelineManager.GetInstance().dontmove = false;
                    ProgressManager.GetInstance().gameProgress = "progress3";
                    callonce = true;
                }

            }

            if(ProgressManager.GetInstance().gameProgress == "progress3" && DialogueVariableObserver.variables["mainquest_progress"].ToString() == "2")
            {
                if (!continuestory)//QuestManager.GetInstance().questlist.ElementAt(0).questState==QuestState.COMPLETED 
                {
                    callonce = false;
                    StartCoroutine(fadeTransition("moveplayer2"));    
                }
                else if (continuestory)//QuestManager.GetInstance().questlist.ElementAt(0).questState==QuestState.COMPLETED && DialogueVariableObserver.variables["mainquest_progress"].ToString() == "2"
                {
                    callonce = false;
                    continuestory = false;
                    TextAsset textAsset = Resources.Load<TextAsset>("Story/MainStoryPart1");
                    DialogueManager.GetInstance().notInteractDialogue = true;
                    DialogueManager.GetInstance().EnterDialogueMode(textAsset);
                    TimelineManager.GetInstance().dontmove = false;

                    ProgressManager.GetInstance().gameProgress = "progress4";
                    callonce = true;
                }

                
            }

            if (ProgressManager.GetInstance().gameProgress == "progress4"&&DialogueVariableObserver.variables["mainquest_progress"].ToString() == "3")
            {

                callonce = false;
                continuestory = false;
                obj_openedgate1.SetActive(true);
                Debug.Log("open gate");
                ProgressManager.GetInstance().gameProgress = "progress5";
                callonce = true;
                
            }

        }
        
    }

    public IEnumerator fadeTransition(string type)
    {
        TimelineManager.GetInstance().dontmove = true;
        Toverlay.SetActive(true);
        //if (overlayanimator.GetCurrentAnimatorStateInfo(0).IsName("fadeout"))
        //{

        //}
        Debug.Log("fadein");

        Toverlayanimator.SetTrigger("Fadein");
        yield return new WaitForSeconds(1.5f);
        Toverlayanimator.ResetTrigger("Fadein");
        addActions(type);
        Toverlayanimator.SetTrigger("Fadeout");
        yield return new WaitForSeconds(1.5f);
        Toverlayanimator.ResetTrigger("Fadeout");

        Debug.Log("fadeout");

        continuestory = true;
        callonce = true;
        Toverlayanimator.SetTrigger("Reset");
        yield return new WaitForSeconds(1.5f);
        Toverlayanimator.ResetTrigger("Reset");
        

    }

    public void addActions(string type)
    {
        if (type == "moveplayer")
        {
            player.transform.position = new Vector3(0, 0.5f, 0);
        }
        else if (type == "moveplayer2")
        {

            mainenemyAnimator.SetTrigger("Fadeout");
            darkauraAnimator.SetTrigger("Fadeout");

            player.transform.position = new Vector3(0, -0.5f, 0);
        }
    }

    public void performAction(string type)
    {
        if (type == "mainenemyappear")
        {
            mainenemyObj.SetActive(true);
            darkauraAnimator.SetTrigger("Fadein");
            mainenemyAnimator.SetTrigger("Fadein");
        }
    }

    public void onCloseControlsBtn()
    {
        controlsPanel.SetActive(false);
        SoundManager.GetInstance().playSE(Resources.Load<AudioClip>("Sound/SE/submit"));
    }
}
