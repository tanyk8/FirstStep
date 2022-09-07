using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.04f;

    [Header("LoadGlobalsJSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;

    [Header("Dialogue UI")]

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject continueIcon;

    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private TextMeshProUGUI displayNameText;

    [SerializeField] private Animator portraitAnimator;

    private Animator layoutAnimator;

    [Header("ChoiceListRef")]
    [SerializeField] private GameObject choiceListref;
    [SerializeField] private GameObject dialoguechoicepanel;

    [Header("Talkingperson")]
    [SerializeField] private GameObject talkingActor;

    private Story currentStory;

    public bool dialogueIsPlaying { get; private set; }

    private bool canContinueToNextLine = false;
    
    private Coroutine displayLineCoroutine;

    private static DialogueManager instance;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";
    private const string QUESTTRIGGER_TAG = "questevent";

    public static event Action startQuestTrigger;
    public static event Action updateQuestTrigger;
    public static event Action completeQuestTrigger;

    public static event Action updateTalkingActor;

    private DialogueVariableObserver dialoguevariableobserver;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;

        dialoguevariableobserver = new DialogueVariableObserver(loadGlobalsJSON);
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialoguechoicepanel.SetActive(false);

        layoutAnimator = dialoguePanel.GetComponent<Animator>();
    }

    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }
        if (canContinueToNextLine&& currentStory.currentChoices.Count == 0 && InputManager.GetInstance().GetSubmitPressed())
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {

        //update reference of talking person to corresponding object
        updateTalkingActor?.Invoke();
        
        

        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        dialoguevariableobserver.startListening(currentStory);



        if (talkingActor.GetComponent<Quest>() != null)
        {
            startQuestTrigger += talkingActor.GetComponent<Quest>().startQuest;
            updateQuestTrigger += talkingActor.GetComponent<Quest>().updateQuest;
            completeQuestTrigger += talkingActor.GetComponent<Quest>().completeQuest;
        }
        

        displayNameText.text = "???";
        portraitAnimator.Play("default");
        layoutAnimator.Play("layout_left");

        ContinueStory();

    }

    public IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);

        dialoguevariableobserver.stopListening(currentStory);

        if (talkingActor.GetComponent<Quest>() != null)
        {
            startQuestTrigger -= talkingActor.GetComponent<Quest>().startQuest;
            updateQuestTrigger -= talkingActor.GetComponent<Quest>().updateQuest;
            completeQuestTrigger -= talkingActor.GetComponent<Quest>().completeQuest;
        }
        

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            if(displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
            

            HandleTags(currentStory.currentTags);
            
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;

        continueIcon.SetActive(false);
        

        canContinueToNextLine = false;

        bool isAddingRichTextTag = false;

        foreach(char letter in line.ToCharArray())
        {
            if (InputManager.GetInstance().GetSubmitPressed())
            {
                dialogueText.maxVisibleCharacters = line.Length;
                break;
            }

            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            else
            {
                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(typingSpeed);
            }

            
        }

        continueIcon.SetActive(true);

        if (currentStory.currentChoices.Count != 0)
        {
            DisplayChoices();
        }
        

        canContinueToNextLine = true;
    }

    private void HandleTags(List<string> currentTags)
    {
        foreach(string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);

            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue);
                    break;
                case LAYOUT_TAG:
                    layoutAnimator.Play(tagValue);
                    break;
                case QUESTTRIGGER_TAG:
                    if (tagValue == "start")
                    {
                        startQuestTrigger?.Invoke();
                    }
                    if (tagValue == "update")
                    {
                        updateQuestTrigger?.Invoke();
                    }
                    if (tagValue == "complete")
                    {
                        completeQuestTrigger?.Invoke();
                    }
                    break;
                default:
                    Debug.LogWarning("Tag in switch case but not handled: "+tag);
                    break;
            }
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (dialoguechoicepanel.activeInHierarchy == false)
        {
            dialoguechoicepanel.SetActive(true);
            choiceListref.GetComponent<ListLayout>().createChoiceList(currentChoices.Count, currentChoices);
        }
        


    }

    public void MakeChoice(int choiceIndex,string response)
    {

        if (canContinueToNextLine)
        {

            currentStory.ChooseChoiceIndex(choiceIndex);

            InputManager.GetInstance().RegisterSubmitPressed();

            choiceListref.GetComponent<ListLayout>().destroyListSelection();
            dialoguechoicepanel.SetActive(false);

            ContinueStory();
        }
        
    }

    public Story getStory()
    {
        return currentStory;
    }

    public void setTalkingActor(GameObject actor)
    {
        talkingActor = actor;
    }
}
