using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Ink.Runtime;
using UnityEngine.Playables;

public class LevelZero : MonoBehaviour
{
    [SerializeField] GameObject obj_void1;
    [SerializeField] GameObject obj_void2;
    [SerializeField] GameObject obj_void3;

    [SerializeField] GameObject obj_gate1;
    [SerializeField] GameObject obj_gate2;
    [SerializeField] GameObject obj_gate3;

    [SerializeField] GameObject obj_openedgate1;

    [SerializeField] GameObject obj_crystal;

    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject overlay;
    [SerializeField] Animator overlayanimator;

    [SerializeField] Animator mainenemyAnimator;
    [SerializeField] Animator darkauraAnimator;

    public bool continuestory = false;
    public bool callonce = true;

    private void Start()
    {
        if (ProgressManager.GetInstance().gameProgress == "opening")
        {
            //PlayableAsset opCutscene = Resources.Load<PlayableAsset>("Timeline/Opening");
            //TimelineManager.GetInstance().playTimeline(opCutscene);
            ProgressManager.GetInstance().gameProgress = "progress1";
        }
    }
    private void LateUpdate()
    {
        if (TimelineManager.GetInstance().getPlayState() != PlayState.Playing && ProgressManager.GetInstance().gameProgress == "progress1")
        {
            ProgressManager.GetInstance().gameProgress = "progress2";
            TextAsset textAsset = Resources.Load<TextAsset>("Story/MainStoryPart1");
            DialogueManager.GetInstance().notInteractDialogue = true;
            DialogueManager.GetInstance().EnterDialogueMode(textAsset);
        }

        if (!DialogueManager.GetInstance().dialogueIsPlaying&&callonce)
        {
            if (QuestManager.GetInstance().questlist.Count!=0&&QuestManager.GetInstance().questlist.ElementAt(0).quest_progress == 1)
            {
                if (!obj_void1.activeInHierarchy&&DialogueVariableObserver.variables["void_checked"].ToString() == "false")
                {

                    obj_void1.SetActive(true);
                    obj_void2.SetActive(true);
                    obj_void3.SetActive(true);
                }
                else if(obj_void1.activeInHierarchy&& DialogueVariableObserver.variables["void_checked"].ToString() == "true")
                {
                    obj_void1.SetActive(false);
                    obj_void2.SetActive(false);
                    obj_void3.SetActive(false);
                }
                if (!obj_gate1.activeInHierarchy&&DialogueVariableObserver.variables["gate_checked"].ToString() == "false")
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
                if (!obj_crystal.transform.GetChild(0).gameObject.activeInHierarchy&&DialogueVariableObserver.variables["crystal_checked"].ToString() == "false")
                {
                    obj_crystal.transform.GetChild(0).gameObject.SetActive(true);
                    obj_crystal.transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (obj_crystal.transform.GetChild(0).gameObject.activeInHierarchy && DialogueVariableObserver.variables["crystal_checked"].ToString() == "true")
                {
                    obj_crystal.transform.GetChild(0).gameObject.SetActive(false);
                    obj_crystal.transform.GetChild(1).gameObject.SetActive(false);
                }
                
            }

            if (QuestManager.GetInstance().questlist.Count != 0 && QuestManager.GetInstance().questlist.ElementAt(0).quest_progress == 1)
            {
                
                if (QuestManager.GetInstance().questlist.ElementAt(0).quest_progressvalue == QuestManager.GetInstance().questlist.ElementAt(0).questData.quest_progress[0].requirement)
                {
                    callonce = false;
                    QuestManager.GetInstance().questlist.ElementAt(0).quest_progress = 2;
                    continuestory = false;
                    
                    QuestManager.GetInstance().updateQuestProgress(QuestManager.GetInstance().questlist.ElementAt(0).questData,"force");
                    StartCoroutine(fadeTransition("moveplayer"));
                }
                
            }

            else if (continuestory&& DialogueVariableObserver.variables["mainquest_progress"].ToString() == "1" && QuestManager.GetInstance().questlist.Count != 0 && QuestManager.GetInstance().questlist.ElementAt(0).quest_progress == 2)
            {
                callonce = false;
                continuestory = false;
                TextAsset textAsset = Resources.Load<TextAsset>("Story/MainStoryPart1");
                DialogueManager.GetInstance().notInteractDialogue = true;
                DialogueManager.GetInstance().EnterDialogueMode(textAsset);
                TimelineManager.GetInstance().dontmove = false;
                callonce = true;
            }

            
            
            if (continuestory&& DialogueVariableObserver.variables["mainquest_progress"].ToString() == "2" && QuestManager.GetInstance().questlist.ElementAt(0).quest_progress == 2)
            {
                callonce = false;

                
                continuestory = false;
                TextAsset textAsset = Resources.Load<TextAsset>("Story/MainStoryPart1");
                DialogueManager.GetInstance().notInteractDialogue = true;
                DialogueManager.GetInstance().EnterDialogueMode(textAsset);
                TimelineManager.GetInstance().dontmove = false;

                
                callonce = true;
            }
            else if (DialogueVariableObserver.variables["mainquest_progress"].ToString() == "2" && QuestManager.GetInstance().questlist.ElementAt(0).quest_progress == 2)
            {
                callonce = false;
                StartCoroutine(fadeTransition("moveplayer2"));
            }

            if (DialogueVariableObserver.variables["mainquest_progress"].ToString() == "3" && QuestManager.GetInstance().questlist.ElementAt(0).quest_progress == 2)
            {
                callonce = false;
                obj_openedgate1.SetActive(true);
                callonce = true;
            }

        }
        
    }

    public IEnumerator fadeTransition(string type)
    {
        TimelineManager.GetInstance().dontmove = true;
        overlay.SetActive(true);
        //if (overlayanimator.GetCurrentAnimatorStateInfo(0).IsName("fadeout"))
        //{

        //}
        Debug.Log("fadein");

        overlayanimator.SetTrigger("Fadein");
        yield return new WaitForSeconds(1.5f);
        overlayanimator.ResetTrigger("Fadein");
        addActions(type);
        overlayanimator.SetTrigger("Fadeout");
        yield return new WaitForSeconds(1.5f);
        overlayanimator.ResetTrigger("Fadeout");

        Debug.Log("fadeout");

        continuestory = true;
        callonce = true;
        overlayanimator.SetTrigger("Reset");
        yield return new WaitForSeconds(1.5f);
        overlayanimator.ResetTrigger("Reset");
        

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



}
