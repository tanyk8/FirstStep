using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour
{
    [Header("VisualCue")]
    [SerializeField] private GameObject visualcue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [Header("type")]
    [SerializeField] public string triggertype="";

    //[Header("DialogueManager")]
    //[SerializeField] private GameObject dialoguemanager;

    private bool playerInRange;
    private bool callonce=true;
    //private bool eventIsnotNULL;


    private void Awake()
    {
        playerInRange = false;
        if (triggertype == "")
        {
            visualcue.SetActive(false);
        }
        


    }

    private void Update()
    {
        if (triggertype == ""&&playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualcue.SetActive(true);
            if (InputManager.GetInstance().getInteractPressed())
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
        }
        else if(triggertype == "")
        {
            visualcue.SetActive(false);
        }

        if (triggertype == "area" && playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            if (DialogueVariableObserver.variables["mainquest_progress"].ToString() == "4")
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                ProgressManager.GetInstance().gameProgress = "progress7";
            }
        }
        
        if (callonce&&playerInRange&& !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            callonce = false;
            if (triggertype == "area2"&&DialogueVariableObserver.variables["quest1_progress"].ToString() == "7")
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
        }
        if (playerInRange)
        {
            if (triggertype == "portal_treehouse")
            {
                SceneManager.LoadScene("SQ_1_treehouse");
            }
            else if(triggertype == "portal_park")
            {
                SceneManager.LoadScene("FirstStage_Park");
            }
            else if (triggertype == "portal_class1")
            {
                GameStateManager.GetInstance().lastentrance = "class1";
                SceneManager.LoadScene("SecondStage_Classroom");
            }
            else if (triggertype == "portal_class2")
            {
                GameStateManager.GetInstance().lastentrance = "class2";
                SceneManager.LoadScene("SecondStage_Classroom");
            }
            else if (triggertype == "portal_class3")
            {
                
                GameStateManager.GetInstance().lastentrance = "class3";
                SceneManager.LoadScene("SecondStage_shadowroom");
            }
            else if (triggertype == "portal_hall1")
            {
                GameStateManager.GetInstance().lastentrance = "hall1";
                SceneManager.LoadScene("SecondStage_Hallway");
            }
            else if (triggertype == "portal_hall2")
            {
                GameStateManager.GetInstance().lastentrance = "hall2";
                SceneManager.LoadScene("SecondStage_Hallway");
            }
            else if (triggertype == "portal_hall3")
            {
                GameStateManager.GetInstance().lastentrance = "hall3";
                SceneManager.LoadScene("SecondStage_Hallway");
            }
            else if (triggertype == "portal_hospitalroom1")
            {
                GameStateManager.GetInstance().lastentrance = "hospitalroom1";
                SceneManager.LoadScene("ThirdStage_roomone");
            }
            else if (triggertype == "portal_hospitalroom2")
            {
                GameStateManager.GetInstance().lastentrance = "hospitalroom2";
                SceneManager.LoadScene("ThirdStage_shadow");
            }
            else if (triggertype == "portal_hospitalhall1")
            {
                GameStateManager.GetInstance().lastentrance = "hospitalhall1";
                SceneManager.LoadScene("ThirdStage_hospitalhall");
            }
            else if (triggertype == "portal_hospitalhall2")
            {
                GameStateManager.GetInstance().lastentrance = "hospitalhall2";
                SceneManager.LoadScene("ThirdStage_hospitalhall");
            }

            //portal_hospitalroom1
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;

            callonce = true;
            //DialogueManager.GetInstance().updateTalkingActor += updateParentObjRef;
            //eventIsnotNULL = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            //DialogueManager.GetInstance().updateTalkingActor -= updateParentObjRef;
            playerInRange = false;
        }
    }

    private void OnDestroy()
    {
        //if (eventIsnotNULL)
        //{
        //      DialogueManager.GetInstance().updateTalkingActor -= updateParentObjRef;
        //}
    }

    private void updateParentObjRef()
    {
        //dialoguemanager.GetComponent<DialogueManager>().setTalkingActor(this.gameObject.transform.parent.gameObject);
        //Debug.Log(gameObject.transform.parent.gameObject);

        //Debug.Log(gameObject.transform.parent.gameObject);
        //DialogueManager.GetInstance().setTalkingActor(gameObject.transform.parent.gameObject);
        
    }

}
