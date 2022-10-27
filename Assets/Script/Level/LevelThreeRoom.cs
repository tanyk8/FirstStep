using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LevelThreeRoom : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] GameObject window1;
    [SerializeField] GameObject window1T;
    [SerializeField] GameObject window1_1;
    [SerializeField] GameObject window2;
    [SerializeField] GameObject window2T;
    [SerializeField] GameObject window2_1;
    [SerializeField] GameObject shadowaura;

    public bool continuestory = false;
    public bool callonce = true;

    [SerializeField] GameObject Toverlay;
    [SerializeField] Animator Toverlayanimator;

    [SerializeField] PlayableDirector lvlthreedirector;

    void Start()
    {
        Toverlay = GameObject.Find("TransitionOverlay").gameObject;
        Toverlayanimator = GameObject.Find("TransitionOverlay").GetComponent<Animator>();

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
        //lvlthreedirector.state != PlayState.Playing&&
        if (!DialogueManager.GetInstance().dialogueIsPlaying && callonce)
        {
            if(!shadowaura.activeInHierarchy&& DialogueVariableObserver.variables["shadow3_escaped"].ToString() == "false")
            {
                shadowaura.SetActive(true);
            }
            else if (shadowaura.activeInHierarchy && DialogueVariableObserver.variables["shadow3_escaped"].ToString() == "true")
            {
                shadowaura.SetActive(false);
            }

            if (!window1T.activeInHierarchy && DialogueVariableObserver.variables["quest3_progress"].ToString() == "2" && DialogueVariableObserver.variables["window1_checked"].ToString() == "false")
            {
                window1T.SetActive(true);
            }
            else if (window1T.activeInHierarchy&& DialogueVariableObserver.variables["quest3_progress"].ToString() == "2"&& DialogueVariableObserver.variables["window1_checked"].ToString() == "true")
            {
                window1T.SetActive(false);
            }
            else if (window1T.activeInHierarchy && DialogueVariableObserver.variables["quest3_progress"].ToString() != "2")
            {
                window1T.SetActive(false);
            }
            
            if (!window2T.activeInHierarchy && DialogueVariableObserver.variables["quest3_progress"].ToString() == "2" && DialogueVariableObserver.variables["window2_checked"].ToString() == "false")
            {
                window2T.SetActive(true);
            }
            else if (window2T.activeInHierarchy && DialogueVariableObserver.variables["quest3_progress"].ToString() == "2" && DialogueVariableObserver.variables["window2_checked"].ToString() == "true")
            {
                window2T.SetActive(true);
            }
            else if (window2T.activeInHierarchy && DialogueVariableObserver.variables["quest3_progress"].ToString() != "2")
            {
                window2T.SetActive(false);
            }

            if (!window1_1.activeInHierarchy&&DialogueVariableObserver.variables["window1_checked"].ToString() == "true")
            {
                window1_1.SetActive(true);

                if (!window1.activeInHierarchy && DialogueVariableObserver.variables["window1_checked"].ToString() == "false")
                {
                    window1.SetActive(true);
                }
                else if (window1.activeInHierarchy && DialogueVariableObserver.variables["window1_checked"].ToString() == "true")
                {
                    window1.SetActive(false);
                }

            }
            else if (window1_1.activeInHierarchy && DialogueVariableObserver.variables["window1_checked"].ToString() == "false")
            {
                window1_1.SetActive(false);
            }

            if (!window2_1.activeInHierarchy && DialogueVariableObserver.variables["window2_checked"].ToString() == "true")
            {
                window2_1.SetActive(true);

                if (!window2.activeInHierarchy && DialogueVariableObserver.variables["window2_checked"].ToString() == "false")
                {
                    window2.SetActive(true);
                }
                else if (window2.activeInHierarchy && DialogueVariableObserver.variables["window2_checked"].ToString() == "true")
                {
                    window2.SetActive(false);
                }
            }
            else if (window2_1.activeInHierarchy && DialogueVariableObserver.variables["window2_checked"].ToString() == "false")
            {
                window2_1.SetActive(false);
            }



            if(DialogueVariableObserver.variables["quest3_progress"].ToString() == "4" && ProgressManager.GetInstance().gameProgress == "progress22")
            {
                callonce = false;
                ProgressManager.GetInstance().gameProgress = "progress23";
                player.transform.position = new Vector3(-0.2f,0.5f,0);
                PlayableAsset cutscene = Resources.Load<PlayableAsset>("Timeline/SQ_3_shadow");
                lvlthreedirector.Play(cutscene);

                TextAsset textAsset = Resources.Load<TextAsset>("Story/HospitalNPC");
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
