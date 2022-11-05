using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LevelTwoClass : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject player;

    [SerializeField] GameObject girlnpctrigger;
    [SerializeField] GameObject shadowaura;

    [SerializeField] GameObject girlnpc;

    public bool continuestory = false;
    public bool callonce = true;

    [SerializeField] GameObject Toverlay;
    [SerializeField] Animator Toverlayanimator;

    [SerializeField] PlayableDirector lvltwodirector;

    void Start()
    {
        Toverlay = GameObject.Find("TransitionOverlay").gameObject;
        Toverlayanimator = GameObject.Find("TransitionOverlay").GetComponent<Animator>();

        if (GameStateManager.GetInstance().lastentrance == "class1")
        {
            player.transform.position = new Vector3(0.875f, -0.95f, 0);
        }
        else if (GameStateManager.GetInstance().lastentrance == "class2")
        {
            player.transform.position = new Vector3(-0.885f, -0.95f, 0);
        }

        //if (GameStateManager.GetInstance().lastscene == "SQ_2_backstory")
        //{
        //    player.transform.position = GameStateManager.GetInstance().position;
        //}
        //else if (GameStateManager.GetInstance().lastscene == "SQ_2_mindworld")
        //{
        //    player.transform.position = GameStateManager.GetInstance().position;
        //}

        if (ProgressManager.GetInstance().loaded)
        {
            ProgressManager.GetInstance().loaded = false;
            GameObject.Find("Player").transform.position = ProgressManager.GetInstance().loadedposition;
        }

        if ( SoundManager.GetInstance().musicSource.clip.name != "bgm_stage2")
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

        if (lvltwodirector.state!=PlayState.Playing&&!DialogueManager.GetInstance().dialogueIsPlaying && callonce)
        {
            if (girlnpctrigger.activeInHierarchy&&DialogueVariableObserver.variables["girlhelped"].ToString() == "true")
            {
                girlnpctrigger.SetActive(false);
            }
            if (shadowaura.activeInHierarchy&&DialogueVariableObserver.variables["shadow2_escaped"].ToString() == "true")
            {
                shadowaura.SetActive(false);
                girlnpc.transform.position = new Vector3(-0.416f,0.941f,0);
            }

            if (DialogueVariableObserver.variables["mainquest_progress"].ToString() == "91"&& DialogueVariableObserver.variables["areatrigger103"].ToString() == "false")
            {
                callonce = false;
                TextAsset textAsset = Resources.Load<TextAsset>("Story/MainstoryPart3");
                DialogueManager.GetInstance().notInteractDialogue = true;
                DialogueManager.GetInstance().EnterDialogueMode(textAsset);
                callonce = true;
            }


            if (DialogueVariableObserver.variables["quest2_progress"].ToString() == "3"&& ProgressManager.GetInstance().gameProgress == "progress10")
            {
                callonce = false;
                ProgressManager.GetInstance().gameProgress = "progress11";
                TextAsset textAsset = Resources.Load<TextAsset>("Story/SchoolNPC");
                DialogueManager.GetInstance().notInteractDialogue = true;
                DialogueManager.GetInstance().EnterDialogueMode(textAsset);
                callonce = true;
            }

            if (DialogueVariableObserver.variables["quest2_progress"].ToString() == "42" && ProgressManager.GetInstance().gameProgress == "progress11")
            {
                callonce = false;
                StartCoroutine(fadeTransition("progress12"));
            }

            if(DialogueVariableObserver.variables["quest2_progress"].ToString() == "42" && ProgressManager.GetInstance().gameProgress == "progress12")
            {
                callonce = false;
                ProgressManager.GetInstance().gameProgress = "progress13";
                TextAsset textAsset = Resources.Load<TextAsset>("Story/SchoolNPC");
                DialogueManager.GetInstance().notInteractDialogue = true;
                DialogueManager.GetInstance().EnterDialogueMode(textAsset);
                TimelineManager.GetInstance().dontmove = false;
                callonce = true;
            }

            if (DialogueVariableObserver.variables["quest2_progress"].ToString() == "5" && ProgressManager.GetInstance().gameProgress == "progress13")
            {
                callonce = false;
                ProgressManager.GetInstance().gameProgress = "progress14";
                TextAsset textAsset = Resources.Load<TextAsset>("Story/SchoolNPC");
                DialogueManager.GetInstance().notInteractDialogue = true;
                DialogueManager.GetInstance().EnterDialogueMode(textAsset);
                callonce = true;
            }



            if (DialogueVariableObserver.variables["quest2_progress"].ToString() == "61" && ProgressManager.GetInstance().gameProgress == "progress14") 
            {
                callonce = false;
                ProgressManager.GetInstance().gameProgress = "progress141";
                PlayableAsset cutscene = Resources.Load<PlayableAsset>("Timeline/SQ_2_arrival");
                lvltwodirector.Play(cutscene);
                callonce = true;
            }

            if (DialogueVariableObserver.variables["quest2_progress"].ToString() == "61" && ProgressManager.GetInstance().gameProgress == "progress141")
            {
                callonce = false;
                ProgressManager.GetInstance().gameProgress = "progress142";
                TextAsset textAsset = Resources.Load<TextAsset>("Story/SchoolNPC");
                DialogueManager.GetInstance().notInteractDialogue = true;
                DialogueManager.GetInstance().EnterDialogueMode(textAsset);
                callonce = true;
            }

            if (DialogueVariableObserver.variables["quest2_progress"].ToString() == "10" && ProgressManager.GetInstance().gameProgress == "progress142")
            {
                callonce = false;
                StartCoroutine(fadeTransition("progress15"));
            }

            if(DialogueVariableObserver.variables["quest2_progress"].ToString() == "10" && ProgressManager.GetInstance().gameProgress == "progress15")
            {
                callonce = false;
                ProgressManager.GetInstance().gameProgress = "progress16";
                TextAsset textAsset = Resources.Load<TextAsset>("Story/SchoolNPC");
                DialogueManager.GetInstance().notInteractDialogue = true;
                DialogueManager.GetInstance().EnterDialogueMode(textAsset);
                TimelineManager.GetInstance().dontmove = false;
                callonce = true;
            }

            if (DialogueVariableObserver.variables["quest2_progress"].ToString() == "11" && ProgressManager.GetInstance().gameProgress == "progress16")
            {
                callonce = false;
                PlayableAsset cutscene = Resources.Load<PlayableAsset>("Timeline/SQ_2_shadow");
                lvltwodirector.Play(cutscene);
                ProgressManager.GetInstance().gameProgress = "progress17";
                callonce = true;
            }

            if (DialogueVariableObserver.variables["quest2_progress"].ToString() == "11" && ProgressManager.GetInstance().gameProgress == "progress17")
            {
                callonce = false;
                ProgressManager.GetInstance().gameProgress = "progress18";
                TextAsset textAsset = Resources.Load<TextAsset>("Story/SchoolNPC");
                DialogueManager.GetInstance().notInteractDialogue = true;
                DialogueManager.GetInstance().EnterDialogueMode(textAsset);
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
        //TimelineManager.GetInstance().dontmove = false;
        continuestory = true;
        callonce = true;
        Toverlayanimator.SetTrigger("Reset");
        yield return new WaitForSeconds(1.5f);
        Toverlayanimator.ResetTrigger("Reset");


    }

    public void addActions(string type)
    {
        if (type == "progress12")
        {
            ProgressManager.GetInstance().gameProgress = "progress12";
        }
        if (type == "progress15")
        {
            ProgressManager.GetInstance().gameProgress = "progress15";
        }
    }
}
