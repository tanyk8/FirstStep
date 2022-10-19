using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Playables;



public class LevelOne : MonoBehaviour
{
    [SerializeField] GameObject Toverlay;
    [SerializeField] Animator Toverlayanimator;

    [SerializeField] GameObject player;

    [SerializeField] GameObject shadowaura;
    [SerializeField] GameObject shadowaura_after;
    [SerializeField] GameObject questareaTrigger1;
    [SerializeField] GameObject questareaTrigger2;

    [SerializeField] GameObject playgroundobj1;
    [SerializeField] GameObject playgroundobj2;
    [SerializeField] GameObject playgroundobj3;
    [SerializeField] GameObject playgroundobj4;
    [SerializeField] GameObject playgroundobj5;

    [SerializeField] GameObject fountainobj;

    [SerializeField] PlayableDirector lvlonedirector;

    [SerializeField] GameObject npc;

    public bool continuestory = false;
    public bool callonce = true;

    public bool triggeronce;

    // Start is called before the first frame update
    void Start()
    {
        Toverlay = GameObject.Find("TransitionOverlay").gameObject;
        Toverlayanimator = GameObject.Find("TransitionOverlay").GetComponent<Animator>();

        if (DialogueVariableObserver.variables["mainquest_progress"].ToString() == "3" && ProgressManager.GetInstance().gameProgress == "progress5")
        {
            ProgressManager.GetInstance().gameProgress = "progress6";
            TextAsset textAsset = Resources.Load<TextAsset>("Story/MainStoryPart2");
            DialogueManager.GetInstance().notInteractDialogue = true;
            DialogueManager.GetInstance().EnterDialogueMode(textAsset);
        }

        if (DialogueVariableObserver.variables["quest1_progress"].ToString() == "6")
        {
            player.transform.position = new Vector3(0, 0, 0);
            ProgressManager.GetInstance().gameProgress = "progress7";
        }

        if (ProgressManager.GetInstance().loaded)
        {
            ProgressManager.GetInstance().loaded = false;
            GameObject.Find("Player").transform.position = ProgressManager.GetInstance().loadedposition;
        }

        else if (GameStateManager.GetInstance().updateOnce == true)
        {
            GameStateManager.GetInstance().updateOnce = false;

            if(GameStateManager.GetInstance().lastscene == "SQ_1_treehouse") 
            {
                player.transform.position = new Vector3(-3.9f, 2.55f, 0);
            }

        }
        

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (ProgressManager.GetInstance().loading)
        {
            return;
        }

        if (!DialogueManager.GetInstance().dialogueIsPlaying && callonce)
        {
            if(!questareaTrigger1.activeInHierarchy&& DialogueVariableObserver.variables["areatrigger102_1"].ToString() == "false"&& QuestManager.GetInstance().questlist.ElementAt(1).quest_progress == 2)
            {
                questareaTrigger1.SetActive(true);
            }
            else if (questareaTrigger1.activeInHierarchy && DialogueVariableObserver.variables["areatrigger102_1"].ToString() == "true")
            {
                questareaTrigger1.SetActive(false);
            }

            if (!shadowaura.activeInHierarchy && lvlonedirector.state != PlayState.Playing)
            {
                //if (QuestManager.GetInstance().checkQuestExist(1)&& QuestManager.GetInstance().questlist.ElementAt(2).quest_progress<3)
                //{
                //    shadowaura.SetActive(true);
                //}
                //else if (QuestManager.GetInstance().checkQuestExist(1)==false)
                //{
                //    shadowaura.SetActive(true);
                //}
                if (!ProgressManager.GetInstance().stageOne_newpos)
                {
                    shadowaura.SetActive(true);
                }

            }
            else if(shadowaura.activeInHierarchy && lvlonedirector.state != PlayState.Playing)
            {
                if(ProgressManager.GetInstance().stageOne_newpos && lvlonedirector.state != PlayState.Playing && npc.transform.position.x != -6.663799e-05f)
                {
                    shadowaura.SetActive(false);
                }
            }

            if(!questareaTrigger2.activeInHierarchy&&DialogueVariableObserver.variables["quest1_progress"].ToString() == "7")
            {
                questareaTrigger2.SetActive(true);
            }
            else if(questareaTrigger2.activeInHierarchy && DialogueVariableObserver.variables["quest1_progress"].ToString() != "7")
            {
                questareaTrigger2.SetActive(false);
            }

            if(ProgressManager.GetInstance().stageOne_newpos&& lvlonedirector.state != PlayState.Playing&&npc.transform.position.x != -6.663799e-05f)
            {
                npc.transform.position = new Vector3(-6.663799e-05f, 0.18f,0);
                
                

            }

            if (!shadowaura_after.activeInHierarchy&& DialogueVariableObserver.variables["mainquest_progress"].ToString() == "6")
            {
                shadowaura_after.SetActive(true);
            }
            else if(shadowaura_after.activeInHierarchy && DialogueVariableObserver.variables["mainquest_progress"].ToString() != "6")
            {
                shadowaura_after.SetActive(false);
            }

            if (DialogueVariableObserver.variables["quest1_progress"].ToString() == "0"&& DialogueVariableObserver.variables["quest1_progress"].ToString() == "8")
            {

                //!QuestManager.GetInstance().checkQuestExist(1)
                playgroundobj1.SetActive(false);
                playgroundobj2.SetActive(false);
                playgroundobj3.SetActive(false);
                playgroundobj4.SetActive(false);
                playgroundobj5.SetActive(false);
                fountainobj.SetActive(false);
            }

            else if(QuestManager.GetInstance().checkQuestExist(1))
            {
                
                int tempindex = QuestManager.GetInstance().findQuestIndexwithID(1);

                if (playgroundobj1.activeInHierarchy&&QuestManager.GetInstance().questlist.ElementAt(tempindex).questState != QuestState.INPROGRESS)
                {
                    playgroundobj1.SetActive(false);
                    playgroundobj2.SetActive(false);
                    playgroundobj3.SetActive(false);
                    playgroundobj4.SetActive(false);
                    playgroundobj5.SetActive(false);
                    fountainobj.SetActive(false);
                }
                if (QuestManager.GetInstance().questlist.ElementAt(tempindex).questState == QuestState.INPROGRESS && QuestManager.GetInstance().questlist.ElementAt(tempindex).quest_progress == 2)
                {
                    if(!fountainobj.activeInHierarchy&& DialogueVariableObserver.variables["fountain_checked"].ToString() == "false")
                    {
                        fountainobj.SetActive(true);
                    }
                    else if(fountainobj.activeInHierarchy && DialogueVariableObserver.variables["fountain_checked"].ToString() == "true")
                    {
                        fountainobj.SetActive(false);
                    }
                }

                    if (QuestManager.GetInstance().questlist.ElementAt(tempindex).questState==QuestState.INPROGRESS&& QuestManager.GetInstance().questlist.ElementAt(tempindex).quest_progress==1)
                {
                    if (!playgroundobj1.activeInHierarchy && DialogueVariableObserver.variables["playground1_checked"].ToString() == "false")
                    {

                        playgroundobj1.SetActive(true);
                    }
                    else if (playgroundobj1.activeInHierarchy && DialogueVariableObserver.variables["playground1_checked"].ToString() == "true")
                    {
                        playgroundobj1.SetActive(false);
                    }

                    if (!playgroundobj2.activeInHierarchy && DialogueVariableObserver.variables["playground2_checked"].ToString() == "false")
                    {

                        playgroundobj2.SetActive(true);
                    }
                    else if (playgroundobj2.activeInHierarchy && DialogueVariableObserver.variables["playground2_checked"].ToString() == "true")
                    {
                        playgroundobj2.SetActive(false);
                    }

                    if (!playgroundobj3.activeInHierarchy && DialogueVariableObserver.variables["playground3_checked"].ToString() == "false")
                    {

                        playgroundobj3.SetActive(true);
                    }
                    else if (playgroundobj3.activeInHierarchy && DialogueVariableObserver.variables["playground3_checked"].ToString() == "true")
                    {
                        playgroundobj3.SetActive(false);
                    }

                    if (!playgroundobj4.activeInHierarchy && DialogueVariableObserver.variables["playground4_checked"].ToString() == "false")
                    {

                        playgroundobj4.SetActive(true);
                    }
                    else if (playgroundobj4.activeInHierarchy && DialogueVariableObserver.variables["playground4_checked"].ToString() == "true")
                    {
                        playgroundobj4.SetActive(false);
                    }

                    if (!playgroundobj5.activeInHierarchy && DialogueVariableObserver.variables["playground5_checked"].ToString() == "false")
                    {

                        playgroundobj5.SetActive(true);
                    }
                    else if (playgroundobj5.activeInHierarchy && DialogueVariableObserver.variables["playground5_checked"].ToString() == "true")
                    {
                        playgroundobj5.SetActive(false);
                    }
                }
            }
            if (DialogueVariableObserver.variables["quest1_progress"].ToString() == "7" && ProgressManager.GetInstance().gameProgress == "progress7")
            {
                callonce = false;
                //StartCoroutine(fadeTransition("updateprogress"));
                shadowaura.SetActive(false);
                Debug.Log(shadowaura.activeInHierarchy);
                PlayableAsset cutscene = Resources.Load<PlayableAsset>("Timeline/SQ_1_Music");            
                shadowaura_after.SetActive(true);
                lvlonedirector.Play(cutscene);
                ProgressManager.GetInstance().gameProgress = "progress8";
                callonce = true;
            }

            if(ProgressManager.GetInstance().gameProgress == "progress8"&& lvlonedirector.state != PlayState.Playing)
            {
                ProgressManager.GetInstance().stageOne_newpos = true;
            }
            

            //if (ProgressManager.GetInstance().gameProgress == "progress8")
            //{
            //    callonce = false;
            //    ProgressManager.GetInstance().gameProgress = "progress9";
            //    PlayableAsset cutscene = Resources.Load<PlayableAsset>("Timeline/SQ_1_Music");
            //    TimelineManager.GetInstance().playTimeline(cutscene);
            //    callonce = true;
            //}



                //curr: progress7 after finding the person  b4 talking

                //quest

                //if quest completed show the shadow appear
                //battle

                //sense a big source

                //find tree house

                //found him, battle


                //memory cutscene
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
        if (type == "updateprogress")
        {
            ProgressManager.GetInstance().gameProgress = "progress8";
        }
        else if (type == "moveplayer2")
        {


        }
    }
}
