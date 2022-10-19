using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SQThree : MonoBehaviour
{

    public string sqprogress3 = "progress0";

    [SerializeField] GameObject Toverlay;
    [SerializeField] Animator Toverlayanimator;

    public bool continuestory = false;
    public bool callonce = true;

    // Start is called before the first frame update
    void Start()
    {
        Toverlay = GameObject.Find("TransitionOverlay").gameObject;
        Toverlayanimator = GameObject.Find("TransitionOverlay").GetComponent<Animator>();

        if (DialogueVariableObserver.variables["quest2_progress"].ToString() == "7")
        {
            sqprogress3 = "progress1";
            TextAsset textAsset = Resources.Load<TextAsset>("Story/SchoolNPC_Girl");
            DialogueManager.GetInstance().notInteractDialogue = true;
            DialogueManager.GetInstance().EnterDialogueMode(textAsset);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (callonce)
        {
            if (!DialogueManager.GetInstance().dialogueIsPlaying && sqprogress3 == "progress1" && DialogueVariableObserver.variables["quest2_progress"].ToString() == "8")
            {
                callonce = false;
                StartCoroutine(fadeTransition("progress2"));

            }

            if (!DialogueManager.GetInstance().dialogueIsPlaying && sqprogress3 == "progress2" && DialogueVariableObserver.variables["quest2_progress"].ToString() == "8")
            {
                callonce = false;
                sqprogress3 = "progress3";
                TextAsset textAsset = Resources.Load<TextAsset>("Story/SchoolNPC_Girl");
                DialogueManager.GetInstance().notInteractDialogue = true;
                DialogueManager.GetInstance().EnterDialogueMode(textAsset);
                TimelineManager.GetInstance().dontmove = false;
                callonce = true;
            }

            if (!DialogueManager.GetInstance().dialogueIsPlaying && sqprogress3 == "progress3" && DialogueVariableObserver.variables["quest2_progress"].ToString() == "9")
            {
                sqprogress3 = "progress4";
                SceneManager.LoadScene("SecondStage_Classroom");
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
        //TimelineManager.GetInstance().dontmove = false;
        Toverlayanimator.SetTrigger("Reset");
        yield return new WaitForSeconds(1.5f);
        Toverlayanimator.ResetTrigger("Reset");


    }

    public void addActions(string type)
    {
        if (type == "progress2")
        {
            sqprogress3 = "progress2";
        }
    }
}
