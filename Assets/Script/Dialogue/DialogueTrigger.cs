using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    [Header("VisualCue")]
    [SerializeField] private GameObject visualcue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [Header("DialogueManager")]
    [SerializeField] private GameObject dialoguemanager;

    private bool playerInRange;


    private void Awake()
    {
        playerInRange = false;
        visualcue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange&& !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualcue.SetActive(true);
            if (InputManager.getInstance().getInteractPressed())
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
        }
        else
        {
            visualcue.SetActive(false);
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
            DialogueManager.updateTalkingActor += updateParentObjRef;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DialogueManager.updateTalkingActor -= updateParentObjRef;
            playerInRange = false;
        }
    }

    private void updateParentObjRef()
    {
        dialoguemanager.GetComponent<DialogueManager>().setTalkingActor(this.gameObject.transform.parent.gameObject);
        
    }

}
